using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using WebApplication2.Model.DAL;



namespace WebApplication2.Model
{
    public class Service
    {
        ////Skapar Fält.
        private CategoryDAL _categoryDAL;

       
        //Skapar Egenskap.
        private CategoryDAL CategoryDAL
        {

            //Ett CategoryDAL-objekt skapas först då det behövs för första gången, 
            //det skapas då  ej ngt nytt objekt för varje gång tex. en ändring ska ske.
            get { return _categoryDAL ?? (_categoryDAL = new CategoryDAL()); }

        }

         ////Skapar Fält.
        private FBDAL _fbDAL;

        //Skapar Egenskap.
        private FBDAL FBDAL
        {
            get { return _fbDAL ?? (_fbDAL = new FBDAL()); }
        }

        ////Skapar Fält.
        private VideoDAL _videoDAL;

        //Skapar Egenskap.
        private VideoDAL VideoDAL
        {
            get { return _videoDAL ?? (_videoDAL = new VideoDAL()); }
        }

        public void InsertFaceBook(string access_token, string userid, string name)
        {
            FBDAL.InsertFaceBook(access_token,userid,name);
        }


        public string getuserdata(string userid)
        {
           return FBDAL.getuserdata(userid);
        }

        public void UpdateTitle(string Titel , int VideoID, int KategoriID)
        {
            VideoDAL.UpdateTitle(Titel, VideoID, KategoriID);
        }



        public void InsertVideos(string VidName, string UserID, int KategoriID, string Titel)

        {
            VideoDAL.InsertVideos(VidName,UserID,KategoriID, Titel);
        }

        public void DeleteVideoName(string VidName)
        {
            VideoDAL.DeleteVideoName(VidName);
        }

        public IEnumerable<Category> GetVideoCategory()
        {
            return CategoryDAL.GetVideoCategory();
        }


        public static IEnumerable<Video> GetVideosPageWiseByID(int maximumRows, int startRowIndex, out int totalRowCount, int KategoriID)
        {
            return VideoDAL.GetVideosPageWiseByID(maximumRows, startRowIndex, out totalRowCount, KategoriID);
        }


        public static IEnumerable<Video> GetVideosPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            return VideoDAL.GetVideosPageWise(maximumRows, startRowIndex, out totalRowCount);
        }

        public static IEnumerable<Video> GetMyVideosPageWiseByID(int maximumRows, int startRowIndex, out int totalRowCount, string userid)
        {
            return VideoDAL.GetMyVideosPageWiseByID(maximumRows, startRowIndex, out totalRowCount, userid);
        }

        public Video GetVideoDataByID(int videoid)
        {
            return VideoDAL.GetVideoDataByID(videoid);
        }
    }
}