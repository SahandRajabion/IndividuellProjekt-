using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Model
{
      public class Video
    {
        public int VideoID { get; set; }
        public string VidName { get; set; }
        public int KategoriID { get; set; }
        public string UserID { get; set; }
        public DateTime Datum { get; set; }
        public string Titel { get; set; }
    }
}
