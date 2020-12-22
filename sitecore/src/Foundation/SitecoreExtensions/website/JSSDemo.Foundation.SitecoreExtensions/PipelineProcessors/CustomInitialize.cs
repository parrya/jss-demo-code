using System.Collections.Generic;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.LayoutService.Configuration;
using Sitecore.LayoutService.ItemRendering;
using Sitecore.LayoutService.Presentation.Pipelines.RenderJsonRendering;
using Sitecore.LayoutService.Serialization;
using Sitecore.Mvc.Presentation;
using System.Linq;

namespace JSSDemo.Foundation.LayoutService.PipelineProcessors
{
    public class CustomInitialize : Initialize
    {
        IRenderingConfiguration _renderingConfiguration;

        public CustomInitialize(IConfiguration configuration) : base(configuration) { }

        protected override RenderedJsonRendering CreateResultInstance(RenderJsonRenderingArgs args)
        {
            _renderingConfiguration = args.RenderingConfiguration;

            return new RenderedJsonRendering()
            {
                Name = args.Rendering.RenderingItem.Name,
                DataSource = args.Rendering.DataSource,
                RenderingParams = SerializeRenderingParams(args.Rendering),
                Uid = args.Rendering.UniqueId
            };
        }

        protected virtual IDictionary<string, string> SerializeRenderingParams(Rendering rendering)
        {
            IDictionary<string, string> paramDictionary = rendering.Parameters.ToDictionary(pair => pair.Key, pair => pair.Value);
            foreach (string key in paramDictionary.Keys.ToList())
            {
                if (ID.TryParse(paramDictionary[key], out var itemId))
                {
                    Item item = rendering.RenderingItem.Database.GetItem(itemId);
                    paramDictionary[key] = _renderingConfiguration.ItemSerializer.Serialize(item, new SerializationOptions() { DisableEditing = true });
                }
            }
            return paramDictionary;
        }
    }
}