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
    public partial class frmCategories : Form
    {
        DataBaseContext db = new DataBaseContext();

        public frmCategories()
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
            dgvCategories.Width = (int)(Width * .8);
            dgvCategories.Height = (int)(Height * .7);
            dgvCategories.Location = new Point((int)(Width * .1), (int)(Height * .15));
            lblTitle.Location = new Point((int)(Width * .1), (int)(Height * .1));
            btnBack.Location = new Point((int)(Width * .02), (int)(Height * .05));
            btnAdd.Location = new Point((int)(Width * .9), (int)(Height * .05));
        }

        void Filldgv()
        {
            var list = db.Categories.ToList();
            dgvCategories.Rows.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                dgvCategories.Rows.Add(list[i].CategoryId, list[i].CategoryName, "Editar", "Eliminar");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Visible = false;
            frmAddCategory add = new frmAddCategory(null);
            add.ShowDialog();
            Filldgv();
            Visible = true;

        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = int.Parse(dgvCategories.CurrentRow.Cells[0].Value.ToString());
            if (e.ColumnIndex == 3)
            {
                if (MessageBox.Show($"¿Seguro que desea eliminar {dgvCategories.CurrentRow.Cells[1].Value}?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Category model = db.Categories.Where(x => x.CategoryId == id).FirstOrDefault();
                    var entry = db.Entry(model);
                    if (entry.State == EntityState.Unchanged)
                    {
                        db.Categories.Remove(model);
                        db.SaveChanges();
                        Filldgv();
                    }
                }
            }

            if (e.ColumnIndex == 2)
            {
                Category model = db.Categories.Where(c => c.CategoryId == id).FirstOrDefault();
                Visible = false;
                frmAddCategory edit = new frmAddCategory(model);
                edit.ShowDialog();
                Filldgv();
                Visible = true;
            }
        }
    }
}
