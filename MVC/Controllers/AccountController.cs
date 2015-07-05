using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebMatrix.WebData;

using MVC.Models;
using BusinessLayer;
using DomainModel;

namespace MVC.Controllers
{
    [Authorize]

    public class AccountController : Controller
    {
        //
        // GET: /Main/

        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (string.IsNullOrEmpty(returnUrl))
                {
                    if (Roles.IsUserInRole(User.Identity.Name, "User"))
                    {
                        return RedirectToAction("Index", "Profile");
                    }
                }
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.Email, model.Password, persistCookie: model.RememberMe))
            {
                if (returnUrl != null)
                    return RedirectToLocal(returnUrl);

                if (Roles.IsUserInRole(model.Email, "User"))
                {
                    return RedirectToAction("Index", "Profile");
                }

                if (Roles.IsUserInRole(model.Email, "Company"))
                {
                    return RedirectToAction("Index", "Company");
                }
            }

            if (WebSecurity.UserExists(model.Email))
            {
                if (!WebSecurity.IsConfirmed(model.Email))
                {
                    return RedirectToAction("Resend", "Account", new { invalid = "1" });
                }
            }
            

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);

        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");

        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            BindCountry();

            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            BindCountry();

            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    string result = WebSecurity.CreateUserAndAccount(model.Email, model.Password, new
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Country = model.Country,
                        City = model.City,
                        Gender = model.Gender,
                        DOB = model.DOB,
                        Nationality = model.Nationality
                    }, true);


                    Roles.AddUserToRole(model.Email, "User");
                    //Send Email
                    string body = string.Empty;
                    using (
                        StreamReader reader =
                            new StreamReader(Server.MapPath("~/content/template/AccountConfirmationTemplate.html")))
                    {
                        body = reader.ReadToEnd();
                    }
                    body = body.Replace("{NAME}", model.FirstName + ' ' + model.LastName);
                    body = body.Replace("{HREF}", Request.Url.Host + "/Account/Verify?cid=" + result);


                    Notifications.Send(model.Email, "", "", "Registration Confirmation", body, true);
                    return RedirectToAction("Confirmation", "Account");




                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
            else
            {
                ModelState.AddModelError("", "Error submitting data.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Confiramtion/
        [AllowAnonymous]
        public ActionResult Confirmation()
        {
            return View();
        }

        //
        // GET: /Verify/
        [AllowAnonymous]
        public ActionResult Verify()
        {
            if (Request.QueryString["cid"] != null)
            {
                string confirmationToken = Request.QueryString["cid"];
                if (WebSecurity.ConfirmAccount(confirmationToken))
                {
                    return View(true);
                }
                else
                {
                    return View(false);
                }

            }

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
         return View();
        }
    
        // POST: /ForgotPassword
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(VerificationModel vModel)
        {
            if (ModelState.IsValid)
            {
                BUserProfile bUserProfile=new BUserProfile();
                    UserProfile userProfile = bUserProfile.GetSingle(vModel.Email);
                if (userProfile != null)
                {
                    if (!string.IsNullOrEmpty(vModel.Email))
                {
                    string token = WebSecurity.GeneratePasswordResetToken(vModel.Email, 2880);
                    //Send Email
                    string body = string.Empty;
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/content/template/PasswordResetTemplate.html")))
                    {
                        body = reader.ReadToEnd();
                    }

                    
                    body = body.Replace("{USERNAME}",userProfile.FirstName +" "+ userProfile.LastName);
                    body = body.Replace("{RESET_LINK}",  "http://jobs.schoolgap.com/Account/PasswordReset?reset-id="+token);

                    Notifications.Send(vModel.Email, "", "", "Password Reset Request", body, true);
                    return RedirectToAction("ForgotPassword", "Account", new { sent = "1" });

                }
                }
                
                
            }
            return View();
        }

        //
        //GET: /PasswordReset/
        [AllowAnonymous]
        public ActionResult PasswordReset()
        {
            if (Request.QueryString["reset-id"] != null)
            {
                PasswordResetModel psModel=new PasswordResetModel();
                psModel.ResetToken = Request.QueryString["reset-id"].ToString();
                return View(psModel);
            }
            
            return View();
        }

        //
        //POST: /PasswordReset/
        [AllowAnonymous, ValidateAntiForgeryToken, HttpPost]
        public ActionResult PasswordReset(PasswordResetModel password)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(password.ResetToken))
                {
                    BUserProfile bUserProfile=new BUserProfile();
                    UserProfile uProfile = bUserProfile.GetSingle(password.Email);
                    if (uProfile != null)
                    {
                        ;
                        if (WebSecurity.ResetPassword(password.ResetToken, password.NewPassword))
                        {
                            return RedirectToAction("PasswordReset", "Account", new { sent = "1" });
                        }
                        else
                        {
                            return RedirectToAction("PasswordReset", "Account", new { sent = "-1" });
                        }
                    }
                }
            }
            return View(password);
        }



        //
         // GET: /Resend/
         [AllowAnonymous]
         public ActionResult Resend()
         {
             return View();
         }

         //
         // GET: /Resend/
         [AllowAnonymous, ValidateAntiForgeryToken, HttpPost]
         public ActionResult Resend(VerificationModel vModel)
         {
             if (ModelState.IsValid)
             {
                 if (!WebSecurity.IsConfirmed(vModel.Email))
                 {
                     Bwebpages_Membership bwebpagesMembership=new Bwebpages_Membership();
                     webpages_Membership webpagesMembership = bwebpagesMembership.GetSingle(vModel.Email);

                     //Send Email
                     string body = string.Empty;
                     using (StreamReader reader = new StreamReader(Server.MapPath("~/content/template/ResendConfirmationTemplate.html")))
                     {
                         body = reader.ReadToEnd();
                     }
                     body = body.Replace("{HREF}",  "http://jobs.schoolgap.com/Account/Verify?cid=" + webpagesMembership.ConfirmationToken);

                     Notifications.Send(vModel.Email, "", "", "Email Verification", body, true);
                     return RedirectToAction("Resend", "Account", new {sent = "1"});
                 }
                 else
                 {
                     ModelState.AddModelError("", "Invalid Email Address");
                 }
             }
             return View();
         }

        //=============================================
        //Methods
        //=============================================

        private void BindCountry()
        {
            BCountry bCountry = new BCountry();
            SelectList country = new SelectList(bCountry.GetAll(), "Country", "Country");

            ViewData["Countries"] = country;
        }

        //
        // GET: /Security/

        public ActionResult Security()
        {
            return View();
        }

        //
        // POST: /Passowrd Reset
        [HttpPost]
        public ActionResult Security(LocalPasswordModel localPasswordModel)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.ChangePassword(User.Identity.Name, localPasswordModel.OldPassword,
                localPasswordModel.NewPassword))
                {
                    return View();
                }
            }

            List<string> errorList=new List<string>();
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    errorList.Add(error.ErrorMessage);
                }
            }

            foreach (string s in errorList)
            {
                ModelState.AddModelError("", s);
            }

            
            return View();
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }



        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion

    }
}
