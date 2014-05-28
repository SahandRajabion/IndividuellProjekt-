using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace WebApplication2
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {


            routes.MapPageRoute("upload", "laddaupp/videoklipp", "~/Upload.aspx");
            routes.MapPageRoute("musik", "musik/videoklipp", "~/Pages/Music.aspx");
            routes.MapPageRoute("dans", "dans/videoklipp", "~/Pages/Dance.aspx");
            routes.MapPageRoute("talang", "talang/videoklipp", "~/Pages/Talent.aspx");
            routes.MapPageRoute("beatbox", "beatbox/videoklipp", "~/Pages/Beatbox.aspx");
            routes.MapPageRoute("ovrigt", "ovrigt/videoklipp", "~/Pages/Other.aspx");
            routes.MapPageRoute("edit", "redigera/videoklipp/{id}", "~/Pages/EditVideo.aspx");
            routes.MapPageRoute("about", "omoss", "~/Pages/About.aspx");
            routes.MapPageRoute("contact", "kontakt", "~/Pages/Contact.aspx");
            routes.MapPageRoute("startsida", "", "~/Default.aspx");
           

            
        }
    }
}
