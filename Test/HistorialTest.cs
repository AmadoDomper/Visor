using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EPostgres;
using EPostgres.Helper;
using LNPostgres;

namespace Test
{
    [TestClass]
    public class HistorialTest
    {
        [TestMethod]
        public void Test_CrearHistorial()
        {
            HistorialLN oHistorialLN = new HistorialLN();
            Historial oHistorial = new Historial();
            oHistorial.nRefId = 2;
            oHistorial.nTipoReferencia = 1;
            oHistorial.dFechaCreacion = DateTime.Now;
            oHistorial.nEstado = 1;

            var nHistId =  oHistorialLN.CrearHistorial(oHistorial);

            Assert.IsTrue(nHistId > 0, "Resultado no es mayor que 0");
        }

        [TestMethod]
        public void Test_GetRecordIdByPublicationId()
        {
            HistorialLN oHistorialLN = new HistorialLN();
            Historial oHistorial = new Historial();

            var nHistId = oHistorialLN.GetRecordIdByReferenciaId(1, TipoReferencia.Publicaciones);

            Assert.IsTrue(nHistId > 0, "Resultado no es mayor que 0");
        }

    }
}
