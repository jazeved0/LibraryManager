using LibraryManager.Data.Item.Status;
using LibraryManager.Data.Member;
using LibraryManager.ViewModels;
using System;
using System.Collections.Generic;
using static LibraryManager.Data.Item.Status.ItemStatus;

namespace LibraryManager
{
    public class Configuration
    {
        public struct Entry<T>
        {
            public String ID { get; internal set; }
            public String Label { get; internal set; }
            public String Units { get; internal set; }
            public T DefaultValue { get; internal set; }
            public Entry(String id, String label, T defaultValue, String units = "")
            {
                ID = id;
                Label = label;
                Units = units;
                DefaultValue = defaultValue;
            }
        }

        public class Value<T> : NotifyPropertyChanged, ICloneable where T : IEquatable<T>
        {
            private T _currentValue;

            public T CurrentValue
            {
                get { return _currentValue; }
                set
                {
                    if (_currentValue.Equals(value)) return;
                    _currentValue = value;
                    ForcePropertyChanged();
                }
            }
            public Entry<T> Entry { get; internal set; }
            public Value(Entry<T> correspondingEntry, T startingValue)
            {
                CurrentValue = startingValue;
                Entry = correspondingEntry;
            }

            public object Clone()
            {
                return new Value<T>(Entry, CurrentValue);
            }
        }

        public IList<Entry<ushort>> DiscreteEntries;
        public IList<Entry<decimal>> DecimalEntries;
        
        public IDictionary<MemberType, Dictionary<String, Value<ushort>>> DiscreteValues { get; internal set; }
        public IDictionary<MemberType, Dictionary<String, Value<decimal>>> DecimalValues { get; internal set; }

        public Configuration()
        {
            DiscreteEntries = new List<Entry<ushort>>();
            DecimalEntries = new List<Entry<decimal>>();
            DiscreteValues = new Dictionary<MemberType, Dictionary<String, Value<ushort>>>();
            DecimalValues = new Dictionary<MemberType, Dictionary<String, Value<decimal>>>();

            // Initialize group (membertype) config dictionaries
            foreach (MemberType type in Enum.GetValues(typeof(MemberType)))
            {
                DiscreteValues.Add(type, new Dictionary<String, Value<ushort>>());
                DecimalValues.Add(type, new Dictionary<String, Value<decimal>>());
            }

            RegisterEntries();
        }

        private void RegisterEntries()
        {
            RegisterDiscreteEntry("issuance_max_duration", "Maximum check-out duration", "days", 14);
            RegisterDiscreteEntry("reservation_max_duration", "Reservation duration", "days", 5);
            RegisterDiscreteEntry("issuance_max", "Maximum simultaneous check-outs", "items", 3);
            RegisterDiscreteEntry("reservation_max", "Maximum simultaneous reservations", "items", 2);
            RegisterDecimalEntry("max_fee", "Maximum overdue fee", "USD", 1.5m);
        }

        private void RegisterDiscreteEntry(String id, String label, String units, ushort defaultValue = 0)
        {
            Entry<ushort> entry = new Entry<ushort>(id, label, defaultValue, units);
            DiscreteEntries.Add(entry);
            foreach(MemberType type in Enum.GetValues(typeof(MemberType)))
            {
                DiscreteValues[type].Add(id, new Value<ushort>(entry, defaultValue));
            }
        }

        private void RegisterDecimalEntry(String id, String label, String units, decimal defaultValue = 0m)
        {
            Entry<decimal> entry = new Entry<decimal>(id, label, defaultValue, units);
            DecimalEntries.Add(entry);
            foreach (MemberType type in Enum.GetValues(typeof(MemberType)))
            {
                DecimalValues[type].Add(id, new Value<decimal>(entry, defaultValue));
            }
        }

        public void UpdateDependencies()
        {
            MainWindowViewModel.Instance.Refresh();
        }

        public TimeSpan GetMaxDuration(StatusType statusType, MemberType memberType)
        {
            String idPrefix = "";
            if (statusType == StatusType.Reserved) idPrefix = "reservation";
            else idPrefix = "issuance";
            int maxDuration = DiscreteValues[memberType][idPrefix + "_max_duration"].CurrentValue;
            return new TimeSpan(maxDuration, 0, 0, 0);
        }

        public decimal GetOverdueFee(ItemStatus item)
        {
            if (item.Type != StatusType.Overdue && item.Remainder.Milliseconds >= 0) return 0m;
            const decimal feeDecayHalfLife = 7m; // days
            decimal maxOverdueFee = DecimalValues[item.Owner.Type]["max_fee"].CurrentValue;
            decimal a = maxOverdueFee / (2m * feeDecayHalfLife); // linear growth slope
            decimal days = (decimal) -item.Remainder.TotalDays;
            if (days <= feeDecayHalfLife)
            {
                // Use linear growth
                return a * days;
            }
            else
            {
                // Use negative exponential falloff
                decimal b = (-2m * maxOverdueFee * maxOverdueFee) / (8m * a); // negative exponential falloff coefficient
                return b / days + maxOverdueFee;
            }
        }
    }
}
