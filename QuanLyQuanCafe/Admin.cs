using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
            LoadAccountList();
        }
        void LoadFoodList()
        {
            string query = "select * from food";

            dtgvFood.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }
        void LoadAccountList()
        {
            string query = "USP_GetAccountByUsername @username";
            
            dtgvAccount.DataSource = DataProvider.Instance.ExecuteQuery(query, new object[] { "an" });
        }
        
    }
}
