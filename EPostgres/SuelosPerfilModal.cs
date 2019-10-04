using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EPostgres
{
    public class SuelosPerfilModal
    {
         private int  _nSuelosId;
         private int _nPerfilId;
         private string  _cDepartamento;
         private string  _cLongitud;
         private string  _cLatitud;
         private string  _cWkb;
         private string  _cPerfilModal;
         private string  _cNroCalicata;
         private string  _cMsnm;
         private string  _cZonaMuestreo;
         private string  _cClasificacionNatural;
         private string  _cFisiografia;
         private string  _cPendiente;
         private string  _cRelieve;
         private string  _cClima;
         private string  _cZonaVida;
         private string  _cMaterialParental;
         private string  _cVegetacion;
         private string  _cModoColecta;
         private string  _cDrenaje;
         private string  _cProfundidadEfectiva;
         private string  _cFactorLimitante;
         private int  _nEstado;

        [JsonProperty(PropertyName = "nSuelosId")]
        public int nSuelosId
            {
            get { return _nSuelosId; }
            set { _nSuelosId = value; }
        }

        [JsonProperty(PropertyName = "nPerfilId")]
        public int nPerfilId
            {
            get { return _nPerfilId; }
            set { _nPerfilId = value; }
        }

        [JsonProperty(PropertyName = "cDepartamento")]
        public string cDepartamento
        {
            get { return _cDepartamento; }
            set { _cDepartamento = value; }
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

        [JsonProperty(PropertyName = "cPerfilModal")]
        public string cPerfilModal
        {
            get { return _cPerfilModal; }
            set { _cPerfilModal = value; }
        }

        [JsonProperty(PropertyName = "cNroCalicata")]
        public string cNroCalicata
        {
            get { return _cNroCalicata; }
            set { _cNroCalicata = value; }
        }

        [JsonProperty(PropertyName = "cMsnm")]
        public string cMsnm
        {
            get { return _cMsnm; }
            set { _cMsnm = value; }
        }

        [JsonProperty(PropertyName = "cZonaMuestreo")]
        public string cZonaMuestreo
        {
            get { return _cZonaMuestreo; }
            set { _cZonaMuestreo = value; }
        }

        [JsonProperty(PropertyName = "cClasificacionNatural")]
        public string cClasificacionNatural
        {
            get { return _cClasificacionNatural; }
            set { _cClasificacionNatural = value; }
        }

        [JsonProperty(PropertyName = "cFisiografia")]
        public string cFisiografia
        {
            get { return _cFisiografia; }
            set { _cFisiografia = value; }
        }

        [JsonProperty(PropertyName = "cPendiente")]
        public string cPendiente
        {
            get { return _cPendiente; }
            set { _cPendiente = value; }
        }

        [JsonProperty(PropertyName = "cRelieve")]
        public string cRelieve
        {
            get { return _cRelieve; }
            set { _cRelieve = value; }
        }

        [JsonProperty(PropertyName = "cClima")]
        public string cClima
        {
            get { return _cClima; }
            set { _cClima = value; }
        }

        [JsonProperty(PropertyName = "cZonaVida")]
        public string cZonaVida
        {
            get { return _cZonaVida; }
            set { _cZonaVida = value; }
        }

        [JsonProperty(PropertyName = "cMaterialParental")]
        public string cMaterialParental
        {
            get { return _cMaterialParental; }
            set { _cMaterialParental = value; }
        }

        [JsonProperty(PropertyName = "cVegetacion")]
        public string cVegetacion
        {
            get { return _cVegetacion; }
            set { _cVegetacion = value; }
        }

        [JsonProperty(PropertyName = "cModoColecta")]
        public string cModoColecta
        {
            get { return _cModoColecta; }
            set { _cModoColecta = value; }
        }

        [JsonProperty(PropertyName = "cDrenaje")]
        public string cDrenaje
        {
            get { return _cDrenaje; }
            set { _cDrenaje = value; }
        }

        [JsonProperty(PropertyName = "cProfundidadEfectiva")]
        public string cProfundidadEfectiva
        {
            get { return _cProfundidadEfectiva; }
            set { _cProfundidadEfectiva = value; }
        }

        [JsonProperty(PropertyName = "cFactorLimitante")]
        public string cFactorLimitante
        {
            get { return _cFactorLimitante; }
            set { _cFactorLimitante = value; }
        }

        [JsonProperty(PropertyName = "nEstado")]
        public int nEstado
        {
            get { return _nEstado; }
            set { _nEstado = value; }
        }

    }
}
