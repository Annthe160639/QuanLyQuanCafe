using QuanLyQuanCafe.Models;
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
        QuanLyQuanCaPheContext context = new QuanLyQuanCaPheContext();
        public Admin()
        {
            InitializeComponent();
            LoadAccountList();
        }
        void LoadFoodList()
        {
            dtgvFood.DataSource = context.Foods.ToList();
        }
        void LoadAccountList()
        {
            string query = "USP_GetAccountByUsername @username";
            
            dtgvAccount.DataSource = context.Accounts.ToList();
        }
        
    }
}
