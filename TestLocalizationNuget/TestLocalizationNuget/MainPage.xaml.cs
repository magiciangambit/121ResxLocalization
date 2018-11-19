using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestLocalizationNuget
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}
		
		//change language during runtime
		void OnButtonClicked(object sender, EventArgs e)
		{
		    if (((Button)sender).Text == "Set as English")
		    {
			setLanguage("en-US");
		    }
		    else if (((Button)sender).Text == "Set as Spanish")
		    {
			setLanguage("es-US");
		    }
		}

		public void setLanguage(string languageCode)
		{

		    if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
		    {
			CultureInfo ci = new CultureInfo(languageCode);
			DependencyService.Get<Plugin._121ResxLocalization.I_121ResxLocalization>().SetLocale(ci); // set the Thread for locale-aware methods
			Resx.AppResources.Culture = ci;

			Application.Current.Properties["currentLanguage"] = languageCode;
			Application.Current.MainPage = new MainPage();
		    }
		}
	}
}
