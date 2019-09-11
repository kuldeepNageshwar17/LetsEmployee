using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Data;
using context = System.Web.HttpContext;
using System.Web.Hosting;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Generic;
using System.Web;
/// <summary>
/// Summary description for Utility
/// </summary>
public static class Utility
{
  private static Random random = new Random();
  public static bool SendMessage(string MobileNumber, string SmsText)
  {
    try
    {
      string url = "http://www.smsjust.com/sms/user/urlsms.php?";
      url = url + "username=" + "kaptransdemo" + "&pass=" + "kap@user!123" + "&senderid=" + "KAPMSG" + " &dest_mobileno=_Mobile_number_&message=_smstext_&response=Y";

      url = url.Replace("_Mobile_number_", MobileNumber);
      url = url.Replace("_smstext_", SmsText);

      //var mg = Utility.SMSManager.createMessageLog(ml);
      HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
      webReq.Method = "GET";
      HttpWebResponse webResponse = (HttpWebResponse)webReq.GetResponse();
      //I don't use the response for anything right now. But I might log the response answer later on.   
      Stream answer = webResponse.GetResponseStream();
      StreamReader _recivedAnswer = new StreamReader(answer);
      string text = _recivedAnswer.ReadToEnd();

      return true;
    }
    catch (Exception ex)
    {
      return false;
    }






  }

  public static string GenerateOtpPass(int generateOtpLength)
  {
    var passwordString = string.Empty;
    try
    {
      // int lenthofpass = 6;
      var allowedChars = "1,2,3,4,5,6,7,8,9,0";
      char[] sep = { ',' };
      var arr = allowedChars.Split(sep);
      var rand = new Random();
      // for (int i = 0; i < lenthofpass; i++)
      for (var i = 0; i < generateOtpLength; i++)
      {
        var temp = arr[rand.Next(0, arr.Length)];
        passwordString += temp;
      }
    }
    catch (Exception ex)
    {
      //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + ex.Message + "');", true);
    }
    return passwordString;
  }


  // utilty function to convert string to byte[]        


  // utilty function to convert byte[] to string        



  public static string RandomString(int length)
  {
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    return new string(Enumerable.Repeat(chars, length)
      .Select(s => s[random.Next(s.Length)]).ToArray());
  }
  public static string RandomNumber(int length)
  {
    const string chars = "0123456789";
    return new string(Enumerable.Repeat(chars, length)
      .Select(s => s[random.Next(s.Length)]).ToArray());
  }


  // <summary>
  //Encryption and Decryption 
  // </summary>
  public static class Cryptography
  {
    private const string PassPhrase = "letsweemp@se"; // can be any string
    private const string SaltValue = "letsweemp@se"; // can be any string
    private const string HashAlgorithm = "SHA1"; // can be "MD5"
    private const int PasswordIterations = 2; // can be any number
    private const string InitVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
    private const int KeySize = 256; // can be 192 or 128

    /// <summary>
    /// methode for encrypt string
    /// </summary>
    /// <param name="plainText">panin string</param>
    /// <returns>encrypted string</returns>
    public static string Encrypt(string plainText)
    {
      var initVectorBytes = Encoding.ASCII.GetBytes(InitVector);
      var saltValueBytes = Encoding.ASCII.GetBytes(SaltValue);
      var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
      var password = new PasswordDeriveBytes(PassPhrase, saltValueBytes, HashAlgorithm, PasswordIterations);
      var keyBytes = password.GetBytes(KeySize / 8);
      var symmetricKey = new RijndaelManaged
      {
        Mode = CipherMode.CBC,
        Padding = PaddingMode.Zeros
      };
      var encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
      string cipherText;
      using (var memoryStream = new MemoryStream())
      {
        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
        {
          cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
          cryptoStream.FlushFinalBlock();
          var cipherTextBytes = memoryStream.ToArray();
          cipherText = Convert.ToBase64String(cipherTextBytes);
          //cipherText = cipherText.Replace("+", "$");
        }
      }
      return cipherText;
    }

