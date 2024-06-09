using proiect.Domain.Entities.Ancheta;
using proiect.Domain.Entities.News;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.BusinessLogic.DBModel.Seed
{
     public class NewsContext : DbContext
     {
          public NewsContext() :
             base("name=proiect") { }

          public virtual DbSet<AddNews> News { get; set; }
     }
}
