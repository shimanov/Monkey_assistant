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
using System.Windows.Forms;
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
            DialogResult result = System.Windows.Forms.MessageBox.Show("ВНИМАНИЕ!",
                                                    "НЕОБХОДИМО ЗАВЕРШИТЬ РАБОТУ НА ВСЕХ ОКНАХ, ВКЛЮЧАЯ ДОСТАВКУ!",
                                                    MessageBoxButtons.YesNo);

            //Переводим в однопользовательский режим.
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                GetpcName getpcName = new GetpcName();
                SqlConnection connection = new SqlConnection("Server = localhost; "
                                                             + "Initial Catalog = " + getpcName.NamePc() + ";"
                                                             + "Integrated Security = SSPI");

                #region Переводим БД в однопользовательский режим
                SqlCommand command = new SqlCommand
                {
                    CommandText = "ALTER DATABASE " + "DB" + getpcName.NamePc() + "SET SINGLE_USER",
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

                #endregion

                #region Восстанавливаем БД

                SqlCommand command1 = new SqlCommand
                {
                    CommandText = "DBCC CHECKDB (" + "'DB" + getpcName.NamePc() + "'" + ",REPAIR_ALLOW_DATA_LOSS);",
                    CommandType = CommandType.Text,
                    Connection = connection
                };
                try
                {
                    connection.Open();

                    //Logger.Log.Info("Успешно подключились к БД " + NamePc() + "для выполнения скрипта ReplicaExport");
                    command1.CommandTimeout = 240;
                    command1.ExecuteNonQuery();
                    connection.Close();
                    //Logger.Log.Info("Успешно выполнил скрипт ReplicaExport на БД " + NamePc() + "соединение закрыл");
                }
                catch (Exception exception)
                {
                    //Logger.Log.Error(exception.Message);
                }

                #endregion

                //TODO: Реализовать вывод процента выполнения
                #region Выводим процент выполнения

                SqlCommand command3 = new SqlCommand
                {
                    CommandText = "SELECT [command],[start_time],[percent_complete] FROM sys.dm_exec_requests WHERE [command] LIKE '%DBCC%'",
                    CommandType = CommandType.Text,
                    Connection = connection
                };
                try
                {
                    connection.Open();
                    SqlDataReader reader = command3.ExecuteReader();
                    while (reader.Read())
                    {
                        var percent = reader.GetDouble(2);
                        percentLabel.Content = percent;
                    }

                    //Logger.Log.Info("Успешно подключились к БД " + NamePc() + "для выполнения скрипта ReplicaExport");
                    command3.CommandTimeout = 240;
                    command3.ExecuteNonQuery();
                    connection.Close();
                    //Logger.Log.Info("Успешно выполнил скрипт ReplicaExport на БД " + NamePc() + "соединение закрыл");
                }
                catch (Exception exception)
                {
                    //Logger.Log.Error(exception.Message);
                }

                #endregion

                #region Переводим в многопользовательский режим

                SqlCommand command2 = new SqlCommand
                {
                    CommandText = "ALTER DATABASE " + "DB" + getpcName.NamePc() + "SET MULTI_USER",
                    CommandType = CommandType.Text,
                    Connection = connection
                };
                try
                {
                    connection.Open();

                    //Logger.Log.Info("Успешно подключились к БД " + NamePc() + "для выполнения скрипта ReplicaExport");
                    command2.CommandTimeout = 240;
                    command2.ExecuteNonQuery();
                    connection.Close();
                    //Logger.Log.Info("Успешно выполнил скрипт ReplicaExport на БД " + NamePc() + "соединение закрыл");
                }
                catch (Exception exception)
                {
                    //Logger.Log.Error(exception.Message);
                }

                #endregion

                #region Переводим в онлайн режим

                SqlCommand command4 = new SqlCommand
                {
                    CommandText = "ALTER DATABASE " + "DB" + getpcName.NamePc() + "SET ONLINE",
                    CommandType = CommandType.Text,
                    Connection = connection
                };
                try
                {
                    connection.Open();

                    //Logger.Log.Info("Успешно подключились к БД " + NamePc() + "для выполнения скрипта ReplicaExport");
                    command4.CommandTimeout = 240;
                    command4.ExecuteNonQuery();
                    connection.Close();
                    //Logger.Log.Info("Успешно выполнил скрипт ReplicaExport на БД " + NamePc() + "соединение закрыл");
                }
                catch (Exception exception)
                {
                    //Logger.Log.Error(exception.Message);
                }

                #endregion
            }

            //Закрываем окно, если нажали - нет
            else
            {
                RepairDBWindow repairDBWindow = new RepairDBWindow();
                repairDBWindow.Close();
            }

            
        }
    }
}
