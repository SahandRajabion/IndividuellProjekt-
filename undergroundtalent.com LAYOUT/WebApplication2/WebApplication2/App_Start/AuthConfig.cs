using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASPSnippets.FaceBookAPI;

namespace WebApplication2
{
    internal static class AuthConfig
    {

        

        public static void RegisterOpenAuth()
        {
            FaceBookConnect.API_Key = "586484814797975";
            FaceBookConnect.API_Secret = "61314ad0c7e54742762edc9c9c30aa07";

          
        }

     
    }
}