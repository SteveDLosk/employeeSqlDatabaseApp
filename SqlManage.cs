using System;


public class SqlManage
{
    private String connectionString = "Data Source=desktop-arqq1nh;Initial Catalog=Employee;Integrated Security=True";



    public static void CreateEmployee (String firstName, String lastName)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = connectionString;

        con.Open();

        SqlCommand command = new SqlCommand();
        command.Connection = con;
        command.CommandType = CommandType.Text;

        // Build command string
        StringBuilder sb = new StringBuilder();
        sb.Append("INSERT INTO Employee (firstName, LastName)");
        sb.Append(" VALUES ('");
        sb.Append(firstName);
        sb.Append("', '");
        sb.Append(lastName);
        sb.Append(")");
        String queryStr = sb.ToString();
        Debug.Log(queryStr);
        command.CommandText = queryStr;

        // execute
        command.ExecuteNonQuery();
        con.Close();

    }
}
