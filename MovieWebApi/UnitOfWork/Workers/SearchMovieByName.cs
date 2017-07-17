using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using MovieWebApi.Models;
using Newtonsoft.Json.Linq;

namespace MovieWebApi.UnitOfWork.Workers
{
    public class SearchMovieByName : IUnitOfWork, IDisposable
    {
        private readonly string _movieName;

        private static readonly string imdbbaseUrl = "https://moviesapi.com/";

        public SearchMovieByName(string movieName)
        {
            _movieName = movieName;
        }
        /// <summary>
        /// Job executer
        /// </summary>
        /// <returns></returns>
        public UnitOfWorkResult Execute()
        {
            if (string.IsNullOrEmpty(_movieName))
            {
                return new UnitOfWorkResult(new Exception("Movie name is empty"), UnitOfWorkStatus.Invalid);
            }
            // constructing query string
            var imdbParameters = "m.php?type=movie&t=" + _movieName;
            var imdbresult = GetHttpClientResult(imdbbaseUrl, imdbParameters).Result;
            var jsonResults = JArray.Parse(imdbresult);
            var movieInfoList = (from result in jsonResults
                                 let movieTitle = result["title"].ToString()
                                 select new ImdbMovieInfo()
                                 {
                                     Title = movieTitle,
                                     Year = result["year"].ToString(),
                                     YoutubeUrl = Search(movieTitle),
                                     Id = result["id"].ToString()
                                 }).ToList();
            return jsonResults.Count > 0 ? new UnitOfWorkResult(movieInfoList, UnitOfWorkStatus.Ok) : new UnitOfWorkResult("Not found", UnitOfWorkStatus.Ok);
        }

        public async Task<string> GetHttpClientResult(string baseUrl, string parameter)
        {
            var result = "";
            try
            {
                // initiate the httpclient
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(parameter).Result;

                if (response.IsSuccessStatusCode)
                {
                    // read out response stream
                    result = await response.Content.ReadAsStringAsync();

                }
            }
            catch (Exception e)
            {
                throw new HttpException(e.Message);
            }
            return result;
        }

        private string Search(string searchItem)
        {
            // calling youtube Api v3 for getting videos
            // since we only want the url to the related videos found from movieApi, 
            // one per each is enough
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyA3Sq3FU4yT1KK1vysJfyno2fSGfxTPHtY",
                ApplicationName = "test"
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = searchItem;
            // google api only allow 50 results as max for free users
            searchListRequest.MaxResults = 50;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = searchListRequest.ExecuteAsync().Result;

            var firstMovie = searchListResponse.Items.FirstOrDefault();
            if (firstMovie != null)
            {
                var videoUrl = firstMovie.Id.VideoId;
                return videoUrl;
            }
            return "";
        }
        public void Dispose()
        {
            GC.Collect();

        }
    }
}