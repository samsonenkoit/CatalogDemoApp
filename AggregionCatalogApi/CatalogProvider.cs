using AggregionCatalogApi.Model;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ViewModel.Interface;
using ViewModel.Model;

namespace AggregionCatalogApi
{
    public class CatalogProvider : BaseService, ICatalogProvider
    {
        #region Static

        internal static string GetCoverUrl(string covertId)
        {
            return $"https://storage.aggregion.com/api/files/{covertId}/shared/data";
        }

        #endregion

        public Task<IList<Catalog>> GetAsync(CancellationToken token)
        {
            return GetAsync("", token);
        }

        public async Task<IList<Catalog>> GetAsync(string searchStr, CancellationToken token)
        {
            string url = $"https://ds.aggregion.com/api/public/catalog?filter=title.default(\"{searchStr}\",iregex)";

            var result = await GetAsync(url, null, null, token).ConfigureAwait(false);

            result.EnsureSuccessStatusCode();

            var catalogs = await ParseCatalogsAsync(result).ConfigureAwait(false);


            return catalogs.Select(t => t.ToCatalog()).ToList();
        }


        #region Helper

        private async Task<IList<CatalogJson>> ParseCatalogsAsync(HttpResponseMessage responseMessage)
        {
            var contentStr = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            var catalogs = JsonConvert.DeserializeObject<List<CatalogJson>>(contentStr);

            return catalogs;
        }

        #endregion
    }
}
