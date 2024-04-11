let teams = [];
let connection = null;

let teamIdToUpdate = -1;

getdata();
setupSignalR();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:53910/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("TeamCreated", (user, message) => {
        getdata();
    });

    connection.on("TeamDeleted", (user, message) => {
        getdata();
    });
    connection.on("TeamUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();


}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};
async function getdata() {
    await fetch('http://localhost:53910/team')
        .then(x => x.json())
        .then(y => {
            teams = y;
            display();
        });
    document.getElementById('updateformdiv').style.display = 'none';
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    teams.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.team_id + "</td><td>"
            + t.team_name + "</td><td>" +
        `<button type="button" onclick="remove(${t.team_id})">Delete</button>` +
        `<button type="button" onclick="showupdate(${t.team_id})">Update</button>`
            + "</td></tr>";
    });
}

function create() {
    let name = document.getElementById('teamname').value;
    
    fetch('http://localhost:53910/team', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                team_name: name            
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function remove(id) {
    fetch('http://localhost:53910/team/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
function showupdate(id) {   
    document.getElementById('teamnametoUpdate').value = teams.find(t => t['team_id'] == id)['team_name'];
    document.getElementById('updateformdiv').style.display = 'flex';
    teamIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('teamnametoUpdate').value;   
    fetch('http://localhost:53910/team', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                team_name: name,
                team_id: teamIdToUpdate               
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}