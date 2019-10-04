using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EPostgres;
using LNPostgres;

namespace Test
{
    [TestClass]
    public class SuelosPerfilModalTest
    {
        [TestMethod]
        public void Test_RegistraInventarioSuelosPerfilModal()
        {
            SuelosPerfilModalLN oSuePerModLN = new SuelosPerfilModalLN();
            SuelosPerfilModal oSuePerMod = new SuelosPerfilModal();
            int id;

            oSuePerMod.nSuelosId = 1;
            //oSuePerMod.nPerfilId = 1;
            oSuePerMod.cDepartamento = "San martin";
            oSuePerMod.cLongitud = "-30.060316";
            oSuePerMod.cLatitud = "12.422044";
            oSuePerMod.cWkb = "-30.060316 12.422044";
            oSuePerMod.cPerfilModal = "Calera ii";
            oSuePerMod.cNroCalicata = "M8";
            oSuePerMod.cMsnm = "";
            oSuePerMod.cZonaMuestreo = "Alto bello horizonte";
            oSuePerMod.cClasificacionNatural = "Lithic ustorthents";
            oSuePerMod.cFisiografia = "Ladera de montaña";
            oSuePerMod.cPendiente = "20";
            oSuePerMod.cRelieve = "Montañoso";
            oSuePerMod.cClima = "Cálido ligeramente húmedo";
            oSuePerMod.cZonaVida = "Cálido ligeramente húmedo";
            oSuePerMod.cMaterialParental = "Rocas sedimentarias en areniscas";
            oSuePerMod.cVegetacion = "Rocas sedimentarias en areniscas";
            oSuePerMod.cModoColecta = "";
            oSuePerMod.cDrenaje = "Moderadamente bien drenado";
            oSuePerMod.cProfundidadEfectiva = "";
            oSuePerMod.cFactorLimitante = "";

            id = oSuePerModLN.RegistraInventarioSuelosPerfilModal(oSuePerMod);

            Assert.IsTrue(id > 0, "Resultado no es mayor que 0");
        }

        [TestMethod]
        public void Test_ActualizaInventarioSuelosPerfilModal()
        {
            SuelosPerfilModalLN oSuePerModLN = new SuelosPerfilModalLN();
            SuelosPerfilModal oSuePerMod = new SuelosPerfilModal();
            int id;

            oSuePerMod.nSuelosId = 1;
            oSuePerMod.nPerfilId = 1;
            oSuePerMod.cDepartamento = "San martin";
            oSuePerMod.cLongitud = "-30.060316";
            oSuePerMod.cLatitud = "12.422044";
            oSuePerMod.cWkb = "-30.060316 12.422044";
            oSuePerMod.cPerfilModal = "Calera ii";
            oSuePerMod.cNroCalicata = "M8";
            oSuePerMod.cMsnm = "";
            oSuePerMod.cZonaMuestreo = "Alto bello horizonte";
            oSuePerMod.cClasificacionNatural = "Lithic ustorthents";
            oSuePerMod.cFisiografia = "Ladera de montaña";
            oSuePerMod.cPendiente = "20";
            oSuePerMod.cRelieve = "Montañoso";
            oSuePerMod.cClima = "Cálido ligeramente húmedo";
            oSuePerMod.cZonaVida = "Cálido ligeramente húmedo";
            oSuePerMod.cMaterialParental = "Rocas sedimentarias en areniscas";
            oSuePerMod.cVegetacion = "Rocas sedimentarias en areniscas";
            oSuePerMod.cModoColecta = "";
            oSuePerMod.cDrenaje = "Moderadamente bien drenado";
            oSuePerMod.cProfundidadEfectiva = "";
            oSuePerMod.cFactorLimitante = "";

            id = oSuePerModLN.ActualizaInventarioSuelosPerfilModal(oSuePerMod);

            Assert.IsTrue(id > 0, "Resultado no es mayor que 0");
        }

        [TestMethod]
        public void Test_EliminaInventarioSuelosPerfilModal()
        {
            SuelosPerfilModalLN oSuePerModLN = new SuelosPerfilModalLN();
            SuelosPerfilModal oSuePerMod = new SuelosPerfilModal();
            int id;

            oSuePerMod.nSuelosId = 1;
            oSuePerMod.nPerfilId = 1;

            id = oSuePerModLN.EliminaInventarioSuelosPerfilModal(oSuePerMod);

            Assert.IsTrue(id > 0, "Resultado no es mayor que 0");
        }


        [TestMethod]
        public void Test_CargaDatosInventarioSuelosPerfilModals()
        {
            SuelosPerfilModalLN oSuePerModLN = new SuelosPerfilModalLN();
            List<SuelosPerfilModal> oListaSuePerMod = new List<SuelosPerfilModal>();
            int nSueId = 1;

            oListaSuePerMod = oSuePerModLN.CargaListaInventarioSuelosPerfilModal(nSueId);

            Assert.IsNotNull(oListaSuePerMod);
        }

        [TestMethod]
        public void Test_CargaInventarioSuelosPerfilModal()
        {
            SuelosPerfilModalLN oSuePerModLN = new SuelosPerfilModalLN();
            SuelosPerfilModal oSuePerMod = new SuelosPerfilModal();
            int nPerModId = 1;


            oSuePerMod = oSuePerModLN.CargaInventarioSuelosPerfilModal(nPerModId);

            Assert.IsNotNull(oSuePerMod);
        }


    }
}
