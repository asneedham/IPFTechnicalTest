using System.ComponentModel.DataAnnotations.Schema;

namespace IPFTechnicalTest.Models
{
    public class Beer
    {
        public int BeerId { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal PercentageAlcoholByVolume { get; set; }

        public int? BreweryId { get; set; }
        public virtual Brewery Brewery { get; set; }

        [ForeignKey("BeerId")]
        public virtual ICollection<Bar> Bars { get; set; }

    }
}
