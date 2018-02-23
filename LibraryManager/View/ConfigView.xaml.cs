using LibraryManager.Data.Member;
using LibraryManager.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Automation;
using System.Windows.Data;
using MahApps.Metro.Controls;

namespace LibraryManager.View
{
    /// <summary>
    /// Interaction logic for ConfigView.xaml
    /// </summary>
    public partial class ConfigView : UserControl
    {
        public ConfigView()
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            InitializeComponent();
            this.DataContext = new ConfigViewModel();
            Configuration Config = ((ConfigViewModel)DataContext).Configuration;

            // Initialize row definitions
            layoutGrid.RowDefinitions.Clear();
            layoutGrid.RowDefinitions.Add(CreateAutoRowDefinition()); // MemberType headers
            foreach (Configuration.Entry<ushort> discreteEntry in Config.DiscreteEntries)
            {
                layoutGrid.RowDefinitions.Add(CreateAutoRowDefinition());
            }
            foreach (Configuration.Entry<decimal> decimalEntry in Config.DecimalEntries)
            {
                layoutGrid.RowDefinitions.Add(CreateAutoRowDefinition());
            }
            layoutGrid.RowDefinitions.Add(CreateRowDefinition(10d)); // Value/commit spacing
            layoutGrid.RowDefinitions.Add(CreateAutoRowDefinition()); // Commit Changes button
            layoutGrid.RowDefinitions.Add(CreateAutoRowDefinition()); // Reset Changes button

            // Initialize column definitions
            layoutGrid.ColumnDefinitions.Clear();
            layoutGrid.ColumnDefinitions.Add(CreateAutoColumnDefinition()); // Property headers
            foreach (MemberType type in Enum.GetValues(typeof(MemberType)))
            {
                layoutGrid.ColumnDefinitions.Add(CreateAutoColumnDefinition(120d)); // Textbox/valueUpDn
                layoutGrid.ColumnDefinitions.Add(CreateAutoColumnDefinition()); // Unit label
                layoutGrid.ColumnDefinitions.Add(CreateColumnDefinition(1d)); // Spacing
            }

            // Create MemberType headers
            for(int i = 0; i < Enum.GetValues(typeof(MemberType)).Length; ++i)
            {
                MemberType type = (MemberType) Enum.GetValues(typeof(MemberType)).GetValue(i);
                Label mtLabel = new Label
                {
                    Content = (type.ToString() + "s:"),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Bottom,
                    Margin = new Thickness(20d, 0d, 20d, 0d),
                    FontWeight = FontWeights.Bold
                };
                Grid.SetColumn(mtLabel, 1 + 3 * i);
                AutomationProperties.SetIsColumnHeader(mtLabel, true);
                layoutGrid.Children.Add(mtLabel);
            }

