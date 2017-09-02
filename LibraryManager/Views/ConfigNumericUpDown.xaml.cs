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

namespace LibraryManager.Views
{
    /// <summary>
    /// Interaction logic for ConfigNumericUpDown.xaml
    /// </summary>
    public partial class ConfigNumericUpDown : UserControl
    {
        public ConfigNumericUpDown()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty UnitsProperty = DependencyProperty.Register("Units", typeof(String), typeof(ConfigNumericUpDown), new PropertyMetadata("days"));
        public String Units
        {
            get
            {
                return (String)this.GetValue(UnitsProperty);
            }
            set
            {
                this.SetValue(UnitsProperty, value);
            }
        }
    }
}
