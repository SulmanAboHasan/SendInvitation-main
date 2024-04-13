using DemoUsersManagementCommandSide.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DemoUsersManagementCommandSide.Infrastructuer.Persistence.Configuration
{
    public class GenericEventConfiguration<TEntity, TData> : IEntityTypeConfiguration<TEntity>
        where TEntity : Event<TData>
        where TData : class
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(e => e.data).HasConversion(
                x => Serialize(x),
                x => Deserialize(x)
                ).HasColumnName("data");
        }

        private static JsonSerializerOptions GetJsonSerializerOptions() => new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        private static string Serialize(TData data) =>
            JsonSerializer.Serialize(data, GetJsonSerializerOptions());

        private static TData Deserialize(string data) =>
            JsonSerializer.Deserialize<TData>(data, GetJsonSerializerOptions())
            ?? throw new InvalidOperationException("Failed to deserialize JSON data");
    }
}
