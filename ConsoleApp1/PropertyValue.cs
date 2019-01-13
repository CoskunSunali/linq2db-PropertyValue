using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public sealed class PropertyValue : IEquatable<PropertyValue>
    {
        public PropertyValue(string name, Type type, object value)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type ?? throw new ArgumentNullException(nameof(type));

            Value = value; // Value can be null
        }

        public string Name { get; }

        public Type Type { get; }

        public object Value { get; set; }

        public static PropertyValue Create<T>(string name, T value)
        {
            return new PropertyValue(name, typeof(T), value);
        }

        #region int

        public static implicit operator int(PropertyValue propertyValue)
        {
            propertyValue.Type.ThrowIfTypeMismatch<int>();

            return (int)propertyValue.Value;
        }

        public static bool operator >(PropertyValue propertyValue, int value)
        {
            propertyValue.Type.ThrowIfTypeMismatch<int>();

            return (int)propertyValue.Value > value;
        }

        public static bool operator <(PropertyValue propertyValue, int value)
        {
            propertyValue.Type.ThrowIfTypeMismatch<int>();

            return (int)propertyValue.Value < value;
        }

        public static bool operator >=(PropertyValue propertyValue, int value)
        {
            propertyValue.Type.ThrowIfTypeMismatch<int>();

            return (int)propertyValue.Value >= value;
        }

        public static bool operator <=(PropertyValue propertyValue, int value)
        {
            propertyValue.Type.ThrowIfTypeMismatch<int>();

            return (int)propertyValue.Value <= value;
        }

        #endregion

        #region string

        public static implicit operator string(PropertyValue propertyValue)
        {
            propertyValue.Type.ThrowIfTypeMismatch<string>();

            return propertyValue.Value as string;
        }

        public static bool operator ==(PropertyValue propertyValue, string value)
        {
            propertyValue.Type.ThrowIfTypeMismatch<string>();

            return propertyValue.Value as string == value;
        }

        public static bool operator !=(PropertyValue propertyValue, string value)
        {
            propertyValue.Type.ThrowIfTypeMismatch<string>();

            return propertyValue.Value as string != value;
        }

        #endregion

        public static PropertyValue operator ++(PropertyValue propertyValue)
        {
            propertyValue.Type.ThrowIfTypeMismatch<int>(); // supports just int. has to support any other type that supports ++ operator.

            var newValue = Convert.ToInt32(propertyValue.Value) + 1;

            return new PropertyValue(propertyValue.Name, propertyValue.Type, newValue);
        }

        public static bool operator ==(PropertyValue value1, PropertyValue value2)
        {
            return EqualityComparer<PropertyValue>.Default.Equals(value1, value2);
        }

        public static bool operator !=(PropertyValue value1, PropertyValue value2)
        {
            return !(value1 == value2);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as PropertyValue);
        }

        public bool Equals(PropertyValue other)
        {
            return other != null &&
                   Name == other.Name &&
                   EqualityComparer<Type>.Default.Equals(Type, other.Type) &&
                   EqualityComparer<object>.Default.Equals(Value, other.Value); // This one checks as object, probably wrong thing to do
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Type, Value);
        }
    }

    internal static class TypeExtensions
    {
        internal static void ThrowIfTypeMismatch<T>(this Type type)
        {
            if (type != typeof(T))
            {
                throw new InvalidOperationException("Type mismatch.");
            }
        }
    }
}
