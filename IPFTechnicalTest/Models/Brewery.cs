namespace IPFTechnicalTest.Models
{
    public class Brewery
    {
        public int BreweryId { get; set; }

        public string Name { get; set; } = String.Empty;

        public virtual ICollection<Beer>? Beers { get; set; }
    }
}
