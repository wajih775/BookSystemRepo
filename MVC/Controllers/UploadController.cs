using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFileUploader;
using MvcFileUploader.Models;

namespace MVC.Controllers
{
    [Authorize]
    public class UploadController : Controller
    {
        //
        // GET: /Upload/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Photo() 
        {

            var statuses = new List<ViewDataUploadFileResult>();
            for (var i = 0; i < Request.Files.Count; i++)
            {
                var st = FileSaver.StoreFile(x =>
                {
                    x.File = Request.Files[i];
                    x.StorageDirectory = Server.MapPath("~/content/static");
                    x.UrlPrefix = "/content/static";// this is used to generate the relative url of the file

                    string fileExtension = Request.Files[i].FileName;
                    fileExtension = fileExtension.Contains(".") ? fileExtension.Substring(fileExtension.LastIndexOf('.')) : "";

                    x.FileName = Guid.NewGuid().ToString() + fileExtension;// default is filename suffixed with filetimestamp
                    x.ThrowExceptions = true;//default is false, if false exception message is set in error property
                });

                statuses.Add(st);
            }



            var viewresult = Json(new { files = statuses });
            //for IE8 which does not accept application/json
            if (Request.Headers["Accept"] != null && !Request.Headers["Accept"].Contains("application/json"))
                viewresult.ContentType = "text/plain";

            return viewresult;
        }





    }
}
