using AsaderoVivaMexicoPV.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsaderoVivaMexicoPV
{
    public partial class frmAddOrder : Form
    {
        DataBaseContext db = new DataBaseContext();
        public List<Product> products = new List<Product>();
        public List<Product> removedProducts = new List<Product>();
        public bool saved = false;
        public List<int> quantities = new List<int>();
        int actualCategory = -1;

        public frmAddOrder()
        {
            InitializeComponent();
            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
            Location = new Point();
            Responsive();
            addCategories();
        }

        void addProducts(int categoryId)
        {
            var products = db.Products.Where(p => p.CategoryId == categoryId).ToList();
            int x = (int)(pProducts.Width * .04);
            int y = (int)(pProducts.Width * .02);
            pProducts.Controls.Clear();
            for (int i = 0; i < products.Count; i++)
            {
                PictureBox pb = new PictureBox
                {
                    Name = products[i].ProductName,
                    SizeMode= PictureBoxSizeMode.StretchImage,
                    Text = $"{products[i].ProductName}",
                    Width = (int)(pProducts.Width * .20),
                    Height = (int)(pProducts.Width * .20),
                    Location = new Point(x, y),
                    
                    //BorderStyle = BorderStyle.FixedSingle
                };
                if (products[i].Image != null)
                {
                    MemoryStream ms = new MemoryStream(products[i].Image);
                    pb.Image = Image.FromStream(ms);
                }
                pb.Click += addToCart;
                Rectangle r = new Rectangle(0, 0, pb.Width, pb.Height);
                GraphicsPath gp = new GraphicsPath();
                int d = 50;
                gp.AddArc(r.X, r.Y, d, d, 180, 90);
                gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
                gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
                gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
                pb.Region = new Region(gp);
                pb.BackColor = Color.Azure;

                Label label = new Label
                {
                    Text = "",
                    Font = new Font(FontFamily.GenericSansSerif, 15),
                    Name = products[i].ProductName,
                    Width = (int)(pProducts.Width * .04),
                    Height = (int)(pProducts.Width * .04),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.Blue
                };
                label.Location = new Point(x + pb.Width - (label.Width / 2), y - (label.Width / 2));

                Label label2 = new Label
                {
                    Text = products[i].ProductName,
                    Font = new Font(FontFamily.GenericSansSerif, 10),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.Transparent
                };
                label2.Click += addToCart;
                label2.Width = (int)(pProducts.Width * .18);
                label2.Height = label2.Height * 2;
                label2.Location = new Point(x + (pb.Width / 2) - (label2.Width / 2), y + pb.Width - label2.Height - 10);
                pProducts.Controls.Add(label);
                pProducts.Controls.Add(label2);
                pProducts.Controls.Add(pb);
                x += (int)(pProducts.Width * .24);
                if ((i + 1) % 4 == 0)
                {
                    y += (int)(pProducts.Width * .23);
                    x = (int)(pProducts.Width * .04);
                }
            }
        }

        void bag(string name, int quantity)
        {
            for (int i = 0; i < pProducts.Controls.Count; i++)
            {
                if (pProducts.Controls[i].Name == name)
                {
                    pProducts.Controls[i].Text = quantity.ToString();
                }
            }
        }

        void add(Product product)
        {
            if (!products.Contains(product))
            {
                products.Add(product);
                quantities.Add(1);
                bag(product.ProductName, 1);
            }
            else
            {
                quantities[products.IndexOf(product)]++;
                bag(product.ProductName, quantities[products.IndexOf(product)]);
            }
            lblTotalProducts.Text = quantities.Sum().ToString();
        }

        private void addToCart(object sender, EventArgs e)
        {
            try
            {
                Product product = db.Products.Where(p => p.ProductName == ((PictureBox)sender).Name).FirstOrDefault();
                add(product);
            }
            catch (Exception)
            {
                Product product = db.Products.Where(p => p.ProductName == ((Label)sender).Text).FirstOrDefault();
                add(product);
            }
        }

        void addCategories()
        {
            pCategories.Controls.Clear();
            var categories = db.Categories.ToList();
            int x = 0;
            int y = 3;
            Font font;
            font = new Font(FontFamily.GenericSansSerif, 17);
            for (int i = 0; i < categories.Count; i++)
            {
                Button button = new Button
                {
                    Name = categories[i].CategoryName,
                    Text = categories[i].CategoryName,
                    Width = pCategories.Width,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = font,
                    Height = pCategories.Height / 12,
                    Location = new Point(x, y)
                };

                button.Click += dispayProducts;
                y += button.Height;
                pCategories.Controls.Add(button);
            }

            y += pCategories.Height / 6;

            btnPendientes.Width = pCategories.Width;
            btnPendientes.Height = pCategories.Height / 12;
            btnPendientes.Font = font;
            btnPendientes.Location = new Point(x, y);
            pCategories.Controls.Add(btnPendientes);

            y += btnPendientes.Height;
            btnBack.Width = pCategories.Width;
            btnBack.Height = pCategories.Height / 12;
            btnBack.Font = font;
            btnBack.Location = new Point(x, y);
            pCategories.Controls.Add(btnBack);

            y += btnBack.Height;
            btnPagadas.Width = pCategories.Width;
            btnPagadas.Height = pCategories.Height / 12;
            btnPagadas.Font = font;
            btnPagadas.Location = new Point(x, y);
            pCategories.Controls.Add(btnPagadas);

            actualCategory = actualCategory == -1 ? categories[0].CategoryId : actualCategory;
            addProducts(actualCategory);
        }

        void dispayProducts(object sender, EventArgs e)
        {
            int id = db.Categories.Where(c => c.CategoryName == ((Button)sender).Name).FirstOrDefault().CategoryId;
            actualCategory = id;
            addProducts(id);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        void Responsive()
        {
            pProducts.Width = (int)(Width * .7);
            pProducts.Height = (int)(Height * .9);
            pProducts.Location = new Point((int)(Width * .15), (int)(Height * .05));

            pCategories.Width = (int)(Width * .15);
            pCategories.Height = (int)(Height * .9);
            pCategories.Location = new Point(0, (int)(Height * .05));

            btnBack.Location = new Point(05, (int)(Height * .01));
            btnPendientes.Location = new Point(Height - (btnPendientes.Width + 10), (int)(Height * .02));
            pbCart.Height = (int)(Height * .15);
            pbCart.Width = (int)(Width * .1);
            pbCart.Location = new Point(Width - pbCart.Width - 55, (int)(Height * .05));

            lblTotalProducts.Location = new Point(Width - 55 - (lblTotalProducts.Width / 2), (int)(Height * .1 - (lblTotalProducts.Height / 2)));
            addCategories();
        }

        private void pbCart_Click(object sender, EventArgs e)
        {
            Visible = false;
            removedProducts.Clear();
            frmProductsInOrder productsInOrder = new frmProductsInOrder(this);
            productsInOrder.ShowDialog();
            lblTotalProducts.Text = quantities.Sum().ToString();
            Changes();
            Visible = true;
        }

        void Changes()
        {
            foreach (Control item in pProducts.Controls)
            {
                Product prod = products.Where(p => p.ProductName == item.Name).FirstOrDefault();
                Product removed = removedProducts.Where(p => p.ProductName == item.Name).FirstOrDefault();
                if (prod != null)
                {
                    int index = products.IndexOf(prod);
                    item.Text = quantities[index].ToString();
                }
                else if (removed != null)
                {
                    item.Text = "";
                }
            }
        }

        private void btnPendientes_Click(object sender, EventArgs e)
        {
            Visible = false;
            frmOrders orders = new frmOrders();
            orders.ShowDialog();
            Visible = true;
        }

        private void btnPagadas_Click(object sender, EventArgs e)
        {
            Visible = false;
            frmPaidOrders paidOrders = new frmPaidOrders();
            paidOrders.ShowDialog();
            Visible = true;
        }
    }
}
