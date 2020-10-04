using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Async_Operations
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<string> path = new ObservableCollection<string>();
        string ToPath = "";
        string fromPath = "";
       static DirectoryInfo sourceDir;
       static DirectoryInfo destinationDir;
        public MainWindow()
        {
            InitializeComponent();
            lb_paths.ItemsSource = path;
        }


        private void btnFrom_Click(object sender, RoutedEventArgs e)
        {
            path.Clear();
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fromPath = FBD.SelectedPath;
                sourceDir = new DirectoryInfo(fromPath);
                path.Add("From : "+fromPath);

            }
        }

        private void btnTo_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();

            if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ToPath = FBD.SelectedPath;
                destinationDir = new DirectoryInfo(ToPath);

                path.Add("To : "+ToPath);

            }
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            Thread t1 = new Thread(CopyDirectory);
            t1.Start();
            Thread t2 = new Thread(WorkProgressBar);
            t2.Start();

        }

        private  void CopyDirectory(object ob)
        {
            if (!destinationDir.Exists)
            {
                destinationDir.Create();
            }

            FileInfo[] files = sourceDir.GetFiles();
            foreach (FileInfo file in files)
            {
                file.CopyTo(System.IO.Path.Combine(destinationDir.FullName,
                    file.Name));
            }

            DirectoryInfo[] dirs = sourceDir.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                string destinationD = System.IO.Path.Combine(destinationDir.FullName, dir.Name);

                CopyDirectory(ob);
            }
          
        }

        private static long GetDirectorySize()
        {
            long size = 0;

            foreach (var item in sourceDir.GetFiles())
                size += item.Length;

            foreach (var item in sourceDir.GetDirectories())
                size += GetDirectorySize();

            return size;
        }

        private void WorkProgressBar(object ob)
        {
            long size = Convert.ToInt64(ob);
            Dispatcher.Invoke(() => { progressBar.Maximum = size; });
            progressBar.Maximum = GetDirectorySize();
            for (int i = 0; i < size; i++)
            {
                Dispatcher.Invoke(() => { progressBar.Value = i; });
            }
        }
    }
}
