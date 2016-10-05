namespace Sitecore.AutoMapper.Data
{
  /// <summary>
  ///   Link details
  /// </summary>
  internal class LinkDetails
  {
    /// <summary>
    ///   Gets the URL.
    /// </summary>
    /// <value>
    ///   The URL.
    /// </value>
    public string Url { get; set; }

    /// <summary>
    ///   Gets or sets the href.
    /// </summary>
    /// <value>
    ///   The href.
    /// </value>
    public string Href => this.Url;

    /// <summary>
    ///   Gets or sets the text.
    /// </summary>
    /// <value>
    ///   The text.
    /// </value>
    public string Text { get; set; }

    /// <summary>
    ///   Gets or sets the title.
    /// </summary>
    /// <value>
    ///   The title.
    /// </value>
    public string Title { get; set; }

    /// <summary>
    ///   Gets or sets the target.
    /// </summary>
    /// <value>
    ///   The target.
    /// </value>
    public string Target { get; set; }

    /// <summary>
    ///   Gets or sets the CSS class.
    /// </summary>
    /// <value>
    ///   The CSS class.
    /// </value>
    public string CssClass { get; set; }

    /// <summary>
    ///   Gets the class.
    /// </summary>
    /// <value>
    ///   The class.
    /// </value>
    public string Class => this.CssClass;

    /// <summary>
    /// Gets or sets the script.
    /// </summary>
    /// <value>
    /// The script.
    /// </value>
    public string Script { get; set; }
  }
}