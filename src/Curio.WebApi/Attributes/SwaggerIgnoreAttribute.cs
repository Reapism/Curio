using System;

namespace Curio.WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class SwaggerIgnoreAttribute : Attribute
    {
        public SwaggerIgnoreAttribute()
        {
        }
    }
}
