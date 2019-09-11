using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROJECTSOLUTION.ViewModel
{
  public class LoginViewModel
  {
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool IsRemeber { get; set; } = false;
  }
}
