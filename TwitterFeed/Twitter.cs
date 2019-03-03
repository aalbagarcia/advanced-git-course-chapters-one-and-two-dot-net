using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Net.Http;

namespace TwitterFeed
{
    class Twitter
    {
        private string OAuthConsumerKey;
        private string OAuthConsumerSecret;

        public Twitter(string key, string secret)
        {
            OAuthConsumerKey = key;
            OAuthConsumerSecret = secret;
        }


        private async Task<string> GetAccessToken()
        {
            Console.WriteLine("Requesting Access Token...");
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
            Console.WriteLine(item["access_token"]);
            return item["access_token"];
        }

        public async Task<IEnumerable<dynamic>> GetTweets(string userName, int count, string accessToken = null)
        {
            if (accessToken == null)
            {
                accessToken = await GetAccessToken();
            }
            Console.WriteLine("Requesting Twitts...");
        
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
            return enumerableTweets;
        }
    }
}
