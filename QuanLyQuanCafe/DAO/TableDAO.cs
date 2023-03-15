using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    internal class TableDAO
    {
        private static TableDAO instance;
        public static TableDAO Instance
        {
            get { if(instance == null) instance = new TableDAO(); return instance;}
            set { instance = value; }
        }
        public static int TableWidth = 100;
        public static int TableHeight = 100;
        public void SwitchTable(int id1, int id2)
        {
            DataProvider.Instance.ExecuteQuery("exec USP_SwitchTable @idTable1 , @idTable2", new object[] {id1, id2});
        }
        public List<Table> LoadTableList()
        {
            List<Table> list = new List<Table>();

            DataTable dataTable= DataProvider.Instance.ExecuteQuery("exec USP_GetTableList");
            foreach (DataRow row in dataTable.Rows)
            {
                Table table = new Table(row);
                list.Add(table);
            }
            return list;
        }

    }
}
