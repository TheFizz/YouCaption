using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace YouCaptionBase.ViewModels
{
    public class AboutPageViewModel : BaseViewModel
    {

        public ICommand ShowLaerdalLicenseCommand { get; }
        public ICommand ShowExplodeLicenseCommand { get; }

        public AboutPageViewModel()
        {
            Title = "About";
            ShowLaerdalLicenseCommand = new Command(ShowLaerdalLicense);
            ShowExplodeLicenseCommand = new Command(ShowExplodeLicense);
        }

        private void ShowExplodeLicense(object obj)
        {
            Launcher.OpenAsync(new Uri("https://licenses.nuget.org/LGPL-3.0-only"));
        }

        private void ShowLaerdalLicense(object obj)
        {
            Launcher.OpenAsync(new Uri("https://www.nuget.org/packages/Laerdal.FFmpeg.Full.Gpl/4.4.27/license"));
        }
    }
}
