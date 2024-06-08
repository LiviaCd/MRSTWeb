using AutoMapper.Configuration;
using proiect.BusinessLogic;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities.Profile;
using proiect.Domain.Entities.User;
using proiect.Extensions;
using proiect.Models.Profile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proiect.Controllers
{
    public class ProfileController : BaseController
    {
          private readonly IProfile _profile;
          public ProfileController()
          {
               var bl = new BussinessLogic();
               _profile = bl.GetProfileBL();
          }
          public ActionResult ProfilePage()
          {
               SessionStatus();
               if (Session["Username"] != null)
               {
                    string email = Session["Username"].ToString();
                    var userFromDB = _profile.GetUserByEmail(email);
                    if (userFromDB != null)
                    {
                         var model = new MProfile
                         {
                              Email = userFromDB.Email,
                              PhotoPath = userFromDB.PhotoPath,
                              PhotoFile = userFromDB.PhotoFile,
                         };
                         return View(userFromDB);
                    }
               }
               return RedirectToAction("LogIn", "Login");
          }

          [HttpPost]
          public ActionResult ProfilePage(UserMinimal user)
          {
               var userProfile = _profile.GetUserByEmail(user.Email);
               if (userProfile == null)
               {
                    return RedirectToAction("Error", "Home");
               }

               var model = new MProfile
               {
                    Email = userProfile.Email,
                    PhotoPath = userProfile.PhotoPath,
                    PhotoFile = userProfile.PhotoFile,
               };

               return View(model);
          }

          [HttpGet]
          public ActionResult EditProfile()
          {
               SessionStatus();

               if (Session["Username"] != null)
               {
                    string email = Session["Username"].ToString();
                    var userFromDB = _profile.GetUserByEmail(email);

                    if (userFromDB != null)
                    {
                         var userModel = new UserMinimal
                         {
                              Email = userFromDB.Email,
                              FirstName = userFromDB.FirstName,
                              LastName = userFromDB.LastName,
                              Address = userFromDB.Address,
                              Phone = userFromDB.Phone,
                              LastLogin = userFromDB.LastLogin,
                              Level = userFromDB.Level,
                              LasIp = userFromDB.LasIp,
                         };

                         return View(userModel);
                    }
               }

               return RedirectToAction("LogIn", "Login");
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult EditProfile(UserMinimal userMinimal)
          {
               SessionStatus();

               if (userMinimal == null)
               {
                    ModelState.AddModelError(string.Empty, "Invalid user data.");
                    return View(userMinimal);
               }

               _profile.EditProfile(userMinimal);
               TempData["SuccessMessage"] = "Profile updated successfully.";
               return RedirectToAction("ProfilePage");
          }
          public ActionResult AddPhoto()
          {
               SessionStatus();
               return View();
          }
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult AddPhoto(MProfile profil)
          {
               SessionStatus();
               var user = System.Web.HttpContext.Current.GetMySessionObject();

               if (user != null)
               {
                    if (ModelState.IsValid)
                    {
                         if (profil.PhotoFile != null)
                         {
                              string fileName = Path.GetFileName(profil.PhotoFile.FileName);
                              string extension = Path.GetExtension(fileName);
                              fileName = fileName + extension;
                              string path = Path.Combine(Server.MapPath("~/Content/PhotosUsers/"), fileName);
                              profil.PhotoPath = "~/Content/PhotosUsers/" + fileName;
                              profil.PhotoFile.SaveAs(path);
                         }
                         var userProfile = new UserProfileDBTable
                         {
                              Email = Session["Username"].ToString(),
                              PhotoPath = profil.PhotoPath
                         };
                         _profile.AddPhotoUser(userProfile);
                         return RedirectToAction("ProfilePage", "Profile");
                    }
               }
               return RedirectToAction("Error", "Home");
          }

     }
}