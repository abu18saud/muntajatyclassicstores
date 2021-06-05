using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace muntajatyClassicStores.Models
{
    public class fileUploadViewModel
    {
        public IFormFile file { get; set; }
    }
}