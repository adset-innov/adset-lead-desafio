using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Adset.Lead.Infrastructure.Extensions;

internal static class BuilderJsonExtensions
{
    /// <summary>
    /// Configura uma propriedade para ser serializada como JSON no banco de dados.
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade</typeparam>
    /// <typeparam name="TProperty">Tipo da propriedade</typeparam>
    /// <param name="propertyBuilder">Builder da propriedade</param>
    /// <param name="jsonSerializerOptions">Opções do JsonSerializer (opcional)</param>
    /// <returns>PropertyBuilder configurado</returns>
    public static PropertyBuilder<TProperty> HasJsonConversion<TEntity, TProperty>(
        this PropertyBuilder<TProperty> propertyBuilder,
        JsonSerializerOptions? jsonSerializerOptions = null)
        where TEntity : class
        where TProperty : class
    {
        return propertyBuilder
            .HasConversion(
                value => JsonSerializer.Serialize(value, jsonSerializerOptions),
                json => JsonSerializer.Deserialize<TProperty>(json, jsonSerializerOptions) ?? Activator.CreateInstance<TProperty>(),
                ValueComparer.CreateDefault(typeof(TProperty), true))
            .HasColumnType("nvarchar(max)");
    }

    /// <summary>
    /// Configura uma propriedade de lista para ser serializada como JSON no banco de dados.
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade</typeparam>
    /// <typeparam name="TItem">Tipo dos itens da lista</typeparam>
    /// <param name="propertyBuilder">Builder da propriedade</param>
    /// <param name="jsonSerializerOptions">Opções do JsonSerializer (opcional)</param>
    /// <returns>PropertyBuilder configurado</returns>
    public static PropertyBuilder<List<TItem>> HasJsonConversion<TEntity, TItem>(
        this PropertyBuilder<List<TItem>> propertyBuilder,
        JsonSerializerOptions? jsonSerializerOptions = null)
        where TEntity : class
    {
        return propertyBuilder
            .HasConversion(
                list => JsonSerializer.Serialize(list, jsonSerializerOptions),
                json => JsonSerializer.Deserialize<List<TItem>>(json, jsonSerializerOptions) ?? new List<TItem>(),
                ValueComparer.CreateDefault(typeof(List<TItem>), true))
            .HasColumnType("nvarchar(max)");
    }
}
