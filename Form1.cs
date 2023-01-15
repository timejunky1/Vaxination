using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mathee_Steophanus_PRG282_ST
{
    public partial class VaxinationForm : Form
    {
        DataHandler dh = new DataHandler();
        public VaxinationForm()
        {
            InitializeComponent();
        }

        private void VaxinationForm_Load(object sender, EventArgs e)
        {
            dh.GetData();
            dgvData.DataSource = dh.bs;
            ClearData();
        }

        public void ClearData()
        {
            txtFirstName.Clear();
            txtStudentNumber.Clear();
            txtLastname.Clear();
            rbFemale.Checked = false;
            rbMale.Checked = false;
            rbJJ.Checked = false;
            rbViser.Checked = false;
            rbNoV.Checked = false;
            rbNoC.Checked = false;
            rbYesC.Checked = false;
        }

        public bool CheckData()
        {
            if(txtFirstName.Text != "" && txtLastname.Text!="" && txtStudentNumber.Text != "" && (rbFemale.Checked || rbMale.Checked) && (rbNoV.Checked || rbViser.Checked || rbJJ.Checked) && (rbNoC.Checked || rbYesC.Checked))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int stNumber = 0;
            string Fname = "*", Lname = "*";
            DateTime date = DateTime.Now.AddMonths(2000);
            MessageBox.Show(date.ToString());
            if (txtStudentNumber.Text != "")
            {
                try
                {
                    stNumber = int.Parse(txtStudentNumber.Text);
                }
                catch
                {

                    MessageBox.Show("That is not a Student 'Number'");
                    return;
                }
            }
            else if(txtFirstName.Text != "")
            {
                Fname = txtFirstName.Text;
            }
            else if (txtLastname.Text != "")
            {
                Lname = txtLastname.Text;
            }
            else
            {
                date = dtpBirthdate.Value;
            }
            dh.GetStudent(stNumber, txtFirstName.Text, txtLastname.Text, date);
            dgvData.DataSource = dh.bs;
            ClearData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string gender, vaxeneStatus, exCovidStatus;
            int stNumber;
            if (CheckData())
            {
                try
                {
                    stNumber = int.Parse(txtStudentNumber.Text);
                }
                catch
                {

                    MessageBox.Show("That is not a Student 'Number'");
                    return;
                }


                if (rbMale.Checked)
                {
                    gender = "Male";
                }
                else
                {
                    gender = "Female";
                }

                if (rbViser.Checked)
                {
                    vaxeneStatus = "Viser";
                }
                else if (rbJJ.Checked)
                {
                    vaxeneStatus = "JJ";
                }
                else
                {
                    vaxeneStatus = "No";
                }

                if (rbNoC.Checked)
                {
                    exCovidStatus = "No";
                }
                else
                {
                    exCovidStatus = "Yes";
                }

                dh.AddStudent(stNumber, txtFirstName.Text, txtLastname.Text, gender, dtpBirthdate.Value, vaxeneStatus, exCovidStatus);
            }
            else
            {
                MessageBox.Show("Not All required fields are entered");
            }
            VaxinationForm_Load(sender,e);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string gender, vaxeneStatus, exCovidStatus;
            int stNumber;
            if (CheckData())
            {
                try
                {
                    stNumber = int.Parse(txtStudentNumber.Text);
                }
                catch
                {

                    MessageBox.Show("That is not a Student 'Number'");
                    return;
                }


                if (rbMale.Checked)
                {
                    gender = "Male";
                }
                else
                {
                    gender = "Female";
                }

                if (rbViser.Checked)
                {
                    vaxeneStatus = "Viser";
                }
                else if (rbJJ.Checked)
                {
                    vaxeneStatus = "JJ";
                }
                else
                {
                    vaxeneStatus = "No";
                }

                if (rbNoC.Checked)
                {
                    exCovidStatus = "No";
                }
                else
                {
                    exCovidStatus = "Yes";
                }

                dh.UpdateStudent(stNumber, txtFirstName.Text, txtLastname.Text, gender, dtpBirthdate.Value, vaxeneStatus, exCovidStatus);
            }
            else
            {
                MessageBox.Show("Not All required fields are entered");
            }
            VaxinationForm_Load(sender, e);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int stNumber;
            try
            {
                stNumber = int.Parse(txtStudentNumber.Text);
            }
            catch
            {

                MessageBox.Show("That is not a Student 'Number'");
                return;
            }
            dh.DeleteStudent(stNumber);
            VaxinationForm_Load(sender, e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            DbDataRecord dr = dh.bs.Current as DbDataRecord;
            if(dr != null)
            {
                txtStudentNumber.Text = dr[0].ToString();
                txtFirstName.Text = dr[1].ToString();
                txtLastname.Text = dr[2].ToString();
                if(dr[3].ToString() == "Male")
                {
                    rbMale.Checked = true;
                    rbFemale.Checked = false;
                }
                else
                {
                    rbMale.Checked = false;
                    rbFemale.Checked = true;
                }

                dtpBirthdate.Text = dr[4].ToString();

                if (dr[5].ToString() == "Viser")
                {
                    rbViser.Checked = true;
                    rbJJ.Checked = false;
                    rbNoV.Checked = false;
                }
                else if (dr[5].ToString() == "JJ")
                {
                    rbViser.Checked = false;
                    rbJJ.Checked = true;
                    rbNoV.Checked = false;
                }
                else
                {
                    rbViser.Checked = false;
                    rbJJ.Checked = false;
                    rbNoV.Checked = true;
                }

                if (dr[6].ToString() == "Yes")
                {
                    rbYesC.Checked = true;
                    rbNoC.Checked = false;
                }
                else
                {
                    rbYesC.Checked = false;
                    rbNoC.Checked = true;
                }
            }
        }
    }
}
