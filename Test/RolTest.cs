using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EPostgres;
using LNPostgres;

namespace Test
{
    [TestClass]
    public class RolTest
    {
        [TestMethod]
        public void ValidaPermiso_Test()
        {
            RolLN oRolLN = new RolLN();
            bool res;
            int nUsuario = 57;
            int nPermiso = 2;

            res = oRolLN.ValidaPermiso(nUsuario, nPermiso);

            Assert.IsNotNull(res);
        }
    }
}
