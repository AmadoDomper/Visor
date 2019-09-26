using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EPostgres;
using LNPostgres;

namespace Test
{
    [TestClass]
    public class VegetacionParcelaTest
    {
        [TestMethod]
        public void Test_RegistraInventarioVegetacionParcela()
        {
            VegetacionParcelaLN oVegParLN = new VegetacionParcelaLN();
            VegetacionParcela oVegPar = new VegetacionParcela();
            int id;

            oVegPar.nVegetacionId = 27;
            oVegPar.cDepartamento = "Departamento Prueba";
            oVegPar.cRegistrador = "Amado Domper Test";
            oVegPar.cLongitud = "642854";
            oVegPar.cLatitud = "9525314";
            oVegPar.cWkb = "-60.060316 22.422044";
            oVegPar.cCodigoMuestra = "RTC - 1";
            oVegPar.cAltitud = "132";
            oVegPar.cPrecision = "± 7";
            oVegPar.cTipoVegetacion = "Bosque colinoso";
            oVegPar.cClaseFisonomica = "Bosque";
            oVegPar.cCobertura = "Semi cerrada";
            oVegPar.cConfianzaClasificacion = "Alta";  
            oVegPar.cClaseHidrologica = "Tierra firme bien drenada";
            oVegPar.cFisiografia = "Colina";
            oVegPar.cAltitudSistemaHidrico = "106";

            id = oVegParLN.RegistraInventarioVegetacionParcela(oVegPar);

            Assert.IsTrue(id > 0, "Resultado no es mayor que 0");
        }

        [TestMethod]
        public void Test_ActualizaInventarioVegetacionParcela()
        {
            VegetacionParcelaLN oVegParLN = new VegetacionParcelaLN();
            VegetacionParcela oVegPar = new VegetacionParcela();
            int id;

            oVegPar.nVegetacionId = 36;
            oVegPar.nParcelaId = 1;
            oVegPar.cDepartamento = "Departamento Prueba Actualiza";
            oVegPar.cRegistrador = "Amado Domper Actualiza";
            oVegPar.cLongitud = "-60.060316";
            oVegPar.cLatitud = "22.422045";
            oVegPar.cWkb = "-60.060316 22.422045";
            oVegPar.cCodigoMuestra = "RTC - 12";
            oVegPar.cAltitud = "1322";
            oVegPar.cPrecision = "± 71";
            oVegPar.cTipoVegetacion = "Bosque colinoso Actualiza";
            oVegPar.cClaseFisonomica = "Bosque Actualiza";
            oVegPar.cCobertura = "Semi cerrada Actualiza";
            oVegPar.cConfianzaClasificacion = "Alta Actualiza";
            oVegPar.cClaseHidrologica = "Tierra firme bien drenada Actualiza";
            oVegPar.cFisiografia = "Colina Actualiza";
            oVegPar.cAltitudSistemaHidrico = "105";

            id = oVegParLN.ActualizaInventarioVegetacionParcela(oVegPar);

            Assert.IsTrue(id > 0, "Resultado no es mayor que 0");
        }

        [TestMethod]
        public void Test_EliminaInventarioVegetacionParcela()
        {
            VegetacionParcelaLN oVegParLN = new VegetacionParcelaLN();
            VegetacionParcela oVegPar = new VegetacionParcela();
            int id;

            oVegPar.nVegetacionId = 35;
            oVegPar.nParcelaId = 2;

            id = oVegParLN.EliminaInventarioVegetacionParcela(oVegPar);

            Assert.IsTrue(id > 0, "Resultado no es mayor que 0");
        }


        [TestMethod]
        public void Test_CargaDatosInventarioVegetacionParcelas()
        {
            VegetacionParcelaLN oVegParLN = new VegetacionParcelaLN();
            List<VegetacionParcela> oListaVegPar = new List<VegetacionParcela>();
            int nParId = 1;

            oListaVegPar = oVegParLN.CargaListaInventarioVegetacionParcelas(nParId);

            Assert.IsNotNull(oListaVegPar);
        }

        [TestMethod]
        public void Test_CargaInventarioVegetacionParcela()
        {
            VegetacionParcelaLN oVegParLN = new VegetacionParcelaLN();
            VegetacionParcela oVegPar = new VegetacionParcela();
            int nParId = 10;


            oVegPar = oVegParLN.CargaInventarioVegetacionParcela(nParId);

            Assert.IsNotNull(oVegPar);
        }

    }
}
