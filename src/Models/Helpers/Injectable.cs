using System;

namespace IQuality.Models.Helpers
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Injectable : Attribute
    {
        public InjectionType Type { get; }
        public Type Interface { get; }
        public Type TestingDummy { get; }

        public Injectable(InjectionType type = InjectionType.Scoped, Type interfaceType = null, Type testingDummy = null)
        {
            TestingDummy = testingDummy;
            Interface = interfaceType;
            Type = type;
        }
    }

    public enum InjectionType
    {
        Scoped,
        Singleton,
        Transient
    }
}