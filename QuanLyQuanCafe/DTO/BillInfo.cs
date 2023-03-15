using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    internal class BillInfo
    {
        private int iD;

        public int ID { get => iD; set => iD = value; }
        public int BillId { get => billId; set => billId = value; }
        public int FoodId { get => foodId; set => foodId = value; }
        public int Count { get => count; set => count = value; }

        private int billId;
        private int foodId;
        private int count;
        public BillInfo(int iD, int billId, int foodID, int count) 
        {
            this.ID= iD;
            this.BillId= billId;
            this.FoodId= foodID;
            this.Count = count;

        }
        public BillInfo (DataRow row)
        {
            this.ID = (int)row["id"];
            this.BillId = (int)row["bill_id"];
            this.FoodId = (int)row["food_id"];
            this.Count = (int)row["count"];
        }
        
    }
}
