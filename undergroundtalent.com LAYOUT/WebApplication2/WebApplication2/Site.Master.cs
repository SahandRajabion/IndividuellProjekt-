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
using System.Reflection;
using System.Collections.Specialized;




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
        /// Sparar accestoken i Session / åtkomst från alla sidor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        private void Page_Init(object sender, EventArgs e)
        {

         // Om access token = null, requestar efter den och lagrar i session.
            if (code == null)
            {

                code = Request.QueryString["code"];
                //Sparar accesstoken i Session (by default 20min).
                HttpContext.Current.Session["code"] = code;



                //Efter att användaren loggat in och access token hämtats utför följande (url visas nu utan access-token):
                if (code != null)
                {
                    // Taget från StackOverflow.
                    // Delar upp Urlen, och tar bort access token från urlen(krashar annars när man backar med historik knappen i webbläsaren då access token inte 
                    //längre är i bruk och systemet försöker fetcha om igen som finns kvar i urlen.)
                    string url = HttpContext.Current.Request.Url.AbsoluteUri;
                    string[] separateURL = url.Split('?');
                    NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(separateURL[1]);
                    queryString.Remove("code");
                    url = separateURL[0] + queryString.ToString();
                    Response.Redirect(url);
                


                }
                
             

                //Nedanstående äldre version som jag testat innan som fungerar, men sparar "code" i Viewstate.
                //Page.ViewStateUserKey = code;



            }

             //I annat fall hämtar direkt från session och anropas sedan nedanstående metod.
            else
            {   //Sparar accesstoken i Session (by default 20min), finns access token i session kvar inom 20min om användaren kommer tillbaka hämtas den här.
                HttpContext.Current.Session["code"] = code;

                //Nedanstående äldre version som jag testat innan som fungerar, men sparar "code" i Viewstate.
                //Page.ViewStateUserKey = code;
            }

            Page.PreLoad += master_Page_PreLoad;

    
         }


        /// <summary>
        /// Ser till att användaren är inloggad och  hämtar sedan info om användaren. Förstagångs användare lagras i databas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        //Tagit delvis kod från ASPsnippet och facebook Developer och redigerat koden utefter min applikation och den info som jag 
        //vill komma åt om användaren via facebook och sedan Cashat den själv och även sparat accesstoken i session.
        protected void master_Page_PreLoad(object sender, EventArgs e)
        {

                AuthConfig.RegisterOpenAuth();

                
                    if (Request.QueryString["logout"] == "true")
                    {
                        //Rensar chasheinfo från Casheminnet vid utloggning av användare.
                        HttpContext.Current.Cache.Remove("fbData");
                        code = null;
                        
                        return;   
                    }

                    

            
                    //Ifall access token finns tilgängligt(aktivt): hämta in data om user via facebook.
                    if (!string.IsNullOrEmpty(code))
                    {
                        //Anropar ChacheInfo metoden och inhämtat userdata från Chache minnet för vidare användning i Extencion klassen.
                        var fbUSER = ExtencionCashe.CasheInfo(code);
                        fbUSER.PictureUrl = string.Format("https://graph.facebook.com/{0}/picture", fbUSER.Id);
                        pnlFaceBookUser.Visible = true;
                        lblName.Text = fbUSER.Name;
                        ProfileImage.ImageUrl = fbUSER.PictureUrl;

                        // Förändring av log in knappar i olika lägen (ut-inloggad).
                        if (code == null)
                        {
                            btnLogin.Visible = true;
                        }
                        else
                        {
                            btnLogin.Visible = false;
                        }


                     
                        //Ifall förstagångs användare: Lagra data.
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
        /// Metod för inloggning via Facebook, hämtar specifik data som användaren först måste acceptera åtkomst av tredje part.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Login(object sender, EventArgs e)
        {
            FaceBookConnect.Authorize("user_photos,email", Request.Url.AbsoluteUri.Split('?')[0]);
            
        }


        /// <summary>
        /// Metod för utloggning av Facebook.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Logout(object sender, EventArgs e)
        {
            code = null;
           
            FaceBookConnect.Logout(code);

        
           
          
            
           
        }


        


    }
}