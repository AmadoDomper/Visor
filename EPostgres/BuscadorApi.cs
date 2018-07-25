using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EPostgres
{
    public class BuscadorApi
    {
        private string _cTexto;
        private int _nTipo;
        private string _cAnoPub;
        private int _nAreaTema;
        private int _nPage;
        private int _nPageSize;

        public string cTexto
        {
            get { return _cTexto; }
            set { _cTexto = value; }
        }

        public int nTipo
        {
            get { return _nTipo; }
            set { _nTipo = value; }
        }

        public string cAnoPub
        {
            get { return _cAnoPub; }
            set { _cAnoPub = value; }
        }

        public int nAreaTema
        {
            get { return _nAreaTema; }
            set { _nAreaTema = value; }
        }

        public int nPage
        {
            get { return _nPage; }
            set { _nPage = value; }
        }

        public int nPageSize
        {
            get { return _nPageSize; }
            set { _nPageSize = value; }
        }


    }
}
