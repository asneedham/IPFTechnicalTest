namespace IPFTechnicalTest.ViewModels
{
    public class BreweryViewModel
    {
        public int BreweryId { get; set; }

        public string Name { get; set; } = String.Empty;

        public List<BeerViewModel> Beers { get; set; } = new List<BeerViewModel>();
    }
}
