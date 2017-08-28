using Monkey_assistant.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Monkey_assistant
{
    /// <summary>
    /// Логика взаимодействия для RepairDBWindow.xaml
    /// </summary>
    public partial class RepairDBWindow : Window
    {
        public RepairDBWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetpcName getpcName = new GetpcName();
            SqlConnection connection = new SqlConnection("Server = localhost; "
                                                         + "Initial Catalog = " + getpcName.NamePc() + ";"
                                                         + "Integrated Security = SSPI");

            SqlCommand command = new SqlCommand
            {
                CommandText = "DBCC CHECKDB (" + "'DB" + getpcName.NamePc() + "'" + ",REPAIR_ALLOW_DATA_LOSS);",
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
            }
            catch (Exception exception)
            {
                //Logger.Log.Error(exception.Message);
            }
        }
    }
}
