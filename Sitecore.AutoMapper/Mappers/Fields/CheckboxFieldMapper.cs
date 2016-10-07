using AutoMapper;
using Sitecore.Data.Fields;

namespace Sitecore.AutoMapper.Mappers.Fields
{
  /// <summary>
  /// Checkbox field mapper
  /// </summary>
  /// <seealso cref="Sitecore.AutoMapper.Mappers.Fields.CustomFieldMapper{Sitecore.Data.Fields.CheckboxField}" />
  public class CheckboxFieldMapper : CustomFieldMapper<CheckboxField>
  {
    /// <summary>
    ///   Maps the specified field.
    /// </summary>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public override TDestination Map<TDestination>(CheckboxField field, TDestination destination,
      ResolutionContext context)
    {
      return SetMemberValue(field.InnerField, field.Checked, destination, context);
    }
  }
}