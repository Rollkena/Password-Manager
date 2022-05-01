using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Password_Manager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Closing Manager", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                e.Cancel = true;
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        public static void Warning()
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Повреждены файлы программы\nЗагрузите предыдущую сохранённую версию или живите в проклятом мире, который сами и создали", "Error");
        }
        public static void WarningAllowDelete()
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Please, reset search and filter to delete account.", "Error");
        }
        public static int SwitchDel = 0;

        public static void Warning_Delete()
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Closing Manager", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                SwitchDel = 1;
            }
            else
            {
                SwitchDel = 0;
            }
        }

    }
}
