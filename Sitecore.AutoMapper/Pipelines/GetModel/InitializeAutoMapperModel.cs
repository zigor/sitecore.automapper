using Sitecore.AutoMapper.Extensions;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Pipelines.Response.GetModel;
using Sitecore.Mvc.Presentation;

namespace Sitecore.AutoMapper.Pipelines.GetModel
{
  /// <summary>
  /// Initialize AutoMapper model
  /// </summary>
  /// <seealso cref="Sitecore.Mvc.Pipelines.Response.GetModel.GetModelProcessor" />
  public class InitializeAutoMapperModel : GetModelProcessor
  {
    /// <summary>
    /// Processes the specified arguments.
    /// </summary>
    /// <param name="args">The arguments.</param>
    public override void Process(GetModelArgs args)
    {
      if (args.Rendering.Item == null ||
          args.Result == null ||
          args.Result is RenderingModel)
      {
        return;
      }
      HighResTimer timer = new HighResTimer(true);

      args.Rendering.Item.Project(args.Result);

      var elapsed = timer.ElapsedTimeSpan.Milliseconds;
      Log.Info("elapsed:" + elapsed, this);
      timer.Stop();
    }
  }
}
