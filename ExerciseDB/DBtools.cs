using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ExerciseDB
{
    class DBtools : ItemSource
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["ItemsDB"].ConnectionString;
        private static SqlConnection sqlConnection = null;
        public List<Item> GetListItems()
        {
            Item item = null;
            List<Item> listItems = new List<Item>();

            if (sqlConnection==null || sqlConnection.State != ConnectionState.Open)
            {
                OpenConnect();
            }

            SqlDataReader sqlDataReader = null;
            string command = string.Empty;

            command = "SELECT name, hind, kogus FROM Items";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                item = new Item();
                item.Name = sqlDataReader["name"].ToString();
                item.Hind = decimal.Parse(sqlDataReader["hind"].ToString());
                item.Kogus = int.Parse(sqlDataReader["kogus"].ToString());
                listItems.Add(item);
            }
            if (sqlDataReader != null)
            {
                sqlDataReader.Close();
            }

            CloseConnect();

            return listItems;
        }
        private static void OpenConnect()
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
        } 

        private static void CloseConnect()
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public void updateKogus(string strItems)
        {
            if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
            {
                OpenConnect();
            }

            SqlDataReader sqlDataReader = null;
            string command = string.Empty;

            command = string.Format("UPDATE Items SET kogus=kogus-1 WHERE CHARINDEX(LEFT(name,1), '{0}')>0", strItems);

            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader != null)
            {
                sqlDataReader.Close();
            }

            CloseConnect();
        }

        public void InfoToConsole()
        {
            Console.WriteLine("---------- WORK WITH Database ---------- (A.Avanesov)\n");
        }

        public void InitSource(List<Item> listItems)
        {
            if (sqlConnection == null || sqlConnection.State != ConnectionState.Open)
            {
                OpenConnect();
            }

            SqlDataReader sqlDataReader = null;
            string command = string.Empty;

            command = "TRUNCATE TABLE Items";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader != null)
            {
                sqlDataReader.Close();
            }
            foreach (var item in listItems)
            {
                command = String.Format("INSERT INTO Items (name, hind, kogus) VALUES('{0}',{1}, {2})",
                    item.Name,
                    item.Hind.ToString().Replace(',','.'),
                    item.Kogus.ToString().Replace(',', '.')) ;
                SqlCommand sqlComm = new SqlCommand(command, sqlConnection);
                sqlDataReader = sqlComm.ExecuteReader();

                if (sqlDataReader != null)
                {
                    sqlDataReader.Close();
                }

            }

            CloseConnect();
            Console.WriteLine("OK INIT Database\n");
        }
    }
}
