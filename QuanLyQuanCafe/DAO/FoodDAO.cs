using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        internal static FoodDAO Instance
        {
            get
            {
                if (instance == null) instance = new FoodDAO(); return instance;
            }
            set => instance = value;
        }
        private FoodDAO() { }
        internal List<Food> getFoodByCategoryId(int id)
        {
            List<Food> list = new List<Food >();
            string query = "select * from Food where category_id = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                Food food = new Food(row);
                list.Add(food);
            }
            return list;
        }
    }
}
