namespace ARSoft.NH.MappingByCodeConvention
{
    using System;

    using NHibernate.Mapping.ByCode;

    public class IdConventions
    {
        public static void AllIdNamedPOIDAndHilo(IModelInspector modelInspector, Type type, IClassAttributesMapper map)
        {
            map.Id(x => x.Generator(Generators.HighLow));
            map.Id(x => x.Column("POID"));
            map.Id(x => x.Type(null));
        }

        public static void AllIdIdentity(IModelInspector modelinspector, Type type, IClassAttributesMapper classcustomizer)
        {
            classcustomizer.Id(x => x.Generator(Generators.Identity));
        }

        public static void AllIdHilo(IModelInspector modelinspector, Type type, IClassAttributesMapper classcustomizer)
        {
            classcustomizer.Id(x => x.Generator(Generators.HighLow));
        }
    }
}