using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace EPostgres
{
    public class Tipo
    {

        private int _nTipoId;
        private string _cDesc;
        private string _cUri;

        [JsonProperty(PropertyName = "id")]
        public int nTipoId
        {
            get { return _nTipoId; }
            set { _nTipoId = value; }
        }

        [JsonProperty(PropertyName = "desc")]
        public string cDesc
        {
            get { return _cDesc; }
            set { _cDesc = value; }
        }

        [JsonProperty(PropertyName = "uri")]
        public string cUri
        {
            get { return _cUri; }
            set { _cUri = value; }
        }

    }

}
