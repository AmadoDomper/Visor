using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EPostgres;
using LNPostgres;

namespace Test
{
    [TestClass]
    public class InventarioSuelosTest
    {
        [TestMethod]
        public void Test_RegistraInventarioSuelos()
        {
            InventarioSuelosLN oInvSueLN = new InventarioSuelosLN();
            InventarioSuelos oInvSue = new InventarioSuelos();
            int id;

            oInvSue.cAnoColecta = "2027";
            oInvSue.cNombreProyecto = "Proyecto Prueba Unit Test";
            oInvSue.cNombreColector = "Jose Perez";
            oInvSue.nUsuarioId = 2;

            id = oInvSueLN.RegistraInventarioSuelos(oInvSue);

            Assert.IsTrue(id > 0, "Resultado no es mayor que 0");
        }

        [TestMethod]
        public void Test_ActualizaInventarioSuelos()
        {
            InventarioSuelosLN oInvSueLN = new InventarioSuelosLN();
            InventarioSuelos oInvSue = new InventarioSuelos();
            int id;

            oInvSue.nSuelosId = 1;
            oInvSue.cAnoColecta = "2027";
            oInvSue.cNombreColector = "Jose Perez";
            oInvSue.cNombreProyecto = "Proyecto Prueba Unit Test 2";

            id = oInvSueLN.ActualizaInventarioSuelos(oInvSue);

            Assert.IsTrue(id > 0, "Resultado no es mayor que 0");
        }

        [TestMethod]
        public void Test_EliminaInventarioSuelos()
        {
            InventarioSuelosLN oInvSueLN = new InventarioSuelosLN();
            InventarioSuelos oInvSue = new InventarioSuelos();
            int id;

            oInvSue.nSuelosId = 2;

            id = oInvSueLN.EliminaInventarioSuelos(oInvSue);

            Assert.IsTrue(id > 0, "Resultado no es mayor que 0");
        }

        [TestMethod]
        public void Test_CargarDatosInventarioSuelos()
        {
            InventarioSuelosLN oInvSueLN = new InventarioSuelosLN();
            InventarioSuelos oInvSue = new InventarioSuelos();
            int nSueId = 1;

            oInvSue = oInvSueLN.CargaDatosInventarioSuelos(nSueId);

            Assert.IsNotNull(oInvSue);
        }

        [TestMethod]
        public void Test_ListarMisInventariosVegetacion()
        {
            InventarioSuelosLN oInvSueLN = new InventarioSuelosLN();
            ListaPaginada oListaVeg = new ListaPaginada();

            oListaVeg = oInvSueLN.ListarMisInventariosSuelos(1, 1, 5, "", "", "",1);

            Assert.IsNotNull(oListaVeg);
        }




    }
}
