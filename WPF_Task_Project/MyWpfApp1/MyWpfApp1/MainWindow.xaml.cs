using System;
using System.Collections.Generic;
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

namespace MyWpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Work work = new Work();
            //work.LoadData();
            //TV1.DataContext = work;
            //ViemModel vm = new ViemModel();
            //TV1.ItemsSource = vm.Nodes.Where(x => x.parent_id == null); 
            ViemModel treeContext = new ViemModel();
            TV1.ItemsSource = treeContext.ViewSourceVisual.Where(x => x.parent_id == null).ToList();
        }
    }

    
}
