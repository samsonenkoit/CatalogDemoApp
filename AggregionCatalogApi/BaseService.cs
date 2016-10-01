using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using AggregionCatalogApi.Extension;
using System.Threading;

namespace AggregionCatalogApi
{
    public class BaseService
    {
        protected async Task<HttpResponseMessage> GetAsync(string url, IDictionary<string, string> parameters,
    IDictionary<string, string> headers, CancellationToken cancellationToken)
        {

            if (string.IsNullOrEmpty(url)) throw new ArgumentOutOfRangeException(nameof(url));

            if (parameters != null && parameters.Any())
            {
                StringBuilder stringBuilder = new StringBuilder(url);
                stringBuilder.Append("?");

                foreach (var parameter in parameters)
                {
                    stringBuilder.Append($"{parameter.Key}={parameter.Value}");
                }

                url = stringBuilder.ToString();
            }

            using (HttpClient client = new HttpClient())
            {
                if (headers != null)
                    client.DefaultRequestHeaders.AddRange(headers);

                var result = await client.GetAsync(url, cancellationToken).ConfigureAwait(false);

                return result;
            }

        }

        protected async Task<HttpResponseMessage> PostAsync(string url, HttpContent content,
            IDictionary<string, string> headers, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentOutOfRangeException(nameof(url));

            using (HttpClient client = new HttpClient())
            {
                if (headers != null)
                    client.DefaultRequestHeaders.AddRange(headers);

                var result = await client.PostAsync(url, content, cancellationToken);

                return result;
            }
        }
    }
}
