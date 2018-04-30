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
                var parser = new TacoParser();
                var locations = lines.Select(parser.Parse).ToArray();
                ITrackable a = null;
                ITrackable b = null;
                double dist1 = 0;

                foreach (var locA in locations)
                {
                    var origin = new GeoCoordinate(locA.Location.Longitude, locA.Location.Latitude);
                    foreach (var locB in locations)
                    {
                        var dest = new GeoCoordinate(locB.Location.Longitude, locB.Location.Latitude);
                        var dist2 = origin.GetDistanceTo(dest);
                        if (!(dist2 > dist1))
                        {
                            continue;
                        }
                        a = locA;
                        b = locB;
                        dist1 = dist2;
                    }
                }
                Console.WriteLine($"These are the two Taco Bells furthest apart from each other: \n\t{a?.Name} and \n\t{b?.Name}");
                Console.WriteLine($"They are {dist1 / 0.000621371} meters apart.");
                Console.ReadLine();
            }
        }
    }
}
