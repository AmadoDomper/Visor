using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EPostgres;
using EPostgres.Helper;
using LNPostgres;
using System.Linq;

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

        [TestMethod]
        public void ValidaSupervisorEmails_Test()
        {
            RolLN oRolLN = new RolLN();
            string res;

            res = oRolLN.GetSupervisorEmails();

            Assert.IsNotNull(res);
        }

        [TestMethod]
        public void CargarUsuariosPorRol_Test()
        {
            RolLN oRolLN = new RolLN();
            var List = new List<Usuario>();

            List = oRolLN.CargarUsuariosPorRol(RolId.Supervisor);

            Assert.IsNotNull(List);
        }

    }
}
