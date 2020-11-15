using Newtonsoft.Json.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.LayoutService.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace JSSDemo.Feature.Blog.Extensions
{
    public class RecommendedBlogsContentsResolver1 : Sitecore.LayoutService.ItemRendering.ContentsResolvers.RenderingContentsResolver
    {
        private List<Item> items = new List<Item>();

        public override object ResolveContents(Sitecore.Mvc.Presentation.Rendering rendering, IRenderingConfiguration renderingConfig)
        {
            Assert.ArgumentNotNull(rendering, nameof(rendering));
            Assert.ArgumentNotNull(renderingConfig, nameof(renderingConfig));

            Item ds = GetContextItem(rendering, renderingConfig);

            var recommendedItemsFieldId = Templates.RecommendedForYouTemplate.RecommendedItemsFieldId;

            //if the rendering datasource has curated items
            if (ds.Fields.Contains(recommendedItemsFieldId) && !string.IsNullOrWhiteSpace(ds.Fields[recommendedItemsFieldId].Value))
            {
                List<string> recItemIds = ds.Fields[recommendedItemsFieldId].Value.Split('|').ToList();
                foreach (var id in recItemIds)
                {
                    var item = Sitecore.Context.Database.GetItem(new ID(id));
                    items.Add(item);
                }
            }
            
            if (!items.Any())
                return null;

            JObject jobject = new JObject()
            {
                ["items"] = (JToken)new JArray()
            };

            List<Item> objList = items != null ? items.ToList() : null;
            if (objList == null || objList.Count == 0)
                return jobject;
            jobject["items"] = ProcessItems(objList, rendering, renderingConfig);
            return jobject;
        }        
    }
}
