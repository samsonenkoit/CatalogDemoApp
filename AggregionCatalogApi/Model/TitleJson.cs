using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AggregionCatalogApi.Model
{
    internal class TitleJson
    {
        [JsonProperty(PropertyName = "default")]
        internal string Default { get; set; }
    }
}
