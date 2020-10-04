using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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

namespace DZ_AppDomain
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            lb_Assembly.ItemsSource = assembly;
            lb_Classes.ItemsSource = classs;
            lb_Method.ItemsSource = methods;
        }
        public static ObservableCollection<string> assembly = new ObservableCollection<string>();
        public static ObservableCollection<string> classs = new ObservableCollection<string>();
        public static ObservableCollection<string> methods = new ObservableCollection<string>();

           public static AppDomain app = AppDomain.CurrentDomain;
        private void btnLoadAssembly_Click(object sender, RoutedEventArgs e)
        {
            foreach (Assembly item in app.GetAssemblies())
            {
                foreach (var module in item.GetModules())
                {
                    assembly.Add(module.Name);

                }

            }
        }

        private void lb_Assembly_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            classs.Clear();
            foreach (Assembly item in app.GetAssemblies())
            {
                foreach (var module in item.GetModules())
                {
                    if (module.Name == lb_Assembly.SelectedItem.ToString())
                    {
                        foreach (Type t in module.GetTypes())
                        {

                            classs.Add(t.ToString());
                        }
                    }
                }

            }
        }
      

        private void lb_Classes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            methods.Clear();
            var r = app.GetAssemblies();
            foreach (Assembly item in app.GetAssemblies())
            {
                foreach (var module in item.GetModules())
                {
                    if (module.Name == lb_Assembly.SelectedItem.ToString())
                    {
                        foreach (Type t in module.GetTypes())
                        {
                            if (t.FullName == lb_Classes.SelectedItem.ToString())
                            {
                                foreach (MethodInfo mi in t.GetMethods())
                                {
                                    methods.Add(mi.Name);

                                }
                            }
                        }
                    }
                }

            }
        }
    }
 
}
