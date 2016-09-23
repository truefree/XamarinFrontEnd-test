using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XamarinFrontEnd.ViewModels
{
    public class MFAViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        private int _remainSeconds;
        public int RemainSeconds
        {
            get { return this._remainSeconds; }
            set
            {
                this._remainSeconds = value;
                RaisePropertyChanged("RemainSeconds");

            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public MFAViewModel()
        {
            this.RemainSeconds = 60;
        }

        public void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
