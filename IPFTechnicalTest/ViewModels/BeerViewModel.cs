using IPFTechnicalTest.Models;

namespace IPFTechnicalTest.ViewModels
{
    public class BeerViewModel
    {
        public int BeerId { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal PercentageAlcoholByVolume { get; set; }

        public int BreweryId { get; set; }
    }
}
