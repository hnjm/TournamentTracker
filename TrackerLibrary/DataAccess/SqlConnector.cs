using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        private const string db = "Tournaments";

        public TournamentModel CreateTournament(TournamentModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                SaveTournament(model, connection);
                SavePrizes(model, connection);
                SaveTeams(model, connection);

                return model;
            }
        }
        
        private void SaveTournament(TournamentModel model, IDbConnection connection)
        {
            var p = new DynamicParameters();
            p.Add("@TournamentName", model.TournamentName);
            p.Add("@EntryFee", model.EntryFee);
            p.Add("@id", 0, DbType.Int32, direction: ParameterDirection.Output);

            connection.Execute("dbo.spPeople_Insert", p, commandType: CommandType.StoredProcedure);

            model.Id = p.Get<int>("@id");
        }

        private void SavePrizes(TournamentModel model, IDbConnection connection)
        {
            foreach (PrizeModel prize in model.Prizes)
            {
                var p = new DynamicParameters();
                p.Add("@TournamentId", model.Id);
                p.Add("@PrizeId", prize.Id);

                connection.Execute("dbo.spTournamentPrizes_Insert", p, commandType: CommandType.StoredProcedure);
            }
        }

        private void SaveTeams(TournamentModel model, IDbConnection connection)
        {
            foreach (TeamModel team in model.EnteredTeams)
            {
                var p = new DynamicParameters();
                p.Add("@TournamentId", model.Id);
                p.Add("@TeamId", team.Id);

                connection.Execute("dbo.spTournamentEntries_Insert", p, commandType: CommandType.StoredProcedure);
            }
        }

        public PersonModel CreatePerson(PersonModel model)
        {
            using(IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@FirstName", model.FirstName);
                p.Add("@LastName", model.LastName);
                p.Add("@EmailAddress", model.EmailAddress);
                p.Add("@CellphoneNumber", model.CellphoneNumber);
                p.Add("@id", 0, DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPeople_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");

                return model;
            }
        }

        // TODO - Make the CreatePrize method actually save to the database
        /// <summary>
        /// Save a new prize to the database
        /// </summary>
        /// <param name="model">The prize information</param>
        /// <returns>The prize information, with the database assigned unique identifier</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@PlaceNumber", model.PlaceNumber);
                p.Add("@PlaceName", model.PlaceName);
                p.Add("@PrizeAmount", model.PrizeAmount);
                p.Add("@PrizePercentage", model.PrizePercentage);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPrizes_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");

                return model;
            }
        }

        public TeamModel CreateTeam(TeamModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@TeamName", model.TeamName);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spTeams_Insert2", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");

                foreach(PersonModel member in model.TeamMembers)
                {
                    var p2 = new DynamicParameters();
                    p2.Add("@TeamId", model.Id);
                    p2.Add("@PersonId", member.Id);
                    connection.Execute("dbo.spTeamMembers_Insert2", p2, commandType: CommandType.StoredProcedure);
                }

                return model;
            }
        }

        public BindingList<PersonModel> GetPeople()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var output = connection.Query<PersonModel>("dbo.spPeople_GetAll").ToList();

                return new BindingList<PersonModel>(output);
            }
        }

        public BindingList<PrizeModel> GetPrizes_All()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var output = connection.Query<PrizeModel>("dbo.spPrizes_GetAll2").ToList();

                return new BindingList<PrizeModel>(output);
            }
        }

        public BindingList<PersonModel> GetTeamMembers()
        {
            throw new NotImplementedException();
        }

        public BindingList<TeamModel> GetTeams_All()
        {
            using(IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var output = connection.Query<TeamModel>("dbo.spTeam_GetAll2").ToList();

                foreach(TeamModel team in output)
                {
                    var p = new DynamicParameters();
                    p.Add("@TeamId", team.Id);
                    team.TeamMembers = connection.Query<PersonModel>("dbo.spTeamMembers_GetByTeam2", p, commandType: CommandType.StoredProcedure).ToList();
                }

                return new BindingList<TeamModel>(output);
            }
        }
    }
}
