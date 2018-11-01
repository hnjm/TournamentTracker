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
    public partial class CreateTeamForm : Form
    {
        private BindingList<PersonModel> selectedTeamMembers = new BindingList<PersonModel>();
        private BindingList<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPeople();

        public CreateTeamForm()
        {
            InitializeComponent();
            UpdateBindings();
            UpdateTeamMemberCountLabel();
        }

        private void createTeamButton_Click(object sender, EventArgs e)
        {
            if (teamNameValue.Text != "" && selectedTeamMembers.Count > 1)
            {
                List<PersonModel> teamMembers = selectedTeamMembers.ToList();
                string teamName = teamNameValue.Text;

                TeamModel newTeam = new TeamModel { TeamMembers = teamMembers, TeamName = teamName };
                newTeam = GlobalConfig.Connection.CreateTeam(newTeam);
                ResetFormToDefaults();
                UpdateTeamMemberCountLabel();
                MessageBox.Show("Team successfully created!");
            }
            else
            {
                MessageBox.Show("Must enter a valid team name and select two or more players!");
            }
        }

        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if(ValidateForm())
            {
                PersonModel p = new PersonModel();
                p.FirstName = firstNameValue.Text;
                p.LastName = lastNameValue.Text;
                p.EmailAddress = emailValue.Text;
                p.CellphoneNumber = cellPhoneValue.Text;

                p = GlobalConfig.Connection.CreatePerson(p);
                availableTeamMembers.Add(p);
                UpdateTeamMemberCountLabel();

                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                cellPhoneValue.Text = "";
            }
            else
            {
                MessageBox.Show("Invalid form information has been entered!");
            }
        }

        private void teamMembersListBox_DataSourceChanged(object sender, EventArgs e)
        {

        }

        private void addMemberButton_Click(object sender, EventArgs e)
        {
            if(selectedTeamMembers.Count < 5 && selectTeamMemberDropdown.SelectedItem != null)
            {
                var player = (PersonModel)selectTeamMemberDropdown.SelectedItem;
                selectedTeamMembers.Add(player);
                availableTeamMembers.Remove(player);
                UpdateTeamMemberCountLabel();
            }
        }

        private void deleteSelectedMemberButton_Click(object sender, EventArgs e)
        {
            if(teamMembersListBox.SelectedItem != null)
            {
                var player = (PersonModel)teamMembersListBox.SelectedItem;
                availableTeamMembers.Add(player);
                selectedTeamMembers.Remove(player);
                UpdateTeamMemberCountLabel();
            }
        }

        private void ResetFormToDefaults()
        {
            selectedTeamMembers = new BindingList<PersonModel>();
            availableTeamMembers = GlobalConfig.Connection.GetPeople();
            teamNameValue.Text = "";
            UpdateBindings();
        }

        private void UpdateBindings()
        {
            selectTeamMemberDropdown.DataSource = availableTeamMembers;
            selectTeamMemberDropdown.DisplayMember = "FirstAndLast";
            teamMembersListBox.DataSource = selectedTeamMembers;
            teamMembersListBox.DisplayMember = "FirstAndLast";
        }

        private void UpdateTeamMemberCountLabel()
        {
            availablePlayersCountLabelValue.Text = availableTeamMembers.Count.ToString();
            availablePlayersCountLabelValue.ForeColor = availableTeamMembers.Count > 3 ? Color.LimeGreen : Color.Red;
            currentTeamMembersCountLabel.Text = selectedTeamMembers.Count.ToString();
            currentTeamMembersCountLabel.ForeColor = selectedTeamMembers.Count == 5 ? Color.Green : Color.Red;
        }

        private bool ValidateForm()
        {
            bool isValid = true;

            if (firstNameValue.Text == "")
            {
                isValid = false;
            }

            if (lastNameValue.Text == "")
            {
                isValid = false;
            }

            if (emailValue.Text != "")
            {
                string emailLast4 = emailValue.Text.Substring(emailValue.Text.Length - 4);

                if (emailValue.Text.IndexOf('@') < 1 || emailLast4 != ".com")
                {
                    isValid = false;
                }
            }
            else
            {
                isValid = false;
            }

            if (cellPhoneValue.Text == "")
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
