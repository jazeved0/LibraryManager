using LibraryManager.Data.Member;
using System;
using System.Collections.Generic;

namespace LibraryManager.ViewModels
{
    class ConfigViewModel : NotifyPropertyChanged
    {
        public IDictionary<MemberType, Dictionary<String, Configuration.Value<ushort>>> DiscreteValues { get; internal set; }
        public IDictionary<MemberType, Dictionary<String, Configuration.Value<decimal>>> DecimalValues { get; internal set; }

        internal Configuration Configuration { get { return App.Instance.Config; } }

        public ConfigViewModel()
        {
            DiscreteValues = new Dictionary<MemberType, Dictionary<String, Configuration.Value<ushort>>>();
            DecimalValues = new Dictionary<MemberType, Dictionary<String, Configuration.Value<decimal>>>();

            // Initialize group (membertype) cache dictionaries to config values
            foreach (MemberType type in Enum.GetValues(typeof(MemberType)))
            {
                DiscreteValues[type] = new Dictionary<string, Configuration.Value<ushort>>();
                DecimalValues[type] = new Dictionary<string, Configuration.Value<decimal>>();

                // Deep clone discrete config values from the existing config
                foreach (Configuration.Entry<ushort> configEntry in Configuration.DiscreteEntries)
                {
                    DiscreteValues[type].Add(configEntry.ID, (Configuration.Value<ushort>)Configuration.DiscreteValues[type][configEntry.ID].Clone());
                }

                // Deep clone decimal config values from the existing config
                foreach (Configuration.Entry<decimal> configEntry in Configuration.DecimalEntries)
                {
                    DecimalValues[type].Add(configEntry.ID, (Configuration.Value<decimal>)Configuration.DecimalValues[type][configEntry.ID].Clone());
                }
            }
        }

        public void ResetConfigurationCache()
        {
            foreach (MemberType type in Enum.GetValues(typeof(MemberType)))
            {
                // Deep clone discrete config values from the existing config
                foreach (Configuration.Entry<ushort> configEntry in Configuration.DiscreteEntries)
                {
                    DiscreteValues[type][configEntry.ID].CurrentValue = Configuration.DiscreteValues[type][configEntry.ID].CurrentValue;
                }

                // Deep clone decimal config values from the existing config
                foreach (Configuration.Entry<decimal> configEntry in Configuration.DecimalEntries)
                {
                    DecimalValues[type][configEntry.ID].CurrentValue = Configuration.DecimalValues[type][configEntry.ID].CurrentValue;
                }
            }

            ForcePropertyChanged("ConfigChanged");
        }
        
        public bool ConfigChanged
        {
            get
            {
                bool disable = true;
                foreach(MemberType type in Enum.GetValues(typeof(MemberType)))
                {
                    foreach (Configuration.Entry<ushort> configEntry in Configuration.DiscreteEntries)
                    {
                        disable &= (DiscreteValues[type][configEntry.ID].CurrentValue == Configuration.DiscreteValues[type][configEntry.ID].CurrentValue);
                    }
                    
                    foreach (Configuration.Entry<decimal> configEntry in Configuration.DecimalEntries)
                    {
                        disable &= (DecimalValues[type][configEntry.ID].CurrentValue == Configuration.DecimalValues[type][configEntry.ID].CurrentValue);
                    }
                }
                return !disable;
            }
        }

        internal void CommitChanges()
        {
            foreach (MemberType type in Enum.GetValues(typeof(MemberType)))
            {
                foreach (Configuration.Entry<ushort> configEntry in Configuration.DiscreteEntries)
                {
                    if (DiscreteValues[type][configEntry.ID].CurrentValue != Configuration.DiscreteValues[type][configEntry.ID].CurrentValue)
                    {
                        Configuration.DiscreteValues[type][configEntry.ID].CurrentValue = DiscreteValues[type][configEntry.ID].CurrentValue;
                    }
                }

                foreach (Configuration.Entry<decimal> configEntry in Configuration.DecimalEntries)
                {
                    if (DecimalValues[type][configEntry.ID].CurrentValue != Configuration.DecimalValues[type][configEntry.ID].CurrentValue)
                    {
                        Configuration.DecimalValues[type][configEntry.ID].CurrentValue = DecimalValues[type][configEntry.ID].CurrentValue;
                    }
                }
            }

            ForcePropertyChanged("ConfigChanged");
            Configuration.UpdateDependencies();
        }
    }
}
