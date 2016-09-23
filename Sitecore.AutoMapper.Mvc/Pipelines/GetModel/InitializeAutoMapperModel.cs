using Sitecore.AutoMapper.Extensions;
using Sitecore.Mvc.Pipelines.Response.GetModel;
using Sitecore.Mvc.Presentation;

namespace Sitecore.AutoMapper.Mvc.Pipelines.GetModel
{
  public class InitializeAutoMapperModel : GetModelProcessor
  {
    public override void Process(GetModelArgs args)
    {
      if (args.Rendering.Item == null ||
          args.Result == null ||
          args.Result is RenderingModel)
      {
        return;
      }


      args.Rendering.Item.Project(args.Result);
    }
  }
}
