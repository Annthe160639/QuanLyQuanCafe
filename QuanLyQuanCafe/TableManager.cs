using Microsoft.EntityFrameworkCore;
using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.Models;
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
        QuanLyQuanCaPheContext context = new QuanLyQuanCaPheContext();
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
            List<FoodCategory> categories = context.FoodCategories.ToList();
            cbCategory.DataSource= categories;
            cbCategory.DisplayMember = "Name";
            cbCategory.ValueMember = "Id";
        }
        void LoadFoodListByCategoryId(int id)
        {
            if (id > 0)
            {
                List<Food> listFood = context.Foods.Where(f => f.CategoryId == id).ToList();
                cbFood.DataSource = listFood;
                cbFood.DisplayMember = "Name";
                cbFood.ValueMember = "Id";
            }
        }
        void LoadTable()
        {
            flpTable.Controls.Clear();
            List<TableFood> tableList = context.TableFoods.ToList();
            foreach (TableFood table in tableList)
            {
                Button btn = new Button() { Width = 100, Height = 100 };
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
            var listBillInfo = context.BillInfos.Include(b => b.Bill).Include(bi => bi.Food).Where(b => b.Bill.TableId == id).ToList();
            double totalPrice = 0;
            foreach (var menu in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(menu.Food.Name.ToString());
                lsvItem.SubItems.Add(menu.Count.ToString());
                lsvItem.SubItems.Add(menu.Food.Price.ToString());
                lsvItem.SubItems.Add((menu.Food.Price * menu.Count).ToString());
                lsvBill.Items.Add(lsvItem);
                totalPrice += menu.Food.Price * menu.Count;
            }
            CultureInfo cultureInfo= new CultureInfo("vi-VN");
            txbTotalPrice.Text = totalPrice.ToString("c", cultureInfo);
        }

        #endregion
        #region Events
        void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as TableFood).Id;
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
            FoodCategory selected = cb.SelectedItem as FoodCategory;
            id = selected.Id;
            LoadFoodListByCategoryId(id);
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            TableFood table = lsvBill.Tag as TableFood;
            Bill bill = context.Bills.FirstOrDefault(b => b.TableId == table.Id && b.Status == 0);
            int foodId = (int)cbFood.SelectedValue;
            int count = (int)nmFoodCount.Value;
            if (bill is not null)
            {
                BillInfo billInfo = context.BillInfos.FirstOrDefault(b => b.BillId == bill.Id);
                billInfo.Count += count;
                context.BillInfos.Update(billInfo);
                context.SaveChanges();
            }
            else
            {
                Bill insertBill = new Bill();
                insertBill.TableId = table.Id;
                context.Bills.Add(insertBill);
                context.SaveChanges();
                BillInfo billInfo = new BillInfo();
                billInfo.BillId = insertBill.Id;
                billInfo.FoodId = foodId;
                billInfo.Count = count;
                context.BillInfos.Add(billInfo);
                context.SaveChanges();
            }
            ShowBill(table.Id);
            
            LoadTable();

        }
        #endregion

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            TableFood table = lsvBill.Tag as TableFood;
            Bill bill = context.Bills.FirstOrDefault(b => b.TableId == table.Id && b.Status == 0);
            if (bill != null)
            {
                int discount = (int)nmDiscount.Value;
                Console.WriteLine(txbTotalPrice.Text);
                double totalPrice = Convert.ToDouble(txbTotalPrice.Text.Split(' ')[0]);
                double final = totalPrice - (totalPrice / 100 * discount);

                if (MessageBox.Show(
                    string.Format(
                        @"Bạn có chắc thanh toán hóa đơn cho bàn {0}?
                          Tổng tiền - (Tổng tiền / 100) x Giảm giá = {1} - ({1} / 100) * {2} = {3}", 
                          table.Name, totalPrice, discount, final), 
                          "Thông báo", 
                          MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    bill.Status = 1;
                    bill.Discount = discount;
                    context.Bills.Update(bill);
                    context.SaveChanges();
                    ShowBill(bill.TableId);
                    LoadTable();

                }
            }
        }

        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            int id1 = (lsvBill.Tag as TableFood).Id;
            int id2 = (cbSwitchTable.SelectedItem as TableFood).Id;
            if (MessageBox.Show(string.Format("Bạn có thật sự muốn chuyển bàn {0} qua bàn {1}?", (lsvBill.Tag as TableFood).Name, (cbSwitchTable.SelectedItem as TableFood).Name), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                DataProvider.Instance.ExecuteQuery("exec USP_SwitchTable @idTable1 , @idTable2", new object[] { id1, id2 });
            }
        }
        public void LoadComboBoxTable(ComboBox cb)
        {
            cb.DataSource = context.TableFoods.ToList();
            cb.DisplayMember = "Name";  
        }
    }
}
