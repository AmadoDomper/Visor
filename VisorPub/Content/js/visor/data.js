var combo;

function callCtrler() {
    var req = new XMLHttpRequest();
    req.open('GET', 'http://localhost:59423/api/PublicacionApi/GetCombos', true);
    req.setRequestHeader('Content-Type', 'application/json');

    req.onload = function () {
        if (req.status >= 200 && req.status < 400) {
            console.log(req);
            combo = JSON.parse(req.responseText);
            console.log(combo);
        } else {
            console.log('We encountered an error!');
        }
    }

    req.send();
}

//callCtrler();
var info;
function PublicationInfo(id) {
    var req = new XMLHttpRequest();
    req.open('GET', 'http://localhost:59423/api/PublicacionApi/GetDatosPublicacion?id=' + id , true);
    req.setRequestHeader('Content-Type', 'application/json');

    req.onload = function () {
        if (req.status >= 200 && req.status < 400) {

            let infoDet = document.getElementById('info-det');

            console.log(req);
            //info = JSON.parse(req.responseText);
            info = req.responseText;
            console.log(info);
            infoDet.innerHTML = info;
        } else {
            console.log('We encountered an error!');
        }
    }

    req.send();
}

document.getElementById('btnSelec').addEventListener('click', function () {
    map.addInteraction(draw);
    console.log("You finally clicked without jQuery");
});

document.getElementById('btnDeselec').addEventListener('click', function () {
    map.removeInteraction(draw);
    drawingSource.clear()
});