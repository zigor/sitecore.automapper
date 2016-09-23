using System;
using System.Collections.Generic;
using System.Xml;
using AutoMapper;
using AutoMapper.Mappers;
using Sitecore.AutoMapper.Mappers;
using Sitecore.AutoMapper.Profiles;
using Sitecore.Diagnostics;
using Sitecore.Pipelines;

namespace Sitecore.AutoMapper.Configuration.Pipelines.Initialize
{
  /// <summary>
  ///   Items to data models profiles
  /// </summary>
  public class ItemToDataModelProfilesRegistration
  {
    /// <summary>
    /// The model types
    /// </summary>
    private readonly List<Type> modelTypes = new List<Type>();

    /// <summary>
    /// Adds the model.
    /// </summary>
    /// <param name="configNode">The configuration node.</param>
    internal void AddModel(XmlNode configNode)
    {
      Assert.ArgumentNotNull(configNode, nameof(configNode));

      if (string.IsNullOrEmpty(configNode.InnerText))
      {
        Log.Warn("Model type was not found for the specified config node" + Environment.NewLine + configNode.OuterXml, this);
      }

      var type = Type.GetType(configNode.InnerText, false);

      if (type == null)
      {
        Log.Warn("The model type was not found for the specified config node" + Environment.NewLine + configNode.OuterXml, this);
      }

      this.modelTypes.Add(type);
    }

    /// <summary>
    ///   Processes the specified arguments.
    /// </summary>
    /// <param name="args">The arguments.</param>
    public void Process(PipelineArgs args)
    {
      Mapper.Initialize(this.Configure);
    }

    /// <summary>
    /// Configures the specified mce.
    /// </summary>
    /// <param name="mce">The mce.</param>
    private void Configure(IMapperConfigurationExpression mce)
    {
      MapperRegistry.Mappers.Add(new FieldMapper());
      MapperRegistry.Mappers.Add(new FieldCollectionMapper());
      MapperRegistry.Mappers.Add(new BaseItemMapper());
      MapperRegistry.Mappers.Add(new CustomItemMapper());

      foreach (var type in this.modelTypes)
      {
        var profile = Activator.CreateInstance(typeof(ItemProfile<>).MakeGenericType(type)) as Profile;
      //  mce.AddProfile(profile);
      }

      mce.AddProfile(new ItemProfile<object>());
    }
  }
}