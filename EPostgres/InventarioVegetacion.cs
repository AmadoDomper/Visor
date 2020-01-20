using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EPostgres
{
    public class InventarioVegetacion
    {
        private int _nVegetacionId;
        private string _cNombreProyecto;
        private string _cAnoColecta;
        private DateTime _dFechaRegistro;
        private string _cFechaRegistro;
        private DateTime _dFechaActualizacion;
        private int _nUsuarioId;
        private Usuario _oUsuario;
        private List<VegetacionParcela> _ListaParcelas;
        private int _nEstado;
        private string _cEstado;
        private Historial _oHist;

        [JsonProperty(PropertyName = "nVegId")]
        public int nVegetacionId
        {
            get { return _nVegetacionId; }
            set { _nVegetacionId = value; }
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

        [JsonProperty(PropertyName = "ListaParcelas")]
        public List<VegetacionParcela> ListaParcelas
        {
            get { return _ListaParcelas; }
            set { _ListaParcelas = value; }
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
