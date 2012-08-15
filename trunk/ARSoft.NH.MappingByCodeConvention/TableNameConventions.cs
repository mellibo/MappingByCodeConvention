namespace ARSoft.NH.MappingByCodeConvention
{
    using System;
    using System.Data.Entity.Design.PluralizationServices;
    using System.Globalization;

    using NHibernate.Mapping.ByCode;

    public class EntityConventions
    {
        private static readonly PluralizationService EnglishPluralizationService = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en"));

        public static void TableNameEnglishPluralizedConvention(IModelInspector modelInspector, Type type, IClassAttributesMapper map)
        {
            map.Table(EnglishPluralizationService.Pluralize(type.Name));
        }

        public static void ExcludeBaseEntity(ConventionModelMapper modelMapper, Type baseEntityType)
        {
            modelMapper.IsEntity((t, declared) => baseEntityType.IsAssignableFrom(t) && baseEntityType != t && !t.IsInterface);
            modelMapper.IsRootEntity((type, declared) => type.BaseType.Equals(baseEntityType));
        }

        public static void MapBagsWithCascadeAll(IModelInspector modelinspector, PropertyPath member, IBagPropertiesMapper propertycustomizer)
        {
            propertycustomizer.Cascade(Cascade.All);
        }

        public static void MapBagsWithCascadePersist(IModelInspector modelinspector, PropertyPath member, IBagPropertiesMapper propertycustomizer)
        {
            propertycustomizer.Cascade(Cascade.Persist);
        }
    }
}