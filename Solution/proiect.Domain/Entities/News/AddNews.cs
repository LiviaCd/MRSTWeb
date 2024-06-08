using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace proiect.Domain.Entities.News
{
     public class AddNews
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int Id { get; set; }
          public string Title { get; set; }
          public string Body { get; set; }
          public string PhotoPath { get; set; }
          public DateTime Created { get; set; }
          [NotMapped]
          public HttpPostedFileBase PhotoFile { get; set; }
     }
}
