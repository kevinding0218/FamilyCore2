using System;

namespace WebApi.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class IngoreReadToListAttribute : Attribute
    {

    }
}
