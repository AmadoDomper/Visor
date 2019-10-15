using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADPostgres;
using VisorPub.Models.Publicacion;
using Newtonsoft.Json;
using EPostgres;
using LNPostgres;
using VisorPub.Models;
using Seguridad.filters;
using System.Data;
using System.IO;
using ExcelDataReader;
using VisorPub.Models.Inventario;

namespace VisorPub.Controllers
{
    public class InventarioController : Controller
    {
        // GET: Inventario
        public ActionResult MisInventarios()
        {
            return View();
        }

        public ActionResult RevisionInventarios()
        {
            return View("MisInventarios");
        }

        public string ListarMisInventariosVegetacion(int nInvEst, int nPage = 1, int nSize = 10, string cNombreProy = "", string cAno = "")
        {
            InventarioVegetacionLN oInvVeg = new InventarioVegetacionLN();
            Usuario oUsuario = new Usuario();
            oUsuario = (Usuario)Session["Datos"]; //Agregar verificacíon de sessión

            ListaPaginada oLista = oInvVeg.ListarMisInventariosVegetacion(nInvEst, nPage, nSize, cNombreProy, cAno, oUsuario.nUsuarioId);

            return JsonConvert.SerializeObject(oLista, Formatting.None,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
        }

        public string CargaDatosInventarioVegetacion(int nVegId)
        {
            InventarioVegetacionLN oInvVegLN = new InventarioVegetacionLN();
            InventarioVegetacion oInvVeg = new InventarioVegetacion();

            if(nVegId != 0)
                oInvVeg = oInvVegLN.CargaDatosInventarioVegetacion(nVegId);

            return JsonConvert.SerializeObject(oInvVeg, Formatting.None,
                 new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
        }

        public int InsertaActualizaInventarioVegetacionParcela(VegetacionParcela oVegPar)
        {
            int id;
            VegetacionParcelaLN oVegParLN = new VegetacionParcelaLN();

            if (oVegPar.nParcelaId == 0)
            {
                id = oVegParLN.RegistraInventarioVegetacionParcela(oVegPar);
            }
            else
            {
                id = oVegParLN.ActualizaInventarioVegetacionParcela(oVegPar);
            }

            return id;
        }

        public int EliminaInventarioVegetacionParcelas(VegetacionParcela oVegPar)
        {
            int id;
            VegetacionParcelaLN oVegParLN = new VegetacionParcelaLN();

            id = oVegParLN.EliminaInventarioVegetacionParcela(oVegPar);

            return id;
        }

        public int GuardarInventarioVegetacion(InventarioVegetacion oInvVeg)
        {
            int id;
            InventarioVegetacionLN oInvVegLN = new InventarioVegetacionLN();
            Usuario oUsuario = new Usuario();
            oUsuario = (Usuario)Session["Datos"];

            oInvVeg.nUsuarioId = oUsuario.nUsuarioId;

            if (oInvVeg.nVegetacionId == 0)
            {
                id = oInvVegLN.RegistraInventarioVegetacion(oInvVeg);
            }
            else
            {
                id = oInvVegLN.ActualizaInventarioVegetacion(oInvVeg);
            }

            return id;
        }

        public int EliminaInventarioVegetacion(InventarioVegetacion oInvVeg)
        {
            int id;
            InventarioVegetacionLN oInvVegLN = new InventarioVegetacionLN();

            id = oInvVegLN.EliminaInventarioVegetacion(oInvVeg);

            return id;
        }



        // INVENTARIO DE SUELOS

        public ActionResult MisInventariosSuelos()
        {
            return View();
        }

        public string ListarMisInventariosSuelos(int nInvEst, int nPage = 1, int nSize = 10, string cNombreProy = "", string cAno = "", string cNombreColector = "")
        {
            InventarioSuelosLN oInvSue = new InventarioSuelosLN();
            Usuario oUsuario = new Usuario();
            oUsuario = (Usuario)Session["Datos"]; //Agregar verificacíon de sessión

            ListaPaginada oLista = oInvSue.ListarMisInventariosSuelos(nInvEst, nPage, nSize, cNombreProy, cAno, cNombreColector);

            return JsonConvert.SerializeObject(oLista, Formatting.None,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
        }

        public string CargaDatosInventarioSuelos(int nSueId)
        {
            InventarioSuelosLN oInvSueLN = new InventarioSuelosLN();
            InventarioSuelos oInvSue = new InventarioSuelos();

            if (nSueId != 0)
                oInvSue = oInvSueLN.CargaDatosInventarioSuelos(nSueId);

            return JsonConvert.SerializeObject(oInvSue, Formatting.None,
                 new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
        }

        public int InsertaActualizaInventarioSuelosPerfilModal(SuelosPerfilModal oSuePerMod)
        {
            int id;
            SuelosPerfilModalLN oSuePerModLN = new SuelosPerfilModalLN();

            if (oSuePerMod.nPerfilId == 0)
            {
                id = oSuePerModLN.RegistraInventarioSuelosPerfilModal(oSuePerMod);
            }
            else
            {
                id = oSuePerModLN.ActualizaInventarioSuelosPerfilModal(oSuePerMod);
            }

            return id;
        }

        public int EliminaInventarioSuelosPerfilModal(SuelosPerfilModal oSuePerMod)
        {
            int id;
            SuelosPerfilModalLN oSuePerModLN = new SuelosPerfilModalLN();

            id = oSuePerModLN.EliminaInventarioSuelosPerfilModal(oSuePerMod);

            return id;
        }

        public int GuardarInventarioSuelos(InventarioSuelos oInvSue)
        {
            int id;
            InventarioSuelosLN oInvSueLN = new InventarioSuelosLN();
            Usuario oUsuario = new Usuario();
            oUsuario = (Usuario)Session["Datos"];

            oInvSue.nUsuarioId = oUsuario.nUsuarioId;

            if (oInvSue.nSuelosId == 0)
            {
                id = oInvSueLN.RegistraInventarioSuelos(oInvSue);
            }
            else
            {
                id = oInvSueLN.ActualizaInventarioSuelos(oInvSue);
            }

            return id;
        }

        public int EliminaInventarioSuelos(InventarioSuelos oInvSue)
        {
            int id;
            InventarioSuelosLN oInvSueLN = new InventarioSuelosLN();

            id = oInvSueLN.EliminaInventarioSuelos(oInvSue);

            return id;
        }

        public int CargarExcelVegetacion(ExcelViewModel model)
        {
            
            string filename = Guid.NewGuid() + Path.GetExtension(model.File.FileName);
            string filepath = "/ExcelInventarios/" + filename;

            model.File.SaveAs(Path.Combine(Server.MapPath("/ExcelInventarios"), filename));
            InsertExcelDataVegetacion(filepath, filename, model.Id);

            return 1;
        }

        private void InsertExcelDataVegetacion(string fileepath, string filename, int nVegetacionId)
        {
            VegetacionParcelaLN oVegParLN = new VegetacionParcelaLN();
            string fullpath = Server.MapPath("/ExcelInventarios/") + filename;

            using (var stream = System.IO.File.Open(fullpath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    var dataTable = result.Tables[0];

                    for (var i = 1; i < dataTable.Rows.Count; i++)
                    {
                        var data = dataTable.Rows[i][0];

                        VegetacionParcela oVegPar = new VegetacionParcela();

                        oVegPar.nVegetacionId = nVegetacionId;
                        oVegPar.cDepartamento = dataTable.Rows[i][0].ToString();
                        oVegPar.cRegistrador = dataTable.Rows[i][1].ToString();
                        oVegPar.cLongitud = dataTable.Rows[i][2].ToString();
                        oVegPar.cLatitud = dataTable.Rows[i][3].ToString();
                        oVegPar.cCodigoMuestra = dataTable.Rows[i][4].ToString(); ;
                        oVegPar.cAltitud = dataTable.Rows[i][5].ToString(); ;
                        oVegPar.cPrecision = dataTable.Rows[i][6].ToString();
                        oVegPar.cTipoVegetacion = dataTable.Rows[i][7].ToString();
                        oVegPar.cClaseFisonomica = dataTable.Rows[i][8].ToString();
                        oVegPar.cCobertura = dataTable.Rows[i][9].ToString();
                        oVegPar.cConfianzaClasificacion = dataTable.Rows[i][10].ToString();
                        oVegPar.cClaseHidrologica = dataTable.Rows[i][11].ToString();
                        oVegPar.cFisiografia = dataTable.Rows[i][12].ToString();
                        oVegPar.cAltitudSistemaHidrico = dataTable.Rows[i][13].ToString();

                        oVegParLN.RegistraInventarioVegetacionParcela(oVegPar);
                    }
                }
            }
        }


        public int CargarExcelSuelos(ExcelViewModel model)
        {

            string filename = Guid.NewGuid() + Path.GetExtension(model.File.FileName);
            string filepath = "/ExcelInventarios/" + filename;

            model.File.SaveAs(Path.Combine(Server.MapPath("/ExcelInventarios"), filename));
            InsertExcelDataSuelos(filepath, filename, model.Id);

            return 1;
        }

        private void InsertExcelDataSuelos(string fileepath, string filename, int nSuelosId)
        {
            SuelosPerfilModalLN oSuePerModLN = new SuelosPerfilModalLN();
            string fullpath = Server.MapPath("/ExcelInventarios/") + filename;

            using (var stream = System.IO.File.Open(fullpath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    var dataTable = result.Tables[0];

                    for (var i = 1; i < dataTable.Rows.Count; i++)
                    {
                        var data = dataTable.Rows[i][0];

                        SuelosPerfilModal oSuePerMod = new SuelosPerfilModal();

                        oSuePerMod.nSuelosId = nSuelosId;
                        oSuePerMod.cDepartamento = dataTable.Rows[i][0].ToString();
                        oSuePerMod.cLongitud = dataTable.Rows[i][1].ToString();
                        oSuePerMod.cLatitud = dataTable.Rows[i][2].ToString();                                                     
                        oSuePerMod.cPerfilModal = dataTable.Rows[i][3].ToString();                        
                        oSuePerMod.cNroCalicata = dataTable.Rows[i][4].ToString();                        
                        oSuePerMod.cMsnm = dataTable.Rows[i][5].ToString();                               
                        oSuePerMod.cZonaMuestreo = dataTable.Rows[i][6].ToString();                       
                        oSuePerMod.cClasificacionNatural = dataTable.Rows[i][7].ToString();               
                        oSuePerMod.cFisiografia = dataTable.Rows[i][8].ToString();                        
                        oSuePerMod.cPendiente = dataTable.Rows[i][9].ToString();                          
                        oSuePerMod.cRelieve = dataTable.Rows[i][10].ToString();                            
                        oSuePerMod.cClima = dataTable.Rows[i][11].ToString();                              
                        oSuePerMod.cZonaVida = dataTable.Rows[i][12].ToString();                           
                        oSuePerMod.cMaterialParental = dataTable.Rows[i][13].ToString();                   
                        oSuePerMod.cVegetacion = dataTable.Rows[i][14].ToString();                         
                        oSuePerMod.cModoColecta = dataTable.Rows[i][15].ToString();                        
                        oSuePerMod.cDrenaje = dataTable.Rows[i][16].ToString();                            
                        oSuePerMod.cProfundidadEfectiva = dataTable.Rows[i][17].ToString();                
                        oSuePerMod.cFactorLimitante = dataTable.Rows[i][18].ToString();           

                        oSuePerModLN.RegistraInventarioSuelosPerfilModal(oSuePerMod);
                    }
                }
            }
        }


    }
}