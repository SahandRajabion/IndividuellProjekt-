using AjaxControlToolkit;
using ASPSnippets.FaceBookAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication2.Model;
using WebApplication2.Model.DAL;

namespace WebApplication2
{

    public partial class Upload : System.Web.UI.Page
    {



        // Service fält
        private Service _service;

        //Ett service-objekt skapas först då det behövs för första gången, 
        //det skapas då  ej ngt nytt objekt för varje gång tex. en ändring ska ske. 
        private Service Service
        {


            get { return _service ?? (_service = new Service()); }

        }

        //Hämtar accestoken(facebook) genom SiteMastern
        public string Code
        {
            get { return ((SiteMaster)this.Master).Code; }
        }


        //Message session
        private string MSG
        {
            get
            {
                return Session["MSG"] as string;
            }
            set
            {
                Session["MSG"] = value;
            }
        }

        /// <summary>
        /// Metod för inhämtning av kategorierna i dropdownlisten
        /// </summary>
        /// <returns> Kategori Lista </returns>
        public IEnumerable<Category> DropDownCategory_GetData()
        {

            return Service.GetVideoCategory();
        }



        private static string videoRoot = Path.Combine(
                 AppDomain.CurrentDomain.GetData("APPBASE").ToString(),
                 "Videos/"
                 );

        /// <summary>
        /// Metod som checkar ifall uppladdat videoklippNamn redan existerar i rot-mappen
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Video namet</returns>
        public static bool Exists(string name)
        {
            return File.Exists(Path.Combine(videoRoot, name));
        }

        /// <summary>
        /// Ser till i page-load att användaren är inloggad innan tillgång till funktioner och personlig info.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (MSG != null)
            {
                Success.Visible = true;
                Success.Text = MSG;
                Session.Remove("MSG");
            }


            if (Code == null)
            {
                Label1.Visible = true;
                Label1.Text = "Du måste Logga in för att kunna se ditt innehåll / Ladda upp";
                DeleteButton.Visible = false;
                UploadButton.Visible = false;
                Uploadfiles.Visible = false;
                DropDownCategory.Visible = false;
                VideoListView.Visible = false;
                Titel.Visible = false;
                title.Visible = false;
                infoupload.Visible = false;
                Label3.Visible = false;
                Label5.Visible = false;
                Uploadbox.Visible = false;
                StatusLogin.CssClass = "fail";
               
            }


        }

