let races = [];
let connection = null;

let racecodeToUpdate = -1;
let remove_id =[];

getdata();
setupSignalR();


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:53910/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("RaceCreated", (user, message) => {
        getdata();
    });

    connection.on("RaceDeleted", (user, message) => {
         getdata();
    });
    connection.on("RaceUpdated", (user, message) => {
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
    await fetch('http://localhost:53910/race')
        .then(x => x.json())
        .then(y => {
            races = y;           
            display();
            console.log(y);
            console.log(remove_id)
        });
    document.getElementById('updateformdiv').style.display = 'none';
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    races.forEach(t => {       
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.race_code + "</td><td>"
            + t.racename + "</td><td>" +
            t.location + "</td><td>" +
            + t.laps + "</td><td>" +
            + t.length + "</td><td>" +
        t.date + "</td><td>" +
        `<button type="button" onclick="remove(${t.race_Id})">Delete</button>` +
        `<button type="button" onclick="showupdate(${t.race_Id})">Update</button>`
            + "</td></tr>";
        if (!remove_id.includes(t.race_code)) {
            remove_id.push('' + t.race_code);    
        }
            
    });
}
function remove(id) {
    fetch('http://localhost:53910/Race/' + remove_id[id], {
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
    remove_id.splice(id);
}

function create() {
    let code = document.getElementById('racecode').value;
    let name = document.getElementById('racename').value;
    let location = document.getElementById('racelocation').value;
    let laps = document.getElementById('racelaps').value;
    let length = document.getElementById('racelength').value;
    let date = document.getElementById('racedate').value;

    fetch('http://localhost:53910/race', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                race_code: code,
                racename: name,
                location: location,
                laps: laps,
                length: length,
                date:date
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}


function showupdate(id) {
    document.getElementById('racenametoUpdate').value = races.find(t => t['race_Id'] == id)['racename'];
    document.getElementById('racelocationtoUpdate').value = races.find(t => t['race_Id'] == id)['location'];
    document.getElementById('racelapstoUpdate').value = races.find(t => t['race_Id'] == id)['laps'];
    document.getElementById('racelengthtiontoUpdate').value = races.find(t => t['race_Id'] == id)['length'];
    document.getElementById('racedatetoUpdate').value = races.find(t => t['race_Id'] == id)['date'];

    document.getElementById('updateformdiv').style.display = 'flex';
    racecodeToUpdate = id;
}

function updateR() {
    document.getElementById('updateformdiv').style.display = 'none';
    let Name = document.getElementById('racenametoUpdate').value;    
    let Location = document.getElementById('racelocationtoUpdate').value;    
    let Laps = document.getElementById('racelapstoUpdate').value;    
    let Length = document.getElementById('racelengthtiontoUpdate').value;  
    let Date = document.getElementById('racedatetoUpdate').value;  

    fetch('http://localhost:53910/race', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                racename: Name,
                race_code: remove_id[racecodeToUpdate],
                race_Id: racecodeToUpdate,
                location: Location,
                laps: Laps,
                length: Length,
                date: Date


                
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}