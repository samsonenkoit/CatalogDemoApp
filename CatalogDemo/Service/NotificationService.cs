using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModel.Interface;

namespace CatalogDemo.Service
{
    public class NotificationService : INotificationService
    {
        public void Notify(string message)
        {
            MessageBox.Show(message);
        }
    }
}
