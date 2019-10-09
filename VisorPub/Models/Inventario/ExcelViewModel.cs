using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisorPub.Models.Inventario
{
    public class ExcelViewModel
    {
        public int Id { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}