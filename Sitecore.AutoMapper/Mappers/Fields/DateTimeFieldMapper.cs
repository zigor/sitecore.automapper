using AutoMapper;
using Sitecore.Data.Fields;

namespace Sitecore.AutoMapper.Mappers.Fields
{
  /// <summary>
  /// Date field mapper
  /// </summary>
  /// <seealso cref="Sitecore.AutoMapper.Mappers.Fields.CustomFieldMapper{Sitecore.Data.Fields.DateField}" />
  public class DateTimeFieldMapper : CustomFieldMapper<DateField>
  {
    /// <summary>
    /// Maps the specified source.
    /// </summary>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public override TDestination Map<TDestination>(DateField field, TDestination destination, ResolutionContext context)
    {
      return SetMemberValue(field.InnerField, field.DateTime, destination, context);
    }
  }
}
