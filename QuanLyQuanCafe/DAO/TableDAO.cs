using QuanLyQuanCafe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class TableDAO
    {
        QuanLyQuanCaPheContext context = new QuanLyQuanCaPheContext();
        public static string EMPTY_TABLE = "Trống";
        public static string USING_TABLE = "Có người";

        public TableDAO() { }

        public void changeTableStatus(int tableId, string status)
        {
            TableFood table = context.TableFoods.FirstOrDefault(t => t.Id == tableId);
            if (table != null)
            {
                    table.Status = status;
            }
            context.TableFoods.Update(table);
            context.SaveChanges();
        }

        public List<TableFood> getTableFoods()
        {
            return context.TableFoods.ToList();
        }
    }
}
