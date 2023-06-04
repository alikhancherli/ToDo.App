using System.Reflection;

namespace ToDo.App.Shared.Domain
{
    [Serializable]
    public abstract class BaseValueObject : IEquatable<BaseValueObject>
    {
        private List<PropertyInfo>? properties;
        private List<FieldInfo>? fields;

        public static bool operator ==(BaseValueObject? obj1, BaseValueObject? obj2)
        {
            if (Equals(obj1, null))
            {
                if (Equals(obj2, null))
                {
                    return true;
                }
                return false;
            }
            return obj1.Equals(obj2);
        }

        public static bool operator !=(BaseValueObject? obj1, BaseValueObject? obj2)
        {
            return !(obj1 == obj2);
        }

        public bool Equals(BaseValueObject? obj)
        {
            return Equals(obj as object);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            return GetProperties().All(p => PropertiesAreEqual(obj, p))
                && GetFields().All(f => FieldsAreEqual(obj, f));
        }

        private bool PropertiesAreEqual(object obj, PropertyInfo p)
        {
            return Equals(p.GetValue(this, null), p.GetValue(obj, null));
        }

        private bool FieldsAreEqual(object obj, FieldInfo f)
        {
            return Equals(f.GetValue(this), f.GetValue(obj));
        }

        private IEnumerable<PropertyInfo> GetProperties()
        {
            if (properties == null)
            {
                properties = GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(p => p.GetCustomAttribute(typeof(IgnoreMemberAttribute)) == null)
                    .ToList();

                // Not available in Core
                // !Attribute.IsDefined(p, typeof(IgnoreMemberAttribute))).ToList();
            }

            return properties;
        }

        private IEnumerable<FieldInfo> GetFields()
        {
            if (fields == null)
            {
                fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
                    .Where(p => p.GetCustomAttribute(typeof(IgnoreMemberAttribute)) == null)
                    .ToList();
            }

            return fields;
        }
    }
}
