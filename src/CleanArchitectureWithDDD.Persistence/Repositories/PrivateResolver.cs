using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CleanArchitectureWithDDD.Persistence.Repositories;
public class PrivateResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        JsonProperty prop = base.CreateProperty(member, memberSerialization);
        if (!prop.Writable) // if the property is not writable 
        {
            var property = member as PropertyInfo; // casting it to propertyInfo
            bool hasPrivateSetter = property?.GetSetMethod(true) != null; // check if it have private setter
            prop.Writable = hasPrivateSetter;
        }
        return prop;
    }
}
