using ASPSnippets.FaceBookAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using WebApplication2.Model;

namespace WebApplication2
{
    public static class ExtencionDataFetch
    {

        /// <summary>
        /// Fetchar user data när användaren är inloggad.
        /// </summary>
        /// <param name="code"></param>
        /// <returns>FbUserInfo</returns>
        public static UserInfo getdata(string code)
        {
           
            string data = FaceBookConnect.Fetch(code, "me");
            var FbUserInfo = new JavaScriptSerializer().Deserialize<UserInfo>(data);
            return FbUserInfo;
        }








    }
}