    /// <summary>
    /// methode for encript string
    /// </summary>
    /// <returns>decrypted string</returns>
    public static string Decrypt(string cipherTextQ)
    {
      var cipherText = cipherTextQ.Replace(" ", "+");

      var initVectorBytes = Encoding.ASCII.GetBytes(InitVector);
      var saltValueBytes = Encoding.ASCII.GetBytes(SaltValue);
      var cipherTextBytes = Convert.FromBase64String(cipherText);
      var password = new PasswordDeriveBytes(PassPhrase, saltValueBytes, HashAlgorithm, PasswordIterations);
      var keyBytes = password.GetBytes(KeySize / 8);
      var symmetricKey = new RijndaelManaged
      {
        Mode = CipherMode.CBC,
        Padding = PaddingMode.Zeros
      };
      var decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
      string plainText;
      using (var memoryStream = new MemoryStream(cipherTextBytes))
      {
        using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
        {
          var plainTextBytes = new byte[cipherTextBytes.Length];
          var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
          plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
      }
      return plainText;
    }

    /// <summary>
    /// methode for encrypt string
    /// </summary>
    /// <param name="plainText">panin string</param>
    /// <returns>encrypted string</returns>
    public static string funEncrypt(string clearText)
    {
      var encryptionKey = WebConfigurationManager.AppSettings.Get("EncryptKey");
      var clearBytes = Encoding.Unicode.GetBytes(clearText);
      using (var encryptor = Aes.Create())
      {
        var pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        if (encryptor == null) return clearText;
        encryptor.Key = pdb.GetBytes(32);
        encryptor.IV = pdb.GetBytes(16);
        using (var ms = new MemoryStream())
        {
          using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
          {
            cs.Write(clearBytes, 0, clearBytes.Length);
            cs.Close();
          }
          clearText = Convert.ToBase64String(ms.ToArray());
        }
      }
      return clearText;
    }

    /// <summary>
    /// methode for encript string
    /// </summary>
    /// <returns>decrypted string</returns>
    public static string FunDecrypt(string cipherText)
    {
      var encryptionKey = WebConfigurationManager.AppSettings.Get("EncryptKey");
      cipherText = cipherText.Replace(" ", "+");
      var cipherBytes = Convert.FromBase64String(cipherText);
      using (var encryptor = Aes.Create())
      {
        var pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        if (encryptor == null) return cipherText;
        encryptor.Key = pdb.GetBytes(32);
        encryptor.IV = pdb.GetBytes(16);
        using (var ms = new MemoryStream())
        {
          using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
          {
            cs.Write(cipherBytes, 0, cipherBytes.Length);
            cs.Close();
          }
          cipherText = Encoding.Unicode.GetString(ms.ToArray());
        }
      }
      return cipherText;
    }

    public static string GeneratPasswordHash(string strpassword)
    {
      HashAlgorithm hash = new SHA512CryptoServiceProvider();
      var password = strpassword;
      var salt = "Rem@Salt";

      // compute hash of the password prefixing password with the salt
      var plainTextBytes = Encoding.UTF8.GetBytes(salt + password);
      var hashBytes = hash.ComputeHash(plainTextBytes);

      var hashValue = Convert.ToBase64String(hashBytes);
      return hashValue;
    }

    /// <summary>
    /// use for send sms on mobile
    /// </summary>

    public static bool SendMessage(string MobileNumber, string SmsText)
    {
      try
      {
        string url = WebConfigurationManager.AppSettings["SmsApi"].ToString();
        url = url + "key=25BFE6DCBDB856&&routeid=100233&&type=text&&senderid=REEMDS&&contacts=_Mobile_number_&&msg= _smstext_";
        url = url.Replace("_Mobile_number_", MobileNumber);
        url = url.Replace("_smstext_", SmsText);
        //string response = makeHttpRequest(url);
        //string code = GetResponseMessage(response, out isSuccess, out errMsg);
        HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(string.Format(url));
        webReq.Method = "GET";
        HttpWebResponse webResponse = (HttpWebResponse)webReq.GetResponse();
        //I don't use the response for anything right now. But I might log the response answer later on.   
        Stream answer = webResponse.GetResponseStream();
        StreamReader _recivedAnswer = new StreamReader(answer);
        return true;

      }
      catch (Exception ex)
      {

        return false;
      }

    }

  }

