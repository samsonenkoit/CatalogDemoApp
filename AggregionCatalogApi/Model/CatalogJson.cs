using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Model;

namespace AggregionCatalogApi.Model
{
    internal class CatalogJson
    {
        [JsonProperty(PropertyName = "id")]
        internal string Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        internal TitleJson Title { get; set; }

        [JsonProperty(PropertyName = "cover")]
        internal string CoverId { get; set; }
        
        [JsonProperty(PropertyName = "options")]
        internal Dictionary<string, string> Options { get; set; }

        internal Catalog ToCatalog()
        {
            var catalog = new Catalog()
            {
                Title = Title.Default,
                LogoUrl = string.IsNullOrEmpty(CoverId) ? "" : CatalogProvider.GetCoverUrl(CoverId),
                Options = Options ?? new Dictionary<string, string>()
            };

            string author;
            if (catalog.Options.TryGetValue("authors", out author))
                catalog.Authors = author;

            return catalog;
        }
    }
}
