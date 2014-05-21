using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication2.Model.DAL
{
    public class VideoDAL : DALBase
    {

        /// <summary>
        /// Metod för insert av uppladdade videoklipp
        /// </summary>
        /// <param name="VidName"></param>
        /// <param name="UserID"></param>
        /// <param name="KategoriID"></param>
        public void InsertVideos(string VidName, string UserID, int KategoriID, string Titel)
        {
            // Skapar och initierar ett anslutningsobjekt
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att exekvera specifierad lagrad procedur
                    SqlCommand cmd = new SqlCommand("appSchema.InsertVideo", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till de parametrar den lagrade proceduren kräver
                    cmd.Parameters.Add("@VidName", SqlDbType.VarChar, 128).Value = VidName;
                    cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 100).Value = UserID;
                    cmd.Parameters.Add("@KategoriID", SqlDbType.Int, 4).Value = KategoriID;
                    cmd.Parameters.Add("@Titel", SqlDbType.VarChar, 30).Value = Titel;
                       



                    // Öppnar anslutningen till databasen
                    conn.Open();

                    cmd.ExecuteNonQuery();


                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }


        /// <summary>
        /// Metod för borttagning av videoklipp via Videonamn
        /// </summary>
        /// <param name="VidName"></param>
         public void DeleteVideoName(string VidName)
        {
            // Skapar och initierar ett anslutningsobjekt
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att exekvera specifierad lagrad procedur
                    SqlCommand cmd = new SqlCommand("appSchema.DeleteVideoName", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till den parameter den lagrade proceduren kräver
                    cmd.Parameters.Add("@VidName", SqlDbType.VarChar, 128).Value = VidName;

                    // Öppnar anslutningen till databasen
                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
       
        
         }


        /// <summary>
        /// Metod för Paging, hämtar videoklipp by category id.
        /// </summary>
        /// <param name="maximumRows"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="totalRowCount"></param>
        /// <param name="KategoriID"></param>
        /// <returns>videos by KategoriID/Paging</returns>
         public static IEnumerable<Video> GetVideosPageWiseByID(int maximumRows, int startRowIndex, out int totalRowCount, int KategoriID)
         {
             using (var conn = CreateConnection())
             {
                 try
                 {
                     var videos = new List<Video>(100);

                     var cmd = new SqlCommand("appSchema.GetVideosPageWiseByID", conn);
                     cmd.CommandType = CommandType.StoredProcedure;

                     cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = startRowIndex / maximumRows + 1;
                     cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = maximumRows;
                     cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                     cmd.Parameters.Add("@KategoriID", SqlDbType.Int, 4).Value = KategoriID;
                   

                     conn.Open();

                     cmd.ExecuteNonQuery();
                     totalRowCount = (int)cmd.Parameters["@RecordCount"].Value;

                     using (var reader = cmd.ExecuteReader())
                     {
                         var videoidIndex = reader.GetOrdinal("VideoID");
                         var videonameIndex = reader.GetOrdinal("VidName");
                         var useridIndex = reader.GetOrdinal("UserID");
                         var categoryIndex = reader.GetOrdinal("KategoriID");
                         var DatumIndex = reader.GetOrdinal("Datum");
                         var TitelIndex = reader.GetOrdinal("Titel");

                         while (reader.Read())
                         {
                             videos.Add(new Video
                             {
                                 VideoID = reader.GetInt32(videoidIndex),
                                 VidName = reader.GetString(videonameIndex),
                                 UserID = reader.GetString(useridIndex),
                                 KategoriID = reader.GetInt32(categoryIndex),
                                 Datum = reader.GetDateTime(DatumIndex),
                                 Titel = reader.GetString(TitelIndex)
                             });
                         }
                     }

                     videos.TrimExcess();
                     return videos;
                 }
                 catch (Exception)
                 {
                     throw new ApplicationException("An error occured in the data access layer.");
                 }
             }
         }


        /// <summary>
        /// Metod för inhämtning av alla videoklipp i databasen
        /// </summary>
        /// <param name="maximumRows"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="totalRowCount"></param>
        /// <returns>En lista returneras innehållande allt innehåll</returns>
         public static IEnumerable<Video> GetVideosPageWise(int maximumRows, int startRowIndex, out int totalRowCount)
         {
             using (var conn = CreateConnection())
             {
                 try
                 {
                     var videos = new List<Video>(100);

                     var cmd = new SqlCommand("appSchema.GetVideosPageWise", conn);
                     cmd.CommandType = CommandType.StoredProcedure;

                     cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = startRowIndex / maximumRows + 1;
                     cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = maximumRows;
                     cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                     conn.Open();

                     cmd.ExecuteNonQuery();
                     totalRowCount = (int)cmd.Parameters["@RecordCount"].Value;

                     using (var reader = cmd.ExecuteReader())
                     {
                         var videoidIndex = reader.GetOrdinal("VideoID");
                         var videonameIndex = reader.GetOrdinal("VidName");
                         var useridIndex = reader.GetOrdinal("UserID");
                         var categoryIndex = reader.GetOrdinal("KategoriID");
                         var DatumIndex = reader.GetOrdinal("Datum");
                         var TitelIndex = reader.GetOrdinal("Titel");

                         while (reader.Read())
                         {
                             videos.Add(new Video
                             {
                                 VideoID = reader.GetInt32(videoidIndex),
                                 VidName = reader.GetString(videonameIndex),
                                 UserID = reader.GetString(useridIndex),
                                 KategoriID = reader.GetInt32(categoryIndex),
                                 Datum = reader.GetDateTime(DatumIndex),
                                 Titel = reader.GetString(TitelIndex)
                             });
                         }
                     }

                     videos.TrimExcess();
                     return videos;
                 }
                 catch (Exception)
                 {
                     throw new ApplicationException("An error occured in the data access layer.");
                 }
             }
         }


        /// <summary>
        /// Inhämtning av  personliga uppladdade videoklipp för inloggade användare, byr userID.
        /// </summary>
        /// <param name="maximumRows"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="totalRowCount"></param>
        /// <param name="UserID"></param>
        /// <returns>Lista innehållande (videos) by userID</returns>
         public static IEnumerable<Video> GetMyVideosPageWiseByID(int maximumRows, int startRowIndex, out int totalRowCount, string UserID)
         {
             using (var conn = CreateConnection())
             {
                 try
                 {
                     var videos = new List<Video>(100);

                     var cmd = new SqlCommand("appSchema.GetMyVideosPageWiseByID", conn);
                     cmd.CommandType = CommandType.StoredProcedure;

                     cmd.Parameters.Add("@PageIndex", SqlDbType.Int, 4).Value = startRowIndex / maximumRows + 1;
                     cmd.Parameters.Add("@PageSize", SqlDbType.Int, 4).Value = maximumRows;
                     cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                     cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 100).Value = UserID;

                     conn.Open();

                     cmd.ExecuteNonQuery();
                     totalRowCount = (int)cmd.Parameters["@RecordCount"].Value;

                     using (var reader = cmd.ExecuteReader())
                     {
                         var videoidIndex = reader.GetOrdinal("VideoID");
                         var videonameIndex = reader.GetOrdinal("VidName");
                         var useridIndex = reader.GetOrdinal("UserID");
                         var videocategoryIndex = reader.GetOrdinal("KategoriID");
                         var DatumIndex = reader.GetOrdinal("Datum");
                         var TitelIndex = reader.GetOrdinal("Titel");

                         while (reader.Read())
                         {
                             videos.Add(new Video
                             {
                                 VideoID = reader.GetInt32(videoidIndex),
                                 VidName = reader.GetString(videonameIndex),
                                 UserID = reader.GetString(useridIndex),
                                 KategoriID = reader.GetInt32(videocategoryIndex),
                                 Datum = reader.GetDateTime(DatumIndex),
                                 Titel = reader.GetString(TitelIndex)
                             });
                         }
                     }

                     videos.TrimExcess();
                     return videos;
                 }
                 catch (Exception)
                 {
                     throw new ApplicationException("An error occured in the data access layer.");
                 }
             }
         }




         public void UpdateTitle(string Titel, int VideoID, int KategoriID)
         {
             // Skapar och initierar ett anslutningsobjekt.
             using (SqlConnection conn = CreateConnection())
             {
                 try
                 {
                     // Skapar ett SqlCommand-objekt för att exekvera specifierad lagrad procedur.
                     SqlCommand cmd = new SqlCommand("appSchema.UpdateTitelCategory", conn);
                     cmd.CommandType = CommandType.StoredProcedure;

                     // Lägger till de paramterar den lagrade proceduren kräver.
                     cmd.Parameters.Add("@Titel", SqlDbType.VarChar, 30).Value = Titel;
                     cmd.Parameters.Add("@VideoID", SqlDbType.VarChar, 100).Value = VideoID;
                     cmd.Parameters.Add("@KategoriID", SqlDbType.VarChar, 100).Value = KategoriID;
                    


                     // Öppnar anslutningen till databasen.
                     conn.Open();

                     // ExecuteNonQuery används för att exekvera den lagrade proceduren
                     cmd.ExecuteNonQuery();
                 }
                 catch
                 {
                     // Kastar ett eget undantag om ett undantag kastas.
                     throw new ApplicationException("An error occured in the data access layer.");
                 }
             }
         }


         public Video GetVideoDataByID(int videoid)
         {

             using (SqlConnection connection = CreateConnection())
             {
                 try
                 {
                     SqlCommand cmd = new SqlCommand("appSchema.GetVideoDataByID", connection);
                     cmd.CommandType = CommandType.StoredProcedure;

                     cmd.Parameters.Add("@VideoID", SqlDbType.Int, 4).Value = videoid;

                     connection.Open();

                     using (SqlDataReader reader = cmd.ExecuteReader())
                     {
                         if (reader.Read())
                         {
                             var videoidIndex = reader.GetOrdinal("VideoID");
                             var vidnameIndex = reader.GetOrdinal("VidName");
                             var useridIndex = reader.GetOrdinal("UserID");
                             var KategoriIDIndex = reader.GetOrdinal("KategoriID");
                             var titelIndex = reader.GetOrdinal("Titel");
                             var createddateIndex = reader.GetOrdinal("Datum");

                             return new Video
                             {
                                 VideoID = reader.GetInt32(videoidIndex),
                                 VidName = reader.GetString(vidnameIndex),
                                 UserID = reader.GetString(useridIndex),
                                 KategoriID = reader.GetInt32(KategoriIDIndex),
                                 Titel = reader.GetString(titelIndex),
                                 Datum = reader.GetDateTime(createddateIndex)

                             };
                         }
                     }
                     return null;
                 }
                 catch
                 {

                     throw new ApplicationException("An error occured in the data access layer.");
                 }
             }
         }

    }



    }
