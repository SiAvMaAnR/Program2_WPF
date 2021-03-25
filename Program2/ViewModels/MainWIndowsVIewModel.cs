using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program2_WPF.ViewModels
{
    public class MainWindowsViewModel:EventChangedNotify
    {
        private string _ErrorInfo;
        public string ErrorInfo
        {
            get { return _ErrorInfo; }
            set
            {
                _ErrorInfo = value;
                OnPropertyCHanged(nameof(ErrorInfo));
            }
        }


    }
}
