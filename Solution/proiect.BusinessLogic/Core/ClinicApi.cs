using proiect.BusinessLogic.DBModel.Seed;
using proiect.Domain.Entities.BloodR;
using proiect.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace proiect.BusinessLogic.Core
{
     public class ClinicApi
     {
          public BloodResource RGetClinicByEmail(string email)
          {
               var user = new BloodResource();
               using (var dbContext = new BloodContext())
               {
                    var clinic = dbContext.Resources.FirstOrDefault(u => u.EmailClinic == email);
                    if (clinic == null)
                         return null;
                    else
                    {
                         var clinicToReturn = new BloodResource
                         {
                              EmailClinic = clinic.EmailClinic,
                              OnePozitive = clinic.OnePozitive,
                              TwoPozitive = clinic.TwoPozitive,
                              ThirdPozitive = clinic.ThirdPozitive,
                              FourPozitive = clinic.FourPozitive,
                              OneNegative = clinic.OneNegative,
                              TwoNegative = clinic.TwoNegative,
                              ThirdNegative = clinic.ThirdNegative,
                              FourNegative = clinic.FourNegative,
                              Reservat = clinic.Reservat,
                              WhoReservate = clinic.WhoReservate,
                              Id = clinic.Id
                         };

                         
                         return clinicToReturn;
                    }
               }
          }
          public void REditProfile(BloodResource bloodModel)
          {
               using (var dbContext = new BloodContext())
               {
                    string email = HttpContext.Current.Session["Username"].ToString();
                    var clinic = dbContext.Resources.FirstOrDefault(us => us.EmailClinic == email);
                    if (clinic == null) return;

                    clinic.EmailClinic = clinic.EmailClinic;
                    clinic.OnePozitive = clinic.OnePozitive;
                    clinic.TwoPozitive = clinic.TwoPozitive;
                    clinic.ThirdPozitive = clinic.ThirdPozitive;
                    clinic.FourPozitive = clinic.FourPozitive;
                    clinic.OneNegative = clinic.OneNegative;
                    clinic.TwoNegative = clinic.TwoNegative;
                    clinic.ThirdNegative = clinic.ThirdNegative;
                    clinic.FourNegative = clinic.FourNegative;


                    dbContext.SaveChanges();
               }
          }
          public BloodResource RGetClinicById(int Id)
          {
               BloodResource userToReturn;
               
               using (var dbContext = new BloodContext())
               {
                    var user = dbContext.Resources.FirstOrDefault(us => us.Id == Id);
                    if (user == null) return null;
                    userToReturn = new BloodResource
                    {
                         Id = user.Id,
                         EmailClinic = user.EmailClinic,
                         OnePozitive = user.OnePozitive,
                         TwoPozitive = user.TwoPozitive,
                         ThirdPozitive = user.ThirdPozitive,
                         ThirdNegative = user.ThirdNegative,
                         FourNegative = user.FourNegative,
                         OneNegative = user.OneNegative,
                         TwoNegative = user.TwoNegative,
                         FourPozitive = user.FourPozitive,
                         Reservat = user.Reservat,
                         WhoReservate = user.WhoReservate,
                         
                    };
                    return userToReturn;
               }
              
          }

          public void RReservBlood(int Id, BloodResource userModel)
          {
               string email = HttpContext.Current.Session["Username"].ToString();
               using (var dbContext = new BloodContext())
               {
                    var user = dbContext.Resources.FirstOrDefault(us => us.Id == Id);
                    if (user == null) return;

                    // Maparea proprietăților din userModel către user
                    user.Reservat = true;
                    user.WhoReservate = email;

                    dbContext.SaveChanges();
               }
          }

     }
}
