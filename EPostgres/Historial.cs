using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace EPostgres
{
    public class Historial
    {
        private int _nHistorialId;
        private int _nRefId;
        private int _nTipoReferencia;
        private DateTime _dFechaCreacion;
        private string _cFechaCreacion;
        private DateTime _dFechaActualizacion;
        private string _cFechaActualizacion;
        private int _nEstado;
        private string _cUniqueId;

        private List<HistorialDetalle> _ListHistDet;

        [JsonProperty(PropertyName = "Id")]
        public int nHistorialId
        {
            get { return _nHistorialId; }
            set { _nHistorialId = value; }
        }

        [JsonProperty(PropertyName = "RefId")]
        public int nRefId
        {
            get { return _nRefId; }
            set { _nRefId = value; }
        }

        [JsonProperty(PropertyName = "TipoReferencia")]
        public int nTipoReferencia
        {
            get { return _nTipoReferencia; }
            set { _nTipoReferencia = value; }
        }

        [JsonProperty(PropertyName = "dFechaCreacion")]
        public DateTime dFechaCreacion
        {
            get { return _dFechaCreacion; }
            set { _dFechaCreacion = value; }
        }

        [JsonProperty(PropertyName = "cFechaCreacion")]
        public string cFechaCreacion
        {
            get { return _cFechaCreacion; }
            set { _cFechaCreacion = value; }
        }

        [JsonProperty(PropertyName = "dFechaActualizacion")]
        public DateTime dFechaActualizacion
        {
            get { return _dFechaActualizacion; }
            set { _dFechaActualizacion = value; }
        }

        [JsonProperty(PropertyName = "cFechaActualizacion")]
        public string cFechaActualizacion
        {
            get { return _cFechaActualizacion; }
            set { _cFechaActualizacion = value; }
        }

        [JsonProperty(PropertyName = "Estado")]
        public int nEstado
        {
            get { return _nEstado; }
            set { _nEstado = value; }
        }

        [JsonProperty(PropertyName = "UniqueId")]
        public string cUniqueId
        {
            get { return _cUniqueId; }
            set { _cUniqueId = value; }
        }

        [JsonProperty(PropertyName = "ListHistDet")]
        public List<HistorialDetalle> ListHistDet
        {
            get { return _ListHistDet; }
            set { _ListHistDet = value; }
        }
    }
}
