﻿using AutoMapper;
using proiect.BusinessLogic;
using proiect.BusinessLogic.Interfaces;
using proiect.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proiect.Controllers
{
     public class BaseController : Controller
     {
          private readonly ISession _session;
          public BaseController()
          {
               var bl = new BussinessLogic();
               _session = bl.GetSessionBL();
          }

          public void SessionStatus()
          {
               var apiCookie = Request.Cookies["X-KEY"];
               if (apiCookie != null)
               {
                    var profile = _session.GetUserByCookie(apiCookie.Value);
                    if (profile != null && profile.BlockTime < DateTime.Now)
                    {
                         System.Web.HttpContext.Current.SetMySessionObject(profile);
                         System.Web.HttpContext.Current.Session["LoginStatus"] = "login";
                         System.Web.HttpContext.Current.Session["Username"] = profile.Email;
                         System.Web.HttpContext.Current.Session["FirstName"] = profile.FirstName;
                         System.Web.HttpContext.Current.Session["LastName"] = profile.LastName;
                         System.Web.HttpContext.Current.Session["Role"] = profile.Level;
                         System.Web.HttpContext.Current.Session["BlockTime"] = profile.BlockTime;
                        
                         if (profile.PhotoPath != null)
                         {
                              System.Web.HttpContext.Current.Session["Photo"] = profile.PhotoPath;
                         }
                         else
                         {
                              System.Web.HttpContext.Current.Session["Photo"] = "~/Content/PhotosUsers/user.png";
                         }

                    }
                    else
                    {
                         System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";

                    }
               }
          }
     }
}