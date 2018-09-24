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
    public partial class frmAddCategory : Form
    {
        DataBaseContext db = new DataBaseContext();
        Category model = new Category();
        List<string> categories = new List<string>();
        public frmAddCategory(Category model)
        {
            InitializeComponent();
            var categories = db.Categories.ToList();
            foreach (var item in categories)
            {
                this.categories.Add(item.CategoryName);
            }
            if (model != null)
            {
                this.model = model;
                tbName.Text = model.CategoryName;
                lblTitle.Text = "Editar categoría";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        void Save()
        {
            bool save = false;
            if (!String.IsNullOrEmpty(tbName.Text) && !String.IsNullOrWhiteSpace(tbName.Text))
            {
                if (model.CategoryId != 0)
                {
                    model.CategoryName = tbName.Text;
                    db.Entry(model).State = EntityState.Modified;
                    save = true;
                }
                else
                {
                    if (!categories.Contains(tbName.Text))
                    {
                        model.CategoryName = tbName.Text;
                        db.Categories.Add(model);
                        save = true;
                    }
                    else
                    {
                        MessageBox.Show("Esa categoria ya se encuentra agregada");
                    }
                }
                if (save)
                {
                    try
                    {
                        db.SaveChanges();
                        tbName.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Save();
            }
        }
    }
}
