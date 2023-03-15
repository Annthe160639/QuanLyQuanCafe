using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    class CategoryDAO
    {
        private static CategoryDAO instance;

        internal static CategoryDAO Instance 
        { get
            {
                if(instance == null) instance= new CategoryDAO(); return instance;
            } 
            set => instance = value; 
        }
        private CategoryDAO() { }
        public List<Category> getListCategory()
        {
            List<Category> listCategory = new List<Category>();
            string query = "select * from FoodCategory";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach(DataRow row in data.Rows)
            {
                Category category = new Category(row);
                listCategory.Add(category);
            }
            return listCategory;
        }
    }
}
