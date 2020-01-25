using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EPostgres;
using EPostgres.Helper;
using LNPostgres;

namespace Test
{
    [TestClass]
    public class UsuarioTest
    {
        [TestMethod]
        public void Test_RegistraInventarioSuelos()
        {
            UsuarioLN oUsuarioLN = new UsuarioLN();
            Usuario oUsuario = new Usuario();
            string uId;

            oUsuario.nUsuarioId = 0;
            oUsuario.cNombres = "Prueba2";
            oUsuario.cApellidoMa = "Apelllido1";
            oUsuario.cApellidoPa = "Apellido2";
            oUsuario.cInstitucion = "";
            oUsuario.cEmail = "prueba2@gmail.com";
            oUsuario.cContrasena = "123456";
            oUsuario.nRolId = (int)RolId.Investigador;

            uId = oUsuarioLN.RegistrarModificarUsuario(oUsuario);

            Assert.IsTrue(uId != "", "Resultado no es diferente a vacío");
        }

        [TestMethod]
        public void Test_ActualizaEstadoConfirmacionEmail()
        {
            UsuarioLN oUsuarioLN = new UsuarioLN();
            string uId = "07ad907a-a664-441b-ad99-775b9dd9f388";

            var res = oUsuarioLN.ActualizaEstadoConfirmacionEmail(uId);

            Assert.IsTrue(res >= 0, "Resultado no es diferente a vacío");
        }



    }
}
