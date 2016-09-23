using AutoMapper;
using Sitecore.Data.Items;

namespace Sitecore.AutoMapper.Profiles
{
  /// <summary>
  ///   Item profile
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <seealso cref="Profile" />
  public class ItemProfile<T> : Profile
  {
    /// <summary>
    ///   Initializes a new instance of the <see cref="ItemProfile{T}" /> class.
    /// </summary>
    public ItemProfile()
    {
      this.CreateMap<Item, T>()
        .BeforeMap((i, t) => Mapper.Map(i.Fields, t));

      this.CreateMap<CustomItem, T>()
        .BeforeMap((i, t) => Mapper.Map(i.InnerItem, t));
    }
  }
}