using AutoMapper;
using Sitecore.Data.Items;

namespace Sitecore.AutoMapper.Mappers.Primitives
{
  /// <summary>
  ///   Item to string mapper
  /// </summary>
  /// <seealso cref="AutoMapper.ObjectMapper{Sitecore.Data.Items.Item, System.String}" />
  public class ItemToStringMapper : ObjectMapper<Item, string>
  {
    /// <summary>
    ///   When true, the mapping engine will use this mapper as the strategy
    /// </summary>
    /// <param name="context">Resolution context</param>
    /// <returns>
    ///   Is match
    /// </returns>
    public override bool IsMatch(TypePair context)
    {
      return typeof(Item).IsAssignableFrom(context.SourceType) && (context.DestinationType == typeof(string));
    }

    /// <summary>
    ///   Maps the specified source.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public override string Map(Item source, string destination, ResolutionContext context)
    {
      return source?.DisplayName;
    }
  }
}