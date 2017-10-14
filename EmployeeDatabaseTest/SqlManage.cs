using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

public class SqlManage
{
    private static String connectionString = "Data Source=desktop-arqq1nh;Initial Catalog=Employee;Integrated Security=True";
    

    public static void CreateEmployee (int id, String firstName, String lastName)
    {
        firstName = TruncateString(firstName);
        lastName = TruncateString(lastName);

        SqlConnection con = new SqlConnection();
        con.ConnectionString = connectionString;

        con.Open();
        // Find next employee ID
        
        SqlCommand command = new SqlCommand();
        command.Connection = con;
        command.CommandType = CommandType.Text;
        command.CommandText = "SELECT MAX(Id) FROM Employee2";
        SqlDataReader reader = command.ExecuteReader();
        /*
        if (reader.HasRows)
        {
          //  newId = reader.GetInt32(0);
        }
        else
        {
            newId = 1;
        }
        */
        reader.Close();

        // create new employee
        
        // Build command string
        StringBuilder sb = new StringBuilder();
        sb.Append("INSERT INTO Employee2 (id, firstName, LastName, yearsOfService, active)");
        sb.Append(" VALUES (");
        sb.Append(id);
        sb.Append(",'");
        sb.Append(firstName);
        sb.Append("', '");
        sb.Append(lastName);
        sb.Append("',0, 1)");
        
        String queryStr = sb.ToString();
        Debug.WriteLine(queryStr);
        command.CommandText = queryStr;

        // execute
        command.ExecuteNonQuery();
        con.Close();
        
    }

    private static String TruncateString (String s)
    {
        // The default in Sql Server was nvarChar(10).
        // I probably should have upped this limit.
        // Still useful, because even if the limit was 30, a user
        // could still enter more than 30 characters in the text box.
        // useful for removing garbage characters before entering name
        Char[] chars = s.ToCharArray();
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < chars.Length; i++)
        {
            if (Char.IsLetter(chars[i]))
            {

                sb.Append(chars[i]);
                if (sb.Length > 9)
                    break;
            }
        }
        
        return sb.ToString();
    }

    public static String ListAllEmployees()
    {

        String returnedData = "";
        SqlConnection connection = new SqlConnection();
        connection.ConnectionString = connectionString;

        connection.Open();

        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandType = CommandType.Text;
        command.CommandText = "Select * from Employee2 where active = 1";
        SqlDataReader reader = command.ExecuteReader();

        // check for empty table
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                String nextLine = String.Format("Id: {0} First Name: {1} , Last Name {2}", 
                reader["id"], reader["firstName"], reader["lastName"]);
                returnedData += nextLine;
                returnedData += "\n";
            }
        }
        reader.Close();
        connection.Close();

        return returnedData;
    }

    public static void DisableEmployee (String firstName, String lastName)
    {
        SqlConnection connection = new SqlConnection();
        connection.ConnectionString = connectionString;

        connection.Open();

        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandType = CommandType.Text;
        String text = String.Format("Update Employee2 set active = 0 where firstName LIKE '%{0}%'" +
            " and lastName LIKE '%{1}%'", firstName, lastName);
        command.CommandText = text;
        

        // execute
        command.ExecuteNonQuery();
        connection.Close();
    }

    public static void UpdateEmployee (int id, String firstName, String lastName)
    {
        SqlConnection connection = new SqlConnection();
        connection.ConnectionString = connectionString;

        connection.Open();

        // Build command
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandType = CommandType.Text;
        command.CommandText = "UPDATE Employee2 " +
            "SET firstName = '" + firstName + "', lastName = '" + lastName +
            "' WHERE id = " + id;
        
        // execute command
        command.ExecuteNonQuery();
        connection.Close();

    }
}
