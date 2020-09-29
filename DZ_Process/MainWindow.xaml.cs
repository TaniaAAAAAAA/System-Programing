using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DZ_Process
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> list_NoStart = new ObservableCollection<string>();
        public ObservableCollection<string> list_Start = new ObservableCollection<string>();
        public MainWindow()
        {
            InitializeComponent();

            DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory());
             
            foreach (var item in dir.GetFiles())
            {
                if(item.Extension.Equals(".exe"))
                list_NoStart.Add(item.Name);
            }

            lb_NoStart.ItemsSource = list_NoStart;
            lb_Start.ItemsSource = list_Start;
        }

        private void lb_NoStart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb_NoStart.SelectedIndex == -1)
                return;
            txtName.Text = lb_NoStart.SelectedItem.ToString() ;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Process proc = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = txtName.Text;
            proc.StartInfo = startInfo;
            proc.Start();
            list_NoStart.Remove(txtName.Text);
            list_Start.Add(txtName.Text);



        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            
            foreach (var process in Process.GetProcesses())
            {
                if (process.ProcessName == txtName.Text.TrimEnd('.', 'e', 'x', 'e')) 
                {
                    process.Kill();
                    list_NoStart.Add(txtName.Text);
                    list_Start.Remove(txtName.Text);
                }
            }

        }
    }
}
