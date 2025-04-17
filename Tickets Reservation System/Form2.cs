using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Tickets_Reservation_System
{
    public partial class frmEvent1 : Form
    {
        private int selectedEventID;
        public frmEvent1(int eventId)
        {
            InitializeComponent();
            selectedEventID = eventId;
            this.ticketsTableAdapter.Fill(this.ticketsDataSet.Tickets);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);
        }

        private void frmEvent1_Load(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmTicketsOverview formBack = new frmTicketsOverview();
            formBack.StartPosition = FormStartPosition.Manual;
            formBack.Location = new Point(100, 100);
            formBack.ShowDialog();
            this.Close();
        }

        private void btnPrcdTransctn_Click(object sender, EventArgs e)
        {
            ClientInfo user = new ClientInfo();
           
            if (string.IsNullOrEmpty(txtFirstName.Text))
            {
                MessageBox.Show("Please enter your First Name.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (!txtFirstName.Text.All(char.IsLetter))
                {
                    MessageBox.Show("Please enter only Letters for First Name.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                user.firstName = txtFirstName.Text;
            }

            if (string.IsNullOrEmpty(txtLastName.Text))
            {
                MessageBox.Show("Please enter your Last Name.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (!txtLastName.Text.All(char.IsLetter))
                {
                    MessageBox.Show("Please enter only Letters for Last Name.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                user.lastName = txtLastName.Text;
            }

            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("Please enter your Address.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                user.address = txtAddress.Text;
            }

            if (string.IsNullOrEmpty(txtTelephoneNo.Text))
            {
                MessageBox.Show("Please enter your Telephone No.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (!txtTelephoneNo.Text.All(char.IsDigit) || txtTelephoneNo.Text.Length != 10)
                {
                    MessageBox.Show("Please enter your valid 10 Digits Telephone Number.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                user.telephoneNo = txtTelephoneNo.Text;
            }

            if (string.IsNullOrEmpty(txtTotalSeats.Text))
            {
                MessageBox.Show("Please enter the total number of seats you want to buy for this event.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (!txtTotalSeats.Text.All(char.IsDigit))
                {
                    MessageBox.Show("Please enter the valid number of seats you want to book for this event.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int seats = int.Parse(txtTotalSeats.Text);

                if (seats <= 0)
                {
                    MessageBox.Show("Please enter the correct number for seats.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                user.totalSeats = seats;              
            }

            if (string.IsNullOrEmpty(txtCreditCardNo.Text))
            {
                MessageBox.Show("Please enter the credit card number.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (!txtCreditCardNo.Text.All(char.IsDigit) || txtCreditCardNo.Text.Length != 16)
                {
                    MessageBox.Show("Please enter the valid 16 digits credit number.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                user.creditCardNo = txtCreditCardNo.Text;
            }

            int noOfSeatsSold = int.Parse(ticketsDataGridView.Rows[selectedEventID - 1].Cells[6].Value.ToString());
            int noOfAvaSeats = int.Parse(ticketsDataGridView.Rows[selectedEventID - 1].Cells[5].Value.ToString());
            decimal priceOfTicket = decimal.Parse(ticketsDataGridView.Rows[selectedEventID - 1].Cells[4].Value.ToString());

            if (user.totalSeats > noOfAvaSeats)
            {
                MessageBox.Show("Sorry, the entered no of seats are not available at this moment.\nPlease choose less seats or check back later.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                noOfAvaSeats -= user.totalSeats;

                ticketsDataGridView.Rows[selectedEventID - 1].Cells[5].Value = noOfAvaSeats;
                ticketsDataGridView.Rows[selectedEventID - 1].Cells[6].Value = noOfSeatsSold + user.totalSeats;

                this.Validate();
                this.ticketsBindingSource.EndEdit();
                this.ticketsTableAdapter.Update(this.ticketsDataSet.Tickets);

                this.ticketsTableAdapter.Fill(this.ticketsDataSet.Tickets);
            }



            DialogResult result = MessageBox.Show($"Full Name: {user.firstName} {user.lastName}\nEvent ID: {selectedEventID}\nTotal Cost: ${user.totalSeats * priceOfTicket}", "Booking Confirmation!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
            
        }
    }
}
