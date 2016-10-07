using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using AutoMapper.Mappers;
using Sitecore.AutoMapper.Mappers.Primitives;
using Sitecore.Data.Items;

namespace Sitecore.AutoMapper.Mappers
{
  /// <summary>
  ///   Item mapper
  /// </summary>
  /// <seealso cref="IObjectMapper" />
  public class BaseItemMapper : ObjectMapper
  {
    /// <summary>
    /// The after maps
    /// </summary>
    private static readonly List<Action<BaseItem, object>> afterMaps = new List<Action<BaseItem, object>>();

    /// <summary>
    /// Initializes the <see cref="BaseItemMapper"/> class.
    /// </summary>
    static BaseItemMapper()
    {
      MapperRegistry.Mappers.Insert(0, new ItemToStringMapper());
    }

    /// <summary>
    ///   The map method information
    /// </summary>
    private static readonly MethodInfo MapMethodInfo =
      typeof(BaseItemMapper).GetRuntimeMethods().First(_ => _.Name == "Map");

    /// <summary>
    ///   When true, the mapping engine will use this mapper as the strategy
    /// </summary>
    /// <param name="context">Resolution context</param>
    /// <returns>
    ///   Is match
    /// </returns>
    public override bool IsMatch(TypePair context)
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
    public override Expression MapExpression(TypeMapRegistry typeMapRegistry, IConfigurationProvider configurationProvider,
      PropertyMap propertyMap, Expression sourceExpression, Expression destExpression, Expression contextExpression)
    {
      return Expression.Call(null, MapMethodInfo.MakeGenericMethod(destExpression.Type), sourceExpression, destExpression, contextExpression);
    }

    /// <summary>
    /// Afters the map.
    /// </summary>
    /// <param name="afterMapFunction">The after map.</param>
    public static void AddAfterMap(Action<BaseItem, object> afterMapFunction)
    {
      afterMaps.Add(afterMapFunction);
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
      if (source == null)
      {
        return default(TDestination);
      }

      destination = destination == null ? context.Mapper.CreateObject<TDestination>() : destination;

      MapSourceProperties(source, destination, context);

      MapSourceFields(source, destination, context);


      afterMaps.ForEach(a => a(source, destination));

      return destination;
    }

    /// <summary>
    /// Maps the source fields.
    /// </summary>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="context">The context.</param>
    private static void MapSourceFields<TDestination>(BaseItem source, TDestination destination, ResolutionContext context)
    {
      var destTypeDetails = context.ConfigurationProvider.Configuration.CreateTypeDetails(destination.GetType());

      context.Mapper.Map(source.Fields, destination, source.Fields.GetType(), destTypeDetails.Type);
    }

    /// <summary>
    /// Maps the source properties.
    /// </summary>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>  
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="context">The context.</param>
    private static void MapSourceProperties<TDestination>(BaseItem source, TDestination destination,
      ResolutionContext context)
    {
      var mapper = GetMapper(source.GetType(), destination.GetType(), context, false);
      mapper.Map(source, destination, source.GetType(), destination.GetType());
    }
  }
}