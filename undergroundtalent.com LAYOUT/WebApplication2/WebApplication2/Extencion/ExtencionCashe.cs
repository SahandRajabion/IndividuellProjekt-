using ASPSnippets.FaceBookAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using WebApplication2.Model;

namespace WebApplication2
{
     public static class ExtencionCashe
    {

        /// <summary>
        /// Cashar User data av användaren som håller i 20 min (lika länge som accesstoken i session)
        /// Hämar från casheminnet ifall det finns något att hämta, vid annat fall hämtas user info via "Fetch".
        /// </summary>
        /// <param name="refresh"></param>
        /// <returns>FbUserInfo</returns>
        public static UserInfo CasheInfo(string code, bool refresh = false)
        {
            var FbUserInfo = HttpContext.Current.Cache["fbData"] as UserInfo;

            if (FbUserInfo == null || refresh)
            {
                
                string data = FaceBookConnect.Fetch(code, "me");
                FbUserInfo = new JavaScriptSerializer().Deserialize<UserInfo>(data);
                HttpContext.Current.Cache.Insert("fbData", FbUserInfo, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);
            }

            return FbUserInfo;
        }







    }
}