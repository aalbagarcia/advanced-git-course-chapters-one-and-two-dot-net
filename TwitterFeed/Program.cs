using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterFeed
{
    class Program
    {
        static void Main(string[] args)
        {


            Twitter twitter = new Twitter("fsxxk230EmW9lcySr9bxQ", "MtJMRChd0HTnvlQYku6sbQfAyzR1Yol0HZYxomjqiww");
            var tweetsTask = twitter.GetTweets("VisualStudio", 10);
            tweetsTask.Wait();
            var twitts = tweetsTask.Result;

            foreach (var t in twitts)
            {
                //https://developer.twitter.com/en/docs/tweets/timelines/api-reference/get-statuses-user_timeline.html
                string output = string.Format("{0} -> {1}", t["created_at"].ToString(), t["text"].ToString());
                Console.WriteLine(output);
            }

            Console.ReadLine();
        }
    }
}
