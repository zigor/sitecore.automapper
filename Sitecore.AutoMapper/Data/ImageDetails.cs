namespace Sitecore.AutoMapper.Data
{
  /// <summary>
  ///   Image details
  /// </summary>
  internal class ImageDetails
  {
    /// <summary>
    ///   Gets or sets the source.
    /// </summary>
    /// <value>
    ///   The source.
    /// </value>
    public string Src { get; set; }

    /// <summary>
    ///   Gets or sets the image.
    /// </summary>
    /// <value>
    ///   The image.
    /// </value>
    public string Image => this.Src;

    /// <summary>
    ///   Gets or sets the image.
    /// </summary>
    /// <value>
    ///   The image.
    /// </value>
    public string Url => this.Src;

    /// <summary>
    ///   Gets or sets the image.
    /// </summary>
    /// <value>
    ///   The image.
    /// </value>
    public string ImageUrl => this.Src;

    /// <summary>
    ///   Gets or sets the alt.
    /// </summary>
    /// <value>
    ///   The alt.
    /// </value>
    public string Alt { get; set; }

    /// <summary>
    ///   Gets or sets the height.
    /// </summary>
    /// <value>
    ///   The height.
    /// </value>
    public string Height { get; set; }

    /// <summary>
    ///   Gets or sets the width.
    /// </summary>
    /// <value>
    ///   The width.
    /// </value>
    public string Width { get; set; }

    /// <summary>
    ///   Gets or sets the CSS class.
    /// </summary>
    /// <value>
    ///   The CSS class.
    /// </value>
    public string CssClass { get; set; }

    /// <summary>
    ///   Gets or sets the class.
    /// </summary>
    /// <value>
    ///   The class.
    /// </value>
    public string Class => this.CssClass;
  }
}