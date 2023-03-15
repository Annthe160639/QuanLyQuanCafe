using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public float Price { get; set; }

        public Food(int id, string name, int categoryId, float price)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
            Price = price;
        }
        public Food(DataRow row)
        {
            Id = (int)row["id"];
            Name = (string)row["name"];
            CategoryId = (int)row["category_id"];
            Price = (float)Convert.ToDouble((row["price"]).ToString());
        }
    }
}
