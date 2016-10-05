using AutoMapper;
using Sitecore.Data.Fields;

namespace Sitecore.AutoMapper.Mappers.Fields
{
  /// <summary>
  /// Name Values field mapper
  /// </summary>
  /// <seealso cref="Sitecore.AutoMapper.Mappers.Fields.CustomFieldMapper{Sitecore.Data.Fields.NameValueListField}" />
  public class NameValueListFieldMapper : CustomFieldMapper<NameValueListField>
  {
    /// <summary>
    /// Maps the specified field.
    /// </summary>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public override TDestination Map<TDestination>(NameValueListField field, TDestination destination, ResolutionContext context)
    {
      return SetMemberValue(field.InnerField.Name, field.NameValues, destination, context);
    }
  }
}
