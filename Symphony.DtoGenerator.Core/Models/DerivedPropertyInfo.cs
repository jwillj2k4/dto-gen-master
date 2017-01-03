using System;
using System.Reflection;

namespace Deloitte.Symphony.DtoGeneration.Core.Models
{
    public class DerivedPropertyInfo : PropertyInfo
    {
        public DerivedPropertyInfo(string name, Type propertyType)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (propertyType == null)
                throw new ArgumentNullException(nameof(propertyType));
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name");

            Name = name;
            PropertyType = propertyType;
        }

        public override string Name { get; }

        public override Type PropertyType { get; }

        public override PropertyAttributes Attributes
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool CanRead
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool CanWrite
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override Type DeclaringType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override Type ReflectedType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override MethodInfo[] GetAccessors(bool nonPublic)
        {
            throw new NotImplementedException();
        }

        public override MethodInfo GetGetMethod(bool nonPublic)
        {
            throw new NotImplementedException();
        }

        public override ParameterInfo[] GetIndexParameters()
        {
            throw new NotImplementedException();
        }

        public override MethodInfo GetSetMethod(bool nonPublic)
        {
            throw new NotImplementedException();
        }

        public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            throw new NotImplementedException();
        }

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }
    }
}