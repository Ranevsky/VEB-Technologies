using EntityFrameworkCoreLibrary.ValueGenerators;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EntityFrameworkCoreLibrary.Extensions;

public static class PropertyBuilderExtension
{
    public static PropertyBuilder AddSequentialId(this PropertyBuilder propertyBuilder)
    {
        var converter = new ValueConverter<string, Guid>(
            to => new Guid(to),
            from => from.ToString(),
            new ConverterMappingHints(valueGeneratorFactory: (_, _) => new GuidStringGenerator()));
        
        return propertyBuilder.ValueGeneratedOnAdd().HasConversion(converter);
    }
}