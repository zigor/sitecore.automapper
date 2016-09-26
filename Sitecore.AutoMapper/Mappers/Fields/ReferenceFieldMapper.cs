using AutoMapper;
using Sitecore.Data.Fields;

namespace Sitecore.AutoMapper.Mappers.Fields
{
  /// <summary>
  /// Reference field mapper
  /// </summary>
  /// <seealso cref="Sitecore.AutoMapper.Mappers.Fields.CustomFieldMapper{Sitecore.Data.Fields.ReferenceField}" />
  public class ReferenceFieldMapper : CustomFieldMapper<ReferenceField>
  {
    /// <summary>
    /// When true, the mapping engine will use this mapper as the strategy
    /// </summary>
    /// <param name="context">Resolution context</param>
    /// <returns>
    /// Is match
    /// </returns>
    public override bool IsMatch(TypePair context)
    {
      return typeof(ReferenceField).IsAssignableFrom(context.SourceType);
    }

    /// <summary>
    /// Maps the specified field.
    /// </summary>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public override TDestination Map<TDestination>(ReferenceField field, TDestination destination, ResolutionContext context)
    {
      return SetMemberValue(field.InnerField.Name, field.TargetItem, destination, context);
    }
  }
}
