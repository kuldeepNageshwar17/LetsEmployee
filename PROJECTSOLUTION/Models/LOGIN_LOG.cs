using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROJECTSOLUTION.Models
{
  public class LOGIN_LOG
  {
    [Key]
    public int ID { get; set; }
    public int USER_ID { get; set; }
    public DateTime DATE { get; set; }

  }
}
