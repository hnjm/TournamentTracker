using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class MatchupModel
    {
        /// <summary>
        /// Represents a list of matchups
        /// </summary>
        public List<MatchupEntryModel> Entries { get; set; } = new List<MatchupEntryModel>();

        /// <summary>
        /// Represents the winner of this matchup
        /// </summary>
        public TeamModel Winner { get; set; }

        /// <summary>
        /// Represents the current round for which this matchup is taking place
        /// </summary>
        public int MatchupRound { get; set; }
    }
}
