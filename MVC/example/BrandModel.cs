using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.example
{

    public class BrandModel
    {
        public int BrandId { get; set; }

        //[Required(ErrorMessage = "Brand name required")]
        public string Name { get; set; }

        public string FullName { get; set; }

        public string Description { get; set; }

        //[Required(ErrorMessage = "Active Brand required")]
        public bool IsActive { get; set; }
    }
}