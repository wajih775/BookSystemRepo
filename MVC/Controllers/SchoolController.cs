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
    public class SchoolController : Controller
    {

        // GET: /School/

        BSchool _BSchool = new BSchool();

        public ActionResult Index()
        {

            IList<School> ilistSchool = _BSchool.GetAll();

            //for (int i = 0; i < 4; i++)
            //{
            //    JsonData += "{ \"name\": \"" + formaulae[i] + "\",\"data\":[";
            //    foreach (var item in objChannelCityWiseStats)
            //    {
            //        categories.Add(item.CityName);
            //        if (i == 0)
            //            JsonData += item.Weighted_ChannelRating.ToString("N3") + ",";
            //        if (i == 1)
            //            JsonData += item.MarketShare.ToString("N3") + ",";
            //        if (i == 2)
            //            JsonData += item.InterMarketShare.ToString("N3") + ",";
            //        if (i == 3)
            //            JsonData += item.NetReach.ToString("N3") + ",";
            //    }
            //    JsonData = JsonData.Trim(',');
            //    JsonData += "] },";
            //}

            //JsonData = JsonData.Trim(',');
            //JsonCategory = JsonCategory.Trim(',');
            //ViewBag.JsonHiddenValue = "[" + JsonData + "]";

            return View(ilistSchool);
        }


        public PartialViewResult ManageSchoolForm()
        {
            return PartialView();
        }


        [HttpPost]
        public ActionResult ManageSchoolForm(School school)
        {

            School _school = new School();
            _school.Name = school.Name;
            _school.Country = string.IsNullOrEmpty(school.Country) ? string.Empty : school.Country;
            _school.City = string.IsNullOrEmpty(school.City) ? string.Empty : school.City;
            _school.LogoPath = string.IsNullOrEmpty(school.LogoPath) ? string.Empty : school.LogoPath;

            if (school.ID == Guid.Empty)
            {
                school.ID = Guid.NewGuid();
                int isAdded = _BSchool.Add(school);
                if (isAdded != 0)
                {
                    return Json(new { success = true, message = "School added successfully", status = true });
                }
                return Json(new { success = true, message = "School Name already exist", status = false });
            }
            else
            {
                school.ID = school.ID;
                _BSchool.Update(school);
                return Json(new { success = true, message = "School edit successfully", status = true });
            }

        }


        [HttpPost]
        public ActionResult DeleteById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Guid _id = new Guid(id);
                School obj = _BSchool.GetAll().Where(x => x.ID == _id).FirstOrDefault();
                if (obj != null)
                {
                    _BSchool.Remove(obj);
                    return Json(new { success = true, message = "School Deleted successfully", status = true });
                }
            }
            return Json(new { success = true, message = "School Does not exist", status = false });
        }

    }
}
