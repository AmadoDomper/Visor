var lsFeatures = Array();
var lsVectorLayers = Array();
var indexFeature = 0;

function quitarElemento(index) {
    for (var i = 0; i < lsFeatures.length; i++) {
        if (lsFeatures[i].n == index) {
            lsFeatures.splice(i, 1);
            map.removeLayer(lsVectorLayers[i])
            lsVectorLayers.splice(i, 1);
            break;
        }
    }
}



/*Control del mapa*/

var vista = new ol.View({
    center: ol.proj.transform([-75, -9], 'EPSG:4326', 'EPSG:3857'),
    zoom: 5
});

var map = new ol.Map({
    target: 'map',
    view: vista,

    controls: ol.control.defaults().extend([
        new ol.control.ScaleLine(),
        new ol.control.ZoomSlider(),
    ])

});



var wmsLayer2 = new ol.layer.Tile({
    source: new ol.source.OSM({
        projection: 'EPSG:3857',
        url: 'http://mt{0-3}.google.com/vt/lyrs=m&x={x}&y={y}&z={z}',
        attributions: [
            //new ol.Attribution({ html: '© Google' }),
            //new ol.Attribution({ html: '<a href="https://developers.google.com/maps/terms">Terms of Use.</a>' })
        ]
    })
});
map.addLayer(wmsLayer2);

var wmsLayer1 = new ol.layer.Tile({
    type: 'base',
    title: 'Mapa Base',
    source: new ol.source.TileWMS({
        url: 'http://10.10.10.14:8082/geoserver/visor/wms',
        params: {
            LAYERS: 'visor:departamentos',
            FORMAT: 'image/png'
        },
    })
});
map.addLayer(wmsLayer1);

function map_AgregarPunto(punto) {

    var point_feature = new ol.Feature({});
    var point_geom = new ol.geom.Point(ol.proj.transform([+punto[0],+punto[1]], 'EPSG:4326', 'EPSG:3857'));
    point_feature.setGeometry(point_geom);
    var vectorLayer = new ol.layer.Vector({
        source: new ol.source.Vector({
            features: [point_feature]
        })
    })
    map.addLayer(vectorLayer);
    lsVectorLayers.push(vectorLayer);
}


function CargarGrilla() {

    $("#jsGrid").jsGrid({
        width: "100%",
        height: "400px",
        sorting: true,
        paging: true,
        deleteConfirm: "¿Estás seguro de eliminar?",
        noDataContent: "Sin datos",
        //data: datos,

        fields: [
            {
                name: "Tipo", type: "text", width: 20, align: "center", itemTemplate: function (value) {
                    if (value == 1) {
                        return $("<div>").addClass("dv_icpunto");
                    }
                    else if (value == 2) {
                        return $("<div>").addClass("dv_iclinea");
                    }
                    else if (value == 3) {
                        return $("<div>").addClass("dv_icpoligono");
                    }
                    else {
                        return $("<div>");
                    }
                }
            },
            { name: "Info", type: "text", width: 200 },
            { type: "control", editButton: false }
        ],
        onItemDeleted: function (args) {
            quitarElemento(args.item.n);
            console.log(lsFeatures);
        }
    });
}


/*Fin control del mapa*/

$(document).ready(function () {
    $('#ddlTipo').dropdowlist({
        dataShow: 'cDesc',
        dataValue: 'nTipoId',
        dataselect: 'nTipoId',
        datalist: Model.lsTipos
    });

    var myDate = new Date();
    var year = myDate.getFullYear(); var values = '<option value="">Elige una opción</option>';
    for (var i = 1900; i < year + 1; i++) {
        values += '<option value="' + i + '">' + i + '</option>';
    }
    $("#txtAnoPublicacion").html(values);

    $('#ddlTema').dropdowlist({
        dataShow: 'cDesc',
        dataValue: 'nTemaId',
        dataselect: 'nTemaId',
        datalist: Model.lsTemas
    });

    /*Grilla*/


    CargarGrilla();
    /*Fin de la grilla*/

    /*multiselet tema*/

    //function formatDatosTemas(lsTemas) {
    //    var datos = new Array();
    //    for (var i = 0; i < lsTemas.length; i++) {
    //        var fila = { label: lsTemas[i].tem_descripcion, title: lsTemas[i].tem_descripcion, value: lsTemas[i].tem_idtema };
    //        datos.push(fila);
    //    }
    //    return datos;
    //}

    //$('#ddlTema').multiselect({
    //    nonSelectedText: 'Elige un tema',
    //    buttonWidth: '250'
    //});
    //$('#ddlTema').multiselect('dataprovider', formatDatosTemas(Model.lsTemas));

    /*fin multiselect tema*/ 
    


    $('#btAddPunto').click(function () {
        $().addPunto({
            alAceptar: function (lat, lng) {
                indexFeature++;
                var info = lng + '|' + lat;
                var item = { "n": indexFeature, "Tipo": 1, "Info": info };
                $("#jsGrid").jsGrid("insertItem", item);
                
                lsFeatures.push(item);
                map_AgregarPunto([lng, lat]);
            }
        });
    });

   /* $('#btAddLinea').click(function () {
        $().addLinea({
            alAceptar: function (lat1, lng1, lat2, lng2) {

            }
        });
    });*/

    $('#btAddPoli').click(function () {
        $().addPoligono({
            alAceptar: function (lsLat, lsLng) {
                indexFeature++;
                var info = '';
                var puntos = [];
                for (var i = 0; i < lsLat.length; i++) {
                    info += lsLat[i] + '|' + lsLng[i];
                    if (i != lsLat.length - 1) {
                        info += ',';
                    }
                    puntos.push([lsLat[i], lsLng[i]]);
                }
                //el poligono debe cerrarse con su punto inicial (como un lazo)
                if (lsLat.length > 0) {
                    puntos.push([lsLat[0], lsLng[0]]);
                }
                var item = { "n": indexFeature, "Tipo": 3, "Info": info };
                $("#jsGrid").jsGrid("insertItem", item);
                lsFeatures.push(item);
                map_AgregarPoligono(puntos);
            }
        });
    });

    $('#btenviar').click(function () {

        if ($('#frm').smkValidate()) {
            $.fn.Conexion({
                direccion: '../Publicacion/RegistrarPublicacion',
                bloqueo: true,
                datos: {
                    "pub_anopublicacion": $('#txtAnoPublicacion').val(),
                    "pub_referenciabibliografica": $('#txtReferencia').val(),
                    "pub_enlace": $('#txtEnlace').val(),
                    "tip_idtipo": $('#ddlTipo').val(),
                    "ls_tem_idtema": $('#ddlTema').val() /* $('#ddlTema').val().join(',')*/,
                    "features": JSON.stringify(lsFeatures),
                    "pub_titulo": $('#txtTitulo').val()
                },
                terminado: function (data) {
                    data = JSON.parse(data);
                    $.fn.Mensaje({ titulo: "Mensaje", mensaje: "La operación se realizó correctamente." });
                    Limpiar();
                    vista("#target1");
                }
            });
        }

        
    });

});