using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieWebApi.Models
{
    public class ImdbMovieInfo
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string YoutubeUrl { get; set; }

        public string Id { get; set; }
    }
}