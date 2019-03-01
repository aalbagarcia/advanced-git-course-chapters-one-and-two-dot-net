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

            foreach (string t in twitts)
            {
                Console.WriteLine(t);
            }

            Console.ReadLine();
        }
    }
}
