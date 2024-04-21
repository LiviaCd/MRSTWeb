using Microsoft.ApplicationInsights.Extensibility.Implementation;
using proiect.Attributes;
using proiect.BusinessLogic;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace proiect.Controllers
{
    public class AdminController : BaseController
    {
          private readonly IUserMonitoring _monitoring;
          // GET: Admin
          public AdminController()
          {
               var bl = new BussinessLogic();
               _monitoring = bl.GetMonitoringBL();
          }

          [AdminMod]
          public ActionResult Users()
          {
               SessionStatus();
               var allUsers = _monitoring.GetAllUsers();
               return View(allUsers);
          }

     }
}