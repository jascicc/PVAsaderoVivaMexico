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
    public partial class frmPayOrder : Form
    {
        DataBaseContext db = new DataBaseContext();
        string orderId = null;
        int total;
        float totalDollars, dollar;
        bool payInDollars = false;

        public frmPayOrder(string Id, int total, float totalDollars)
        {
            InitializeComponent();
            dollar = db.Configs.FirstOrDefault().Dollar;
            orderId = Id;
            this.total = total;
            this.totalDollars = totalDollars;
            lblInfo.Text = $"Total en pesos: {total}\n" +
                $"Total en dólares: {totalDollars.ToString().Substring(0, totalDollars.ToString().IndexOf('.') + 3)}";
            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
            Location = new Point();
            Responsive();
        }

        private void Responsive()
        {
            btnBack.Location = new Point((int)(Width * .06), (int)(Height * .05));

            pButtons.Width = (int)(Width * .4);
            pButtons.Height = (int)(Height * .6);
            pButtons.Location = new Point((int)(Width * .06), (int)(Height * .3));

            pInfo.Width = (int)(Width * .4);
            pInfo.Height = (int)(Height * .6);
            pInfo.Location = new Point((int)(Width * .53), (int)(Height * .25));

            tbScreen.Width = (int)(Width * .4);
            tbScreen.Height = (int)(Width * .08);
            tbScreen.Location = new Point((int)(Width * .06), (int)(Height * .1));

            List<Button> items = new List<Button>
            {
                btn1,
                btn2,
                btn3,
                btn4,
                btn5,
                btn6,
                btn7,
                btn8,
                btn9,
                btnDelete,
                btnDot,
                btnClear
            };

            for (int i = 0; i < items.Count; i++)
            {

                items[i].Width = (int)(pButtons.Width * .2);
                items[i].Height = (int)(pButtons.Height * .2);
            }
            items.RemoveRange(9, 3);
            int x = (int)(pButtons.Width * .04);
            int y = (int)(pButtons.Height * .04);
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Location = new Point(x, y);
                x += (int)(pButtons.Width * .24);
                if ((i + 1) % 3 == 0)
                {
                    y += (int)(pButtons.Width * .04 + items[i].Height);
                    x = (int)(pButtons.Width * .04);
                }
            }
            btn0.Width = (int)(pButtons.Width * .44);
            btn0.Height = (int)(pButtons.Height * .2);
            btn0.Location = new Point((int)(pButtons.Width * .04), (int)(pButtons.Height * .76));

            btnEnter.Width = (int)(pButtons.Width * .2);
            btnEnter.Height = (int)(pButtons.Height * .44);
            btnEnter.Location = new Point((int)(pButtons.Width * .76), (int)(pButtons.Height * .52));

            btnDot.Height = (int)(pButtons.Height * .2);
            btnDot.Width = (int)(pButtons.Width * .2);
            btnDot.Location = new Point((int)(pButtons.Width * .52), (int)(pButtons.Height * .76));

            btnDelete.Height = (int)(pButtons.Height * .2);
            btnDelete.Width = (int)(pButtons.Width * .2);
            btnDelete.Location = new Point((int)(pButtons.Width * .76), (int)(pButtons.Height * .04));

            btnClear.Height = (int)(pButtons.Height * .2);
            btnClear.Width = (int)(pButtons.Width * .2);
            btnClear.Location = new Point((int)(pButtons.Width * .76), (int)(pButtons.Height * .28));


        }

        private void btn_Click(object sender, EventArgs e)
        {
            string c = null;
            switch (((Button)sender).Name)
            {
                case "btn1":
                    c = "1";
                    break;
                case "btn2":
                    c = "2";
                    break;
                case "btn3":
                    c = "3";
                    break;
                case "btn4":
                    c = "4";
                    break;
                case "btn5":
                    c = "5";
                    break;
                case "btn6":
                    c = "6";
                    break;
                case "btn7":
                    c = "7";
                    break;
                case "btn8":
                    c = "8";
                    break;
                case "btn9":
                    c = "9";
                    break;
            }
            tbScreen.Text += c;
        }

        private void btn_Click2(object sender, EventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "btnEnter":
                    CalcFeria();
                    break;
                case "btnDot":
                    if (!tbScreen.Text.Contains("."))
                    {
                        tbScreen.Text += ".";
                    }
                    break;
                case "btnClear":
                    tbScreen.Clear();
                    break;
                case "btnDelete":
                    if (tbScreen.Text.Length > 0)
                    {
                        tbScreen.Text = tbScreen.Text.Substring(0, tbScreen.Text.Length - 1);
                    }
                    break;
            }
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            if (tbScreen.Text.Length > 0)
            {
                tbScreen.Text += "0";
            }
        }

        private void tbScreen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))
            {
                if (e.KeyChar != '0')
                {
                    e.Handled = false;
                }
                else
                {
                    if (tbScreen.Text.Length > 0)
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
            else if (e.KeyChar == (char)Keys.Back)
            {
                if (tbScreen.Text.Length > 0)
                {
                    tbScreen.Text = tbScreen.Text.Substring(0, tbScreen.Text.Length - 1);
                }
            }
            else if (e.KeyChar == (char)Keys.Enter)
            {
                Charge();
            }
        }

        void Charge()
        {
            var orders = db.Orders.Where(o => o.Active == "POR PAGAR").ToList();
            orders = orders.Where(o => o.GroupID == orderId).ToList();
            for (int i = 0; i < orders.Count; i++)
            {
                Order model = orders[i];
                model.Active = "PAGADO";
                db.Entry(model).State = EntityState.Modified;
            }
            db.SaveChanges();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            Charge();
        }

        void CalcFeria()
        {
            if (tbScreen.Text.Length > 0)
            {
                float pay = float.Parse(tbScreen.Text);
                string _out;
                float feriaP;
                float feriaD;
                if (payInDollars)
                {
                    feriaD = pay - totalDollars;
                    feriaP = (pay * dollar) - total;
                }
                else
                {
                    feriaP = pay - total;
                    feriaD = feriaP / dollar;
                }
                int indexD = feriaD.ToString().IndexOf('.');
                int indexP = feriaP.ToString().IndexOf('.');
                if (indexP == -1 || indexP + 3 > feriaP.ToString().Length)
                {
                    _out = $"Feria en pesos {feriaP}\n";
                }
                else
                {
                    _out = $"Feria en pesos {feriaP.ToString().Substring(0, indexP + 3)}\n";
                }
                if (indexD == -1 || indexD + 3 > feriaD.ToString().Length)
                {
                    _out += $"Feria en dolares {feriaP}";
                }
                else
                {
                    _out += $"Feria en dolares {feriaD.ToString().Substring(0, indexD + 3)}";
                }

                lblferia.Text = _out;
            }
        }
    }
}
