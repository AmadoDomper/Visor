using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace EPostgres
{
    public class Publicacion
    {
        private int _nPubliId;
        private int _nUsuId;
        private string _cTitulo;
        private string _cEnlace;
        private string _cRefBiblio;
        private int _nPubliAno;
        private DateTime _dFechaRegistro;
        private string _cFechaRegistro;
        //private DateTime _dFechaAceptado;
        private Usuario _oUsuario;
        private Tipo _oTipo;
        private int _nEstado;
        private string _cEstado;
        private List<Tema> _ListaTemas;
        private List<Feature> _ListaFeatures;
        private Historial _oHist;

        [JsonProperty(PropertyName = "Id")]
        public int nPubliId
        {
            get { return _nPubliId; }
            set { _nPubliId = value; }
        }

        [JsonProperty(PropertyName = "nUsuId")]
        public int nUsuId
        {
            get { return _nUsuId; }
            set { _nUsuId = value; }
        }

        [JsonProperty(PropertyName = "ano")]
        public int nPubliAno
        {
            get { return _nPubliAno; }
            set { _nPubliAno = value; }
        }

        [JsonProperty(PropertyName = "dFeReg")]
        public DateTime dFechaRegistro
        {
            get { return _dFechaRegistro; }
            set { _dFechaRegistro = value; }
        }

        [JsonProperty(PropertyName = "cFeReg")]
        public string cFechaRegistro
        {
            get { return _cFechaRegistro; }
            set { _cFechaRegistro = value; }
        }
        
        [JsonProperty(PropertyName = "refBi")]
        public string cRefBiblio
        {
            get { return _cRefBiblio; }
            set { _cRefBiblio = value; }
        }

        //[JsonProperty(PropertyName = "dAcep")]
        //public DateTime dFechaAceptado
        //{
        //    get { return _dFechaAceptado; }
        //    set { _dFechaAceptado = value; }
        //}

        [JsonProperty(PropertyName = "enlace")]
        public string cEnlace
        {
            get { return _cEnlace; }
            set { _cEnlace = value; }
        }

        [JsonProperty(PropertyName = "titulo")]
        public string cTitulo
        {
            get { return _cTitulo; }
            set { _cTitulo = value; }
        }

        [JsonProperty(PropertyName = "usuario")]
        public Usuario oUsuario
        {
            get { return _oUsuario; }
            set { _oUsuario = value; }
        }

        [JsonProperty(PropertyName = "tipo")]
        public Tipo oTipo
        {
            get { return _oTipo; }
            set { _oTipo = value; }
        }

        [JsonProperty(PropertyName = "lsTemas")]
        public List<Tema> ListaTemas
        {
            get { return _ListaTemas; }
            set { _ListaTemas = value; }
        }

        [JsonProperty(PropertyName = "lsFeatures")]
        public List<Feature> ListaFeatures
        {
            get { return _ListaFeatures; }
            set { _ListaFeatures = value; }
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

        //public int pub_anopublicacion { get; set; }
        //public DateTime pub_fechaRegistro { get; set; }
        //public string pub_referenciabibliografica { get; set; }
        //public DateTime pub_fechaaceptado { get; set; }
        //public string pub_enlace { get; set; }
        //public Tipo oTipo { get; set; }
        //public List<Tema> lsTemas { get; set; }
        //public List<Feature> lsFeatures { get; set; }
        //public string pub_titulo { get; set; }
    }
}
