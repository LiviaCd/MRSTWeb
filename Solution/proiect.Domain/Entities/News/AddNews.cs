using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proiect.Domain.Entities.News
{
     public class AddNews
     {
          public string Title { get; set; }
          public string Body { get; set; }
          public string PhotoPath { get; set; }
          public DateTime Created { get; set; }
     }
}
