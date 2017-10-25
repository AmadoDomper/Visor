using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPostgres;
using ADPostgres;

namespace LNPostgres
{
    public class PublicacionLN
    {
        PublicacionAD oPublicacionAD;

        public PublicacionLN()
        {
            oPublicacionAD = new PublicacionAD();
        }

        public ListaPaginada ListarMisPublicaciones(int nPubEst, int nPage = 1, int nSize = 10, int nPubId = -1, string cPubTitulo = "", string cDni = "", string cInst = "")
        {
            return oPublicacionAD.ListarMisPublicacionesPag(nPubEst, nPage, nSize, nPubId, cPubTitulo, cDni, cInst);
        }

        public ListaPaginada ListarRevPublicaciones(int nPubEst, int nPage = 1, int nSize = 10, int nPubId = -1, string cPubTitulo = "", string cDni = "", string cInst = "", string cNom = "")
        {
            return oPublicacionAD.ListarRevPublicacionesPag(nPubEst, nPage, nSize, nPubId, cPubTitulo, cDni, cInst, cNom);
        }

        public Publicacion RegistrarPublicacion(Publicacion oPublicacion)
        {
            return oPublicacionAD.registrar(oPublicacion);
        }

        public Publicacion CargaDatosPublicacion(int nPubId)
        {
            return oPublicacionAD.CargaDatosPublicacion(nPubId);
        }



    }
}
