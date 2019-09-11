using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;
using PROJECTSOLUTION.Business;
using PROJECTSOLUTION.ViewModel;

namespace PROJECTSOLUTION.Controllers.Api
{
  [RoutePrefix("api/Account")]
  public class AccountController : ApiController
  {
    // GET: api/Account
    AccountBusiness _buss;
    AccountController()
    {
      this._buss = new AccountBusiness();
    }
    [Route("DirectLogin")]
    [HttpPost]
    public HttpResponseMessage DirectLogin()
    {
      //var User = _buss.Login(vm);
      //if (User == null)
      //{
      //  return Request.CreateResponse(HttpStatusCode.BadRequest);
      //}
      FormsAuthentication.SetAuthCookie("Coolkn17@gmail.com", false);
      //var Role = Enum.GetName(typeof(AllEnums.Rolls), User.ROLE);
      var authTicket = new FormsAuthenticationTicket(1,"kuldeep", DateTime.Now, DateTime.Now.AddMinutes(5), false, "user");

      //var authTicket = new FormsAuthenticationTicket(1, User.EMAIL, DateTime.Now, DateTime.Now.AddMinutes(5), false,  Convert.ToString(Role));
      string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
      var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
      return Request.CreateResponse(HttpStatusCode.OK, authCookie);
    }

    [Route("Login")]
    [HttpGet]
    public HttpResponseMessage Login(LoginViewModel vm)
    {
      var User = _buss.Login(vm);
      if (User == null)
      {
        return Request.CreateResponse(HttpStatusCode.BadRequest);
      }
      FormsAuthentication.SetAuthCookie(User.EMAIL, vm.IsRemeber);
      var Role = Enum.GetName(typeof(AllEnums.Rolls), User.ROLE);
      var authTicket = new FormsAuthenticationTicket(1, User.EMAIL, DateTime.Now, DateTime.Now.AddMinutes(5), false, Convert.ToString(Role));

      //var authTicket = new FormsAuthenticationTicket(1, User.EMAIL, DateTime.Now, DateTime.Now.AddMinutes(5), false,  Convert.ToString(Role));
      string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
      var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
      return Request.CreateResponse(HttpStatusCode.OK, authCookie);
    }
    [Route("CheckLogin")]
    [HttpGet]
    public HttpResponseMessage CheckLogin()
    {
      HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName);
      FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
      var userName = ticket.UserData;
      //var isLogedIn = User.Identity.IsAuthenticated;
      //var userName = User.Identity.Name;
      //var Role = User.Identity.ToString;

      return Request.CreateResponse(HttpStatusCode.OK,new {
        isAuthenticated=false,
        username="",
        Role="",
      });
    }


    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    // GET: api/Account/5
    public string Get(int id)
    {
      return "value";
    }

    // POST: api/Account
    public void Post([FromBody]string value)
    {
    }

    // PUT: api/Account/5
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE: api/Account/5
    public void Delete(int id)
    {
    }
  }
}
