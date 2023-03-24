using QuanLyQuanCafe.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class AccountProfile : Form
    {
        QuanLyQuanCaPheContext context = new QuanLyQuanCaPheContext();

        Account currentAccount;
        public AccountProfile(Account account)
        {
            InitializeComponent();
            this.currentAccount = account;
            txbDisplayName.Text = account.DisplayName;
            txbUsername.Text = account.Username;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            bool isMatchPassword = txbPassword.Text.Equals(currentAccount.Password);
            currentAccount.DisplayName = txbDisplayName.Text;
            if (isMatchPassword)
            {
                bool isMatchRePassword = txbNewPassword.Text.Equals(txbReEnterPass);
                if (isMatchRePassword)
                {
                    currentAccount.Password = txbNewPassword.Text;
                }
            }
            context.Accounts.Update(currentAccount);
            context.SaveChanges();
        }
    }
}
