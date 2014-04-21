using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class Upload : System.Web.UI.Page
    {
        private static string fromRootTovideo = Path.Combine(
                 AppDomain.CurrentDomain.GetData("APPBASE").ToString(),
                 "Images/"
                 );

        public static bool Exists(string name)
        {
            return File.Exists(Path.Combine(fromRootTovideo, name));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{

                string videoFolder = Path.Combine(fromRootTovideo, User.Identity.Name);

                if (Directory.Exists(videoFolder))
                {
                    DisplayUploadedVideos(videoFolder);
                }
            //}
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fuUpload.HasFile)
            {
                if ((fuUpload.PostedFile.ContentType == "video/avi") ||
                     (fuUpload.PostedFile.ContentType == "video/mp4") ||
                    (fuUpload.PostedFile.ContentType == "video/wmv") ||
                     (fuUpload.PostedFile.ContentType == "video/MOV") ||
                     (fuUpload.PostedFile.ContentType == "video/mpeg"))
                {
                    if (Convert.ToInt64(fuUpload.PostedFile.ContentLength) < 2500000000)
                    {
                        string vidFolder = Path.Combine(fromRootTovideo, User.Identity.Name);
                        int count = 1;

                        if (Exists(fuUpload.FileName))
                        {
                            string f = fuUpload.FileName;
                            //string directory = Path.GetDirectoryName(fuUpload.FileName);
                            string extension = Path.GetExtension(fuUpload.FileName);
                            string nameOnly = Path.GetFileNameWithoutExtension(fuUpload.FileName);
                            //string uniqueFileName = Path.ChangeExtension(fuUpload.FileName, DateTime.Now.Ticks.ToString());
                            while (Exists(f))
                            {
                                f = string.Format("{0}({1}){2}", nameOnly, count, extension);
                                count++;

                            }
                            fuUpload.SaveAs(Path.Combine(vidFolder, f));




                        }
                        fuUpload.SaveAs(Path.Combine(vidFolder, fuUpload.FileName));
                        DisplayUploadedVideos(vidFolder);
                        lblStatus.Text = "<font color='Green'>Successfully uploaded " + fuUpload.FileName + "</font>";
                    }
                    else
                        lblStatus.Text = "File must be less than 25 MB.";
                }
                else
                    lblStatus.Text = "File must be of type avi, mp4, wmv or mpeg.";
            }
            else
                lblStatus.Text = "No file selected to upload.";
        }

        public void DisplayUploadedVideos(string folder)
        {
            string[] allvideoFiles = Directory.GetFiles(folder);
            IList<string> allvideoPaths = new List<string>();
            string fileName;

            foreach (string f in allvideoFiles)
            {
                fileName = Path.GetFileName(f);
                allvideoPaths.Add("Images/" + User.Identity.Name + "/" + fileName);
            }

            DataList1.DataSource = allvideoPaths;
            DataList1.DataBind();
            
        }
        
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            bool deletionOccurs = false;

            foreach (DataListItem ri in DataList1.Items)
            {
                CheckBox cb = ri.FindControl("cbDelete") as CheckBox;

                if (cb.Checked)
                {
                    string fromVideoToExtension = cb.Attributes["special"];
                    string fromRootToHome = Path.Combine(
                AppDomain.CurrentDomain.GetData("APPBASE").ToString()

                );
                    string fileToDelete = Path.Combine(fromRootToHome, fromVideoToExtension);
                    File.Delete(fileToDelete);

                    lblStatus.Text = "<font color='Green'>Video(s) successfully deleted.</font>";
                    deletionOccurs = true;
                }
            }

            if (deletionOccurs)
                DisplayUploadedVideos(Path.Combine(fromRootTovideo, User.Identity.Name));
            else
                lblStatus.Text = "No file selected to delete.";
        }

        //protected void vidUser_Command(object sender, CommandEventArgs e)
        //{
        //    StringBuilder script = new StringBuilder();

        //    script.Append("<script type='text/javascript'>");
        //    script.Append("var viewer = new PhotoViewer();");
        //    script.Append("viewer.setBorderWidth(0);");
        //    script.Append("viewer.disableToolbar();");
        //    script.Append("viewer.add('" + e.CommandArgument + "');");
        //    script.Append("viewer.show(0);");
        //    script.Append("</script>");

        //    ClientScript.RegisterStartupScript(GetType(), "viewer", script.ToString());
        //}
    }
}