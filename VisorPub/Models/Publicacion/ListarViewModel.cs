using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPostgres;

namespace VisorPub.Models.Publicacion
{
    public class ListarViewModel
    {
        public List<Tipo> lsTipos;
        public List<Tema> lsTemas;
        public List<EPostgres.Publicacion> lsPublicaciones;
    }
}