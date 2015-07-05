using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using DomainModel;
using MVC.Models;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            BSchool _BSchool = new BSchool();
            IList<School> ilistSchool = _BSchool.GetAll();
            return View(ilistSchool);
        }

    }
}
