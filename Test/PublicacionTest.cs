using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EPostgres;
using LNPostgres;
using Newtonsoft.Json;


namespace Test
{
    [TestClass]
    public  class PublicacionTest
    {

        [TestMethod]
        public void Test_Registrainventariosuelos()
        {
            PublicacionLN oPubli = new PublicacionLN();
            object json;

            json = oPubli.GetAllPublicationPoints();

            Assert.IsNotNull(json);
        }

    }
}
