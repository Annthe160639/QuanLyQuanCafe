using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Bill
    {
        private int discount;
        private int status;

        private DateTime? dateCheckIn;
        
        private DateTime? dateCheckOut;
        private int id;
        public DateTime? DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public int Id { get => id; set => id = value; }
        public int Status { get => status; set => status = value; }
        public int Discount { get => discount; set => discount = value; }

        public Bill(int id, DateTime? dateCheckIn, DateTime? dateCheckOut, int status, int discount = 0)
        {
            DateCheckIn = dateCheckIn;
            DateCheckOut = dateCheckOut;
            Id = id;
            Status = status;
            Discount = discount;
            
        }

        public Bill(DataRow row) 
        {
            
            var dateCheckOutTemp = row["date_checkout"];
            
            if (dateCheckOutTemp.ToString() != "")
            {
                this.DateCheckOut = (DateTime?)dateCheckOutTemp;
            }
            this.DateCheckIn = (DateTime?)row["date_checkin"];
            this.Id = (int)row["id"];
            this.Status = (int)row["status"];
            this.Discount = (int)row["discount"];  
        }
    }
}
