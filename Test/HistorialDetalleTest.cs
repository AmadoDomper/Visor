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
    public class HistorialDetalleTest
    {
        [TestMethod]
        public void Test_RegistrarSolicitudHistorialDetalle()
        {
            HistorialDetalleLN oHistDetLN = new HistorialDetalleLN();
            HistorialDetalle oHistDet = new HistorialDetalle();

            oHistDet.nHistorialId = 4;
            oHistDet.nUsuarioRegistra = 218;
            oHistDet.cDescripcion = "Primer registro del historial de la publicación";
            oHistDet.Estado = (int)EstadoSolicitud.Solicitado;

            var nHisDet = oHistDetLN.RegistrarHistorialDetalle(oHistDet);

            Assert.IsTrue(nHisDet > 0, "Resultado no es mayor que 0");

        }

        [TestMethod]
        public void Test_RegistrarObservarHistorialDetalle()
        {
            HistorialDetalleLN oHistDetLN = new HistorialDetalleLN();
            HistorialDetalle oHistDet = new HistorialDetalle();

            oHistDet.nHistorialId = 1;
            oHistDet.nUsuarioRegistra = 2;
            oHistDet.cDescripcion = "Es necesario corregir las referencias bibliograficas de acuerdo a lo indicado en los estandares. ";
            oHistDet.Estado = (int)EstadoSolicitud.Observado;

            var nHisDet = oHistDetLN.RegistrarHistorialDetalle(oHistDet);

            Assert.IsTrue(nHisDet > 0, "Resultado no es mayor que 0");

        }

        [TestMethod]
        public void Test_RegistrarRechazoHistorialDetalle()
        {
            HistorialDetalleLN oHistDetLN = new HistorialDetalleLN();
            HistorialDetalle oHistDet = new HistorialDetalle();

            oHistDet.nHistorialId = 1;
            oHistDet.nUsuarioRegistra = 2;
            oHistDet.cDescripcion = "Agredecemos su registro, sin embargo no se cuenta con la información correcta para crear una publicación. ";
            oHistDet.Estado = (int)EstadoSolicitud.Rechazado;

            var nHisDet = oHistDetLN.RegistrarHistorialDetalle(oHistDet);

            Assert.IsTrue(nHisDet > 0, "Resultado no es mayor que 0");

        }

        [TestMethod]
        public void Test_RegistrarAprobacionHistorialDetalle()
        {
            HistorialDetalleLN oHistDetLN = new HistorialDetalleLN();
            HistorialDetalle oHistDet = new HistorialDetalle();

            oHistDet.nHistorialId = 1;
            oHistDet.nUsuarioRegistra = 2;
            oHistDet.cDescripcion = "";
            oHistDet.Estado = (int)EstadoSolicitud.Aprobado;

            var nHisDet = oHistDetLN.RegistrarHistorialDetalle(oHistDet);

            Assert.IsTrue(nHisDet > 0, "Resultado no es mayor que 0");

        }

        [TestMethod]
        public void Test_GetHistorialDetalleJSON()
        {
            HistorialDetalleLN oHistDetLN = new HistorialDetalleLN();
            string cHistorialId = "f68b3317-6861-4505-90c6-cd0d43c4d65c";

            var cJsonHist = oHistDetLN.GetHistorialDetalleJSON(cHistorialId);

            Assert.IsTrue(cJsonHist != null, "Resultado no es mayor que 0");

        }
    }
}
