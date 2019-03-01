using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace TwitterFeed
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hello!!");

            Console.WriteLine("Requesting Access Token...");
            Task<string> task = GetAccessToken();
            task.Wait();
            var token = task.Result;
            Console.WriteLine("Token: " + token);

            var twittsTask = GetTweets("VisualStudio", 10, token);
            Console.WriteLine("Requesting Twitts...");
            twittsTask.Wait();
            var twitts = twittsTask.Result;

            foreach (string t in twitts)
            {
                Console.WriteLine(t);
            }

            Console.ReadLine();
        }

        static async Task<string> GetAccessToken()
        {
            string OAuthConsumerKey = "fsxxk230EmW9lcySr9bxQ";
            string OAuthConsumerSecret = "MtJMRChd0HTnvlQYku6sbQfAyzR1Yol0HZYxomjqiww";
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.twitter.com/oauth2/token");
            var customerInfo = Convert.ToBase64String(new UTF8Encoding()
                                      .GetBytes(OAuthConsumerKey + ":" + OAuthConsumerSecret));
            request.Headers.Add("Authorization", "Basic " + customerInfo);
            request.Content = new StringContent("grant_type=client_credentials",
                                                    Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage response = await httpClient.SendAsync(request);

            string json = await response.Content.ReadAsStringAsync();
            var serializer = new JavaScriptSerializer();
            dynamic item = serializer.Deserialize<object>(json);
            return item["access_token"];
        }

        static async Task<IEnumerable<string>> GetTweets(string userName, int count, string accessToken = null)
        {
            if (accessToken == null)
            {
                accessToken = await GetAccessToken();
            }

            var requestUserTimeline = new HttpRequestMessage(HttpMethod.Get,
                string.Format("https://api.twitter.com/1.1/statuses/user_timeline.json?count={0}&screen_name={1}&trim_user=1&exclude_replies=1", count, userName));

            requestUserTimeline.Headers.Add("Authorization", "Bearer " + accessToken);
            var httpClient = new HttpClient();
            HttpResponseMessage responseUserTimeLine = await httpClient.SendAsync(requestUserTimeline);
            var serializer = new JavaScriptSerializer();
            dynamic json = serializer.Deserialize<object>(await responseUserTimeLine.Content.ReadAsStringAsync());
            var enumerableTweets = (json as IEnumerable<dynamic>);

            if (enumerableTweets == null)
            {
                return null;
            }
            return enumerableTweets.Select(t => (string)(t["text"].ToString()));
        }
    }
}
