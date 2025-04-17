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
    public partial class frmTicketsOverview: Form
    {
        public frmTicketsOverview()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);
        }

        
        private void frmTicketsOverview_Load(object sender, EventArgs e)
        {
            this.ticketsTableAdapter.Fill(this.ticketsDataSet.Tickets);
            this.AutoSize = true;
        }

        private void picboxJobFair_Click(object sender, EventArgs e)
        {
            frmEvent1 formSecond = new frmEvent1(1);
            this.Hide();
            formSecond.StartPosition = FormStartPosition.Manual;
            formSecond.Location = new Point(100, 100);
            formSecond.ShowDialog();
            this.Close(); ;
        }

        private void picboxSeneca_Click(object sender, EventArgs e)
        {
            frmEvent1 formThird = new frmEvent1(2);
            this.Hide();
            formThird.StartPosition = FormStartPosition.Manual;
            formThird.Location = new Point(100, 100);
            formThird.ShowDialog();
            this.Close();
        }

        private void picboxJobTips_Click(object sender, EventArgs e)
        {
            frmEvent1 formFour = new frmEvent1(3);
            this.Hide();
            formFour.StartPosition = FormStartPosition.Manual;
            formFour.Location = new Point(100, 100);
            formFour.ShowDialog();
            this.Close();
        }

    }
}
