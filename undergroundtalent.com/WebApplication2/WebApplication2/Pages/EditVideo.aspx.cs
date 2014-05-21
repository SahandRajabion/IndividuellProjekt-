using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication2.Model;

namespace WebApplication2.Pages
{
    public partial class EditVideo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private Service _service;

        //Ett service-objekt skapas först då det behövs för första gången, 
        //det skapas då  ej ngt nytt objekt för varje gång tex. en ändring ska ske. 
        private Service Service
        {


            get { return _service ?? (_service = new Service()); }

        }

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

        public IEnumerable<Category> DropDownCategory_GetData()
        {

            return Service.GetVideoCategory();
        }


        // The id parameter name should match the DataKeyNames value set on the control
        public void VideoEditFormView_UpdateItem(Video video)
        {
            try
            {

                var KategoriID = 0;
                DropDownList Dropdown = (DropDownList)VideoEditFormView.FindControl("DropDownCategory");

                foreach (ListItem bm in Dropdown.Items)
                {
                    if (bm.Selected)
                    {
                        KategoriID = int.Parse(bm.Value);

                    }
                }



                if (video == null)
                {
                    // The item wasn't found
                    ModelState.AddModelError("", String.Format("Videon med id {0} hittades ej", video.VideoID));
                    return;
                }



                TryUpdateModel(video);
                if (ModelState.IsValid)
                {

                    Service.UpdateTitle(video.Titel, video.VideoID, KategoriID);
                    MSG = "Videon är uppdaterad.";
                    Response.RedirectToRoute("upload");
                    Context.ApplicationInstance.CompleteRequest();

                }


            }
            catch { ModelState.AddModelError(String.Empty, "Fel inträffade då Titeln skulle uppdateras. Titel måste anges."); }

        }

        public WebApplication2.Model.Video VideoEditFormView_GetItem([RouteData] int id)
        {
            return Service.GetVideoDataByID(id);
        }

    }
}