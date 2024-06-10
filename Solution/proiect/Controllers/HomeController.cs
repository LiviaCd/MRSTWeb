using Antlr.Runtime.Tree;
using proiect.Attributes;
using proiect.BusinessLogic;
using proiect.BusinessLogic.DBModel.Seed;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities.News;
using proiect.Domain.Entities.Profile;
using proiect.Domain.Entities.Responce;
using proiect.Domain.Entities.User;
using proiect.Extensions;
using proiect.Models;
using proiect.Models.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace proiect.Controllers
{
     public class HomeController : BaseController
     {
          private readonly IUserMonitoring _monitoring;
          // GET: Admin
          public HomeController()
          {
               var bl = new BussinessLogic();
               _monitoring = bl.GetMonitoringBL();
          }
          public ActionResult Index()
          {
               SessionStatus();
               if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
               {
                    return View (); 
               }
               return View();
              // return RedirectToAction("Index", "Home");
          }
          
          public ActionResult About()
          {
               SessionStatus();
               return View();
          }

          public ActionResult News()
          {
               SessionStatus();

               using (var dbContext = new NewsContext())
               {
                    // Retrieve and sort the data in descending order by the Created date
                    var newsEntities = dbContext.News
                        .OrderByDescending(n => n.Created)
                        .ToList();

                    // Project the sorted entities into the view model
                    var nNews = newsEntities.Select(u => new AddNews
                    {
                         Title = u.Title,
                         Body = u.Body,
                         Created = u.Created,
                         PhotoPath = u.PhotoPath,
                         Id = u.Id
                    }).ToList();

                    return View(nNews);
               }
          }


          [AdminMod]
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult AddNewsPage(AddNews news)
          {
               if (ModelState.IsValid)
               {
                    if (news.PhotoFile != null)
                    {
                         // Ensure unique file name
                         string fileName = Path.GetFileNameWithoutExtension(news.PhotoFile.FileName);
                         string extension = Path.GetExtension(news.PhotoFile.FileName);
                         fileName = fileName + DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;
                         string path = Path.Combine(Server.MapPath("~/Content/PhotoNews/"), fileName);

                         // Save file to server
                         news.PhotoFile.SaveAs(path);

                         // Set the photo path relative to the server root
                         news.PhotoPath = "~/Content/PhotoNews/" + fileName;
                    }

                    AddNews newNews = new AddNews
                    {
                         Title = news.Title,
                         Body = news.Body,
                         Created = DateTime.Now,
                         PhotoPath = news.PhotoPath,
                         PhotoFile = news.PhotoFile,
                         Id = news.Id
                    };

                    bool isAdded = _monitoring.AddNewNews(newNews);
                    if (isAdded)
                    {
                         ViewData["ConfirmationMessage"] = "Noutatea a fost creata cu succes";
                         return RedirectToAction("News", "Home");
                    }
                    else
                    {
                         ViewData["ConfirmationMessage"] = "Noutatea nu a fost creata";
                    }
               }

               return View(news);
          }

          [AdminMod]
          public ActionResult AddNewsPage()
          {
               SessionStatus();
               return View();
          }

          [AdminMod]
          public ActionResult EditNews()
          {
               SessionStatus();
               return View();
          }

          public ActionResult Information()
          {
               SessionStatus();
               return View();
          }
          
          public ActionResult ErrorAccessDenied()
          {
               return View();
          }
          public ActionResult BloodType()
          {
               var type = Request.QueryString["t"];

               UserData u = new UserData();
               u.UserName = "Customer";
               u.SingleType = type;

               return View(u);
          }
          [HttpPost]
          public ActionResult BloodType(string btn) 
          {
               return RedirectToAction("BloodType", "Home", new {@t = btn}); 
          }

          [AdminMod]
          [HttpGet]
          [Route("Home/EditNews/{id}")]
          public ActionResult EditNews(int id)
          {
               SessionStatus();
               var userFromDB = _monitoring.RGetNewsById(id);
               if (userFromDB == null)
               {
                    return View();
               }
               else
               {
                    return View("EditNews", userFromDB);
               }
          }

          [AdminMod]
          [HttpPost]
          [Route("Home/EditNews/{id}")]
          [ValidateAntiForgeryToken]
          public ActionResult EditNews(int id, AddNews newsModel)
          {
               SessionStatus();
               if (ModelState.IsValid)
               {
                    _monitoring.EditNewsAction(id, newsModel);
                    return RedirectToAction("News");
               }
               return View("EditNews", newsModel);
          }
          [AdminMod]
          [HttpGet]
          [Route("Home/DeleteNews/{id}")]
          public ActionResult DeleteNews(int id)
          {
               SessionStatus();
               var userFromDB = _monitoring.RGetNewsById(id);
               if (userFromDB == null)
               {
                    return View();
               }
               else
               {
                    return View("DeleteNews", userFromDB);
               }
          }
          [AdminMod]
          [HttpPost]
          [Route("Home/DeleteNews/{id}")]
          [ValidateAntiForgeryToken]
          public ActionResult DeleteNews(int id, AddNews newsModel)
          {
               SessionStatus();
               if (ModelState.IsValid)
               {
                    _monitoring.DeleteNewsAction(id, newsModel);
                    return RedirectToAction("News");
               }
               return View("DeleteNews", newsModel);
          }
     }
}