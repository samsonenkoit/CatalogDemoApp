using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Model
{
    public class Catalog
    {
        /// <summary>
        /// Ссылка на логотип каталога, может быть null
        /// </summary>
        public string LogoUrl { get; set; }

        /// <summary>
        /// Авторы каталога
        /// </summary>
        public string Authors { get; set; }

        public string Title { get; set; }

        public Dictionary<string, string> Options { get; set; }
    }
}
