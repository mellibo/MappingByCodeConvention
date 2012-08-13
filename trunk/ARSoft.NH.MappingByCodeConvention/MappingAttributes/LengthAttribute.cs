namespace ARSoft.NH.MappingByCodeConvention.MappingAttributes
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class LengthAttribute : Attribute
    {
        public LengthAttribute(int length)
        {
            this.Length = length;
        }

        public int Length { get; set; }
    }
}