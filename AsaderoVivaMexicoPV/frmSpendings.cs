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
    public partial class frmSpendings : Form
    {
        DataBaseContext db = new DataBaseContext();
        public frmSpendings()
        {
            InitializeComponent();
            Filldgv();
        }

        void Filldgv()
        {
            var list = db.Spendings.ToList();
            dgvSpendings.Rows.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                dgvSpendings.Rows.Add(list[i].SpendingID, list[i].Description, list[i].Cost, list[i].Date, "Editar", "Eliminar");
            }
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Visible = false;
            frmAddSpending add = new frmAddSpending(null);
            add.ShowDialog();
            Filldgv();
            Visible = true;
        }

        private void dgvSpendings_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = int.Parse(dgvSpendings.CurrentRow.Cells[0].Value.ToString());
            if (e.ColumnIndex == 5)
            {
                if (MessageBox.Show($"¿Seguro que desea eliminar {dgvSpendings.CurrentRow.Cells[1].Value}?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Spending model = db.Spendings.Where(x => x.SpendingID == id).FirstOrDefault();
                    var entry = db.Entry(model);
                    if (entry.State == EntityState.Unchanged)
                    {
                        db.Spendings.Remove(model);
                        db.SaveChanges();
                        Filldgv();
                    }
                }
            }

            if (e.ColumnIndex == 4)
            {
                Spending model = db.Spendings.Where(x => x.SpendingID == id).FirstOrDefault();
                Visible = false;
                frmAddSpending edit = new frmAddSpending(model);
                edit.ShowDialog();
                Filldgv();
                Visible = true;
            }
        }
    }
}
