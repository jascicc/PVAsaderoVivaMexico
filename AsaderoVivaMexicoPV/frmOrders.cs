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
    public partial class frmOrders : Form
    {
        DataBaseContext db = new DataBaseContext();
        float totalInDollars;
        float dollar;
        int totalInPesos;
        public frmOrders()
        {
            InitializeComponent();
            dollar = db.Configs.FirstOrDefault().Dollar;
            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
            Location = new Point();
            Responsive();
            CreateFloatingExpander();
        }

        private void CreateFloatingExpander()
        {
            pOrders.Controls.Clear();
            int x = 0, y = 0;
            var orders = db.Orders.Where(o => o.Active == "POR PAGAR")
                .Include(o => o.Table)
                .Include(o => o.Product)
                .OrderBy(o => o.GroupID)
                .ToList();

            var ids = orders
                .GroupBy(o => o.GroupID)
                .Select(grp => grp.First())
                .ToList();

            for (int i = 0; i < ids.Count; i++)
            {
                Expander expander = new Expander
                {
                    Location = new Point(x, y),
                    BorderStyle = BorderStyle.FixedSingle
                };

                Label headerLabel = new Label
                {
                    Text = "Mesa " + ids[i].Table.TableNumber,
                    AutoSize = false,
                    Font = new Font(FontFamily.GenericSansSerif, 15, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleLeft,
                    BackColor = SystemColors.ActiveBorder,
                    Size = new Size(pOrders.Width, 35)
                };
                

                expander.Header = headerLabel;

                Label labelContent = new Label
                {
                    Location = new Point(0, 0)
                };
                var ords = orders.Where(o => o.GroupID == ids[i].GroupID).ToList();
                float total = 0;
                int size = 0;
                for (int e = 0; e < ords.Count; e++)
                {
                    labelContent.Text += $"Producto: {ords[e].Product.ProductName}\n";
                    labelContent.Text += $"Cantidad: {ords[e].Quantity}\n";
                    labelContent.Text += $"Subtotal: {ords[e].Product.Price * ords[e].Quantity}\n\n";
                    total += ords[e].Product.Price * ords[e].Quantity;
                    size += 80;
                }
                Label lblTotal = new Label();
                lblTotal.Name = $"lblTotal{ids[i]}";
                lblTotal.Text = $"Total en pesos: {total}";

                Label lblTotalDollars = new Label();
                lblTotalDollars.Name = $"lblTotalDollars{ids[i]}";
                string totalDolares = (total / dollar).ToString();
                lblTotalDollars.Text = $"Total en dólares: {totalDolares.Substring(0, totalDolares.IndexOf('.') + 3)}";

                expander.Size = new Size((int)(pOrders.Width * .95), headerLabel.Height);
                labelContent.Size = new Size(expander.Width / 4, size + 50);
                labelContent.Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);
                lblTotal.Font = labelContent.Font;
                lblTotalDollars.Font = labelContent.Font;

                Panel p = new Panel();
                p.Width = (int)(expander.Width * .98);
                p.Height = size + 50;

                Button button = new Button
                {
                    Name = ids[i].GroupID,
                    Width = (int)(p.Width * .15),
                    Height = 40,
                    Text = "Pagar",
                };
                button.Location = new Point((int)(p.Width * .95 - button.Width), (int)(p.Height * .1));
                button.Click += Pay;

                lblTotal.Size = new Size((int)(p.Width * .18), 30);
                lblTotalDollars.Size = new Size((int)(p.Width * .18), 30);

                lblTotal.Location = new Point((int)(p.Width * .95 - lblTotal.Width), (button.Height + button.Location.Y));
                lblTotalDollars.Location = new Point((int)(p.Width * .95 - lblTotalDollars.Width), (lblTotal.Height + lblTotal.Location.Y));

                headerLabel.Click += delegate
                {
                    expander.Toggle();
                    button.Focus();
                    MoveOthers(expander);
                };

                p.Controls.Add(button);
                p.Controls.Add(lblTotal);
                p.Controls.Add(lblTotalDollars);
                p.Controls.Add(labelContent);
                expander.Content = p;
                expander.Toggle();
                pOrders.Controls.Add(expander);
                y += 35;
            }
        }

        void Pay(string id)
        {
            Visible = false;
            var list = db.Orders.Where(o => o.GroupID == id).Include(o => o.Product).ToList();
            totalInPesos = 0;
            for (int i = 0; i < list.Count; i++)
            {
                totalInPesos += (int)(list[i].Quantity * list[i].Product.Price);
            }
            totalInDollars = totalInPesos / dollar;
            frmPayOrder payOrder = new frmPayOrder(id,totalInPesos,totalInDollars);
            payOrder.ShowDialog();
            Visible = true;
            
        }

        private void Pay(object sender, EventArgs e)
        {
            var button = sender as Button;
            Pay(button.Name);
        }

        void MoveOthers(Expander expander)
        {
            for (int i = pOrders.Controls.IndexOf(expander) + 1; i < pOrders.Controls.Count; i++)
            {
                if (!expander.Expanded)
                {
                    pOrders.Controls[i].Location = new Point(pOrders.Controls[i].Location.X, pOrders.Controls[i].Location.Y - expander.Content.Height);
                }
                else
                {
                    pOrders.Controls[i].Location = new Point(pOrders.Controls[i].Location.X, pOrders.Controls[i].Location.Y + expander.Content.Height);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        void Responsive()
        {
            btnBack.Location = new Point((int)(Width * .05), (int)(Height * .05));
            pOrders.Width = (int)(Width * .9);
            pOrders.Height = (int)(Height * .85);
            pOrders.Location = new Point((int)(Width * .05), (int)(Height * .1));

        }
    }
}
