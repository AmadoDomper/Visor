using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EPostgres;
using EPostgres.Helper;
using LNPostgres;

namespace Test
{
    [TestClass]
    public class SeguridadTest
    {
        [TestMethod]
        public void Test_ValidaAccesoUsuario()
        {
            SeguridadLN oSeguridadLN = new SeguridadLN();
            Usuario oUsuario = new Usuario();

            string cUsuarioEmail = "amado.domper@gmail.com";
            string cClave = "123456";

            oUsuario = oSeguridadLN.ValidaAccesoUsuario(cUsuarioEmail, cClave);

            if (oUsuario.bEmailConfirmed)
            {
                Assert.IsTrue(oUsuario.bEmailConfirmed, "No es verdadero");
            }
            else
            {
                Assert.IsFalse(oUsuario.bEmailConfirmed, "No es falso");
            }

            
        }



    }
}
