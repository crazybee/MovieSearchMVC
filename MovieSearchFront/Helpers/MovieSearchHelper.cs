using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MovieSearchFront.Models;
using System.Net.Http.Headers;

namespace MovieSearchFront.Helpers
{
    public class MovieSearchHelper : IDisposable
    {
        public string MovieName;
        private readonly string baseuri = "http://localhost:40483/api/movie/searchbyname?moviename=";
        public MovieSearchHelper(string movieName) {
            MovieName = movieName;
        }

        public async Task<List<MovieModel>> GetMoviesAsync()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync(baseuri + MovieName);
            var model = await response.Content.ReadAsAsync<List<MovieModel>>();
            return model;

        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}