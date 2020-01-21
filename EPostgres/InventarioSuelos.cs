using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EPostgres
{
    public class InventarioSuelos
    {
        private int _nSuelosId;
        private string _cNombreProyecto;
        private string _cAnoColecta;
        private string _cNombreColector;
        private DateTime _dFechaRegistro;
        private string _cFechaRegistro;
        private DateTime _dFechaActualizacion;
        private int _nUsuarioId;
        private Usuario _oUsuario;
        private List<SuelosPerfilModal> _ListaPerfilModal;
        private int _nEstado;
        private string _cEstado;
        private Historial _oHist;

        [JsonProperty(PropertyName = "nSuelosId")]
        public int nSuelosId
        {
            get { return _nSuelosId; }
            set { _nSuelosId = value; }
        }

        [JsonProperty(PropertyName = "cNombreProyecto")]
        public string cNombreProyecto
        {
            get { return _cNombreProyecto; }
            set { _cNombreProyecto = value; }
        }

        [JsonProperty(PropertyName = "cAnoColecta")]
        public string cAnoColecta
        {
            get { return _cAnoColecta; }
            set { _cAnoColecta = value; }
        }

        [JsonProperty(PropertyName = "cNombreColector")]
        public string cNombreColector
        {
            get { return _cNombreColector; }
            set { _cNombreColector = value; }
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

        [JsonProperty(PropertyName = "dFechaActualizacion")]
        public DateTime dFechaActualizacion
        {
            get { return _dFechaActualizacion; }
            set { _dFechaActualizacion = value; }
        }

        [JsonProperty(PropertyName = "nUsuarioId")]
        public int nUsuarioId
        {
            get { return _nUsuarioId; }
            set { _nUsuarioId = value; }
        }

        [JsonProperty(PropertyName = "oUsuario")]
        public Usuario oUsuario
        {
            get { return _oUsuario; }
            set { _oUsuario = value; }
        }

        [JsonProperty(PropertyName = "ListaPerfilModal")]
        public List<SuelosPerfilModal> ListaPerfilModal
        {
            get { return _ListaPerfilModal; }
            set { _ListaPerfilModal = value; }
        }

        [JsonProperty(PropertyName = "nEstado")]
        public int nEstado
        {
            get { return _nEstado; }
            set { _nEstado = value; }
        }

        [JsonProperty(PropertyName = "cEstado")]
        public string cEstado
        {
            get { return _cEstado; }
            set { _cEstado = value; }
        }

        [JsonProperty(PropertyName = "oHist")]
        public Historial oHist
        {
            get { return _oHist; }
            set { _oHist = value; }
        }
    }
}
