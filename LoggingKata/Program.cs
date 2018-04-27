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
               // ITrackable locA = null;
                //ITrackable locB = null;
                double dist1 = 0.0;
                for (int i = 0; i < locations.Count(); i++)
                {
                    var aCoord1 = locations[i].Location.Latitude;
                    var aCoord2 = locations[i].Location.Longitude;

                   var pin1 = new GeoCoordinate(aCoord1, aCoord2);
                    for (int j = i; j < locations.Count(); j++)
                    {
                        var bCoord1 = locations[j].Location.Longitude;
                        var bCoord2 = locations[j].Location.Latitude;
                        var pin2 = new GeoCoordinate(bCoord1, bCoord2);
                       var dist2 = pin1.GetDistanceTo(pin2);
                        if (dist2 > dist1)
                        {
                            dist1 = dist2;

                        }
                        //Console.WriteLine("this is the farthest distance" + dist1);
                    }
                }
              
                Console.WriteLine("this is the farthest distance" + dist1);
            }
        }
    }
}