            // Create ConfigurationEntry headers
            for (int i = 0; i < Config.DiscreteEntries.Count; ++i)
            {
                Configuration.Entry<ushort> discreteConfigEntry = Config.DiscreteEntries.ElementAt(i);
                Label discreteCELabel = new Label
                {
                    Content = (discreteConfigEntry.Label + ":"),
                    HorizontalContentAlignment = HorizontalAlignment.Right,
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                Grid.SetRow(discreteCELabel, 1 + i);
                AutomationProperties.SetIsRowHeader(discreteCELabel, true);
                layoutGrid.Children.Add(discreteCELabel);
            }
            for (int i = Config.DiscreteEntries.Count; i < Config.DiscreteEntries.Count + Config.DecimalEntries.Count; ++i)
            {
                Configuration.Entry<decimal> decimalConfigEntry = Config.DecimalEntries.ElementAt(i - Config.DiscreteEntries.Count);
                Label decimalCELabel = new Label
                {
                    Content = (decimalConfigEntry.Label + ":"),
                    HorizontalContentAlignment = HorizontalAlignment.Right,
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                Grid.SetRow(decimalCELabel, 1 + i);
                AutomationProperties.SetIsRowHeader(decimalCELabel, true);
                layoutGrid.Children.Add(decimalCELabel);
            }

            // Create propertyID x memberType group content
            for (int i = 0; i < Enum.GetValues(typeof(MemberType)).Length; ++i)
            {
                int columnStart = 1 + (3 * i);
                MemberType type = (MemberType)Enum.GetValues(typeof(MemberType)).GetValue(i);
                for (int j = 0; j < Config.DiscreteEntries.Count; ++j)
                {
                    int rowStart = 1 + j;
                    Configuration.Entry<ushort> discreteConfigEntry = Config.DiscreteEntries.ElementAt(j);

                    NumericUpDown discreteValueEntryNUD = new NumericUpDown
                    {
                        Minimum = 0,
                        Maximum = ushort.MaxValue,
                        Margin = new Thickness(4d),
                        VerticalAlignment = VerticalAlignment.Center,
                        HasDecimals = true
                    };
                    discreteValueEntryNUD.ValueChanged += Config_ValueChanged;
                    Binding discreteValueEntryBinding = new Binding
                    {
                        Source = ((ConfigViewModel)this.DataContext).DiscreteValues[type][discreteConfigEntry.ID],
                        Path = new PropertyPath("CurrentValue"),
                        Mode = BindingMode.TwoWay,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    };
                    discreteValueEntryNUD.SetBinding(NumericUpDown.ValueProperty, discreteValueEntryBinding);
                    Grid.SetRow(discreteValueEntryNUD, rowStart);
                    Grid.SetColumn(discreteValueEntryNUD, columnStart);
                    layoutGrid.Children.Add(discreteValueEntryNUD);

                    Label discreteValueUnitsLabel = new Label
                    {
                        Margin = new Thickness(0d, 0d, 2d, 0d),
                        Content = discreteConfigEntry.Units,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    Grid.SetRow(discreteValueUnitsLabel, rowStart);
                    Grid.SetColumn(discreteValueUnitsLabel, columnStart + 1);
                    layoutGrid.Children.Add(discreteValueUnitsLabel);
                }
                for (int j = Config.DiscreteEntries.Count; j < Config.DiscreteEntries.Count + Config.DecimalEntries.Count; ++j)
                {
                    int rowStart = 1 + j;
                    Configuration.Entry<decimal> decimalConfigEntry = Config.DecimalEntries.ElementAt(j - Config.DiscreteEntries.Count);

                    NumericUpDown decimalValueEntryNUD = new NumericUpDown
                    {
                        Minimum = 0,
                        Maximum = (double) decimal.MaxValue,
                        Margin = new Thickness(4d),
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Right,
                        VerticalAlignment = VerticalAlignment.Center,
                        StringFormat = "0.00"
                    };
                    decimalValueEntryNUD.ValueChanged += Config_ValueChanged;
                    Binding decimalValueEntryBinding = new Binding
                    {
                        Source = ((ConfigViewModel)this.DataContext).DecimalValues[type][decimalConfigEntry.ID],
                        Path = new PropertyPath("CurrentValue"),
                        Mode = BindingMode.TwoWay,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    };
                    decimalValueEntryNUD.SetBinding(NumericUpDown.ValueProperty, decimalValueEntryBinding);
                    Grid.SetRow(decimalValueEntryNUD, rowStart);
                    Grid.SetColumn(decimalValueEntryNUD, columnStart);
                    layoutGrid.Children.Add(decimalValueEntryNUD);

                    Label decimalValueUnitsLabel = new Label
                    {
                        Margin = new Thickness(0d, 0d, 2d, 0d),
                        Content = decimalConfigEntry.Units,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    Grid.SetRow(decimalValueUnitsLabel, rowStart);
                    Grid.SetColumn(decimalValueUnitsLabel, columnStart + 1);
                    layoutGrid.Children.Add(decimalValueUnitsLabel);
                }
            }

            // Control buttons
            int controlButtonsRowStart = Config.DiscreteEntries.Count + Config.DecimalEntries.Count + 2;
            int controlButtonsColumnSpan = (Enum.GetValues(typeof(MemberType)).Length) * 3 + 1;
            Button commitChangedBtn = new Button
            {
                Content = "Commit Changes",
                Margin = new Thickness(4d)
            };
            commitChangedBtn.SetBinding(IsEnabledProperty, new Binding("ConfigChanged") { Source = this.DataContext });
            commitChangedBtn.Click += CommitChangesPressed;
            Grid.SetRow(commitChangedBtn, controlButtonsRowStart);
            Grid.SetColumn(commitChangedBtn, 1);
            Grid.SetColumnSpan(commitChangedBtn, controlButtonsColumnSpan);
            layoutGrid.Children.Add(commitChangedBtn);

            Button resetChangesBtn = new Button
            {
                Content = "Reset Changes",
                Margin = new Thickness(4d)
            };
            resetChangesBtn.SetBinding(IsEnabledProperty, new Binding("ConfigChanged") { Source = this.DataContext });
            resetChangesBtn.Click += ResetChangesPressed;
            Grid.SetRow(resetChangesBtn, controlButtonsRowStart + 1);
            Grid.SetColumn(resetChangesBtn, 1);
            Grid.SetColumnSpan(resetChangesBtn, controlButtonsColumnSpan);
            layoutGrid.Children.Add(resetChangesBtn);
        }

        private RowDefinition CreateAutoRowDefinition(double minHeight = 0d)
        {
            RowDefinition rd = new RowDefinition
            {
                Height = GridLength.Auto,
                MinHeight = minHeight
            };
            return rd;
        }

        private RowDefinition CreateRowDefinition(double height, double minHeight = 0d)
        {
            RowDefinition rd = new RowDefinition
            {
                
                Height = new GridLength(height),
                MinHeight = minHeight
            };
            return rd;
        }

        private ColumnDefinition CreateAutoColumnDefinition(double minWidth = 0d)
        {
            ColumnDefinition cd = new ColumnDefinition
            {
                Width = GridLength.Auto,
                MinWidth = minWidth
            };
            return cd;
        }

        private ColumnDefinition CreateColumnDefinition(double width, double minWidth = 0d)
        {
            ColumnDefinition cd = new ColumnDefinition
            {
                Width = new GridLength(width),
                MinWidth = minWidth
            };
            return cd;
        }

        private void Config_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            ((ConfigViewModel)this.DataContext).ForcePropertyChanged("ConfigChanged");
        }

        private void CommitChangesPressed(object sender, RoutedEventArgs e)
        {
            ((ConfigViewModel)this.DataContext).CommitChanges();
        }
        
        private void ResetChangesPressed(object sender, RoutedEventArgs e)
        {
            ((ConfigViewModel)this.DataContext).ResetConfigurationCache();
        }
    }
}
