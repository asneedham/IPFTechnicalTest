namespace IPFTechnicalTest.Models
{
    public class Bar
    {
        public int BarId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public List<Beer>? Beers { get; set; }
    }
}
