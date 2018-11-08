using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public interface IDataConnection
    {
        PrizeModel CreatePrize(PrizeModel model);
        PersonModel CreatePerson(PersonModel model);
        BindingList<PersonModel> GetPeople();
        TeamModel CreateTeam(TeamModel model);
        BindingList<TeamModel> GetTeams_All();
        BindingList<PersonModel> GetTeamMembers();
        BindingList<PrizeModel> GetPrizes_All();
        TournamentModel CreateTournament(TournamentModel model);
    }
}
