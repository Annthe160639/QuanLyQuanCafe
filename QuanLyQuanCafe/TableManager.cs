using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class TableManager : Form
    {
        public TableManager()
        {
            InitializeComponent();
            LoadTable();
            LoadCategory();
            LoadComboBoxTable(cbSwitchTable);
        }
        #region Method
        void LoadCategory()
        {
            List<Category> categories = CategoryDAO.Instance.getListCategory();
            cbCategory.DataSource= categories;
            cbCategory.DisplayMember = "Name";
        }
        void LoadFoodListByCategoryId(int id)
        {
            List<Food> listFood = FoodDAO.Instance.getFoodByCategoryId(id);
            cbFood.DataSource= listFood; 
            cbFood.DisplayMember = "Name";
        }
        void LoadTable()
        {
            flpTable.Controls.Clear();
            List<Table> tableList = TableDAO.Instance.LoadTableList();
            foreach (Table table in tableList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight };
                btn.Text = table.Name + Environment.NewLine + table.Status;
                btn.Click += btn_Click;
                btn.Tag = table;
                switch (table.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.Lavender;
                        break;
                    default:
                        btn.BackColor = Color.LightSkyBlue;
                        break;
                }
                flpTable.Controls.Add(btn);
            }
        }
        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            List<Menu> listBillInfo = MenuDAO.Instance.GetMenuByTableId(id);
            float totalPrice = 0;
            foreach (Menu menu in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(menu.FoodName.ToString());
                lsvItem.SubItems.Add(menu.Count.ToString());
                lsvItem.SubItems.Add(menu.Price.ToString());
                lsvItem.SubItems.Add(menu.TotalPrice.ToString());
                lsvBill.Items.Add(lsvItem);
                totalPrice += menu.TotalPrice;
            }
            CultureInfo cultureInfo= new CultureInfo("vi-VN");
            txbTotalPrice.Text = totalPrice.ToString("c", cultureInfo);
        }

        #endregion
        #region Events
        void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(tableID);
        }
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountProfile f = new AccountProfile();
            f.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admin f = new Admin();
            f.ShowDialog();

        }
        

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null) return;
            Category selected = cb.SelectedItem as Category;
            id = selected.Id;
            LoadFoodListByCategoryId(id);
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            int billId = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            int foodId = (cbFood.SelectedItem as Food).Id;
            int count = (int)nmFoodCount.Value;
            if (billId == -1)
            {
                BillDAO.Instance.InserBill(table.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIdBill(), foodId, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(billId, foodId, count);
            }
            ShowBill(table.ID);
            LoadTable();

        }
        #endregion

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            int billId = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            int discount = (int)nmDiscount.Value;
            Console.WriteLine(txbTotalPrice.Text);
            double totalPrice = Convert.ToDouble(txbTotalPrice.Text.Split(' ')[0]);
            double final = totalPrice - (totalPrice / 100 * discount);
            if(billId != -1)
            {
                if(MessageBox.Show(string.Format("Bạn có chắc thanh toán hóa đơn cho bàn {0}?\nTổng tiền - (Tổng tiền / 100) x Giảm giá = {1} - ({1} / 100) * {2} = {3}",table.Name, totalPrice, discount, final), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(billId, discount );
                    ShowBill(table.ID);
                    LoadTable();

                }
            }
        }

        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            int id1 = (lsvBill.Tag as Table).ID;
            int id2 = (cbSwitchTable.SelectedItem as Table).ID;
            if (MessageBox.Show(string.Format("Bạn có thật sự muốn chuyển bàn {0} qua bàn {1}?", (lsvBill.Tag as Table).Name, (cbSwitchTable.SelectedItem as Table).Name), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                TableDAO.Instance.SwitchTable(0, 1);
            }
        }
        public void LoadComboBoxTable(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.LoadTableList();
            cb.DisplayMember = "Name";  
        }
    }
}
