using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using AutoMapper.Execution;
using Sitecore.Collections;

namespace Sitecore.AutoMapper.Mappers
{
  /// <summary>
  ///   Mapper from FieldCollection to destination object properties
  /// </summary>
  /// <seealso cref="IObjectMapper" />
  public class FieldCollectionMapper : IObjectMapper
  {
    /// <summary>
    ///   The map method information
    /// </summary>
    private static readonly MethodInfo MapMethodInfo =
      typeof(FieldCollectionMapper).GetRuntimeMethods().First(_ => _.IsStatic);

    /// <summary>
    ///   When true, the mapping engine will use this mapper as the strategy
    /// </summary>
    /// <param name="context">Resolution context</param>
    /// <returns>
    ///   Is match
    /// </returns>
    public bool IsMatch(TypePair context)
    {
      return typeof(FieldCollection).IsAssignableFrom(context.SourceType);
    }

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
    public Expression MapExpression(TypeMapRegistry typeMapRegistry, 
                                    IConfigurationProvider configurationProvider,
                                    PropertyMap propertyMap, 
                                    Expression sourceExpression, 
                                    Expression destExpression,
                                    Expression contextExpression)
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
    internal static TDestination Map<TDestination>(FieldCollection source, TDestination destination, ResolutionContext context)
    {
      destination = destination == null ? context.Mapper.CreateObject<TDestination>() : destination;
      var destTypeDetails = context.ConfigurationProvider.Configuration.CreateTypeDetails(destination.GetType());

      var members = destTypeDetails.PublicWriteAccessors.Where(n => source[n.Name] != null);
      foreach (var member in members)
      {
        var field = source[member.Name];
        
        context.Mapper.Map(field, destination, field.GetType(), destTypeDetails.Type);       
      }
      return destination;
    }
  }
}