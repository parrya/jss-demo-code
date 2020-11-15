using Newtonsoft.Json.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.LayoutService.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace JSSDemo.Feature.Blog.Extensions
{
    public class RecommendedBlogsContentsResolver2 : Sitecore.LayoutService.ItemRendering.ContentsResolvers.RenderingContentsResolver
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
                    var articleModel = MapItemToRenderingModelItem(item, GetItemUrl(item));
                    items.Add(articleModel);
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

        private Item MapItemToRenderingModelItem(Item item, string itemUrl)
        {
            var newId = new ID();
            var def = new ItemDefinition(newId, item.Name, Templates.BlogCardRenderingModel.TemplateId, ID.Null);

            //Populate fields
            var fields = new FieldList();
            fields.Add(Templates.BlogCardRenderingModel.Fields.ArticleTitle, item.Fields[Templates.BlogArticlePage.Fields.ArticleTitle].Value);

            //Use SuccessImage if available, else use HeaderImage
            if (item.Fields[Templates.BlogArticlePage.Fields.SuggestedImage].HasValue)
            {
                fields.Add(Templates.BlogCardRenderingModel.Fields.Image, item.Fields[Templates.BlogArticlePage.Fields.SuggestedImage].Value);
            }
            else
            {
                fields.Add(Templates.BlogCardRenderingModel.Fields.Image, item.Fields[Templates.BlogArticlePage.Fields.HeaderImage].Value);
            }

            fields.Add(Templates.BlogCardRenderingModel.Fields.Categories, item.Fields[Templates.BlogArticlePage.Fields.Categories].Value);
            fields.Add(Templates.BlogCardRenderingModel.Fields.Url, itemUrl);
            fields.Add(Templates.BlogCardRenderingModel.Fields.Teaser, item.Fields[Templates.BlogArticlePage.Fields.Teaser].Value);

            var data = new ItemData(def, Language.Current, Sitecore.Data.Version.First, fields);
            var db = Sitecore.Context.Database;

            var resultItem = new Item(newId, data, db);
            return resultItem;
        }

        private string GetItemUrl(Item item)
        {
            return Sitecore.Links.LinkManager.GetItemUrl(item);
        }
    }
}
