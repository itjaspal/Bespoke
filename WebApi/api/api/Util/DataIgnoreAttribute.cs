using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.Util
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DataIgnoreAttribute : Attribute
    {
        public DataIgnoreAttribute()
        {
        }
    }
}