let pilots = [];
let connection = null;

let pilotIdToUpdate = -1;

getdata();
setupSignalR();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:53910/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("PilotCreated", (user, message) => {
        getdata();
    });

    connection.on("PilotDeleted", (user, message) => {
        getdata();
    });
    connection.on("PilotUpdated", (user, message) => {
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
    await fetch('http://localhost:53910/pilot')
        .then(x => x.json())
        .then(y => {
            pilots = y;            
            display();
        }); 
    document.getElementById('updateformdiv').style.display = 'none';
}

function display()
{
    document.getElementById('resultarea').innerHTML = "";
    pilots.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.pilot_Id + "</td><td>"
        + t.name + "</td><td>" + t.dateofbirth + "</td><td>"+
        `<button type="button" onclick="remove(${t.pilot_Id})">Delete</button>`+
        `<button type="button" onclick="showupdate(${t.pilot_Id})">Update</button>`
            + "</td></tr>";  
    });
}

function create() {
    let name = document.getElementById('pilotname').value;
    let Dateofbirth = document.getElementById('dateofbirth').value;
    fetch('http://localhost:53910/pilot', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
             name: name,
                dateofbirth: Dateofbirth
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
    fetch('http://localhost:53910/pilot/' + id, {
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
function showupdate(id)
{
    document.getElementById('pilotnametoUpdate').value = pilots.find(t => t['pilot_Id'] == id)['name'];
    document.getElementById('dateofbirthtoUpdate').value = pilots.find(t => t['pilot_Id'] == id)['dateofbirth'];
    document.getElementById('updateformdiv').style.display = 'flex';
    pilotIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let Name = document.getElementById('pilotnametoUpdate').value;
    let Dateofbirth = document.getElementById('dateofbirthtoUpdate').value;
    fetch('http://localhost:53910/pilot', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                name: Name,
                pilot_Id: pilotIdToUpdate,
                dateofbirth: Dateofbirth
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}