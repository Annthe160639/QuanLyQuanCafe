using QuanLyQuanCafe.Models;

namespace QuanLyQuanCafe
{
    public partial class Login : Form
    {
        QuanLyQuanCaPheContext context = new QuanLyQuanCaPheContext();
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txbUsername.Text;
            string password = txbPassword.Text; 
            if (login(username, password))
            {
            TableManager f = new TableManager();
            this.Hide();
            f.ShowDialog();
            this.Show();
            }
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu.");
            }
        }

        bool login(string username, string password)
        {
            return context.Accounts.FirstOrDefault(a => a.Username.Equals(username) && a.Password.Equals(password)) is not null;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát chương trình?", "Thông báo",MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }else
            {
                e.Cancel = false;
            }
        }
    }
}