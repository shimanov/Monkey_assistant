using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using MahApps.Metro.Controls;
using Monkey_assistant.Classes;

namespace Monkey_assistant
{
    /// <summary>
    /// Логика взаимодействия для SearchTransaction.xaml
    /// </summary>
    public partial class SearchTransaction : MetroWindow
    {
        public SearchTransaction()
        {
            InitializeComponent();
        }

        //Search transaction
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string trans = TransBox.Text;
            GetpcName getpcName = new GetpcName();
            SqlConnection connection = new SqlConnection("Server = localhost; "
                                                         + "Initial Catalog = " + getpcName.NamePc() + ";"
                                                         + "Integrated Security = SSPI");
            SqlCommand command = new SqlCommand
            {
                CommandText = "select * from RETAILTRANSACTIONTABLE where RECEIPTID ='" + trans + "'",
                CommandType = CommandType.Text,
                Connection = connection
            };
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                //выводим данные
                while (reader.Read())
                {
                    string grossamount = reader.GetString(18); //18
                    //ResultLabel.Text = grossamount;
                    Console.WriteLine(grossamount);
                    Console.ReadKey();
                }
                reader.Close();
                //Logger.Log.Info("Успешно подключились к БД " + NamePc() + "для выполнения скрипта ReplicaExport");
                command.CommandTimeout = 240;
                command.ExecuteNonQuery();
                connection.Close();
                //Logger.Log.Info("Успешно выполнил скрипт ReplicaExport на БД " + NamePc() + "соединение закрыл");
            }
            catch (Exception exception)
            {
                //Logger.Log.Error(exception.Message);
            }
        }

        //Удаление транзакции
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GetpcName getpcName = new GetpcName();
            SqlConnection connection = new SqlConnection("Server = localhost; "
                                                         + "Initial Catalog = " + getpcName.NamePc() + ";"
                                                         + "Integrated Security = SSPI");

            SqlCommand command = new SqlCommand
            {
                CommandText = "delete from RETAILTRANSACTIONTABLE where RECEIPTID ='" + TransBox.Text + "'",
                CommandType = CommandType.Text,
                Connection = connection
            };
            try
            {
                connection.Open();

                //Logger.Log.Info("Успешно подключились к БД " + NamePc() + "для выполнения скрипта ReplicaExport");
                command.CommandTimeout = 240;
                command.ExecuteNonQuery();
                connection.Close();
                //Logger.Log.Info("Успешно выполнил скрипт ReplicaExport на БД " + NamePc() + "соединение закрыл");
                MessageBox.Show("Зависшая транзакция удалена.");
            }
            catch (Exception exception)
            {
                //Logger.Log.Error(exception.Message);
            }
        }
    }
}
