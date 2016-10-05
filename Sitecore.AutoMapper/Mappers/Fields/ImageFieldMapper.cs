using AutoMapper;
using AutoMapper.Mappers;
using Sitecore.AutoMapper.Data;
using Sitecore.AutoMapper.Mappers.Primitives;
using Sitecore.Collections;
using Sitecore.Data.Fields;

namespace Sitecore.AutoMapper.Mappers.Fields
{
  /// <summary>
  ///   Image imageField mapper
  /// </summary>
  /// <seealso cref="ImageField" />
  public class ImageFieldMapper : CustomFieldMapper<ImageField>
  {
    /// <summary>
    /// Initializes the <see cref="ImageFieldMapper"/> class.
    /// </summary>
    static ImageFieldMapper()
    {
      MapperRegistry.Mappers.Insert(0, new ImageDetailsToStringMapper());
    }

    /// <summary>
    ///   Maps the specified imageField.
    /// </summary>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>
    /// <param name="field">The imageField.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public override TDestination Map<TDestination>(ImageField field, TDestination destination, ResolutionContext context)
    {
      if (string.IsNullOrEmpty(field.InnerField.Value))
      {
        return destination;
      }

      var renderer = new ImageRenderer(field);
      var imageDetails = renderer.MapToImageDetails();

      if (imageDetails == null)
      {
        return destination;
      }

      return SetMemberValue(field.InnerField.Name, imageDetails, destination, context);
    }

    /// <summary>
    ///   ImageRenderer
    /// </summary>
    /// <seealso cref="Sitecore.Xml.Xsl.ImageRenderer" />
    private class ImageRenderer : Xml.Xsl.ImageRenderer
    {
      /// <summary>
      ///   The source
      /// </summary>
      private string source;

      /// <summary>
      ///   Initializes a new instance of the <see cref="ImageRenderer" /> class.
      /// </summary>
      /// <param name="imageField">The image field.</param>
      public ImageRenderer(ImageField imageField)
      {
        this.ImageField = imageField;
        this.Item = imageField.InnerField.Item;
        this.FieldName = imageField.InnerField.Name;
        this.FieldValue = imageField.InnerField.Value;
      }

      /// <summary>
      ///   Gets or sets the image field.
      /// </summary>
      /// <value>
      ///   The image field.
      /// </value>
      public ImageField ImageField { get; }

      /// <summary>
      ///   Gets the source.
      /// </summary>
      /// <returns></returns>
      protected new string GetSource()
      {
        if (this.source != null)
        {
          return this.source;
        }

        this.Item = this.ImageField.InnerField.Item;
        this.FieldName = this.ImageField.InnerField.Name;
        this.FieldValue = this.ImageField.InnerField.Value;
        this.Parameters = new SafeDictionary<string>
        {
          {"la", this.Item.Language.Name},
          {"vs", this.Item.Version.Number.ToString()},
          {"db", this.Item.Database.Name}
        };

        var isEmpty = this.Render().IsEmpty;

        this.source = isEmpty ? string.Empty : base.GetSource();

        return this.source;
      }

      /// <summary>
      ///   Maps to image details.
      /// </summary>
      /// <returns></returns>
      public ImageDetails MapToImageDetails()
      {
        var src = this.GetSource();

        if (string.IsNullOrEmpty(src))
        {
          return null;
        }
        
        return new ImageDetails
        {
          Alt = this.ImageField.Alt,
          CssClass = this.ImageField.Class,
          Height = this.ImageField.Height,
          Width = this.ImageField.Width,
          Src = src
        };
      }
    }
  }
}