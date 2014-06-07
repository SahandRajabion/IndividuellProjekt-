using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication2.Model.DAL
{
    public class RatingDAL : DALBase
    {
        
        //Får en referens av ett list objekt i return med alla Trådar i databasen
        public Ratings GetRating()
        {
            //Skapar och initierar ett anslutningsobjekt genom basklassen.
            //Använder Using (ist. för conn.Close) för att stänga ner objektet per automatik efter användning.
            using (var conn = CreateConnection())
            {

                try
                {
                
                    
                    //Exekverar den lagrade proceduren "appSchema.usp_GetThreads", som har samma anslutningsobjekt (conn).
                    var cmd = new SqlCommand("appSchema.GetRating", conn);
                    //Sätter om typen till Stored procedure då den av standard är av typen "Text".
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Öppnar upp en anslutning till databasen.
                    conn.Open();

                    //Exekverar SELECT-frågan i den lagrade proceduren och retunerar en Datareader-Objekt som gör att vi kan få ut rätt index nedan.
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        //Via "GetOrdinal", får vi tillbaka rätt index som tillhör det angivna fältet. (Bättre än hårdkodat index).
                        var videoIdIndex = reader.GetOrdinal("VideoID");
                        var vidNameIndex = reader.GetOrdinal("VidName");
                        var ratingIndex = reader.GetOrdinal("Rating");

                        //Loopar igenom det retunerade SqlDataReader-objektet tills det ej finns poster kvar att läsa.(While Statement Returns True).
                        while (reader.Read())
                        {

                            //Refererar till klassen "Thread".
                            return new Ratings
                            {
                                //Tolkar posterna från databasen till C#, datatyper.
                                VideoID = reader.GetInt32(videoIdIndex),
                                VidName = reader.GetString(vidNameIndex),
                                Ratinghejs = reader.GetString(ratingIndex)

                            };

                        }

                    }
               
                    return null;
                }

                catch (Exception)
                {
                    throw new ApplicationException("Fel uppstod när betygen skulle hämtas från databasen.");
                }
            }

        }


        



        //Skapar ny Tråd i databasen.
        public void InsertRating(int VideoID, string Value)
        {
            //Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    //Exekverar den lagrade proceduren "appSchema.usp_NewThread", som har samma anslutningsobjekt (conn).
                    SqlCommand cmd = new SqlCommand("appSchema.InsertRating", conn);
                    //Sätter om typen till Stored procedure då den av standard är av typen "Text".
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Lägger till de parametrar som behövs för tillägg av ny tråd i proceduren, samt datatyper.
                    cmd.Parameters.Add("@VideoID", SqlDbType.Int).Value = VideoID;
                    cmd.Parameters.Add("@Rating", SqlDbType.Int).Value = Value;

                  

                    //Öppnar anslutning till databasen
                    conn.Open();

                    //Exekverar den del av den lagrade proceduren (ej SELECT) som används till att lägga till en ny tråd(INSERT-sats).
                    //Antalet påverkade poster retuneras.
                    cmd.ExecuteNonQuery();


                }

                catch
                {
                    throw new ApplicationException("Ett fel har uppstått i dataåtkomst lagret. Gick ej lägga till betyg.");
                }

            }
        }






    }
}