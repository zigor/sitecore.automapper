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
      this.CreateMap<Item, T>();

      this.CreateMap<CustomItem, T>()
        .BeforeMap((i, t) => Mapper.Map(i.InnerItem, t));
    }

    /// <summary>
    /// Gets the name of the profile.
    /// </summary>
    /// <value>
    /// The name of the profile.
    /// </value>
    public override string ProfileName => this.GetType().FullName;
  }
}