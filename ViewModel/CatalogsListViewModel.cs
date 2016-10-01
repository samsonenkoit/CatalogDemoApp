using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ViewModel.Common;
using ViewModel.Interface;
using ViewModel.Model;

namespace ViewModel
{
    public class CatalogsListViewModel: INotifyPropertyChanged
    {

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null) =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

        #region Command

        private DelegateCommand _searchCommand;
        public DelegateCommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new DelegateCommand(async (a) => { await UpdateCatalogsAsync(); },
                    (a) => { return !IsSearching; }));
            }
        }

        #endregion

        private readonly ICatalogProvider _catalogProvider;
        private readonly INotificationService _notificationService;

        private List<Catalog> _catalogs;

        public List<Catalog> Catalogs
        {
            get
            {
                return _catalogs ?? (_catalogs = new List<Catalog>());
            }
            set
            {
                _catalogs = value;
                RaisePropertyChanged();
            }
        }

        private Catalog _selectedCatalog;

        public Catalog SelectedCatalog
        {
            get
            {
                return _selectedCatalog;
            }
            set
            {
                _selectedCatalog = value;
                RaisePropertyChanged();
            }
        }

        private bool _isSearching;
        public bool IsSearching
        {
            get
            {
                return _isSearching;
            }
            set
            {
                _isSearching = value;
                RaisePropertyChanged();
            }
        }

        private string _searchStr;
        public string SearchStr
        {
            get { return _searchStr; }
            set
            {
                _searchStr = value;
                RaisePropertyChanged();
            }
        }

        public CatalogsListViewModel(ICatalogProvider catalogProvider, INotificationService notificationService)
        {
            if (catalogProvider == null) throw new ArgumentNullException(nameof(_catalogProvider));
            if (notificationService == null) throw new ArgumentNullException(nameof(notificationService));

            _catalogProvider = catalogProvider;
            _notificationService = notificationService;

            var updateTask = UpdateCatalogsAsync();
        }

        private async Task UpdateCatalogsAsync()
        {
            if (IsSearching) return;

            try
            {
                IsSearching = true;
                SearchCommand.RaiseCanExecuteChanged();
                Catalogs = (await _catalogProvider.GetAsync(SearchStr,CancellationToken.None)).ToList();
            }
            catch(Exception exp)
            {
                _notificationService.Notify(exp.ToString());
            }
            finally
            {
                IsSearching = false;
                SearchCommand.RaiseCanExecuteChanged();
            }
        }
    }
}
