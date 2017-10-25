(function ($) {
    $.fn.Conexion = function (m) {
        /// <signature>
        ///   <summary>Conexión AJAX.</summary>
        ///   <param name="m" type="">CMACM - Objeto con los datos para la conexión AJAX</param>
        ///   <returns type="jQuery" />
        /// </signature>

        var terminado = m.terminado || function () { };
        var durante = m.durante || function () { };
        var fError = m.error || function () { };
        var bloqueo = m.bloqueo || false;
        var async = m.async || true;
        var contentType = m.contentType || "application/x-www-form-urlencoded; charset=UTF-8";
        
        
        
        var resultadoajax;
        //$.ajaxSetup({ 'beforeSend': function (xhr) { xhr.setRequestHeader("Accept", "text/javascript,application/javascript,text/html") } });

        if (bloqueo) { BloquearCarga(); }

        $.ajax({
            type: "POST",
            async: async,
            url: m.direccion,
            //dataType: "json",
            data: m.datos,
            contentType: m.contentType,
            beforeSend: function () { durante() },
            error: function (v, status) { fError(v, status) },
            success: function (data) {
                if (bloqueo) { DesbloquearCarga(); }
                terminado(data);
                resultadoajax = data;
            }
        });
        //return resultadoajax;
    }
})(jQuery);


function BloquearCarga() {
    var html = "";
    html=('<div id="vntEspera" class="modal fade espera" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="true"> <div class="modal-dialog modal-sm"> <div class="modal-content"> <div class="row"> <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3"> <img class="img-responsive" src="../Public/img/imgload.gif" /> </div> <div class="col-xs-9 col-sm-9 col-md-9 col-lg-9"> <div class="row"> <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 colorOficial"> <b>Por Favor Espere</b> </div> <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"> <b>La solicitud esta siendo procesada</b> </div> </div> </div> </div> </div> </div> </div>');
    $(html).appendTo('body');
   // corregirModalboots();
    $("#vntEspera").modal('show');
  
   /* var html = "";
    html=('<div class="row"> <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3"> <img class="img-responsive" src="/Content/img/cmacloader.gif" /> </div> <div class="col-xs-9 col-sm-9 col-md-9 col-lg-9"> <div class="row"> <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 colorOficial"> <b>Por Favor Espere</b> </div> <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"> <b>La solicitud esta siendo procesada</b> </div> </div> </div> </div>');

    $.fn.Ventana({
        id: 'vntEspera',
        titulo: 'espera',
        tamano: 'sm',
        cuerpo: html,
    });
    $('#vntEspera').modal('show');*/


}

function DesbloquearCarga() {
    $("#vntEspera").modal('hide');
    //$("div#vntEspera").remove();
    //$(".modal-backdrop").remove();
}