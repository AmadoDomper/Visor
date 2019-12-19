using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace EPostgres
{
    public class HistorialDetalle
    {
        private int _nHistorialDetId;
        private int _nHistorialId;
        private int _nUsuarioRegistra;
        private DateTime _dFechaRegistro;
        private string _cFechaRegistro;
        private string _cDescripcion;
        private int _nEstado;

        [JsonProperty(PropertyName = "Id")]
        public int nHistorialDetId
        {
            get { return _nHistorialDetId; }
            set { _nHistorialDetId = value; }
        }

        [JsonProperty(PropertyName = "HistId")]
        public int nHistorialId
        {
            get { return _nHistorialId; }
            set { _nHistorialId = value; }
        }

        [JsonProperty(PropertyName = "UsuarioReg")]
        public int nUsuarioRegistra
        {
            get { return _nUsuarioRegistra; }
            set { _nUsuarioRegistra = value; }
        }

        [JsonProperty(PropertyName = "dFechaRegistro")]
        public DateTime dFechaRegistro
        {
            get { return _dFechaRegistro; }
            set { _dFechaRegistro = value; }
        }

        [JsonProperty(PropertyName = "cFechaRegistro")]
        public string cFechaRegistro
        {
            get { return _cFechaRegistro; }
            set { _cFechaRegistro = value; }
        }

        [JsonProperty(PropertyName = "Descripcion")]
        public string cDescripcion
        {
            get { return _cDescripcion; }
            set { _cDescripcion = value; }
        }

        [JsonProperty(PropertyName = "Estado")]
        public int Estado
        {
            get { return _nEstado; }
            set { _nEstado = value; }
        }
    }
}
