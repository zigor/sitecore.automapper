using System;
using AutoMapper;
using AutoMapper.Mappers;
using Sitecore.AutoMapper.Mappers;
using Sitecore.AutoMapper.Mappers.Fields;
using Sitecore.Data.Fields;
using Sitecore.Pipelines;

namespace Sitecore.AutoMapper.Configuration.Pipelines.Initialize
{
  /// <summary>
  ///   Items to data models profiles
  /// </summary>
  public class ItemToDataModelProfilesRegistration
  {
    /// <summary>
    ///   Processes the specified arguments.
    /// </summary>
    /// <param name="args">The arguments.</param>
    public void Process(PipelineArgs args)
    {
      this.Configure();
    }

    /// <summary>
    /// Configures the specified mce.
    /// </summary>
    private void Configure()
    {
      MapperRegistry.Mappers.Add(new NameValueListFieldMapper());
      MapperRegistry.Mappers.Add(new LinkFieldMapper());
      MapperRegistry.Mappers.Add(new ImageFieldMapper());
      MapperRegistry.Mappers.Add(new CheckboxFieldMapper());
      MapperRegistry.Mappers.Add(new DateTimeFieldMapper());
      MapperRegistry.Mappers.Add(new ReferenceFieldMapper());
      MapperRegistry.Mappers.Add(new MultilistFieldMapper());
      MapperRegistry.Mappers.Add(new LookupFieldMapper());
      MapperRegistry.Mappers.Add(new CustomFieldMapper<CustomField>());
      MapperRegistry.Mappers.Add(new FieldMapper());
      MapperRegistry.Mappers.Add(new FieldCollectionMapper());
      MapperRegistry.Mappers.Add(new BaseItemMapper());
      MapperRegistry.Mappers.Add(new CustomItemMapper());

      Mapper.Initialize((mce) => { });
    }
  }
}