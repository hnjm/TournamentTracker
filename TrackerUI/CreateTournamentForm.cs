using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.DataAccess;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTournamentForm : Form, IPrizeRequestor, ITeamRequestor
    {
        private BindingList<TeamModel> selectedTeams = new BindingList<TeamModel>();
        private BindingList<TeamModel> teamsList = GlobalConfig.Connection.GetTeams_All();
        private BindingList<PrizeModel> prizeList = new BindingList<PrizeModel>();

        public CreateTournamentForm()
        {
            InitializeComponent();
            UpdateBindings();
        }

        private void selectTeamDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void UpdateBindings()
        {
            selectTeamDropdown.DataSource = teamsList;
            selectTeamDropdown.DisplayMember = "TeamName";

            tournamentPlayersListbox.DataSource = selectedTeams;
            tournamentPlayersListbox.DisplayMember = "TeamName";

            prizesListbox.DataSource = prizeList;
            prizesListbox.DisplayMember = "PrizeInfo";
        }

        private void addTeamButton_Click(object sender, EventArgs e)
        {
            if(selectTeamDropdown.SelectedItem != null)
            {
                TeamModel selectedTeam = (TeamModel) selectTeamDropdown.SelectedItem;
                selectedTeams.Add(selectedTeam);
                teamsList.Remove(selectedTeam);
            }
        }

        private void deleteSelectedPlayerButton_Click(object sender, EventArgs e)
        {
            if(tournamentPlayersListbox.SelectedItem != null)
            {
                TeamModel selectedTeam = (TeamModel)tournamentPlayersListbox.SelectedItem;
                selectedTeams.Remove(selectedTeam);
                teamsList.Add(selectedTeam);
            }
        }

        private void deleteSelectedPrizeButton_Click(object sender, EventArgs e)
        {
            PrizeModel selectedPrize = (PrizeModel) prizesListbox.SelectedItem;
            prizeList.Remove(selectedPrize);
        }

        private void createNewTeamLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateTeamForm form = new CreateTeamForm(this);
            form.Show();
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            CreatePrizeForm form = new CreatePrizeForm(this);
            form.Show();
        }

        public void PrizeComplete(PrizeModel model)
        {
            prizeList.Add(model);
        }

        public void TeamComplete(TeamModel model)
        {
            selectedTeams.Add(model);
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            if(ValidateForm())
            {
                decimal entryFee = 0;
                bool validFee = decimal.TryParse(entryFeeValue.Text, out entryFee);

                if(!validFee)
                {
                    MessageBox.Show("Please enter a valid entry fee.", "Invalid Fee", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                TournamentModel model = new TournamentModel();
                model.TournamentName = tournamentNameValue.Text;
                model.EntryFee = entryFee;
                model.Prizes = prizeList.ToList();
                model.EnteredTeams = selectedTeams.ToList();

                GlobalConfig.Connection.CreateTournament(model);
                
            }
            else
            {
                MessageBox.Show("The form contains invalid information!");
            }
        }

        private bool ValidateForm()
        {
            bool isValid = true;

            if(string.IsNullOrEmpty(tournamentNameValue.Text.Trim()))
            {
                isValid = false;
            }

            if(tournamentPlayersListbox.Items.Count < 2)
            {
                isValid = false;
            }

            if(prizesListbox.Items.Count < 1)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
