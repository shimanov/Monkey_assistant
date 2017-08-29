using System;
using System.Diagnostics;
using System.Windows;
using MahApps.Metro.Controls;

namespace Monkey_assistant
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CheckDb check = new CheckDb();
            check.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Process pr = new Process();
            pr.StartInfo.UseShellExecute = false;
            pr.StartInfo.RedirectStandardOutput = true;
            pr.StartInfo.FileName = "C:\\Program Files\\Microsoft SQL Server\\110\\Setup Bootstrap\\SQLServer2012\\setup.exe";
            pr.StartInfo.Arguments = "/Q /ACTION=editionupgrade /INSTANCENAME=MSSQLSERVER  /PID=YFC4R-BRRWB-TVP9Y-6WJQ9-MCJQ7 /IACCEPTSQLSERVERLICENSETERMS /Indicateprogress";
            pr.Start();
            string output = pr.StandardOutput.ReadToEnd();
            Console.WriteLine(output);
            pr.WaitForExit();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SearchTransaction searchTransaction = new SearchTransaction();
            searchTransaction.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            RepairDBWindow repair = new RepairDBWindow();
            repair.Show();
        }
    }
}
