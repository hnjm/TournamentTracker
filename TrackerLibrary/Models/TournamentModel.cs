using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class TournamentModel
    {
        public int Id { get; set; }
        /// <summary>
        /// Represents the name of this tournament
        /// </summary>
        public string TournamentName { get; set; }

        /// <summary>
        /// Represents the entry fee for participating in this tournament
        /// </summary>
        public decimal EntryFee { get; set; }

        /// <summary>
        /// Represents the list of teams participating in this tournament
        /// </summary>
        public List<TeamModel> EnteredTeams { get; set; } = new List<TeamModel>();

        /// <summary>
        /// Represents the list of prizes for participants in this tournament
        /// </summary>
        public List<PrizeModel> Prizes { get; set; } = new List<PrizeModel>();

        /// <summary>
        /// Represents the list of rounds supported in this tournament
        /// </summary>
        public List<List<MatchupModel>> Rounds { get; set; } = new List<List<MatchupModel>>();
    }
}
