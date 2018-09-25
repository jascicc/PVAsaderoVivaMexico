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
using AsaderoVivaMexicoPV.Models;

namespace AsaderoVivaMexicoPV
{
    public partial class frmMenu : Form
    {
        DataBaseContext db = new DataBaseContext();

        public frmMenu()
        {
            InitializeComponent();
            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
            Location = new Point();
            Responsive();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            Filldgv();
        }

        void Filldgv()
        {
            var list = db.Products.Include("category").OrderBy(p=>p.CategoryId).ToList();
            dgvProducts.Rows.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                dgvProducts.Rows.Add(list[i].ProductID, list[i].ProductName, list[i].category.CategoryName, list[i].Price, "Editar", "Eliminar");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Visible = false;
            frmAddProduct frmAddProduct = new frmAddProduct(null);
            frmAddProduct.ShowDialog();
            Filldgv();
            Visible = true;
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = int.Parse(dgvProducts.CurrentRow.Cells[0].Value.ToString());
            if (e.ColumnIndex == 5)
            {
                if (MessageBox.Show($"¿Seguro que desea eliminar '{dgvProducts.CurrentRow.Cells[1].Value}'?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Product model = db.Products.Where(p => p.ProductID == id).FirstOrDefault();
                    var entry = db.Entry(model);
                    if (entry.State == EntityState.Unchanged)
                    {
                        db.Products.Remove(model);
                        db.SaveChanges();
                        Filldgv();
                    }
                }
            }

            if (e.ColumnIndex == 4)
            {
                Product model = db.Products.Where(p => p.ProductID == id).FirstOrDefault();
                Visible = false;
                frmAddProduct frmAddProduct = new frmAddProduct(model);
                frmAddProduct.ShowDialog();
                Filldgv();
                Visible = true;
            }
        }

        void Responsive()
        {
            dgvProducts.Width = (int)(Width * .8);
            dgvProducts.Height = (int)(Height * .7);
            dgvProducts.Location = new Point((int)(Width * .1), (int)(Height * .15));
            lblTitle.Location = new Point((int)(Width * .1), (int)(Height * .15 - lblTitle.Height));
            btnBack.Location = new Point(20, (int)(Height * .02));
            btnAdd.Location = new Point(Width - btnAdd.Width - 20, (int)(Height * .02));
        }
    }
}
