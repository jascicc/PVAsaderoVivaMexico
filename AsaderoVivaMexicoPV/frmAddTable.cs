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
    public partial class frmAddTable : Form
    {
        DataBaseContext db = new DataBaseContext();
        Table model;

        public frmAddTable(Table model)
        {
            InitializeComponent();
            if (model != null)
            {
                this.model = model;
                tbNumber.Text = model.TableNumber.ToString();
                lblTitle.Text = "Editar mesa";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (model == null)
            {
                Table table = new Table
                {
                    TableNumber = int.Parse(tbNumber.Text)
                };
                db.Tables.Add(table);
            }
            else
            {
                model.TableNumber = int.Parse(tbNumber.Text);
                db.Entry(model).State = EntityState.Modified;
            }
            db.SaveChanges();
            tbNumber.Clear();
            model = null;
            lblTitle.Text = "Agregar mesa";

        }

        private void tbNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
