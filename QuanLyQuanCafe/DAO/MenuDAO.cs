using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyQuanCafe.DTO;

namespace QuanLyQuanCafe.DAO
{
    class MenuDAO
    {
        private static MenuDAO instance;

        internal static MenuDAO Instance { 
            get
            {
                if(instance == null) instance= new MenuDAO();
                return instance;
            } 
            set => instance = value; }
        private MenuDAO() { }
        public List<Menu> GetMenuByTableId(int id) 
        { 
            List<Menu> list = new List<Menu>();
            string query = "select f.name, bi.count, f.price, f.price * bi.count as totalPrice from BillInfo as bi, Bill as b, Food as f \r\nwhere bi.bill_id = b.id and bi.food_id = f.id and b.status = 0 and b.table_id = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                Menu menu = new Menu(row);
                list.Add(menu);
            }
            return list;
        }
    }
}
