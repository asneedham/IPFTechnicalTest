using IPFTechnicalTest.Models;

namespace IPFTechnicalTest.ViewModels
{
    public class BarViewModel
    {
        public int BarId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public List<BeerViewModel> Beers { get; set; } = new List<BeerViewModel>();
    }
}
