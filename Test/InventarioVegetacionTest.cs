using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EPostgres;
using LNPostgres;

namespace Test
{
    [TestClass]
    public class InventarioVegetacionTest
    {
        [TestMethod]
        public void Test_RegistraInventarioVegetacion()
        {
            InventarioVegetacionLN oInvVegLN = new InventarioVegetacionLN();
            InventarioVegetacion oInvVeg = new InventarioVegetacion();
            int id;

            oInvVeg.cAnoColecta = "2027";
            oInvVeg.cNombreProyecto = "Proyecto Prueba Unit Test";
            oInvVeg.nUsuarioId = 2;

            id = oInvVegLN.RegistraInventarioVegetacion(oInvVeg);

            Assert.IsTrue(id > 0, "Resultado no es mayor que 0");
        }

        [TestMethod]
        public void Test_ActualizaInventarioVegetacion()
        {
            InventarioVegetacionLN oInvVegLN = new InventarioVegetacionLN();
            InventarioVegetacion oInvVeg = new InventarioVegetacion();
            int id;

            oInvVeg.nVegetacionId = 2;
            oInvVeg.cAnoColecta = "2027";
            oInvVeg.cNombreProyecto = "Proyecto Prueba Unit Test 2";

            id = oInvVegLN.ActualizaInventarioVegetacion(oInvVeg);

            Assert.IsTrue(id > 0, "Resultado no es mayor que 0");
        }

        [TestMethod]
        public void Test_EliminaInventarioVegetacion()
        {
            InventarioVegetacionLN oInvVegLN = new InventarioVegetacionLN();
            InventarioVegetacion oInvVeg = new InventarioVegetacion();
            int id;

            oInvVeg.nVegetacionId = 35;

            id = oInvVegLN.EliminaInventarioVegetacion(oInvVeg);

            Assert.IsTrue(id > 0, "Resultado no es mayor que 0");
        }

        [TestMethod]
        public void Test_CargarDatosInventarioVegetacion()
        {
            InventarioVegetacionLN oInvVegLN = new InventarioVegetacionLN();
            InventarioVegetacion oInvVeg = new InventarioVegetacion();
            int nVegId = 36;

            oInvVeg = oInvVegLN.CargaDatosInventarioVegetacion(nVegId);

            Assert.IsNotNull(oInvVeg);
        }

        [TestMethod]
        public void Test_ListarMisInventariosVegetacion()
        {
            InventarioVegetacionLN oInvVegLN = new InventarioVegetacionLN();
            ListaPaginada oListaVeg= new ListaPaginada();

            oListaVeg = oInvVegLN.ListarMisInventariosVegetacion(1,1,5,"","",1);

            Assert.IsNotNull(oListaVeg);
        }

    }
}