  /// <summary>
  /// use for send email on email id
  /// </summary>
  public static class MailManager
  {
    public static void sendmail(string Email, String MailBody, String Mail_Subject)
    {

      try
      {

        var senderEmail = new MailAddress("mymspay@gmail.com", "Mymspay");
        var receiverEmail = new MailAddress(Email);
        var password = "OPSahu@2019";
        var sub = Mail_Subject;
        var body = MailBody;
        var smtp = new SmtpClient
        {
          Host = "smtp.gmail.com",
          Port = 587,
          EnableSsl = true,
          DeliveryMethod = SmtpDeliveryMethod.Network,
          UseDefaultCredentials = false,
          Credentials = new NetworkCredential("mymspay@gmail.com", "OPSahu@2019")
        };
        using (var mess = new MailMessage(senderEmail, receiverEmail)
        {
          Subject = sub,
          Body = body
        })
        {
          smtp.Send(mess);
        }




      }
      catch (Exception ex)
      {

        ExceptionLogging.SendErrorToText(ex);
      }
    }
  }


  #region EmailHelper
  #endregion


  #region ExecptionHnadlers


  #endregion

  public static string IsSelectedController(this HtmlHelper html, string controllers = "", string cssClass = "active")
  {
    ViewContext viewContext = html.ViewContext;
    bool isChildAction = viewContext.Controller.ControllerContext.IsChildAction;

    if (isChildAction)
      viewContext = html.ViewContext.ParentActionViewContext;

    RouteValueDictionary routeValues = viewContext.RouteData.Values;
    string currentController = routeValues["controller"].ToString();



    if (String.IsNullOrEmpty(controllers))
      controllers = currentController;

    string[] acceptedControllers = controllers.Trim().Split(',').Distinct().ToArray();

    return acceptedControllers.Contains(currentController) ?
        cssClass : String.Empty;
  }

  public static string IsSelected(this HtmlHelper html, string controllers = "", string actions = "", string cssClass = "active")
  {
    ViewContext viewContext = html.ViewContext;
    bool isChildAction = viewContext.Controller.ControllerContext.IsChildAction;

    if (isChildAction)
      viewContext = html.ViewContext.ParentActionViewContext;

    RouteValueDictionary routeValues = viewContext.RouteData.Values;
    string currentAction = routeValues["action"].ToString();
    string currentController = routeValues["controller"].ToString();

    if (String.IsNullOrEmpty(actions))
      actions = currentAction;

    if (String.IsNullOrEmpty(controllers))
      controllers = currentController;

    string[] acceptedActions = actions.Trim().Split(',').Distinct().ToArray();
    string[] acceptedControllers = controllers.Trim().Split(',').Distinct().ToArray();

    return acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController) ?
        cssClass : String.Empty;
  }



}
public static class AllEnums
{
  public enum Rolls
  {
    Admin =501,
    Employer=502,
    Employee = 503,
  }
}

public static class ExceptionLogging
{


  private static String ErrorlineNo, Errormsg, extype, exurl, hostIp, ErrorLocation, HostAdd;

  public static void SendErrorToText(Exception ex)
  {
    var line = Environment.NewLine + Environment.NewLine;

    ErrorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
    Errormsg = ex.GetType().Name.ToString();
    extype = ex.GetType().ToString();
    exurl = HttpContext.Current.Request.Url.AbsoluteUri;
    ErrorLocation = ex.Message.ToString();

    try
    {
      string filepath = HostingEnvironment.MapPath("~/ExceptionDetailsFile/");  //Text File Path

      if (!Directory.Exists(filepath))
      {
        Directory.CreateDirectory(filepath);

      }
      filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name
      if (!File.Exists(filepath))
      {


        File.Create(filepath).Dispose();

      }
      using (StreamWriter sw = File.AppendText(filepath))
      {
        string error = "Log Written Date:" + " " + DateTime.Now.ToString() + line + "Error Line No :" + " " + ErrorlineNo + line + "Error Message:" + " " + Errormsg + line + "Exception Type:" + " " + extype + line + "Error Location :" + " " + ErrorLocation + line + " Error Page Url:" + " " + exurl + line + "User Host IP:" + " " + hostIp + line;
        sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
        sw.WriteLine("-------------------------------------------------------------------------------------");
        sw.WriteLine(line);
        sw.WriteLine(error);
        sw.WriteLine("--------------------------------*End*------------------------------------------");
        sw.WriteLine(line);
        sw.Flush();
        sw.Close();

      }

    }
    catch (Exception e)
    {
      e.ToString();

    }
  }

}
