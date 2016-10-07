using System;
using System.Collections.Generic;
using System.Reflection;
using Sitecore.Data.Fields;

namespace Sitecore.AutoMapper.Mappers.Fields
{
  /// <summary>
  /// CustomFieldMapper implementation
  /// </summary>
  /// <seealso cref="Sitecore.AutoMapper.Mappers.Fields.CustomFieldMapper" />
  public abstract class CustomFieldMapper : ObjectMapper
  {
    /// <summary>
    /// The after maps
    /// </summary>
    private static readonly List<Action<CustomField, object, MemberInfo>> afterMaps = new List<Action<CustomField, object, MemberInfo>>();

    /// <summary>
    /// Afters the map.
    /// </summary>
    /// <param name="afterMapFunction">The after map.</param>
    public static void AddAfterMap(Action<CustomField, object, MemberInfo> afterMapFunction)
    {
      afterMaps.Add(afterMapFunction);
    }

    /// <summary>
    /// Runs the after map.
    /// </summary>
    /// <param name="field">The field.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="memberInfo">The member information.</param>
    internal static void RunAfterMap(CustomField field, object destination, MemberInfo memberInfo)
    {
      foreach (var action in GetAfterMaps())
      {
        action(field, destination, memberInfo);
      }
    }

    /// <summary>
    /// Gets the after maps.
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Action<CustomField, object, MemberInfo>> GetAfterMaps()
    {
      return afterMaps;
    }
  }
}