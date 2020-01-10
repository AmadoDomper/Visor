using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace EPostgres
{
    public class Alerta
    {
        private int _nUsuarioId;
        private string _cTitulo;
        private string _cMensaje;
        private string _cFechaRegistro;
        private string _cUrl;
        private string _cAlertaIcono;
        private string _cAlertaColor;
        private int _nEstado;

        [JsonProperty(PropertyName = "nUsuId")]
        public int nUsuarioId
        {
            get { return _nUsuarioId; }
            set { _nUsuarioId = value; }
        }

        [JsonProperty(PropertyName = "cTitulo")]
        public string cTitulo
        {
            get { return _cTitulo; }
            set { _cTitulo = value; }
        }

        [JsonProperty(PropertyName = "cMensaje")]
        public string cMensaje
        {
            get { return _cMensaje; }
            set { _cMensaje = value; }
        }

        [JsonProperty(PropertyName = "cFecha")]
        public string cFechaRegistro
        {
            get { return _cFechaRegistro; }
            set { _cFechaRegistro = value; }
        }

        [JsonProperty(PropertyName = "cUrl")]
        public string cUrl
        {
            get { return _cUrl; }
            set { _cUrl = value; }
        }

        [JsonProperty(PropertyName = "cIcono")]
        public string cAlertaIcono
        {
            get { return _cAlertaIcono; }
            set { _cAlertaIcono = value; }
        }

        [JsonProperty(PropertyName = "cColor")]
        public string cAlertaColor
        {
            get { return _cAlertaColor; }
            set { _cAlertaColor = value; }
        }

        [JsonProperty(PropertyName = "nEstado")]
        public int nEstado
        {
            get { return _nEstado; }
            set { _nEstado = value; }
        }


    }
}
