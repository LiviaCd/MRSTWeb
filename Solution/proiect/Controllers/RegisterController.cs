using proiect.BusinessLogic;
using proiect.BusinessLogic.AppBL;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities;
using proiect.Domain.Entities.Responce;
using proiect.Domain.Entities.User;
using proiect.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proiect.Controllers
{
    public class RegisterController : Controller
    {
          private readonly ISession _session;
          
          public RegisterController()
          {
               var bl = new BussinessLogic();
               _session = bl.GetSessionBL();
          }
   




     }
}