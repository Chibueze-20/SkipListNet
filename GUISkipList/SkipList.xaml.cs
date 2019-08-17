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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SkipListNet;

namespace testing
{
     /// <summary>
     /// Interaction logic for SkipList.xaml
     /// </summary>
     public partial class SkipList : Page,IUpdater
     {
          public SkipList()
          {
               InitializeComponent();
               WindowWidth = 1000;
          }
          public void addStuff()
          {
               listField.Children.Clear();
               IList<List<Node<int>>> list = MainWindow.skiplist.SkipLists();
               for ( int j=list.Count-1;j>=0;j--)
               {
                    List<Node<int>> item = list[j];
                    if (item.Count>0)
                    {
                         StackPanel sp = new StackPanel();
                         sp.Orientation = Orientation.Horizontal;
                         for (int i = 0; i < item.Count - 1; i++)
                         {
                              NodeControl nc = new NodeControl(item[i], true);
                              nc.Margin = new Thickness(0, 5, 0, 5);
                              sp.Children.Add(nc);

                         }
                         NodeControl ncl = new NodeControl(item[item.Count - 1], false);
                         ncl.Margin = new Thickness(0, 5, 0, 5);
                         sp.Children.Add(ncl);
                         listField.Children.Add(sp);
                    }
               }
          }
          

          public void Update(int level, int nodeValue)
          {
               StackPanel panel = (StackPanel)listField.Children[level];
               NodeControl node = (NodeControl) panel.Children[nodeValue];
               Brush c = node.Background;
               node.Background = Brushes.Blue;

               
          }

          private void btninsert_Click(object sender, RoutedEventArgs e)
          {
               int value;
               bool x = int.TryParse(txtVal.Text,out value);
               if (x)
               {
                    bool done = MainWindow.skiplist.Insert(value);
                    if (done)
                    {
                         addStuff();
                    }
                    else
                    {
                         MessageBox.Show("Insertion Unsuccessful");
                    }
               }
               else
               {
                    MessageBox.Show("Bad input!");
               }
          }

          private void btndelete_Click(object sender, RoutedEventArgs e)
          {
               int value;
               bool x = int.TryParse(txtVal.Text, out value);
               if (x)
               {
                    bool done = MainWindow.skiplist.Delete(value);
                    if (done)
                    {
                         addStuff();
                    }
                    else
                    {
                         MessageBox.Show("Deletion Unsuccessful");
                    }
               }
               else
               {
                    MessageBox.Show("Bad input!");
               }
          }

          private void btnsearch_Click(object sender, RoutedEventArgs e)
          {
               int value;
               bool x = int.TryParse(txtVal.Text, out value);
               if (x)
               {
                    bool done = MainWindow.skiplist.Search(value);
                    if (done)
                    {
                         MessageBox.Show("Element found");
                    }
                    else
                    {
                         MessageBox.Show("Element not found");
                    }
               }
               else
               {
                    MessageBox.Show("Bad input!");
               }
          }

          private void btnhome_Click(object sender, RoutedEventArgs e)
          {
               NavigationService.Navigate(new Home());
          }
     }
}
