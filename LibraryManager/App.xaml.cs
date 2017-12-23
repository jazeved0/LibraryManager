using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace LibraryManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static App Instance;
        public Configuration Config;

        public App()
        {
            Instance = this;
            Config = new Configuration();
        }

        public static DependencyObject FindChild(DependencyObject parent, Func<DependencyObject, bool> predicate)
        {
            if (parent == null) return null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (predicate(child))
                {
                    return child;
                }
                else
                {
                    var foundChild = FindChild(child, predicate);
                    if (foundChild != null)
                        return foundChild;
                }
            }

            return null;
        }
    }
}
