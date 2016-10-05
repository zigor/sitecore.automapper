using AutoMapper;
using Sitecore.Data.Fields;

namespace Sitecore.AutoMapper.Mappers.Fields
{
  /// <summary>
  /// Word document field mapper
  /// </summary>
  /// <seealso cref="Sitecore.AutoMapper.Mappers.Fields.CustomFieldMapper{Sitecore.Data.Fields.WordDocumentField}" />
  public class WordDocumentFieldMapper : CustomFieldMapper<WordDocumentField>
  {
    /// <summary>
    ///   Maps the specified source.
    /// </summary>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public override TDestination Map<TDestination>(WordDocumentField field, TDestination destination, ResolutionContext context)
    {
      return SetMemberValue(field.InnerField.Name, field.Html, destination, context);
    }
  }
}