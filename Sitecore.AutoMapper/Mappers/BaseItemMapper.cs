using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using Sitecore.Data.Items;

namespace Sitecore.AutoMapper.Mappers
{
  /// <summary>
  ///   Item mapper
  /// </summary>
  /// <seealso cref="IObjectMapper" />
  public class BaseItemMapper : IObjectMapper
  {
    /// <summary>
    ///   The map method information
    /// </summary>
    private static readonly MethodInfo MapMethodInfo =
      typeof(BaseItemMapper).GetRuntimeMethods().First(_ => _.IsStatic);

    /// <summary>
    ///   When true, the mapping engine will use this mapper as the strategy
    /// </summary>
    /// <param name="context">Resolution context</param>
    /// <returns>
    ///   Is match
    /// </returns>
    public bool IsMatch(TypePair context)
    {
      return typeof(BaseItem).IsAssignableFrom(context.SourceType);
    }

    /// <summary>
    ///   Builds a mapping expression equivalent to the base Map method
    /// </summary>
    /// <param name="typeMapRegistry"></param>
    /// <param name="configurationProvider"></param>
    /// <param name="propertyMap"></param>
    /// <param name="sourceExpression">Source parameter</param>
    /// <param name="destExpression">Destination parameter</param>
    /// <param name="contextExpression">ResulotionContext parameter</param>
    /// <returns>
    ///   Map expression
    /// </returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public Expression MapExpression(TypeMapRegistry typeMapRegistry, IConfigurationProvider configurationProvider,
      PropertyMap propertyMap, Expression sourceExpression, Expression destExpression, Expression contextExpression)
    {
      return Expression.Call(null, MapMethodInfo.MakeGenericMethod(destExpression.Type), sourceExpression, destExpression, contextExpression);
    }

    /// <summary>
    ///   Maps the specified source.
    /// </summary>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    internal static TDestination Map<TDestination>(BaseItem source, TDestination destination, ResolutionContext context)
    {
      var map = context.Mapper.ConfigurationProvider.FindTypeMapFor(source.GetType(), destination.GetType());

      if (map == null)
      {
        var config = new MapperConfiguration(cfg =>
        {
          cfg.CreateMap(source.GetType(), destination.GetType());
        });

        config.CreateMapper().Map(source, destination, source.GetType(), destination.GetType());
      }

      var destTypeDetails = context.ConfigurationProvider.Configuration.CreateTypeDetails(destination.GetType());

      context.Mapper.Map(source.Fields, destination, source.Fields.GetType(), destTypeDetails.Type);

      return destination;
    }
  }
}