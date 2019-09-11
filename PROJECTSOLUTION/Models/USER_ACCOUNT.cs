using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROJECTSOLUTION.Models
{
  public class USER_ACCOUNT
  {
    [Key]
    public int ID { get; set; }
    public int ROLE { get; set; }

    public String EMAIL { get; set; }
    public String PASSWORD { get; set; }
    public String PASSWORD_HASH { get; set; }
    public String FIRST_NAME { get; set; }
    public String MIDDLE_NAME { get; set; }

    public String LAST_NAME { get; set; }
    public String FULL_NAME { get; set; }

    public String GENDER { get; set; }
    public bool IS_ACTIVE { get; set; }
    public bool IS_DELETE { get; set; } = false;

    public String MOBILE { get; set; }
    public bool SMS_NOTIFICATION { get; set; } = false;
    public bool EMAIL_NOTIFICATION { get; set; } = false;
    public DateTime REGISTRATION_DATE { get; set; } = DateTime.Now;

    public DateTime MODIFICATION_DATE { get; set; }
  }
}
