namespace ARSoft.NH.MappingByCodeConvention
{
    using System;
    using System.Reflection;

    using NHibernate;
    using NHibernate.Mapping.ByCode;

    using System.Linq;

    public class PropertiesConvention
    {
        public static void MapStringAsVarchar(IModelInspector modelInspector, PropertyPath member, IPropertyMapper propertyCustomizer)
        {
            var propertyInfo = member.LocalMember as PropertyInfo;
            if (propertyInfo != null && propertyInfo.PropertyType == typeof(string))
            {
                propertyCustomizer.Type(NHibernateUtil.AnsiString);
            }
        }

        public static void MapStringLengthFromAttribute(IModelInspector modelinspector, PropertyPath member, IPropertyMapper propertycustomizer)
        {
            var propertyInfo = member.LocalMember as PropertyInfo;
            if (propertyInfo == null || propertyInfo.PropertyType != typeof(string))
            {
                return;
            }

            var attributes = propertyInfo.GetCustomAttributes(true);
            var attribute = attributes.FirstOrDefault(x => x.GetType().Name.IndexOf("Length") > -1);
            if (attribute == null)
            {
                return;
            }
            int value = 0;
            var possiblePropertyNames = new[] { "MaximumLength", "Length", "Max", "MaxLength" };
            foreach (var name in possiblePropertyNames)
            {
                var attributeProperty = attribute.GetType().GetProperty(name);
                if (attributeProperty != null)
                {
                    value = (int)attributeProperty.GetValue(attribute, null);
                    break;
                }
            }

            if (value == 0)
            {
                throw new InvalidOperationException(
                    string.Format("could not get the length of property {0}", propertyInfo.Name));
            }

            propertycustomizer.Length(value);
        }
    }
}