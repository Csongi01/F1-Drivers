let pilot_team = [];
let top2youngest = [];
let best = [];
let Mogyorod = [];
let AVG = [];

function test() {
    getdataPTF()
}
function testtop() {
    getdataTOP()
}
function testbest() {
    getdataBestP()
}
function testMogyorod() {
    getdataMR()
}
function testAVG() {
    getdataAVG()
}

async function getdataPTF() {
    fetch('http://localhost:53910/Noncrud/PilotTeamInfo')
        .then(x => x.json())
        .then(y => {
            pilot_team = y;            
            display();
        });
}
async function getdataTOP() {
    fetch('http://localhost:53910/Noncrud/Top2_YoungestPilots')
        .then(x => x.json())
        .then(y => {
            top2youngest = y;            
            displayT();
        });
}
async function getdataBestP() {
    fetch('http://localhost:53910/Noncrud/Best_Pilot')
        .then(x => x.json())
        .then(y => {
            best = y;           
            displayB();
        });


}
async function getdataMR() {
    fetch('http://localhost:53910/Noncrud/Mogyorod_Results')
        .then(x => x.json())
        .then(y => {
            Mogyorod = y;            
            displayM();
        });


}
async function getdataAVG() {
    fetch('http://localhost:53910/Noncrud/AveragefinishPos')
        .then(x => x.json())
        .then(y => {
            AVG = y;
            displayAVG();
        });
}

function display() {
    document.getElementById('resultareaC').innerHTML = "";
    pilot_team.forEach(c => {
        document.getElementById('resultareaC').innerHTML += "<tr><td>" + c.teamName + "</td><td>" + c.pilotNum + "</td></tr>";

    })

  

}
function displayT() {
    document.getElementById('resultareaC').innerHTML = "";
    top2youngest.forEach(c => {
        document.getElementById('resultareaC').innerHTML += "<tr><td>" + c.pilotName
            + "</td><td>" + c.dateofbirth + "</td></tr>";

    })
}

function displayB() {
    document.getElementById('resultareaC').innerHTML = "";
    best.forEach(c => {
        document.getElementById('resultareaC').innerHTML += "<tr><td>" + c.pilot_Name
            + "</td><td>" + c.wins + "</td></tr>";

    })
}
function displayM() {
    document.getElementById('resultareaC').innerHTML = "";
    Mogyorod.forEach(c => {
        document.getElementById('resultareaC').innerHTML += "<tr><td>" + c.start_pos
            + "</td><td>" + c.finish_pos + "</td></tr>";

    })
}

function displayAVG() {
    document.getElementById('resultareaC').innerHTML = "";
    AVG.forEach(c => {
        document.getElementById('resultareaC').innerHTML += "<tr><td>" + c.pilot_name
            + "</td><td>" + c.averageFinishPos + "</td></tr>";

    })
}