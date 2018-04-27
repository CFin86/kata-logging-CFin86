using System;
using System.Linq;
using System.IO;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        //public ITrackable locB;

        static void Main(string[] args)
        {
            logger.LogInfo("Log initialized");

            var lines = File.ReadAllLines(csvPath);

            if (lines.Length == 0)
            {
                logger.LogError("No lines exist in your csv file!");
            }
            else if (lines.Length == 1)
            {
                logger.LogWarning("There's only one line in your CSV file. " +
                                  "That's not normal");
            }
            else
            {
                //foreach (var line in lines)
                //{
                    logger.LogInfo($"Lines: {lines[0]}");

                var parser = new TacoParser();
                var locations = lines.Select(parser.Parse).ToArray();

                //var locations = lines.Select(parser.Parse);
                ITrackable a = null;
                ITrackable b = null;
                double dist1 = 0.0;
                foreach (var locA in locations)
                {
                    var origin = new GeoCoordinate(locA.Location.Latitude, locA.Location.Longitude);


               
                    foreach (var locB in locations)
                    {
                        var dest = new GeoCoordinate(locB.Location.Latitude, locB.Location.Longitude);
                      
                       var dist2 = origin.GetDistanceTo(dest);
                        if (!(dist2 > dist1))
                        {
                            continue;

                        }
                        a = locA;
                        b = locB;
                        dist2 = dist1;
                    }
                }
              
                Console.WriteLine($"These are the two Taco Bells furthest apart from each other: \n\t{a?.Name} and \n\t{b?.Name}");
                Console.WriteLine($"They are {dist1 / 0.000621371} miles apart.");
                Console.ReadLine();
            }
        }
    }
}