namespace IPFTechnicalTest.Models
{
    public class Brewery
    {
        public int BreweryId { get; set; }

        public string Name { get; set; } = String.Empty;
    
        public List<Beer> Beers { get; set; } = new List<Beer>();
    }
}
