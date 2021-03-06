﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication2.Model.DAL
{
    public class CategoryDAL : DALBase
    {

        /// <summary>
        /// Hämtar kategorierna från databasen
        /// </summary>
        /// <returns> En lista med kategorierna (categorys)</returns>
        public IEnumerable<Category> GetVideoCategory()
        {
            // Skapar ett anslutningsobjekt.
            using (var conn = CreateConnection())
            {
                try
                {

                    // exekveras specifierad lagrad procedur.
                    var cmd = new SqlCommand("appSchema.GetCategory", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    List<Category> categorys = new List<Category>(20);

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // skapar ett SqlDataReader-objekt och returnerar en referens till objektet.
                    using (var reader = cmd.ExecuteReader())
                    {
                        // Tar reda på vilket index de olika kolumnerna har.
                        var categoryIDIndex = reader.GetOrdinal("KategoriID");
                        var CategoryVidIndex = reader.GetOrdinal("Kategori");

                        // Så länge som det finns poster att läsa returnerar Read true annars false.
                        while (reader.Read())
                        {

                            categorys.Add(new Category
                            {
                                KategoriID = reader.GetInt32(categoryIDIndex),
                                Kategori = reader.GetString(CategoryVidIndex),

                            });
                        }
                    }


                    //Avallokerar minne som inte används.
                    categorys.TrimExcess();
                    return categorys;
                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException("An error occured while getting category from the database.");
                }
            }
        }













    }
}