using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AggregionCatalogApi.Extension
{
    public static class HttpClientExtension
    {
        public static void AddRange(this HttpRequestHeaders headers, IDictionary<string, string> addedHeaders)
        {
            foreach (var addedHeader in addedHeaders)
            {
                headers.Add(addedHeader.Key, addedHeader.Value);
            }
        }

        public static Task<HttpResponseMessage> PostAsJsonAsync(this HttpClient client, string requestUri, object content, CancellationToken cancellationToken)
        {
            string postBody = JsonConvert.SerializeObject(content);
            HttpContent httpContent = new StringContent(postBody, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client.PostAsync(requestUri, httpContent, cancellationToken);
        }
    }
}
