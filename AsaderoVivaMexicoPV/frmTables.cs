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
    public partial class frmTables : Form
    {
        DataBaseContext db = new DataBaseContext();

        public frmTables()
        {
            InitializeComponent();
            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
            Location = new Point();
            Responsive();
            Filldgv();
        }

        private void Responsive()
        {
            dgvTables.Width = (int)(Width * .8);
            dgvTables.Height = (int)(Height * .7);
            dgvTables.Location = new Point((int)(Width * .1), (int)(Height *.15));
            lblTitle.Location = new Point((int)(Width * .1), (int)(Height *.1));
            btnBack.Location = new Point((int)(Width * .02), (int)(Height * .05));
            btnAdd.Location = new Point((int)(Width * .9), (int)(Height * .05));
        }

        void Filldgv()
        {
            var list = db.Tables.ToList();
            dgvTables.Rows.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                dgvTables.Rows.Add(list[i].TableId, list[i].TableNumber, "Editar", "Eliminar");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Visible = false;
            frmAddTable add = new frmAddTable(null);
            add.ShowDialog();
            Filldgv();
            Visible = true;
        }

        private void dgvTables_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = int.Parse(dgvTables.CurrentRow.Cells[0].Value.ToString());
            if (e.ColumnIndex == 3)
            {
                if (MessageBox.Show($"¿Seguro que desea eliminar '{dgvTables.CurrentRow.Cells[1].Value}'?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Table model = db.Tables.Where(x => x.TableId == id).FirstOrDefault();
                    var entry = db.Entry(model);
                    if (entry.State == EntityState.Unchanged)
                    {
                        db.Tables.Remove(model);
                        db.SaveChanges();
                        Filldgv();
                    }
                }
            }

            if (e.ColumnIndex == 2)
            {
                Table model = db.Tables.Where(x => x.TableId == id).FirstOrDefault();
                Visible = false;
                frmAddTable edit = new frmAddTable(model);
                edit.ShowDialog();
                Filldgv();
                Visible = true;
            }
        }
    }
}
