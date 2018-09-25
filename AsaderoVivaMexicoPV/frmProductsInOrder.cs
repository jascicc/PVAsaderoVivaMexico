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
    public partial class frmProductsInOrder : Form
    {
        DataBaseContext db = new DataBaseContext();
        frmAddOrder owner = null;
        Button lastTable = null;
        bool wasBusy = false;
        int table = -1;

        public frmProductsInOrder(frmAddOrder owner)
        {
            InitializeComponent();
            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
            Location = new Point();
            Responsive();
            this.owner = owner;
            Filldgv();
            addTables();
        }

        private void Responsive()
        {
            btnBack.Location = new Point((int)(Width * .05), (int)(Height * .05));
            btnSave.Location = new Point((int)(Width * .95 - btnSave.Width), (int)(Height * .95));

            dgvProducts.Width = (int)(Width * .55);
            dgvProducts.Height = (int)(Height * .8);
            dgvProducts.Location = new Point((int)(Width * .05), (int)(Height * .1));

            panel1.Width = (int)(Width * .30);
            panel1.Height = (int)(Height * .8);
            panel1.Location = new Point((int)(Width * .65), (int)(Height * .1));
        }

        void Filldgv()
        {
            dgvProducts.Rows.Clear();
            for (int i = 0; i < owner.products.Count; i++)
            {
                dgvProducts.Rows.Add(owner.products[i].ProductID, owner.products[i].ProductName, owner.products[i].Price, owner.quantities[i], "-", "+", "X");
            }
        }

        void addTables()
        {
            var tables = db.Tables.ToList();
            var orders = db.Orders.Where(o => o.Active == "POR PAGAR").ToList();

            int x = (int)(panel1.Width * .025);
            int y = 3;
            for (int i = 0; i < tables.Count; i++)
            {
                Order order = orders.Where(o => o.TableId == tables[i].TableId).ToList().FirstOrDefault();
                Button button = new Button
                {
                    Name = tables[i].TableNumber.ToString(),
                    Text = "Mesa " + tables[i].TableNumber,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Width = (int)(panel1.Width * .3),
                    Height = (int)(panel1.Width * .1),
                    Font = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Bold),
                    Location = new Point(x, y),
                    FlatStyle = FlatStyle.Flat
                };
                if (order != null)
                {
                    button.BackColor = Color.Red;
                    button.ForeColor = Color.White;
                }
                x += (int)(panel1.Width * .325);
                button.Click += selectTable;
                panel1.Controls.Add(button);

                if ((i + 1) % 3 == 0)
                {
                    y += (int)(panel1.Width * .15);
                    x = (int)(panel1.Width * .025);
                }
            }
        }

        private void selectTable(object sender, EventArgs e)
        {
            if (lastTable != null)
            {
                if (!wasBusy)
                {
                    lastTable.BackColor = SystemColors.Control;
                    lastTable.ForeColor = SystemColors.ControlText;
                }
                else
                {
                    lastTable.BackColor = Color.Red;
                    lastTable.ForeColor = Color.White;
                }
            }
            if ((Button)sender != lastTable)
            {
                if (((Button)sender).BackColor == Color.Red)
                {
                    wasBusy = true;
                }
                else
                {
                    wasBusy = false;
                }
            }
            lastTable = (Button)sender;
            ((Button)sender).BackColor = Color.Green;
            ((Button)sender).ForeColor = Color.White;
            table = int.Parse(((Button)sender).Name);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = int.Parse(dgvProducts.CurrentRow.Cells[0].Value.ToString());
            Product product = owner.products.Where(p => p.ProductID == id).FirstOrDefault();
            int index = owner.products.IndexOf(product);
            if (e.ColumnIndex == 4)
            {
                if (owner.quantities[index] > 1)
                {
                    owner.quantities[index]--;
                    dgvProducts.Rows[index].Cells[3].Value = owner.quantities[index];
                }
                else
                {
                    owner.quantities.RemoveAt(index);
                    owner.products.RemoveAt(index);
                    dgvProducts.Rows.RemoveAt(index);
                    owner.removedProducts.Add(product);
                }
            }
            else if (e.ColumnIndex == 5)
            {
                owner.quantities[index]++;
                dgvProducts.Rows[index].Cells[3].Value = owner.quantities[index];
            }
            else if (e.ColumnIndex == 6)
            {
                owner.quantities.RemoveAt(index);
                owner.products.RemoveAt(index);
                dgvProducts.Rows.RemoveAt(index);
                owner.removedProducts.Add(product);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (owner.products.Count > 0 && table != -1)
            {
                var list = db.Orders.Where(o => o.Active == "POR PAGAR").ToList();
                list = list.Where(o => o.TableId == table).ToList();
                DateTime date = DateTime.Now;
                string GroupID = list.Count > 0 ? list.First().GroupID : $"{date} {table}";
                List<Order> newProducts = new List<Order>();
                List<Order> existingProducts = new List<Order>();
                for (int i = 0; i < owner.products.Count; i++)
                {
                    Order order = new Order
                    {
                        ProductID = owner.products[i].ProductID,
                        Quantity = owner.quantities[i],
                        Date = date,
                        TableId = table,
                        GroupID = GroupID,
                        Active = "POR PAGAR"
                    };

                    Order model = list.Where(o => o.ProductID == order.ProductID).FirstOrDefault();

                    if (model == null)
                    {
                        newProducts.Add(order);
                    }
                    else
                    {
                        existingProducts.Add(order);
                    }
                }

                db.Orders.AddRange(newProducts);
                db.SaveChanges();

                for (int i = 0; i < existingProducts.Count; i++)
                {
                    Order model = list.Where(o => o.ProductID == existingProducts[i].ProductID).FirstOrDefault();
                    model.Quantity += existingProducts[i].Quantity;
                    db.Entry(model).State = EntityState.Modified;
                }

                db.SaveChanges();
                owner.saved = true;
                Close();
            }
        }
    }
}
