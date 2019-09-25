using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EPostgres
{
    public class VegetacionParcela
    {
        private int _nVegetacionId;
        private int _nParcelaId;
        private string _cDepartamento;
        private string _cRegistrador;
        private string _cLongitud;
        private string _cLatitud;
        private string _cWkb;
        private string _cCodigoMuestra;
        private string _cAltitud;
        private string _cPrecision;
        private string _cTipoVegetacion;
        private string _cClaseFisonomica;
        private string _cCobertura;
        private string _cConfianzaClasificacion;
        private string _cClaseHidrologica;
        private string _cFisiografia;
        private string _cAltitudSistemaHidrico;
        private int _nEstado;

        [JsonProperty(PropertyName = "nVegetacionId")]
        public int nVegetacionId
        {
            get { return _nVegetacionId; }
            set { _nVegetacionId = value; }
        }

        [JsonProperty(PropertyName = "nParcelaId")]
        public int nParcelaId
        {
            get { return _nParcelaId; }
            set { _nParcelaId = value; }
        }

        [JsonProperty(PropertyName = "cDepartamento")]
        public string cDepartamento
        {
            get { return _cDepartamento; }
            set { _cDepartamento = value; }
        }

        [JsonProperty(PropertyName = "cRegistrador")]
        public string cRegistrador
        {
            get { return _cRegistrador; }
            set { _cRegistrador = value; }
        }

        [JsonProperty(PropertyName = "cLongitud")]
        public string cLongitud
        {
            get { return _cLongitud; }
            set { _cLongitud = value; }
        }

        [JsonProperty(PropertyName = "cLatitud")]
        public string cLatitud
        {
            get { return _cLatitud; }
            set { _cLatitud = value; }
        }

        [JsonProperty(PropertyName = "cWkb")]
        public string cWkb
        {
            get { return _cWkb; }
            set { _cWkb = value; }
        }

        [JsonProperty(PropertyName = "cCodigoMuestra")]
        public string cCodigoMuestra
        {
            get { return _cCodigoMuestra; }
            set { _cCodigoMuestra = value; }
        }

        [JsonProperty(PropertyName = "cAltitud")]
        public string cAltitud
        {
            get { return _cAltitud; }
            set { _cAltitud = value; }
        }

        [JsonProperty(PropertyName = "cPrecision")]
        public string cPrecision
        {
            get { return _cPrecision; }
            set { _cPrecision = value; }
        }

        [JsonProperty(PropertyName = "cTipoVegetacion")]
        public string cTipoVegetacion
        {
            get { return _cTipoVegetacion; }
            set { _cTipoVegetacion = value; }
        }

        [JsonProperty(PropertyName = "cClaseFisonomica")]
        public string cClaseFisonomica
        {
            get { return _cClaseFisonomica; }
            set { _cClaseFisonomica = value; }
        }

        [JsonProperty(PropertyName = "cCobertura")]
        public string cCobertura
        {
            get { return _cCobertura; }
            set { _cCobertura = value; }
        }

        [JsonProperty(PropertyName = "cConfianzaClasificacion")]
        public string cConfianzaClasificacion
        {
            get { return _cConfianzaClasificacion; }
            set { _cConfianzaClasificacion = value; }
        }

        [JsonProperty(PropertyName = "cClaseHidrologica")]
        public string cClaseHidrologica
        {
            get { return _cClaseHidrologica; }
            set { _cClaseHidrologica = value; }
        }

        [JsonProperty(PropertyName = "cFisiografia")]
        public string cFisiografia
        {
            get { return _cFisiografia; }
            set { _cFisiografia = value; }
        }

        [JsonProperty(PropertyName = "cAltitudSistemaHidrico")]
        public string cAltitudSistemaHidrico
        {
            get { return _cAltitudSistemaHidrico; }
            set { _cAltitudSistemaHidrico = value; }
        }

        [JsonProperty(PropertyName = "nEstado")]
        public int nEstado
        {
            get { return _nEstado; }
            set { _nEstado = value; }
        }

        
    }
}