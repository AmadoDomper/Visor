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
        public void Test_RegistraInventarioSuelos()
        {
            PublicacionLN oPubli = new PublicacionLN();
            string json;

            json = oPubli.GetAllPublicationPoints();

            JsonConvert.SerializeObject(json, Formatting.None,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });

            Assert.IsNotNull(json);
        }

    }
}
