using Sitecore.Data;
using Sitecore.Shell.Applications.ContentEditor;

namespace JSSDemo.Feature.Blog
{
    public static class Templates
    {
        public static class BlogHomePage
        {
            public static readonly ID TemplateId = new ID("{D73AE061-4A9A-4BC9-B42B-B3D8ED72785E}");
        }

        public static class BlogArticlePage
        {
            public static readonly ID TemplateId = new ID("{E2A96FE0-5A50-49CF-9C8D-540209DC5AEE}");
            public static readonly ID StandardValuesItemId = new ID("{0E4E23B9-EF1A-493A-B8CB-4D9328EAF20B}");

            public static class Fields
            {
                public static readonly ID Categories = new ID("{CD012BDC-4429-475F-83A6-3E77FF8B68DF}");
                public static readonly ID ArticlePublishedDate = new ID("{2813D4B1-CB15-4341-8074-7C6ED52E0217}");
                public static readonly ID ArticleTitle = new ID("{C10A6CCB-1A4E-4C30-A4B2-7733F05938B8}");
                public static readonly ID HeaderImage = new ID("{4C568C60-50C8-4A5E-BAFD-BFB84C09E038}");
                public static readonly ID SuggestedImage = new ID("{9D0F0657-98A2-4A1F-A6E0-BD386E8586D9}");
                public static readonly ID Teaser = new ID("{58D47FBB-9636-4BD1-8CF8-42B877A9320B}");
            }
        }

        public static class BlogCardRenderingModel
        {
            public static readonly ID TemplateId = new ID("{F4CF940A-CE58-4855-B36F-AD0A458333D9}");

            public static class Fields
            {
                public static readonly ID Categories = new ID("{6F14D50F-A6F7-4E3C-8D2D-20081B4CB864}");
                public static readonly ID Url = new ID("{19324460-7D91-43B5-9BD0-79B72DC53E80}");
                public static readonly ID ArticleTitle = new ID("{74B45DE9-4E6A-4BBE-A7FF-7CC6E8EBB887}");
                public static readonly ID Image = new ID("{1FBEE3D2-37A4-4997-9E46-22F1B7419DC5}");
                public static readonly ID Teaser = new ID("{58D47FBB-9636-4BD1-8CF8-42B877A9320B}");
            }
        }

        public static class BlogCategoryPage
        {
            public static readonly ID TemplateId = new ID("{CC553EF3-0948-43DB-BE5C-EACC51920F3D}");

            public static class Fields
            {
                public static readonly ID Category = new ID("{58574FBB-5032-4F9B-90B6-580AB472834A}");
            }
        }

        public static class RecommendedForYouTemplate
        {
            public static string RecommendedItemsFieldName = "recommendedItems";
            public static ID RecommendedItemsFieldId = new ID("{21079E78-86F7-44D8-857F-7738BFB42A80}");
            public static int RecommendedItemsMaxArticles = 3;
        }
    }
}