using System;
using System.Linq.Expressions;
using AutoMapper;
using Sitecore.AutoMapper.Extensions;

namespace Sitecore.AutoMapper.Mappers
{
  /// <summary>
  /// Object Mapper
  /// </summary>
  /// <seealso cref="IObjectMapper" />
  public abstract class ObjectMapper : IObjectMapper
  {
    /// <summary>
    /// When true, the mapping engine will use this mapper as the strategy
    /// </summary>
    /// <param name="context">Resolution context</param>
    /// <returns>
    /// Is match
    /// </returns>
    public abstract bool IsMatch(TypePair context);

    /// <summary>
    /// Builds a mapping expression equivalent to the base Map method
    /// </summary>
    /// <param name="typeMapRegistry"></param>
    /// <param name="configurationProvider"></param>
    /// <param name="propertyMap"></param>
    /// <param name="sourceExpression">Source parameter</param>
    /// <param name="destExpression">Destination parameter</param>
    /// <param name="contextExpression">ResulotionContext parameter</param>
    /// <returns>
    /// Map expression
    /// </returns>
    public abstract Expression MapExpression(TypeMapRegistry typeMapRegistry,
      IConfigurationProvider configurationProvider,
      PropertyMap propertyMap, Expression sourceExpression, Expression destExpression, Expression contextExpression);

    /// <summary>
    /// Gets the mapper.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="context">The context.</param>
    /// <param name="includeObjectMappers">if set to <c>true</c> [include object mappers].</param>
    /// <returns></returns>
    public static IMapper GetMapper(Type source, Type destination, ResolutionContext context, bool includeObjectMappers = true)
    {
      MapperConfiguration config = null;

      if (!context.Mapper.ConfigurationProvider.HasMapFor(source, destination, includeObjectMappers))
      {
        config = new MapperConfiguration(cfg => { cfg.CreateMap(source, destination); });
      }

      var mapper = config?.CreateMapper() ?? context.Mapper;
      return mapper;
    }
  }
}