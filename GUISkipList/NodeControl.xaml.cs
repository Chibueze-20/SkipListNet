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

namespace testing
{
     /// <summary>
     /// Interaction logic for NodeControl.xaml
     /// </summary>
     public partial class NodeControl : UserControl
     {
          public NodeControl()
          {
               InitializeComponent();
          }
          public NodeControl(object value,bool next)
          {
               InitializeComponent();
               NodeValue.Text = value.ToString();
               if (!next)
               {
                    pointer.Visibility = Visibility.Collapsed;
               }
               Width = 100;
          }
     }
}
