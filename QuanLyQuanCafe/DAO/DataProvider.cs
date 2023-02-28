using System;
using System.Data;
using System.Data.SqlClient;

public class DataProvider
{
    private static DataProvider instance; //ctrl + r + e
    
    
    

    public static DataProvider Instance { 
        get 
        { 
            if(instance == null) instance= new DataProvider();
            return DataProvider.instance; 
        }
        private set => DataProvider.instance = value; }

    private DataProvider() { }

    private string connectionSTR = "Data Source=ANN\\ANNT;Initial Catalog=QuanLyQuanCaPhe;User ID=g3;Trusted_Connection=true";
    public DataTable ExecuteQuery(string query, object[] parameters = null)
	{
        DataTable data = new DataTable();

        using (SqlConnection connection = new SqlConnection(connectionSTR))
        {

            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            
            if(parameters != null)
            {
                string[] listParams = query.Split(' ');
                int i = 0;
                foreach(string param in listParams)
                {
                    if (param.Contains('@'))
                    {
                        Console.WriteLine(param);
                        command.Parameters.AddWithValue(param, parameters[i]);
                        i++;
                    }
                }
            }

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            adapter.Fill(data);

            connection.Close();
        }


        return data;

    }
    public int ExecuteNonQuery(string query, object[] parameters = null)
    {
        int data = 0;

        using (SqlConnection connection = new SqlConnection(connectionSTR))
        {

            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);

            if (parameters != null)
            {
                string[] listParams = query.Split(' ');
                int i = 0;
                foreach (string param in listParams)
                {
                    if (param.Contains('@'))
                    {
                        command.Parameters.AddWithValue(param, parameters[i]);
                        i++;
                    }
                }
            }

            data = command.ExecuteNonQuery();

            connection.Close();
        }


        return data;

    }
    public object ExecuteScalar(string query, object[] parameters = null)
    {
        object data = 0;

        using (SqlConnection connection = new SqlConnection(connectionSTR))
        {

            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);

            if (parameters != null)
            {
                string[] listParams = query.Split(' ');
                int i = 0;
                foreach (string param in listParams)
                {
                    if (param.Contains('@'))
                    {
                        command.Parameters.AddWithValue(param, parameters[i]);
                        i++;
                    }
                }
            }

            data = command.ExecuteScalar();

            connection.Close();
        }


        return data;

    }
}
