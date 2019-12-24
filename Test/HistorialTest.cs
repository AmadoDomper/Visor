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
            oHistorial.nRefId = 797;
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

            var nHistId = oHistorialLN.GetRecordUniqueIdByReferenciaId(1, TipoReferencia.Publicaciones);

            Assert.IsTrue(nHistId != "", "Resultado no es diferente a vacío");
        }

        [TestMethod]
        public void Test_GetHistorialByPublicationId()
        {
            HistorialLN oHistorialLN = new HistorialLN();
            Historial oHistorial = new Historial();

            oHistorial = oHistorialLN.GetHistorialByReferenciaId(1, TipoReferencia.Publicaciones);

            Assert.IsTrue(oHistorial != null, "Resultado no es diferente a vacío");
        }

    }
}
