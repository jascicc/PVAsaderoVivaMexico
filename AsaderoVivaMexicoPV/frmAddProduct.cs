using AsaderoVivaMexicoPV.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsaderoVivaMexicoPV
{
    public partial class frmAddProduct : Form
    {
        DataBaseContext db = new DataBaseContext();
        Product model = new Product();

        public frmAddProduct(Product model)
        {
            InitializeComponent();
            if (model != null)
            {
                this.model = model;
                lblTitle.Text = "Editar producto";
                tbName.Text = model.ProductName;
                tbPrice.Text = model.Price.ToString();
                if (model.Image != null)
                {
                    MemoryStream ms = new MemoryStream(model.Image);
                    pbImage.Image = Image.FromStream(ms);
                }
            }
            FillCb();
        }

        void FillCb()
        {
            var list = db.Categories.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                cbCategory.Items.Add(list[i].CategoryName);
            }
            if (model.ProductID != 0)
            {
                cbCategory.SelectedIndex = list.IndexOf(list.Where(c => c.CategoryId == model.CategoryId).FirstOrDefault());
            }
            else
            {
                cbCategory.SelectedIndex = 0;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pbImage_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image files|*.jpg;*.jpeg;*.png;";
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                pbImage.Image = Image.FromFile(openFileDialog1.FileName);

                var ms = new MemoryStream();
                pbImage.Image.Save(ms, ImageFormat.Jpeg);
                byte[] photo_aray = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(photo_aray, 0, photo_aray.Length);
                model.Image = photo_aray;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbName.Text) && !String.IsNullOrWhiteSpace(tbName.Text)
                && !String.IsNullOrEmpty(tbPrice.Text) && !String.IsNullOrWhiteSpace(tbPrice.Text))
            {
                model.ProductName = tbName.Text;
                model.Price = int.Parse(tbPrice.Text);
                if (model.ProductID == 0)
                {
                    db.Products.Add(model);
                }
                else
                {
                    db.Entry(model).State = EntityState.Modified;
                }
                try
                {
                    db.SaveChanges();
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
           
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            model.CategoryId = db.Categories.Where(c => c.CategoryName == cbCategory.SelectedItem.ToString()).FirstOrDefault().CategoryId;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            
        }

        private void tbPrice_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
