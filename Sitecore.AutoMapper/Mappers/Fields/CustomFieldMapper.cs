using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using AutoMapper.Execution;
using Sitecore.Data.Fields;

namespace Sitecore.AutoMapper.Mappers.Fields
{
  /// <summary>
  /// Custom field object mapper
  /// </summary>
  /// <seealso cref="IObjectMapper" />
  public class CustomFieldMapper<TSource> : IObjectMapper where TSource : CustomField
  {
    /// <summary>
    ///   The map method information
    /// </summary>
    private static readonly MethodInfo MapMethod =
      typeof(CustomFieldMapper<TSource>).GetRuntimeMethods().FirstOrDefault(mi => mi.Name == "Map");

    /// <summary>
    /// When true, the mapping engine will use this mapper as the strategy
    /// </summary>
    /// <param name="context">Resolution context</param>
    /// <returns>
    /// Is match
    /// </returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public virtual bool IsMatch(TypePair context)
    {
      return typeof(TSource).IsAssignableFrom(context.SourceType);
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
    /// <exception cref="System.NotImplementedException"></exception>
    public virtual Expression MapExpression(TypeMapRegistry typeMapRegistry, IConfigurationProvider configurationProvider,
      PropertyMap propertyMap, Expression sourceExpression, Expression destExpression, Expression contextExpression)
    {
        return Expression.Call(Expression.Constant(this), MapMethod.MakeGenericMethod(destExpression.Type), sourceExpression, destExpression, contextExpression);
    }

    /// <summary>  
    /// Maps the specified source.
    /// </summary>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>
    /// <param name="field">The field.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public virtual TDestination Map<TDestination>(TSource field, TDestination destination, ResolutionContext context)
    {
      return SetMemberValue(field.InnerField.Name, field.Value, destination, context);
    }

    /// <summary>
    /// Sets the member value.
    /// </summary>
    /// <typeparam name="TDestination">The type of the destination.</typeparam>
    /// <param name="name">The name.</param>
    /// <param name="value">The value.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public static TDestination SetMemberValue<TDestination>(string name, object value, TDestination destination, ResolutionContext context)
    {
      destination = destination == null ? context.Mapper.CreateObject<TDestination>() : destination;

      var destTypeDetails = context.ConfigurationProvider.Configuration.CreateTypeDetails(destination.GetType());
      var member = destTypeDetails.PublicWriteAccessors.FirstOrDefault(m => m.Name == name);
      if (member == null)
      {
        return destination;
      }

      var memberType = member.GetMemberType();

      var destinationValue = context.Mapper.Map(value, value?.GetType() ?? memberType, memberType);

      member.SetMemberValue(destination, destinationValue);

      return destination;
    }
  }
}
