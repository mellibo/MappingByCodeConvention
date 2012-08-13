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
            var attribute = attributes.OfType<LengthAttribute>().FirstOrDefault();
            if (attribute != null)
            {
                propertycustomizer.Length(attribute.Length);
            }
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class LengthAttribute:Attribute
    {
        public int Length { get; set; }
    }
}