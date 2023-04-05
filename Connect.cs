using System;
using MySql.Data.MySqlClient;

namespace AllAzAlku2
{
    static class Connect
    {
        public static MySqlConnection InitDB()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.UserID = "root";
            builder.Password = "";
            builder.Database = "allazalku";
            MySqlConnection Con = new MySqlConnection(builder.ToString());

            try
            {
                Con.Open();
                return Con;
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(
                    "Hiba az adatbázishoz való csatlakozás közben:" +
                    Environment.NewLine +
                    ex.Message);
                return default(MySqlConnection);
            }
        }
    }
}
