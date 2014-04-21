using ASPSnippets.FaceBookAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication2.Model.DataBase;
using System.IO;
using System.Net;
using Facebook;
using Facebook.Reflection;
using FB;




namespace WebApplication2
{
    public partial class SiteMaster : MasterPage
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthConfig.RegisterOpenAuth();

            if (!IsPostBack)
            {
                if (Request.QueryString["logout"] == "true")
                {
                    return;
                }

                if (Request.QueryString["error"] == "access_denied")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('User has denied access.')", true);
                    return;
                }

                string code = Request.QueryString["code"];
                if (!string.IsNullOrEmpty(code))
                {
                    string data = FaceBookConnect.Fetch(code, "me");
                    FaceBookUser faceBookUser = new JavaScriptSerializer().Deserialize<FaceBookUser>(data);
                    faceBookUser.PictureUrl = string.Format("https://graph.facebook.com/{0}/picture", faceBookUser.Id);
                    pnlFaceBookUser.Visible = true;
                    lblName.Text = faceBookUser.Name;
                    ProfileImage.ImageUrl = faceBookUser.PictureUrl;
                    if (code == null)
                    {
                        btnLogin.Visible = true;
                    }
                    else
                    {
                        btnLogin.Visible = false;
                    }


                    InsertFB insertFB = new InsertFB();

                    var returnedid = insertFB.getuserdata(faceBookUser.Id);

                    if (returnedid != faceBookUser.Id)
                    {
                        insertFB.InsertInsertFB(code, faceBookUser.Id, faceBookUser.Name);
                    }

                }
            }
        }


        
        protected void Login(object sender, EventArgs e)
        {
            FaceBookConnect.Authorize("user_photos,email", Request.Url.AbsoluteUri.Split('?')[0]);
            
        }

        protected void Logout(object sender, EventArgs e)
        {
            FaceBookConnect.Logout(Request.QueryString["code"]);
          
      
            //redirect to login page
            Response.Redirect("Site.Master");
        }




    }
}