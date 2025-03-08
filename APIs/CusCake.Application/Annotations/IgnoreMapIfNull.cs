using System;
using System.Reflection;
using AutoMapper;

namespace CusCake.Application.Annotations;

[AttributeUsage(AttributeTargets.Property)]
public class IgnoreIfMapNullAttribute : Attribute
{
}
public class IgnoreNullValuesConverter<TSource, TDestination> : ITypeConverter<TSource, TDestination>
    where TDestination : new()
{
    public TDestination Convert(TSource source, TDestination destination, ResolutionContext context)
    {
        if (source == null) return default!;

        destination ??= new TDestination();

        var sourceProperties = typeof(TSource).GetProperties();
        var destProperties = typeof(TDestination).GetProperties();

        foreach (var destProp in destProperties)
        {
            var sourceProp = sourceProperties.FirstOrDefault(p => p.Name == destProp.Name);
            if (sourceProp != null)
            {
                var hasIgnoreIfNull = sourceProp.GetCustomAttribute<IgnoreIfMapNullAttribute>() != null;
                var value = sourceProp.GetValue(source);

                if (!(hasIgnoreIfNull && value == null))
                {
                    destProp.SetValue(destination, value);
                }
            }
        }
        return destination;
    }
}