using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AsaderoVivaMexicoPV.Models;

namespace AsaderoVivaMexicoPV
{
    public partial class frmMainMenu : Form
    {
        DataBaseContext db = new DataBaseContext();
        public frmMainMenu()
        {
            InitializeComponent();
        }
        
        private void btnMenu_Click(object sender, EventArgs e)
        {
            Visible = false;
            frmMenu menu = new frmMenu();
            menu.ShowDialog();
            Visible = true;
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            Visible = false;
            frmCategories categories = new frmCategories();
            categories.ShowDialog();
            Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Visible = false;
            frmTables tables = new frmTables();
            tables.ShowDialog();
            Visible = true;
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            Visible = false;
            frmAddOrder order = new frmAddOrder();
            order.ShowDialog();
            Visible = true;
        }
    }
}
