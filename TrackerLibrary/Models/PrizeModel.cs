using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class PrizeModel
    {
        /// <summary>
        /// The unique identifier for the prize
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Represents the place in the tournament that this price represents
        /// </summary>
        public int PlaceNumber { get; set; }

        /// <summary>
        /// Represents the named value for this place in the tournament
        /// </summary>
        /// <example>
        /// Fifth place = "Beginner"
        /// </example>
        public string PlaceName { get; set; }

        /// <summary>
        /// Represents the amount awarded for earning this prize
        /// </summary>
        public decimal PrizeAmount { get; set; }

        /// <summary>
        /// Represents the percentage of the total prize pool awarded
        /// </summary>
        public double PrizePercentage { get; set; }

        public string PrizeInfo
        {
            get 
            {
                return $"({PlaceNumber}) - {PlaceName}";
            }
        }

        public PrizeModel()
        {

        }

        public PrizeModel(string placeName, string placeNumber, string prizeAmount, string prizePercentage)
        {
            PlaceName = placeName;

            int placeNumberValue = 0;
            int.TryParse(placeNumber, out placeNumberValue);
            PlaceNumber = placeNumberValue;

            decimal prizeAmountValue = 0;
            decimal.TryParse(prizeAmount, out prizeAmountValue);
            PrizeAmount = prizeAmountValue;

            double prizePercentageValue = 0;
            double.TryParse(prizePercentage, out prizePercentageValue);
            PrizePercentage = prizePercentageValue;
        }
    }
}
