using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using УП_PR1.Forms;

namespace УП_PR1
{
    public partial class MenuForm : Form
    {
        public MenuForm(int userId)
        {
            InitializeComponent();
        }

        private void MenuForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void buttonPartnersView_Click(object sender, EventArgs e)
        {
            PartnerViewForm partnerViewForm = new PartnerViewForm();
            partnerViewForm.Show();
            this.Hide();
        }

        private void buttonAddPartner_Click(object sender, EventArgs e)
        {
            PartnerAddForm partnerAddForm = new PartnerAddForm();
            partnerAddForm.Show();
            this.Hide();
        }

        private void buttonEditPartner_Click(object sender, EventArgs e)
        {
            PartnerEditForm partnerEditForm = new PartnerEditForm();
            partnerEditForm.Show();
            this.Hide();
        }

        private void buttonHistory_Click(object sender, EventArgs e)
        {
            HistoryForm histroryForm = new HistoryForm();
            histroryForm.Show();
            this.Hide();
        }
    }
}
