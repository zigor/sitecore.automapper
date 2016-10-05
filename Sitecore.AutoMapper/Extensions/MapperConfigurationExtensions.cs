using System;
using System.Linq;
using AutoMapper;

namespace Sitecore.AutoMapper.Extensions
{
  /// <summary>
  ///   Mapper Configuration extensions
  /// </summary>
  public static class MapperConfigurationExtensions
  {
    /// <summary>
    ///  Types mapping is covered the by object map.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    /// <returns></returns>
    public static bool CoveredByObjectMap(this IConfigurationProvider configuration, Type source, Type destination)
    {
      var typePair = new TypePair(source, destination);
      return configuration.GetMappers().FirstOrDefault(m => m.IsMatch(typePair)) != null;
    }

    /// <summary>
    /// Determines whether [has map for] [the specified source].
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="includeObjectMappers">if set to <c>true</c> [include object mappers].</param>
    /// <returns>
    ///   <c>true</c> if the ocnfiguration has map for the specified source and destination types; otherwise, <c>false</c>.
    /// </returns>
    public static bool HasMapFor(this IConfigurationProvider configuration, Type source, Type destination, bool includeObjectMappers = true)
    {
      return source == destination ||
             (configuration.FindTypeMapFor(source, destination) != null) ||
             includeObjectMappers && configuration.CoveredByObjectMap(source, destination);
    }
  }
}