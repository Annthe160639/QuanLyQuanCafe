using System;

public class DataProvider
{
    private string connectionSTR = "Data Source=ANN\\ANNT;Initial Catalog=QuanLyQuanCaPhe;User ID=g3;Trusted_Connection=true";
	public DataTable ExcuteQuery(string query)
	{
        SqlConnection connection = new SqlConnection(connectionSTR);

        connection.Open();

        SqlCommand command = new SqlCommand(query, connection);

        DataTable data = new DataTable();

        SqlDataAdapter adapter = new SqlDataAdapter(command);

        adapter.Fill(data);

        connection.Close();

    }
}
