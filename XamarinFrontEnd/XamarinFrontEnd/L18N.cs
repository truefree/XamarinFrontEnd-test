using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace XamarinFrontEnd
{
    public class L18N : INotifyPropertyChanged
    {
        public L18N() {
            MainText = "hell no";
        }
        public string MainText { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
