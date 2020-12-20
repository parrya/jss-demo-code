using Sitecore.Diagnostics;
using Sitecore.LayoutService.ItemRendering.Pipelines.GetLayoutServiceContext;
using System.Collections.Generic;
using Sitecore;
using Sitecore.Links;

namespace JSSDemo.Feature.Metadata.PipelineProcessors
{
    public class ItemContext : IGetLayoutServiceContextProcessor
    {
        public const string Key = "itemUrl";

        public void Process(GetLayoutServiceContextArgs args)
        {
            Assert.ArgumentNotNull((object)args, nameof(args));
            var options = LinkManager.GetDefaultUrlBuilderOptions();
            options.AlwaysIncludeServerUrl = true;
            string link = LinkManager.GetItemUrl(Context.Item, options);
            args.ContextData.Add(Key, link);
        }
    }
}
