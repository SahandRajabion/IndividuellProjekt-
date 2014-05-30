using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication2.Model;
using WebApplication2.Model.DAL;

namespace WebApplication2.Pages
{



    public partial class Music1 : System.Web.UI.Page
    {


        ////Skapar Fält.
        private Service _service;


        //Ett service-objekt skapas först då det behövs för första gången, 
        //det skapas då  ej ngt nytt objekt för varje gång tex. en ändring ska ske. 
        private Service Service
        {

            get { return _service ?? (_service = new Service()); }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
         
        }


        /// <summary>
        /// Metod för inhämtning av videoklipp genom ID för rätt kategori via databasen
        /// </summary>
        /// <param name="maximumRows"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="totalRowCount"></param>
        /// <returns>List objekt retuneras med information by ID</returns>
        public IEnumerable<WebApplication2.Model.Video> VideoListView_GetData(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return Service.GetVideosPageWiseByID(maximumRows, startRowIndex, out totalRowCount, 1);
        }


        /// <summary>
        /// Metod för Popup vid kommentar (open / Close)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButton1_Command(object sender, CommandEventArgs e)
        {

            ListViewDataItem item = (ListViewDataItem)(sender as Control).NamingContainer;
            Label lblStatus = (Label)item.FindControl("Label1");
            lblStatus.Visible = true;
           
            
        }

        protected void ImageButton1_Command(object sender, CommandEventArgs e)
       
        {

            ListViewDataItem item = (ListViewDataItem)(sender as Control).NamingContainer;
            Label lblStatus = (Label)item.FindControl("Label1");
            lblStatus.Visible = false;
          
           
        }



     }

   }
