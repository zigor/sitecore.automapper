using AutoMapper;
using AutoMapper.Mappers;
using Sitecore.AutoMapper.Data;
using Sitecore.AutoMapper.Mappers.Primitives;
using Sitecore.Data.Fields;

namespace Sitecore.AutoMapper.Mappers.Fields
{
  /// <summary>
  /// Internal link field mapper
  /// </summary>
  /// <seealso cref="InternalLinkField" />
  public class LinkFieldMapper : CustomFieldMapper<LinkField>
  {
    /// <summary>
    /// Initializes the <see cref="LinkFieldMapper"/> class.
    /// </summary>
    static LinkFieldMapper()
    {
      MapperRegistry.Mappers.Insert(0, new LinkDetailsToStringMapper());
    }

    /// <summary>
    ///   Maps the specified imageField.
    /// </summary>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>
    /// <param name="field">The imageField.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public override TDestination Map<TDestination>(LinkField field, TDestination destination, ResolutionContext context)
    {
      if (string.IsNullOrEmpty(field.InnerField.Value))
      {
        return destination;
      }

      var renderer = new LinkRendrer(field);
      var linkDetails = renderer.MapToLinkDetails();

      if (linkDetails == null)
      {
        return destination;
      }

      return SetMemberValue(field.InnerField.Name, linkDetails, destination, context);
    }

    /// <summary>
    /// Link field renderer
    /// </summary>
    private class LinkRendrer : Xml.Xsl.LinkRenderer
    {
      /// <summary>
      /// The field
      /// </summary>
      private LinkField field;

      /// <summary>
      /// Initializes a new instance of the <see cref="LinkRendrer"/> class.
      /// </summary>
      /// <param name="field">The field.</param>
      public LinkRendrer(LinkField field) : base(field.InnerField.Item)
      {
        this.field = field;
        this.FieldName = field.InnerField.Name;
        this.FieldValue = field.InnerField.Value;
      }

      /// <summary>
      /// Maps to link details.
      /// </summary>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public LinkDetails MapToLinkDetails()
      {
        if (string.IsNullOrEmpty(this.FieldValue))
        {
          return null;
        }

        var url = this.GetUrl(this.LinkField);

        if (string.IsNullOrEmpty(url))
        {
          return null;
        }

        return new LinkDetails
        {
          Url = this.LinkType != "javascript" ? url : null,
          CssClass = this.LinkField.Class,
          Title = this.LinkField.Target,
          Text = this.LinkField.Text,
          Target = this.LinkField.Target,
          Script = this.LinkType == "javascript" ? url : null
        };
      }
    }
  }
}
