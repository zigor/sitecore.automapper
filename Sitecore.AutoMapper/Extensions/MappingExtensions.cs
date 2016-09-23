using AutoMapper;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Sitecore.AutoMapper.Extensions
{
  /// <summary>
  /// Mapping data extensions
  /// </summary>
  public static class MappingExtensions
  {
    /// <summary>
    /// Projects the specified destination.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="item">The item.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static T Project<T>(this Item item, T destination = default(T), MapperConfiguration configuration = null) where T : class
    {
      Assert.ArgumentNotNull(item ,"item");

      var mapper = configuration?.CreateMapper() ?? Mapper.Instance;

      if (destination == null)
      {
        return mapper.Map(item, typeof(T)) as T;
      }
      return mapper.Map(item, destination, typeof(Item), destination.GetType()) as T;
    }
  }
}
