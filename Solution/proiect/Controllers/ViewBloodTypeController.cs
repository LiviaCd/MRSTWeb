using proiect.BusinessLogic;
using proiect.BusinessLogic.AppBL;
using proiect.BusinessLogic.Interfaces;
using proiect.Domain.Entities.BloodType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proiect.Controllers
{
    public class ViewBloodTypeController : Controller
    {
          // GET: ViewBloodType
          private readonly IBloodType _bloodType;
          public ViewBloodTypeController()
          {
               var businessLogic = new BussinessLogic(); 
               _bloodType = businessLogic.GetBloodTypeBL();
          }

          public ActionResult TypePage()
          {
               return View();
          }

          [HttpPost]
          public ActionResult GetBloodType(int id)
          {
               var bloodTypeDetail = _bloodType.GetDetailBloodType(id);
               return View(bloodTypeDetail);
          }
     }
}