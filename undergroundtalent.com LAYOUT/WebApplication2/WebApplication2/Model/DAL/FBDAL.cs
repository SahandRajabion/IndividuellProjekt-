using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication2.Model.DAL
{
    public class FBDAL : DALBase
    {

        /// <summary>
        /// Metod för insert av nya användare, inloggade via facebook på sajten.
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="userid"></param>
        /// <param name="name"></param>
         public void InsertFaceBook(string access_token, string userid, string name)
        {
            // Skapar och initierar ett anslutningsobjekt
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att exekvera specifierad lagrad procedur
                    SqlCommand cmd = new SqlCommand("appSchema.InsertUserData", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till de parametrar den lagrade proceduren kräver
                    cmd.Parameters.Add("@access_token", SqlDbType.VarChar, 255).Value = access_token;
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = name;
                    cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 100).Value = userid;


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
        /// Metod för inhämtning av användar data.
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>userid</returns>
        public string getuserdata(string userid)
        {
            //Skapar en anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    // Skapar ett SqlCommand-objekt för att exekvera specifierad lagrad procedur.
                    SqlCommand cmd = new SqlCommand("appSchema.GetUserData", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Lägger till den paramter den lagrade proceduren kräver.
                    cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 100).Value = userid;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Skapar ett SqlDataReader-objekt och returnerar en referens till objektet.
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Tar reda på vilket index de olika kolumnerna har.
                            var useridIndex = reader.GetOrdinal("UserID");


                            return userid = reader.GetString(useridIndex);
                        }
                        return null;
                    }

                }


                catch
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }

        }


    }
    }
