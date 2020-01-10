using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using EPostgres;
using EPostgres.Helper;
using LNPostgres;

namespace Test
{
    [TestClass]
    public class AlertaTest
    {
        [TestMethod]
        public void Test_RegistrarAlerta_Rechazado()
        {
            AlertaLN oAlertaLN = new AlertaLN();
            Alerta oAlerta = new Alerta();

            oAlerta.nUsuarioId = 1;
            oAlerta.cTitulo = "Solicitud rechazada";
            oAlerta.cMensaje = "Su solicitud rechazada. Favor verifique los detalles en el historial.";
            oAlerta.cUrl = @"http://localhost:59423/Gestion/#/Publicacion/RevisionDeSolicitudes/1";
            oAlerta.cAlertaIcono = AlertIcon.Rechazado;
            oAlerta.cAlertaColor = AlertColor.Rechazado;
            oAlerta.nEstado = 1;

            var nAlertaId = oAlertaLN.RegistrarAlerta(oAlerta);

            Assert.IsTrue(nAlertaId > 0, "Resultado no es mayor que 0");

        }

        [TestMethod]
        public void Test_RegistrarAlerta_Aprobado()
        {
            AlertaLN oAlertaLN = new AlertaLN();
            Alerta oAlerta = new Alerta();

            oAlerta.nUsuarioId = 1;
            oAlerta.cTitulo = "¡Solicitud Aprobada!";
            oAlerta.cMensaje = "Su solicitud ha sido aprobada. Muchas gracias por su trabajo.";
            oAlerta.cUrl = @"http://localhost:59423/Gestion/#/Publicacion/RevisionDeSolicitudes/1";
            oAlerta.cAlertaIcono = AlertIcon.Aprobado;
            oAlerta.cAlertaColor = AlertColor.Aprobado;
            oAlerta.nEstado = 1;

            var nAlertaId = oAlertaLN.RegistrarAlerta(oAlerta);

            Assert.IsTrue(nAlertaId > 0, "Resultado no es mayor que 0");

        }

        [TestMethod]
        public void Test_ActualizarAlertaVisto()
        {
            AlertaLN oAlertaLN = new AlertaLN();

            int nUsuario = 1;

            var nResultado = oAlertaLN.ActualizaAlertaVisto(nUsuario);

            Assert.IsTrue(nResultado > 0, "Resultado no es mayor que 0");

        }

        [TestMethod]
        public void Test_ActualizaAlertaRevisado()
        {
            AlertaLN oAlertaLN = new AlertaLN();

            int nUsuario = 1;
            int nAlertaId = 1;

            var nResultado = oAlertaLN.ActualizaAlertaRevisado(nUsuario, nAlertaId);

            Assert.IsTrue(nResultado > 0, "Resultado no es mayor que 0");

        }

        [TestMethod]
        public void Test_GetAlertasJSON()
        {
            AlertaLN oAlertaLN = new AlertaLN();
            int nUsuario = 1;
            int nPage = 1;
            int nSize = 5;

            var cJsonAlertas = oAlertaLN.GetAlertasJSON(nUsuario, nPage, nSize);

            Assert.IsTrue(cJsonAlertas != null, "Resultado no es mayor que 0");

        }
    }
}
