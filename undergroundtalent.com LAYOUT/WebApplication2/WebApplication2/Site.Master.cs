using ASPSnippets.FaceBookAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication2.Model.DAL;
using System.IO;
using System.Net;
using Facebook;
using Facebook.Reflection;
using FB;
using WebApplication2.Model;




namespace WebApplication2
{
    public partial class SiteMaster : MasterPage
    {

        private Service _service;


        private Service Service
        {

            get { return _service ?? (_service = new Service()); }

        }
 

        //Accestoken egenskap (facebook) / Hämtar access token
        private static string code;

        public string Code
        {

            get { return code; }
        }


        /// <summary>
        /// Sparar accestoken i cookies / åtkomst från alla sidor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Init(object sender, EventArgs e)
        {

         
            if (code == null)
            {
                code = Request.QueryString["code"];

                Page.ViewStateUserKey = code;



            }

            else
            {
                Page.ViewStateUserKey = code;
            }

            Page.PreLoad += master_Page_PreLoad;

    
         }


        /// <summary>
        /// Ser till att användaren är inloggad och  hämtar sedan info om användaren.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void master_Page_PreLoad(object sender, EventArgs e)
        {

                AuthConfig.RegisterOpenAuth();

                
                    if (Request.QueryString["logout"] == "true")
                    {
                        code = null;
                        return;
                    }

                    if (Request.QueryString["error"] == "access_denied")
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('User has denied access.')", true);
                        return;
                    }


                    if (!string.IsNullOrEmpty(code))
                    {
                        string info = FaceBookConnect.Fetch(code, "me");
                        FaceBookUser fbUSER = new JavaScriptSerializer().Deserialize<FaceBookUser>(info);
                        fbUSER.PictureUrl = string.Format("https://graph.facebook.com/{0}/picture", fbUSER.Id);
                        pnlFaceBookUser.Visible = true;
                        lblName.Text = fbUSER.Name;
                        ProfileImage.ImageUrl = fbUSER.PictureUrl;
                        if (code == null)
                        {
                            btnLogin.Visible = true;
                        }
                        else
                        {
                            btnLogin.Visible = false;
                        }


                     

                        var returnedid = Service.getuserdata(fbUSER.Id);

                        if (returnedid != fbUSER.Id)
                        {
                            Service.InsertFaceBook(code, fbUSER.Id, fbUSER.Name);
                        }

                    }
                }

          
        



        protected void Page_Load(object sender, EventArgs e)
        {
            
        }


        /// <summary>
        /// Metod för inloggning via Facebook
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Login(object sender, EventArgs e)
        {
            FaceBookConnect.Authorize("user_photos,email", Request.Url.AbsoluteUri.Split('?')[0]);
            
        }


        /// <summary>
        /// Metod gör utloggning av Facebook
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Logout(object sender, EventArgs e)
        {
            FaceBookConnect.Logout(Request.QueryString["code"]);
          
      
            //redirect to login page
            Response.Redirect("Site.Master");
        }




    }
}