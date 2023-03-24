using Microsoft.EntityFrameworkCore;
using QuanLyQuanCafe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class BillInfoDAO
    {
        QuanLyQuanCaPheContext context = new QuanLyQuanCaPheContext();

        public BillInfoDAO() { }

        public List<BillInfo> GetMenu(int tableId)
        {
            return context.BillInfos.Include(b => b.Bill).Include(bi => bi.Food).Where(b => b.Bill.TableId == tableId && b.Bill.Status == 0).ToList();
        }
    }
}
