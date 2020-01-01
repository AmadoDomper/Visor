using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EPostgres;
using EPostgres.Helper;
using LNPostgres;
using Newtonsoft.Json;


namespace Test
{
    [TestClass]
    public  class PublicacionTest
    {

        [TestMethod]
        public void Test_GetAllPublicationPoints()
        {
            PublicacionLN oPubli = new PublicacionLN();
            object json;

            json = oPubli.GetAllPublicationPoints();

            Assert.IsNotNull(json);
        }


        [TestMethod]
        public void Test_ActualizaEstadoPublicacion()
        {
            PublicacionLN oPubli = new PublicacionLN();

            int nPubId = 1;
            int nEstado = (int)EstadoSolicitud.Observado;

            var result = oPubli.ActualizaEstadoPublicacion(nPubId, nEstado);

            Assert.IsTrue(result > 0, "Resultado no es mayor que 0");
        }


    }
}
