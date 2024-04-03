using Library.Models;
using System.Collections.Generic;

namespace Library.HelperClasses
{
    public class LibraryData
    {
        #region Properties
        public MyCashBox? CashBox { get; set; }
        public List<Users>? Users { get; set; }
        public List<Book>? Books { get; set; }
        public List<Magazine>? Magazines { get; set; }
        #endregion
    }
}
