using AutoMapper;
using Sitecore.AutoMapper.Data;

namespace Sitecore.AutoMapper.Mappers.Primitives
{
  /// <summary>
  /// LinkDetails to string mapper
  /// </summary>
  /// <seealso cref="ObjectMapper{TSource,TDestination}.AutoMapper.Data.LinkDetails, System.String}" />
  internal class LinkDetailsToStringMapper : ObjectMapper<LinkDetails, string>
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
      return typeof(LinkDetails).IsAssignableFrom(context.SourceType) && (context.DestinationType == typeof(string));
    }

    /// <summary>
    ///   Maps the specified source.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public override string Map(LinkDetails source, string destination, ResolutionContext context)
    {
      return source?.Url ?? source?.Script;
    }
  }
}
