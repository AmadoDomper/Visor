using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPostgres;
using ADPostgres;
using Newtonsoft.Json;

namespace LNPostgres
{
    public class PublicacionLN
    {
        PublicacionAD oPublicacionAD;

        public PublicacionLN()
        {
            oPublicacionAD = new PublicacionAD();
        }

        public ListaPaginada ListarMisPublicaciones(int nPubEst, int nUsuId, int nPage = 1, int nSize = 10, int nPubId = -1, string cPubTitulo = "", string cInst = "")
        {
            return oPublicacionAD.ListarMisPublicacionesPag(nPubEst, nUsuId, nPage, nSize, nPubId, cPubTitulo, cInst);
        }

        public ListaPaginada ListarRevPublicaciones(int nPubEst, int nPage = 1, int nSize = 10, int nPubId = -1, string cPubTitulo = "", string cDni = "", string cInst = "", string cNom = "")
        {
            return oPublicacionAD.ListarRevPublicacionesPag(nPubEst, nPage, nSize, nPubId, cPubTitulo, cDni, cInst, cNom);
        }

        public ListaPaginada BuscarPublicacionesPag(int nPage = 1, int nSize = 10, string cPubTexto = "", int nTipo = -1, string cAno = "", int nAreaTem = -1)
        {
            return oPublicacionAD.BuscarPublicacionesPag(nPage, nSize, cPubTexto, nTipo, cAno, nAreaTem);
        }
        

        public Publicacion RegistrarPublicacion(Publicacion oPublicacion)
        {
            return oPublicacionAD.registrar(oPublicacion);
        }

        public Publicacion CargaDatosPublicacion(int nPubId)
        {
            return oPublicacionAD.CargaDatosPublicacion(nPubId);
        }

        public List<Tema> ListarTemas()
        {
            return oPublicacionAD.ListarTemas();
        }

        public List<Tipo> ListarTipos()
        {
            return oPublicacionAD.ListarTipos();
        }

        public Object GetAllPublicationPoints()
        {
            return oPublicacionAD.GetAllPublicationPoints();
        }

        public Object GetSearchPublicactionPoints(string cPubTexto = "", int nTipo = -1, string cAno = "", int nAreaTem = -1)
        {
            return oPublicacionAD.GetSearchPublicactionPoints(cPubTexto, nTipo, cAno, nAreaTem);
        }

        public Object GetSearchPublicactionIds(string cPubTexto = "", int nTipo = -1, string cAno = "", int nAreaTem = -1)
        {
            return oPublicacionAD.GetSearchPublicactionIds(cPubTexto, nTipo, cAno, nAreaTem);
        }

        public Object GetAllPublicactionsJSON()
        {
            return oPublicacionAD.GetAllPublicactionsJSON();
        }

        /// <summary>
        /// Actualiza el estado de una publicación
        /// </summary>
        /// <param name="pubId"></param>
        /// <param name="estado">enum EstadoSolicitud</param>
        /// <returns></returns>
        public int ActualizaEstadoPublicacion(int pubId, int estado)
        {
            return oPublicacionAD.ActualizaEstadoPublicacion(pubId, estado);
        }

    }
}
