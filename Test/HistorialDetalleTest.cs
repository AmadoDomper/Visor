using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EPostgres;
using LNPostgres;

namespace Test
{
    [TestClass]
    public class HistorialDetalleTest
    {
        [TestMethod]
        public void Test_RegistrarHistorialDetalle()
        {
            HistorialDetalleLN oHistDetLN = new HistorialDetalleLN();
            HistorialDetalle oHistDet = new HistorialDetalle();

            oHistDet.nHistorialId = 1;
            oHistDet.nUsuarioRegistra = 1;
            oHistDet.cDescripcion = "Primer registro del historial de la publicación";
            oHistDet.Estado = 1;

            var nHisDet = oHistDetLN.RegistrarHistorialDetalle(oHistDet);

            Assert.IsTrue(nHisDet > 0, "Resultado no es mayor que 0");

        }
    }
}
