using System;

namespace LoggingKata
{
    /* <summary>
    Parses a POI file to locate all the TacoBells
    </summary> */
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();

        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            if (string.IsNullOrEmpty(line))
            {
                logger.LogError("Your string is empty, or had no value!");
                return null;
            }

            var cells = line.Split(',');
            if (cells.Length < 2)
            {
                logger.LogError("Hey, looks like your input doesn't have the coorect amount of columns");
                return null;
            }

            try
            {
                var lon = double.Parse(cells[0]);
                var lat = double.Parse(cells[1]);
                if (lat > 90 || lat < -90 || lon < -180 || lon > 180)
                {
                    logger.LogError("This number is out of the range of lat/lon");
                    return null;
                }

                var location = new Point { Latitude = lat, Longitude = lon };
                var name = cells.Length > 2 ? cells[2] : null;
                return new TacoBell(name, location);
            }
            catch (Exception ex)
            {
                logger.LogError("Failure", ex);
                return null;
            }
        }
    }
}
