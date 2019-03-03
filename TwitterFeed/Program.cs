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
            string twitterHandle = "";
            if (args.Length == 1)
            {
                twitterHandle = args[0];
            }
            else
            {
                Console.WriteLine("No has pasado un nombre de usuario como argumento.");
                Console.WriteLine("Uso: TwitterFeed [twitterHandle]");
                Console.WriteLine("Por ejemplo: TwitterFeed.exe VisualStudio");
                Environment.Exit(0);
            }

            Twitter twitter = new Twitter("fsxxk230EmW9lcySr9bxQ", "MtJMRChd0HTnvlQYku6sbQfAyzR1Yol0HZYxomjqiww");
            var tweetsTask = twitter.GetTweets(twitterHandle, 10);
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
