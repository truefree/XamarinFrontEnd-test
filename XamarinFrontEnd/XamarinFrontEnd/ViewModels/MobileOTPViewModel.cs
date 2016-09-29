using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinFrontEnd.ViewModels
{
    public class MobileOTPViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        private string _passcode;

        public string Passcode
        {
            get
            {
                return this._passcode;
            }

            set
            {
                this._passcode = value;   
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MobileOTPViewModel()
        {
            this.Passcode = string.Empty;
            //MessagingCenter.Send<MobileOTPViewModel>(this, "ProgressFire");

            Xamarin.Forms.Device.StartTimer(
                TimeSpan.FromSeconds(1),
                () =>
                {
                    string s = this.getOTPCode();
                    if(s.Equals(this.Passcode) == false)
                    {
                        this.Passcode = this.getOTPCode();
                        RaisePropertyChanged("Passcode");
                    }
                    return true;
                }
                );
        }

        private string getOTPCode()
        {
            return Helpers.TOTPHelper.GetCode(Helpers.Settings.OTPID);
        }

        public void RaisePropertyChanged(string propName)
        {
            if(PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
            MessagingCenter.Send<MobileOTPViewModel>(this, "ProgressFire");
        }
    }
}
