using System.Collections.Generic;

namespace ConsoleApp1
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Properties = new Dictionary<string, PropertyValue>();
        }

        public BaseEntity(IDictionary<string, PropertyValue> properties)
        {
            Properties = new Dictionary<string, PropertyValue>(properties);
        }

        public PropertyValue this[string name]
        {
            get => Properties[name];
            set => Properties[name] = value;
        }

        public IDictionary<string, PropertyValue> Properties { get; }
    }
}