        /// <summary>
        /// Metod för uppladdning / Validering av fil / 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void buttonUpload_Click(object sender, EventArgs e)
        {
            if (Code != null)
            {
                if (IsValid)
                {


                    if (Titel.Text != string.Empty)
                    {

                        int VidNameChars = Convert.ToInt32(Uploadfiles.FileName.Length);
                        if (Uploadfiles.HasFile)
                        {
                            if (VidNameChars <= 128)
                            {

                                if

                                ((Uploadfiles.PostedFile.ContentType == "video/avi") ||
                                     (Uploadfiles.PostedFile.ContentType == "video/mp4") ||
                                    (Uploadfiles.PostedFile.ContentType == "video/wmv") ||
                                     (Uploadfiles.PostedFile.ContentType == "video/MOV") ||
                                     (Uploadfiles.PostedFile.ContentType == "video/mpeg"))
                                {
                                    if (Convert.ToInt64(Uploadfiles.PostedFile.ContentLength) < 26214400)
                                    {
                                        string vidFolder = Path.Combine(videoRoot, User.Identity.Name);
                                        int count = 1;


                                        string file = Uploadfiles.FileName;
                                        if (Exists(file))
                                        {
                                            // För Loading bar. Hämtar scriptet från sitemaster vid klick på upload knappen och fortsätter igenom koden.
                                            string script = "$(document).ready(function () { $('[id*=MainContent_UploadButton]').click(); });";
                                            ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);

                                            string extension = Path.GetExtension(file);
                                            string nameOnly = Path.GetFileNameWithoutExtension(Uploadfiles.FileName);

                                            while (Exists(file))
                                            {
                                                file = string.Format("{0}({1}){2}", nameOnly, count, extension);
                                                count++;

                                            }


                                        }

                                        Uploadfiles.SaveAs(Path.Combine(vidFolder, file));


                                        MSG = "Uppladdningen Lyckades:  " + file;



                                        //Hämtar User info för att sedan veta vem som laddat upp vad.
                                        string info = FaceBookConnect.Fetch(Code, "me");
                                        UserInfo fbUSER = new JavaScriptSerializer().Deserialize<UserInfo>(info);


                                        var categoryID = 0;

                                        foreach (ListItem bm in DropDownCategory.Items)
                                        {
                                            if (bm.Selected)
                                            {
                                                categoryID = int.Parse(bm.Value);

                                            }
                                        }


                                        Service.InsertVideos(file, fbUSER.Id, categoryID, Titel.Text);
                                        Response.RedirectToRoute("upload");

                                    }


                                    else
                                    {
                                        LabelStatus.Text = "Filen får inte överstiga 25 MB.";
                                        LabelStatus.CssClass = "fail";
                                    }
                                }

                                else
                                {
                                    LabelStatus.Text = "Filen måste vara av typen mp4.";
                                    LabelStatus.CssClass = "fail";
                                }
                            }

                            else
                            {
                                LabelStatus.Text = "Filnamnet får ej överstiga 124 tecken, försök igen.";
                                LabelStatus.CssClass = "fail";
                            }

                        }

                        else
                        {

                            LabelStatus.Text = "Ingen fil vald för uppladdning, försök igen.";
                            LabelStatus.CssClass = "fail";
                        }
                    }
                    else
                    {
                        LabelStatus.Text = "Titel Måste Anges.";
                        LabelStatus.CssClass = "fail";

                    }

                }
            }
        }



        /// <summary>
        /// Metod för borttagning av videoklipp genom checkboxmarkering(1 eller flera via foreach-loop)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void buttonDelete_Click(object sender, EventArgs e)
        {
            if (Code != null)
            {

                bool delOccurs = false;

                foreach (ListViewItem ri in VideoListView.Items)
                {
                    CheckBox check = ri.FindControl("Delete") as CheckBox;

                    if (check.Checked)
                    {
                        // För Loading bar. Hämtar scriptet från sitemaster vid klick på delete knappen och fortsätter igenom koden.
                        string script = "$(document).ready(function () { $('[id*=MainContent_DeleteButton]').click(); });";
                        ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);

                        string fromVideoToExtension = check.Attributes["special"];
                        string correctName = fromVideoToExtension.Replace("Videos//", "");
                        string RootToHome = Path.Combine(
                     AppDomain.CurrentDomain.GetData("APPBASE").ToString(),
                     "Videos/"
                     );
                        string fileDelete = Path.Combine(RootToHome, fromVideoToExtension);
                        File.Delete(fileDelete);
                        Service.DeleteVideoName(correctName);

                        MSG = "Innehåll har tagits bort korrekt.";
                        delOccurs = true;
                    }
                }



                if (delOccurs)
                {
                    Response.RedirectToRoute("upload");
                }
                else
                {
                    LabelStatus.Text = "Ingen fil vald för att ta bort.";
                    LabelStatus.CssClass = "fail";

                }
            }
        }



        /// <summary>
        /// Metod för inhämtning av användarens videoklipp / Paging / Check Admin / jämför user IDn
        /// </summary>
        /// <param name="maximumRows"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="totalRowCount"></param>
        /// <returns> Paging info samt retunerar allt (IF Admin) eller användarens personliga videoklipp</returns>
        public IEnumerable<WebApplication2.Model.Video> VideoListView_GetData(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            UserInfo FbUserInfo = null;
            if (Code != null)
            {
                 FbUserInfo = ExtencionDataFetch.getdata(Code);

                Global AdminUser = new Global();
                var MyAdmin = AdminUser.Admin;


                if (FbUserInfo.Id == MyAdmin)
                {
                    return Service.GetVideosPageWise(maximumRows, startRowIndex, out totalRowCount);
                }
                return Service.GetMyVideosPageWiseByID(maximumRows, startRowIndex, out totalRowCount, FbUserInfo.Id); ;
            }





            return Service.GetVideosPageWise(maximumRows, startRowIndex, out totalRowCount);


            
        }













    }
}