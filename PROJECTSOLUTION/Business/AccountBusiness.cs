using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PROJECTSOLUTION.DATA;
using PROJECTSOLUTION.ViewModel;
using PROJECTSOLUTION.Models;
using System.Threading.Tasks;

namespace PROJECTSOLUTION.Business
{
  public class AccountBusiness
  {
    LetsEmployeeContext db;
    public AccountBusiness()
    {
      db = new LetsEmployeeContext();
    }
    public  USER_ACCOUNT Login(LoginViewModel vm)
    {
      if (!String.IsNullOrEmpty(vm.UserName) && !string.IsNullOrEmpty(vm.Password))
      {
        var passWordHash = Utility.Cryptography.GeneratPasswordHash(vm.Password);
        var user = db.USER_ACCOUNTs.FirstOrDefault(m => m.IS_DELETE != true && (m.MOBILE == vm.UserName || m.EMAIL == vm.UserName) && m.PASSWORD_HASH == passWordHash);
        return user;
      }
      return null;
    }

    public bool  IsMobileNumberExist(string mobile) {
      var user =db.USER_ACCOUNTs.FirstOrDefault(m => m.IS_DELETE != true && (m.MOBILE ==mobile));
      if (user != null)
      {
        return true;
      }
      return false;
    }
  }
}
