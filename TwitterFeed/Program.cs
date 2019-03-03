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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No has pasado un nombre de usuario como argumento.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Uso: TwitterFeed [twitterHandle]");
                Console.WriteLine("Por ejemplo: TwitterFeed.exe VisualStudio");
                Console.ResetColor();
                Environment.Exit(0);
            }

            Twitter twitter = new Twitter("fsxxk230EmW9lcySr9bxQ", "MtJMRChd0HTnvlQYku6sbQfAyzR1Yol0HZYxomjqiww");
            var tweetsTask = twitter.GetTweets(twitterHandle, 10);
            tweetsTask.Wait();
            var twitts = tweetsTask.Result;

            foreach (var t in twitts)
            {

                //https://developer.twitter.com/en/docs/tweets/timelines/api-reference/get-statuses-user_timeline.html
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(t["created_at"].ToString());
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" -> ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(t["text"].ToString());
                Console.Write("\n");
                Console.ResetColor();
            }

            Console.ReadLine();
        }
    }
}
