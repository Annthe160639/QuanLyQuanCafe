using Microsoft.EntityFrameworkCore;
using QuanLyQuanCafe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class BillDAO
    {
        QuanLyQuanCaPheContext context = new QuanLyQuanCaPheContext();

        public BillDAO() { }

        public List<Bill> getBillListBetweenDate(DateTime fromDate, DateTime endDate)
        {
            List<Bill> bills = context.Bills.Include(b => b.BillInfos).Where(b => b.DateCheckin >= fromDate && endDate <= b.DateCheckout).ToList();
            
            return bills;  
        }

        public double CalcPriceBillInfos(List<BillInfo> billInfos)
        {
            double total = 0;
            billInfos.ForEach(billInfo =>
            {
                Food f = context.Foods.FirstOrDefault(f => f.Id == billInfo.FoodId);

                if (f is not null)
                {
                    total += f.Price * billInfo.Count;
                }
            });

            return total;
        }
    }
}
