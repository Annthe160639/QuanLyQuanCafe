using QuanLyQuanCafe.DAO;
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
            cbCategory.DataSource = context.FoodCategories.ToList();
            cbCategory.DisplayMember = "Name";
            cbCategory.ValueMember = "Id";
            cbTableStatus.DataSource = new[] { TableDAO.EMPTY_TABLE, TableDAO.USING_TABLE };
            dtgvBill.AutoGenerateColumns = false;
        }
        void LoadFoodList(string name)
        {
            dtgvFood.DataSource = context.Foods.Where(f => f.Name.Contains(name)).ToList();
            txbFoodID.DataBindings.Clear();
            txbFoodID.DataBindings.Add("Text", dtgvFood.DataSource, "Id");

            txbFoodName.DataBindings.Clear();
            txbFoodName.DataBindings.Add("Text", dtgvFood.DataSource, "Name");

            cbCategory.DataBindings.Clear();
            cbCategory.DataBindings.Add("SelectedValue", dtgvFood.DataSource, "CategoryId");

            nmFoodPrice.DataBindings.Clear();
            nmFoodPrice.DataBindings.Add("Value", dtgvFood.DataSource, "Price");
        }
        void LoadAccountList()
        {
            QuanLyQuanCaPheContext context = new QuanLyQuanCaPheContext();
            dtgvAccount.DataSource = context.Accounts.ToList();
        }

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dtpkFromDate.Value;
            DateTime endDate = dtpkToDate.Value;

            BillDAO billDAO = new BillDAO();
            var bills = billDAO.getBillListBetweenDate(fromDate, endDate).Select(b => new
            {
                b.Id,
                Checkin = b.DateCheckin,
                Checkout = b.DateCheckout,
                b.Discount,
                TotalPrice = billDAO.CalcPriceBillInfos(b.BillInfos.ToList())
            }).ToList();
            lbTotalPrice.Text = bills.Sum(b => b.TotalPrice).ToString();
            dtgvBill.DataSource = bills;
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Food insertFood = new Food();
            insertFood.Name = txbFoodName.Text;
            insertFood.CategoryId = Int32.Parse(cbCategory.SelectedValue.ToString());
            insertFood.Price = (double)nmFoodPrice.Value;
            context.Foods.Add(insertFood);
            context.SaveChanges();
            LoadFoodList("");

        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            context.Foods.Remove(context.Foods.FirstOrDefault(f => f.Id == Int32.Parse(txbFoodID.Text)));
            context.SaveChanges();
            LoadFoodList("");

        }

        private void btnUpdateFood_Click(object sender, EventArgs e)
        {
            Food updateFood = context.Foods.FirstOrDefault(f => f.Id == Int32.Parse(txbFoodID.Text));
            updateFood.Name = txbFoodName.Text;
            updateFood.CategoryId = Int32.Parse(cbCategory.SelectedValue.ToString());
            updateFood.Price = (double)nmFoodPrice.Value;
            context.Foods.Update(updateFood);
            context.SaveChanges();
            LoadFoodList("");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadFoodList(txbSearchFoodName.Text);
        }

        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadFoodList("");
        }

        private void LoadCategories()
        {
            dtgvCategory.DataSource = context.FoodCategories.Select(fc => new
            {
                fc.Id,
                fc.Name,
            }).ToList();

            txbCategoryID.DataBindings.Clear();
            txbCategoryID.DataBindings.Add("Text", dtgvCategory.DataSource, "Id");

            txbCategoryName.DataBindings.Clear();
            txbCategoryName.DataBindings.Add("Text", dtgvCategory.DataSource, "Name");
        }
        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadCategories();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            FoodCategory foodCategory = new FoodCategory();
            foodCategory.Name = txbCategoryName.Text;

            context.FoodCategories.Add(foodCategory);
            context.SaveChanges();
            LoadCategories();
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            context.FoodCategories.Remove(context.FoodCategories.FirstOrDefault(fc => fc.Id == Int32.Parse(txbCategoryID.Text.ToString())));
            context.SaveChanges();
            LoadCategories();
        }

        private void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            FoodCategory foodCategory = context.FoodCategories.FirstOrDefault(fc => fc.Id == Int32.Parse(txbCategoryID.Text.ToString()));
            foodCategory.Id = Int32.Parse(txbCategoryID.Text.ToString());
            foodCategory.Name = txbCategoryName.Text;

            context.FoodCategories.Update(foodCategory);
            context.SaveChanges();
            LoadCategories();
        }
        private void LoadTables()
        {
            dtgvTable.DataSource = context.TableFoods.ToList();

            txbTableId.DataBindings.Clear();
            txbTableId.DataBindings.Add("Text", dtgvTable.DataSource, "Id");

            txbTableName.DataBindings.Clear();
            txbTableName.DataBindings.Add("Text", dtgvTable.DataSource, "Name");

            cbTableStatus.DataBindings.Clear();
            cbTableStatus.DataBindings.Add("Text", dtgvTable.DataSource, "Status");
        }
        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadTables();
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            TableFood tf = new TableFood();
            tf.Name = txbTableName.Text;
            tf.Status = cbTableStatus.Text;

            context.TableFoods.Add(tf);
            context.SaveChanges();
            LoadTables();
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            context.TableFoods.Remove(context.TableFoods.FirstOrDefault(tf => tf.Id == Int32.Parse(txbTableId.Text.ToString())));
            context.SaveChanges();
            LoadTables();
        }

        private void btnUpdateTable_Click(object sender, EventArgs e)
        {
            TableFood tf = context.TableFoods.FirstOrDefault(tf => tf.Id == Int32.Parse(txbTableId.Text.ToString()));
            tf.Name = txbTableName.Text;
            tf.Status = cbTableStatus.Text;

            context.TableFoods.Update(tf);
            context.SaveChanges();
            LoadTables();
        }

        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccountList();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdateAccount_Click(object sender, EventArgs e)
        {

        }
    }
}
