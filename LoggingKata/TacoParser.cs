namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the TacoBells
    /// </summary>
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
            if (cells.Length < 3)
            {
                logger.LogError("Hey, looks like your input doesn't have the coorect amount of columns");
                return null;
            } 
            var lon = double.Parse(cells[0]);
            var lat = double.Parse(cells[1]);
            var name = cells[2];
            var location = new Point(lon, lat);
            return new TacoBell(name, location);
            //DO not fail if one record parsing fails, return null
            //TODO Implement
            //return null;
        }
    }
}