var combo;
var searchPub = [];
var searchPoints = [];
var pub = [];

function LoadSelectSearch() {
    let req = new XMLHttpRequest();
    req.open('GET', 'http://localhost:59423/api/PublicacionApi/GetCombos', true);
    req.setRequestHeader('Content-Type', 'application/json');

    req.onload = function () {
        if (req.status >= 200 && req.status < 400) {
            console.log(req);
            combo = JSON.parse(req.responseText);
            console.log(combo);
            FillSelect("ddlTipo", combo.lsTipos);
            FillSelect("ddlTema", combo.lsTemas);
            FillYears();
        } else {
            console.log('We encountered an error!');
        }
    }

    req.send();
}

var regions = [];
var provincias = []
var distritos = [];
function LoadUbigeo() {
    let req = new XMLHttpRequest();
    req.open('GET', 'http://localhost:59423/api/PublicacionApi/getUbigeo', true);
    req.setRequestHeader('Content-Type', 'application/json');

    req.onload = function () {
        if (req.status >= 200 && req.status < 400) {
            let ubigeo = JSON.parse(req.responseText);
            console.log(ubigeo);
            regions = ubigeo.regions;
            provincias = ubigeo.provincias;
            distritos = ubigeo.distritos;
            FillSelect("ddlDepartamento", regions);
            //FillSelect("ddlTema", combo.lsTemas);
            //FillYears();
        } else {
            console.log('We encountered an error!');
        }
    }

    req.send();
}


