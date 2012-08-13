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
        }
    }
}