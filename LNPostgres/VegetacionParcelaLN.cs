using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPostgres;
using ADPostgres;

namespace LNPostgres
{
    public class VegetacionParcelaLN
    {
        VegetacionParcelaAD oVegetacionParcelaAD;

        public VegetacionParcelaLN()
        {
            oVegetacionParcelaAD = new VegetacionParcelaAD();
        }

        public int RegistraInventarioVegetacionParcela(VegetacionParcela oVegPar)
        {
            return oVegetacionParcelaAD.RegistraInventarioVegetacionParcela(oVegPar);
        }

        public int ActualizaInventarioVegetacionParcela(VegetacionParcela oVegPar)
        {
            return oVegetacionParcelaAD.ActualizaInventarioVegetacionParcela(oVegPar);
        }

        public int EliminaInventarioVegetacionParcela(VegetacionParcela oVegPar)
        {
            return oVegetacionParcelaAD.EliminaInventarioVegetacionParcela(oVegPar);
        }

        public List<VegetacionParcela> CargaListaInventarioVegetacionParcelas(int nVegId)
        {
            return oVegetacionParcelaAD.CargaListaInventarioVegetacionParcelas(nVegId);
        }

        public VegetacionParcela CargaInventarioVegetacionParcela(int nParId)
        {
            return oVegetacionParcelaAD.CargaInventarioVegetacionParcela(nParId);
        }

    }
}
