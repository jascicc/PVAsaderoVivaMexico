using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AsaderoVivaMexicoPV.Models;

namespace AsaderoVivaMexicoPV
{
    class DataBaseContext : DbContext
    {
        //public DataBaseContext() : base(@"Data Source=DESKTOP-MA0RJF5\SQLEXPRESS; Initial Catalog= PVAsadero; Integrated Security= true;")
        public DataBaseContext() : base(@"Data Source=(LocalDb)\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\PVAsadero.mdf; Initial Catalog= PVAsadero; Integrated Security= true;")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Table> Tables { get; set; }
        public virtual DbSet<Spending> Spendings { get; set; }
        public virtual DbSet<Config> Configs { get; set; }
    }
}
