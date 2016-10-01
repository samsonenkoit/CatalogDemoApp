using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ViewModel.Model;

namespace ViewModel.Interface
{
    public interface ICatalogProvider
    {

        Task<IList<Catalog>> GetAsync(string searchStr, CancellationToken token);

        Task<IList<Catalog>> GetAsync(CancellationToken token);
    }
}
