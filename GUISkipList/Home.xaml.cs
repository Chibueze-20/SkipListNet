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
using SkipListNet;

namespace testing
{
     /// <summary>
     /// Interaction logic for Home.xaml
     /// </summary>
     public partial class Home : Page
     {
          public Home()
          {
               InitializeComponent();
          }

          private void btnstart_Click(object sender, RoutedEventArgs e)
          {
               if (perfectSkipList.IsChecked.Value)
               {
                    MainWindow.skiplist = new SkipList<int>(int.MinValue, int.MaxValue, 0);
                    NavigationService.Navigate(new SkipList());
               }
               else if (randomSkipList.IsChecked.Value)
               {
                    MainWindow.skiplist = new SkipList<int>(int.MinValue, int.MaxValue, 0, true);
                    NavigationService.Navigate(new SkipList());
               }
               else
               {
                    MessageBox.Show("Select a type of skip list!");
               }
          }
     }
}
