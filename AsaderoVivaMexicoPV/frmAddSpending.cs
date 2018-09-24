using AsaderoVivaMexicoPV.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsaderoVivaMexicoPV
{
    public partial class frmAddSpending : Form
    {
        DataBaseContext db = new DataBaseContext();
        Spending model;
        public frmAddSpending(Spending model)
        {
            InitializeComponent();
            if (model != null)
            {
                this.model = model;
                tbCost.Text = model.Cost.ToString();
                tbDescription.Text = model.Description;
                dtpDate.Value = model.Date;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbCost.Text) && !String.IsNullOrWhiteSpace(tbCost.Text)
                && !String.IsNullOrEmpty(tbDescription.Text) && !String.IsNullOrWhiteSpace(tbDescription.Text))
            {
                model.Description = tbDescription.Text;
                model.Cost = float.Parse(tbCost.Text);
                model.Date = dtpDate.Value;

                if (model.SpendingID == 0)
                {
                    db.Spendings.Add(model);
                }
                else
                {
                    db.Entry(model).State = EntityState.Modified;
                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tbCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.')
            {
                if (!tbCost.Text.Contains('.'))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }
    }
}