var info;
function PublicationInfo(id) {
    let req = new XMLHttpRequest();
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


function GetAllPublications() {
    let req = new XMLHttpRequest();
    req.open('GET', 'http://localhost:59423/api/PublicacionApi/GetAllPublicactionsJSON', true);
    req.setRequestHeader('Content-Type', 'application/json');

    req.onload = function () {
        if (req.status >= 200 && req.status < 400) {

            console.log(req);
            pub = JSON.parse(req.responseText).Publications;
        } else {
            console.log('We encountered an error!');
        }
    }

    req.send();
}


document.getElementById('btnSelec').addEventListener('click', function () {

    if (highlight != undefined)
        highlight = featureOverlay.getSource().removeFeature(highlight);

    map.addInteraction(draw);
    map.addInteraction(select);
    console.log("You finally clicked without jQuery");
});

document.getElementById('btnDeselec').addEventListener('click', function () {
    map.removeInteraction(draw);
    map.removeInteraction(select);
    drawingSource.clear()
    selectedFeatures.clear();
    ShowInformation("");
});

document.getElementById('ddlDepartamento').addEventListener('change', function () {
    console.log("Change ddlDepartamento");
    console.log(this.value);
    FillSelect("dllProvincias", provincias.filter(p => p.id.substr(0, 2) == this.value));
    document.getElementById('dllProvincias').dispatchEvent(new Event('change'));

    //let feature = GetFeatureRegionById(this.value);
    //DisplayFeature(feature);

});

document.getElementById('dllProvincias').addEventListener('change', function () {
    console.log("Change dllProvincias");
    console.log(this.value);
    FillSelect("ddlDistrito", distritos.filter(d => d.id.substr(0, 4) == this.value))
});

document.getElementById('ddlDistrito').addEventListener('change', function () {
    console.log("Change ddlDistrito");
});

function SetComboUbigeo(ubigeo) {
    let dep = ubigeo.substr(0, 2);
    let pro = ubigeo.substr(2, 2);
    let dis = ubigeo.substr(4, 2);

    if (dep != "") { document.getElementById("ddlDepartamento").value = dep; document.getElementById('ddlDepartamento').dispatchEvent(new Event('change')); }
    if (pro != "") { document.getElementById("dllProvincias").value = dep + pro; document.getElementById('dllProvincias').dispatchEvent(new Event('change')); }
    if (dis != "") document.getElementById("ddlDistrito").value = dep + pro + dis;
}








function ShowInformation(content) {
    let infoDet = document.getElementById('info-det');
    infoDet.innerHTML = "";
    infoDet.innerHTML = content;
}

function ShowInformationLugar(content) {
    let infoLug = document.getElementById('info-lugar');
    infoLug.innerHTML = "";
    infoLug.innerHTML = content;
}

function OrderFeatureAsc(a, b) {
    if (a.getProperties().idpunto > b.getProperties().idpunto) {
        return 1;
    }
    if (a.getProperties().idpunto < b.getProperties().idpunto) {
        return -1;
    }
    return 0;
}

function OrderFeatureDesc(a, b) {
    if (a.getProperties().idpunto < b.getProperties().idpunto) {
        return 1;
    }
    if (a.getProperties().idpunto > b.getProperties().idpunto) {
        return -1;
    }
    return 0;
}

function FillSelect(id, elements) {

    var select = document.getElementById(id);
    select.innerHTML = "";
    select.options[select.options.length] = new Option("- TODOS -", "")

    elements.forEach(obj =>
        select.options[select.options.length] = new Option(obj.desc, obj.id)
    )
}

function FillYears() {

    let myDate = new Date();
    let year = myDate.getFullYear();
    let values = '<option value=""> - TODOS - </option>';
    for (let i = year; i >= 1900; i--) {
        values += '<option value="' + i + '">' + i + '</option>';
    }

    document.getElementById("ddlAnoPub").innerHTML = values;
}

LoadSelectSearch();
GetAllPublications();
LoadUbigeo();

document.getElementById('btnSearch').addEventListener('click', function () {
    featureOverlay.getSource().clear();
    //GetSearchPublicactionPoints();
    //GetSearchData();
    GetSearchPublicactionIds();
});

//Eliminar
function GetSearchData(nPage = 1, nSize = 10) {
    let cSearch = document.getElementById("txtBusqueda").value;
    let nType = document.getElementById("ddlTipo").value || -1;
    let nTheme = document.getElementById("ddlTema").value || -1;
    let cYear = document.getElementById("ddlAnoPub").value;

    let jsonData = { nPage: nPage, nPageSize: nSize, cTexto: cSearch, nTipo: nType, cAnoPub: cYear, nAreaTema: nTheme };
    let formattedJsonData = JSON.stringify(jsonData);

    document.getElementById('ldBuscar').style.display = "";
    document.getElementById('cntListaResultados').style.display = "none";

    let req = new XMLHttpRequest();
    req.open('POST', 'http://localhost:59423/api/PublicacionApi/1', true);
    req.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');

    req.onload = function () {
        if (req.status >= 200 && req.status < 400) {
            info = JSON.parse(req.responseText);
            console.log(info);
            document.getElementById('ldBuscar').style.display = "none";
            document.getElementById('cntListaResultados').style.display = "";
            CreateTable(info, "tblResultados");
        } else {
            console.log('We encountered an error!');
        }
    }

    req.send(formattedJsonData);
}

// Eliminar
function GetSearchPublicactionPoints() {
    let cSearch = document.getElementById("txtBusqueda").value;
    let nType = document.getElementById("ddlTipo").value || -1;
    let nTheme = document.getElementById("ddlTema").value || -1;
    let cYear = document.getElementById("ddlAnoPub").value;

    let req = new XMLHttpRequest();
    req.open('GET', 'http://localhost:59423/api/PublicacionApi/GetSearchPublicactionPoints?cPubTexto=' + cSearch + '&nTipo=' + nType + '&cAno=' + cYear + '&nAreaTem=' + nTheme, true);
    req.setRequestHeader('Content-Type', 'application/json');

    req.onload = function () {
        if (req.status >= 200 && req.status < 400) {
            searchPoints = JSON.parse(req.responseText);
            console.log(searchPoints);
            SelectAllPointFeatures(searchPoints);
        } else {
            console.log('We encountered an error!');
        }
    }

    req.send();
}

function GetSearchPublicactionIds() {
    let cSearch = document.getElementById("txtBusqueda").value;
    let nType = document.getElementById("ddlTipo").value || -1;
    let nTheme = document.getElementById("ddlTema").value || -1;
    let cYear = document.getElementById("ddlAnoPub").value;

    document.getElementById('ldBuscar').style.display = "";
    document.getElementById('cntListaResultados').style.display = "none";

    let req = new XMLHttpRequest();
    req.open('GET', 'http://localhost:59423/api/PublicacionApi/GetSearchPublicactionIds?cPubTexto=' + cSearch + '&nTipo=' + nType + '&cAno=' + cYear + '&nAreaTem=' + nTheme, true);
    req.setRequestHeader('Content-Type', 'application/json');

    req.onload = function () {
        if (req.status >= 200 && req.status < 400) {
            searchPub = JSON.parse(req.responseText);

            let dep = document.getElementById("ddlDepartamento").value;
            let pro = document.getElementById("dllProvincias").value;
            let dis = document.getElementById("ddlDistrito").value;

            if(dep) activarCapa.departamento();
            if(pro) activarCapa.provincia();
            if(dis) activarCapa.distrito();

            document.getElementById('ldBuscar').style.display = "none";
            document.getElementById('cntListaResultados').style.display = "";

            searchPoints = GetPointsByPublicationIds(searchPub);
            searchPoints = FilterPointsByUbigeo(searchPoints);
            SelectLastUbigeoFeature();
            SelectAllPointFeatures(searchPoints);
            searchPub = GeAllPublicationSelectedIds();
            ListSearchPublications(1,10);
        } else {
            console.log('We encountered an error!');
        }
    }

    req.send();

}

var activarCapa = {
    departamento: function () {
        if (!departamento) { btnDepartamento() };
    },
    provincia: function () {
        if (!provincia) { btnProvincia() };
    },
    distrito: function () {
        if (!distrito) { btnDistrito() };
    }
}

var desactivarCapa = {
    departamento: function () {
        if (departamento) { btnDepartamento() };
    },
    provincia: function () {
        if (provincia) { btnProvincia() };
    },
    distrito: function () {
        if (distrito) { btnDistrito() };
    }
}


function ShowSelectedFeaturesInfo() {
    document.getElementById('ldBuscar').style.display = "none";
    document.getElementById('cntListaResultados').style.display = "";

    searchPub = GeAllPublicationSelectedIds();
    searchPoints = GetAllPointsSelectedIds();
    ListSearchPublications(1, 10);
}

function CreateTable(info, tableId) {

    let rows = info.oLista;

    let html = `<table id="${tableId}" class="table table-bordered table-hover">
                    <thead>
                        <th style="display: none;">#</th>
                        <th style="width: 150px;"> Título </th>
                        <th style="width: 200px;"> Referencia Bibliográfica </th>
                        <th style="width: 50px;"> Det. </th>
                    </thead>
                    <tbody>
                    `;

    if (rows != undefined) {
        for (let i = 0; i < rows.length; i++) {
            html += `
                                    <tr index='${i}'>
                                        <td style="display: none;text-align: center;vertical-align: middle;">${rows[i].Id}</td>
                                        <td style="text-align: left; vertical-align: middle;">${rows[i].titulo}</td>
                                        <td style="text-align: left; vertical-align: middle;">${rows[i].refBi}</td>
                                        <td class="edit" style="cursor: pointer;text-align: center;vertical-align: middle;">
                                            <span style="color: #439b43;font-size:15px;" class="fas fa-fw fa-search" aria-hidden="true">
                                            </span>
                                        </td>
                                    </tr>`;
        }
    }

    html += `</tbody></table>`;

    //Add Pagination
    if (info.nPageTot > 1) {

        html += `<div class="text-center" id="cntPaginacion">
                    <ul class="pagination m-t-10 m-b-10">
                        <li class="previous" id="example1_previous">
                        <a data-dt-idx="0" id="pag_0" tabindex="0"><<</a></li>`;


        let ini = 1;
        let fin = info.nPageTot;

        if (info.nPageTot > 5) {

            fin = ini + 4;

            if (info.nPage > 2 && info.nPage <= (info.nPageTot - 3)) {
                ini = info.nPage - 2;
                fin = ini + 4;
            } else if (info.nPage > (info.nPageTot - 3)) {
                ini = info.nPageTot - 4;
                fin = info.nPageTot;
            }
        }

        for (let i = ini ; i <= fin ; i++) {
            html += `<li><a data-dt-idx="${i }" id="pag_${ i }">${ i }</a></li>`;
        }

        html += `<li class="next" id="example1_next">
                 <a data-dt-idx="${ info.nPageTot + 1 }" id="pag_${ info.nPageTot + 1 }">>></a>
                 </li></ul></div>`;

    }

    document.getElementById('cntListaResultados').innerHTML = html;

    if (info.nPageTot > 1) {
        let activePage = document.getElementById("pag_" + info.nPage);

        activePage.parentNode.classList.add('active');
        activePage.focus();

        if (info.nPage == 1) {
            document.querySelector('#cntPaginacion .previous').classList.add('disabled');
        } else if (info.nPage == info.nPageTot) {
            document.querySelector('#cntPaginacion .next').classList.add('disabled');
        } else {
            document.querySelectorAll('#cntPaginacion .previous,#cntPaginacion .next').forEach(e => e.classList.remove('disabled'))
        }

        //Add Click Pagination Event

        document.querySelectorAll("#cntPaginacion a").forEach(a => {
            a.addEventListener('click', function () {
                let nPag = this.getAttribute("data-dt-idx");
                let nAct = document.querySelector('#cntPaginacion .active a').getAttribute("data-dt-idx");

                if (nPag == 0) {
                    nPag = nAct - 1;
                } else if (nPag == (info.nPageTot + 1)) {
                    nPag = (nAct * 1) + 1;
                }

                if (nPag != nAct && (nPag != 0 && nPag != info.nPageTot + 1)) {
                    GetSearchData(nPag, info.nPageSize);
                }

            });
        });
    }
        // Add Click Publication Detail Event

        document.querySelectorAll("#" + tableId + " tbody tr").forEach(tr => {
            tr.addEventListener('click', function () {
                let sel = document.querySelector("#" + tableId + " tbody tr.seleccionado");
                if (sel) {
                    sel.classList.remove('seleccionado')
                };

                this.classList.add("seleccionado");

                let pubId = this.firstElementChild.innerHTML;

                console.log(pubId);
                PublicationDetail2(pubId);
            });
        });

}


function PublicationDetail(id, point) {
    let pointId = document.getElementById("pubId");
    if (pointId) {
        pointId = pointId.innerHTML;
    }

    if (pointId != id) {

        document.getElementById("sidebar").classList.remove("collapsed");
        document.getElementById("cntBusqueda").style.display = 'none';
        document.getElementById("cntDet").style.display = 'none';
        document.getElementById("ldBuscarId").style.display = '';

        let req = new XMLHttpRequest();
        req.open('GET', 'http://localhost:59423/api/PublicacionApi/GetDatosPublicacion?id=' + id, true);
        req.setRequestHeader('Content-Type', 'application/json');

        req.onload = function () {
            if (req.status >= 200 && req.status < 400) {

                let pub = JSON.parse(req.responseText);
                console.log(pub);

                let pointDet = "";
                pub.lsFeatures.forEach(p => {
                    let point = p.Info.split(' ');
                    pointDet += `<tr>
                                <td style="text-align: center; vertical-align: middle;">${ p.Id}</td>
                                <td style="text-align: center; vertical-align: middle;">${ point[0]}</td>
                                <td style="text-align: center; vertical-align: middle;">${ point[1]}</td>
                                <td onclick="' + 'zoom_punto(${ point[1] + "," + point[0]});' + '" class="edit" style="cursor: pointer;text-align: center;vertical-align: middle;">
                    <span style="color: #439b43;font-size:18px;" class="fas fa-fw fa-eye" aria-hidden="true"></span>
                    </td></tr>`;
                });

                let tipo = document.querySelector(`#ddlTipo option[value="${pub.tipo.id}"]`).innerHTML

                let det = `<div class="col-lg-12">
                        <div class="m-t-5">
                            <button title="Eliminar Marcadores" class="btnLimpiar btn btn-danger" type="button" onclick="limpiar_puntos();" disabled>
                                <i class="fas fa-fw fa-trash"></i>
                            </button>
                            <button title="Volver a los resultados" class="btn btn-success pull-right" type="button" onclick="Resultado();">
                                <i class="fas fa-fw fa-arrow-left"></i>
                            </button>
                        </div>
                    </div>
                    <form class="form-horizontal" action="">
                        <div class="col-lg-12">
                            <div class="form-group m-t-5">
                                <div class="panel panel-success m-b-0">
                                    <div data-toggle="collapse" data-target="#cntDetPubli" class="panel-heading">
                                        <div class="row">
                                            <div class="col-xs-10 col-sm-10 col-md-10 col-lg-11">
                                                <h5>Detalle Publicación: <span id="pubId" class="bold-pub">${pub.Id}</span> </h5>
                                            </div>
                                            <div class="col-xs-2 col-sm-2 col-md-1 col-lg-1">
                                                <i class="fas fa-fw fa-chevron-down"></i>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="cntDetPubli" class="collapsed collapse in" style="height: auto;">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="campo">Título</label>
                                                        <p id="lblTitulo" class="">${pub.titulo}</p>
                                                    </div>
                                                </div>
                                            </div><div class="row">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <div class="form-group">
                                                        <label class="campo">Tipo</label>
                                                        <p id="lblTipo">${tipo}</p>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <div class="form-group">
                                                        <label class="campo">Año Publicación</label>
                                                        <p id="lblAnoPub">${pub.ano}</p>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="campo">Area Temática</label>
                                                        <p id="lblAreaTema">${pub.lsTemas[0].desc}</p>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="campo">Enlace (URL)</label>
                                                        <div class="m-b-5"><a id="lblEnlace" target="_blank" href="${pub.enlace}" title="${pub.enlace}">${pub.enlace.substring(0, 50) + (pub.enlace.length > 40 ? "..." : "")}</a> </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="campo">Referencia Bibliográfica</label>
                                                        <p id="lblRefBi">${pub.refBi}</p>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                    <div class="form-group text-center">
                                                        <label class="campo ">Información Georeferencial</label>
                                                        <table data-edit="false" id="tblCoordenadas" class="table table-bordered table-hover">
                                                            <thead>
                                                                <tr>
                                                                    <th style="width: 50px;">Punto</th>
                                                                    <th style="width: 200px;">Longitud</th>
                                                                    <th style="width: 200px;">Latitud</th>
                                                                    <th id="btnTodos" class="pointer" onclick="" style="width: 50px;font-size: 18px"><i class="fas fa-eye"></i></th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>${pointDet}</tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                    </form>
                    `;

                document.getElementById("cntDet").innerHTML = det;

                document.getElementById("cntDet").style.display = '';
                document.getElementById("ldBuscarId").style.display = 'none';
                SelectPointsTable(point);
                featureOverlay.getSource().clear();
                SelectAllPointFeatures(pub.lsFeatures.map(f => f.Id));

            } else {
                console.log('We encountered an error!');
            }
        }
        req.send();

    } else {
        document.getElementById("cntDet").style.display = '';
        document.getElementById("ldBuscarId").style.display = 'none';
        document.getElementById("cntBusqueda").style.display = 'none';
        DeselectPointsTable();
        SelectPointsTable(point);
    }
}


function PublicationDetail2(id, point) {
    let pointId = document.getElementById("pubId");
    if (pointId) {
        pointId = pointId.innerHTML;
    }

    if (pointId != id) {

        document.getElementById("sidebar").classList.remove("collapsed");
        document.getElementById("cntBusqueda").style.display = 'none';
        document.getElementById("cntDet").style.display = 'none';
        document.getElementById("ldBuscarId").style.display = '';

                let p = GetPublicationByIdFromGlobal(id);
                console.log(p);

                let pointDet = "";
                p.points.sort(OrderPointsAsc).forEach(p => {
                    pointDet += `<tr>
                                <td style="text-align: center; vertical-align: middle;">${ p.id }</td>
                                <td style="text-align: center; vertical-align: middle;">${ p.x }</td>
                                <td style="text-align: center; vertical-align: middle;">${ p.y }</td>
                                <td onclick="' + 'zoom_punto(${ p.x + "," + p.y});' + '" class="edit" style="cursor: pointer;text-align: center;vertical-align: middle;">
                    <span style="color: #439b43;font-size:18px;" class="fas fa-fw fa-eye" aria-hidden="true"></span>
                    </td></tr>`;
                });

                let tipo = document.querySelector(`#ddlTipo option[value="${p.ti}"]`).innerHTML
                let tema = document.querySelector(`#ddlTema option[value="${p.te}"]`).innerHTML

                let det = `<div class="col-lg-12">
                        <div class="m-t-5">
                            <button title="Volver a los resultados" class="btn btn-success pull-right" type="button" onclick="Resultado();">
                                <i class="fas fa-fw fa-arrow-left"></i>
                            </button>
                        </div>
                    </div>
                    <form class="form-horizontal" action="">
                        <div class="col-lg-12">
                            <div class="form-group m-t-5">
                                <div class="panel panel-success m-b-0">
                                    <div data-toggle="collapse" data-target="#cntDetPubli" class="panel-heading">
                                        <div class="row">
                                            <div class="col-xs-10 col-sm-10 col-md-10 col-lg-11">
                                                <h5>Detalle Publicación: <span id="pubId" class="bold-pub">${p.id}</span> </h5>
                                            </div>
                                            <div class="col-xs-2 col-sm-2 col-md-1 col-lg-1">
                                                <i class="fas fa-fw fa-chevron-down"></i>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="cntDetPubli" class="collapsed collapse in" style="height: auto;">
                                        <div class="panel-body">
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="campo">Título</label>
                                                        <p id="lblTitulo" class="">${p.t}</p>
                                                    </div>
                                                </div>
                                            </div><div class="row">
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <div class="form-group">
                                                        <label class="campo">Tipo</label>
                                                        <p id="lblTipo">${tipo}</p>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                                    <div class="form-group">
                                                        <label class="campo">Año Publicación</label>
                                                        <p id="lblAnoPub">${p.a}</p>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="campo">Area Temática</label>
                                                        <p id="lblAreaTema">${tema}</p>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="campo">Enlace (URL)</label>
                                                        <div class="m-b-5"><a id="lblEnlace" target="_blank" href="${pub.e}" title="${pub.e}">${p.e.substring(0, 50) + (p.e.length > 40 ? "..." : "")}</a> </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                    <div class="form-group">
                                                        <label class="campo">Referencia Bibliográfica</label>
                                                        <p id="lblRefBi">${p.r}</p>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                    <div class="form-group text-center">
                                                        <label class="campo ">Información Georeferencial</label>
                                                        <table data-edit="false" id="tblCoordenadas" class="table table-bordered table-hover">
                                                            <thead>
                                                                <tr>
                                                                    <th style="width: 50px;">Punto</th>
                                                                    <th style="width: 200px;">Longitud</th>
                                                                    <th style="width: 200px;">Latitud</th>
                                                                    <th id="btnTodos" class="pointer" onclick="" style="width: 50px;font-size: 18px"><i class="fas fa-eye"></i></th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>${pointDet}</tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                    </form>
                    `;

                document.getElementById("cntDet").innerHTML = det;

                document.getElementById("cntDet").style.display = '';
                document.getElementById("ldBuscarId").style.display = 'none';
                SelectPointsTable(point);
                featureOverlay.getSource().clear();
                SelectAllPointFeatures(p.points.map(p => p.id));

    } else {
        document.getElementById("cntDet").style.display = '';
        document.getElementById("ldBuscarId").style.display = 'none';
        document.getElementById("cntBusqueda").style.display = 'none';
        DeselectPointsTable();
        SelectPointsTable(point);
    }
}




function Resultado() {
    document.getElementById("cntBusqueda").style.display = '';
    document.getElementById("cntDet").style.display = 'none';
    featureOverlay.getSource().clear();
    SelectAllPointFeatures(searchPoints);
}

function DeselectPointsTable() {
    document.querySelectorAll("#tblCoordenadas tr.seleccionado").forEach(tr => tr.classList.remove('seleccionado'));
}

function SelectPointsTable(points) {
    if (points == undefined) {return}
    let arrPunto = isNaN(points) ? points.split(",") : [points.toString()];

    document.querySelectorAll("#tblCoordenadas tbody tr").forEach(tr => {
        if (arrPunto.includes(tr.querySelector("td").innerHTML)) {
            tr.classList.add('seleccionado');
        }
    });
}

function SelectAllPointFeatures(points) {
    let pointFeatures = [];

    if (points == null) { return }
    points.forEach(p => pointFeatures.push(pointsLayer.getSource().getFeatureById(p)));
    featureOverlay.getSource().addFeatures(pointFeatures);
}

function GetAllPointsSelectedIds() {
    return featureOverlay.getSource().getFeatures().map(x => x.getProperties().idpunto).filter(a => a != undefined);
}

function GeAllPublicationSelectedIds() {
    return featureOverlay.getSource().getFeatures().map(x => x.getProperties().idpublicacion).filter(a => a != undefined).filter((v, i, a) => a.indexOf(v) === i);
}

function GetFeatureRegionById(id){
    return regionsLayer.getSource().getFeatures().find(x => x.getProperties().FIRST_IDDP == id)
}

function GetFeatureProvinciaById(id) {
    return provinciasLayer.getSource().getFeatures().find(x => x.getProperties().FIRST_IDPR == id)
}

function GetFeatureDistritoById(id) {
    return distritoLayer.getSource().getFeatures().find(x => x.getProperties().IDDIST == id)
}


function GetFeatureLastUbigeoSelected() {
    let dep = document.getElementById("ddlDepartamento").value;
    let pro = document.getElementById("dllProvincias").value;
    let dis = document.getElementById("ddlDistrito").value;

    return dis ? GetFeatureDistritoById(dis) : (pro ? GetFeatureProvinciaById(pro) : (dep ? GetFeatureRegionById(dep) : ""));
}

function FilterPointsByUbigeo(points) {
    ubigeoFeature = GetFeatureLastUbigeoSelected();

    if (!ubigeoFeature || !points) { return points }

    let polygon = ubigeoFeature.getGeometry();
    return points.filter(poi => polygon.intersectsExtent(pointsLayer.getSource().getFeatureById(poi).getGeometry().getExtent()));
}

function SelectLastUbigeoFeature() {
    let ubigioFeature = GetFeatureLastUbigeoSelected();
    if (ubigioFeature) {
        featureOverlay.getSource().addFeature(ubigioFeature);
    }
} 



function ClearSearchResults() {
    document.getElementById('cntListaResultados').innerHTML = "";
}


// Global Functions

function GetPublicationByIdFromGlobal(id) {
    return pub.find(p => p.id == id);
}

function OrderPointsAsc(a, b) {
    return (a.id > b.id) ? 1 : ((b.id > a.id) ? -1 : 0);
}

function GetPublicationsByIdsFromGlobal(arrIds) {
    return pub.filter(u => arrIds.includes(u.id))
}

function GetPointsByPublicationIds(arrIds) {
    if (!arrIds) { return;}

    let points = [];
    pub.filter(u => arrIds.includes(u.id)).forEach(p => p.points.forEach(c => points.push(c.id)))
    return points;
}

function GetPublicationsByPagination(page, size, arrTotal) {
    let arrPag = [];
    for (let i = (page * size) - size, j = (page * size) - 1; i <= j; i++) {
        arrPag.push(arrTotal[i]);
        if (i == arrTotal.length - 1) break;
    }
    return arrPag;
}

function ListSearchPublications(page, size) {

    let tableId = "tblResultados";


    let total = searchPub.length;
    let pages = parseInt(total / 10) + (total % 10 ? 1 : 0);
    let totalPoints = searchPoints.length || 0;

    let arrIds = GetPublicationsByPagination(page, size, searchPub);
    let p = GetPublicationsByIdsFromGlobal(arrIds);

    let html = `<div class="text-left"><span id="page-result">P&aacute;gina ${page} a ${pages || 0} de  ${total || 0} publicaciones</span></div>
                <div class="text-right"><span id="cant-pub"> Puntos encontrados ${totalPoints}</span></div>
                  <table id="${tableId}" class="table table-bordered table-hover">
                    <thead>
                        <th style="display: none;">#</th>
                        <th style="width: 150px;"> Título </th>
                        <th style="width: 200px;"> Referencia Bibliográfica </th>
                        <th style="width: 50px;"> Det. </th>
                    </thead>
                    <tbody>
                    `;

    if (p != undefined) {
        for (let i = 0, j = p.length; i < j ; i++) {
            html += `
                                    <tr index='${i}'>
                                        <td style="display: none;text-align: center;vertical-align: middle;">${p[i].id}</td>
                                        <td style="text-align: left; vertical-align: middle;">${p[i].t}</td>
                                        <td style="text-align: left; vertical-align: middle;">${p[i].r}</td>
                                        <td class="edit" style="cursor: pointer;text-align: center;vertical-align: middle;">
                                            <span style="color: #439b43;font-size:15px;" class="fas fa-fw fa-search" aria-hidden="true">
                                            </span>
                                        </td>
                                    </tr>`;
        }
    }

    html += `</tbody></table>`;

    //Add Pagination
    if (pages > 1) {

        html += `<div class="text-center" id="cntPaginacion">
                    <ul class="pagination m-t-10 m-b-10">
                        <li class="previous" id="example1_previous">
                        <a data-dt-idx="0" id="pag_0" tabindex="0"><<</a></li>`;


        let ini = 1;
        let fin = pages;

        if (pages > 5) {

            fin = ini + 4;

            if (page > 2 && page <= (pages - 3)) {
                ini = page - 2;
                fin = ini + 4;
            } else if (page > (pages - 3)) {
                ini = pages - 4;
                fin = pages;
            }
        }

        for (let i = ini; i <= fin; i++) {
            html += `<li><a data-dt-idx="${i}" id="pag_${i}">${i}</a></li>`;
        }

        html += `<li class="next" id="example1_next">
                 <a data-dt-idx="${ pages + 1}" id="pag_${pages + 1}">>></a>
                 </li></ul></div>`;

    }

    document.getElementById('cntListaResultados').innerHTML = html;

    if (pages > 1) {
        let activePage = document.getElementById("pag_" + page);

        activePage.parentNode.classList.add('active');
        activePage.focus();

        if (page == 1) {
            document.querySelector('#cntPaginacion .previous').classList.add('disabled');
        } else if (page == pages) {
            document.querySelector('#cntPaginacion .next').classList.add('disabled');
        } else {
            document.querySelectorAll('#cntPaginacion .previous,#cntPaginacion .next').forEach(e => e.classList.remove('disabled'))
        }

        //Add Click Pagination Event

        document.querySelectorAll("#cntPaginacion a").forEach(a => {
            a.addEventListener('click', function () {
                let nPag = this.getAttribute("data-dt-idx");
                let nAct = document.querySelector('#cntPaginacion .active a').getAttribute("data-dt-idx");

                if (nPag == 0) {
                    nPag = nAct - 1;
                } else if (nPag == (pages + 1)) {
                    nPag = (nAct * 1) + 1;
                }

                if (nPag != nAct && (nPag != 0 && nPag != pages + 1)) {
                    ListSearchPublications(nPag, size);
                }

            });
        });
    }
    // Add Click Publication Detail Event

    document.querySelectorAll("#" + tableId + " tbody tr").forEach(tr => {
        tr.addEventListener('click', function () {
            let sel = document.querySelector("#" + tableId + " tbody tr.seleccionado");
            if (sel) {
                sel.classList.remove('seleccionado')
            };

            this.classList.add("seleccionado");

            let pubId = this.firstElementChild.innerHTML;

            console.log(pubId);
            PublicationDetail2(pubId);
        });
    });

}