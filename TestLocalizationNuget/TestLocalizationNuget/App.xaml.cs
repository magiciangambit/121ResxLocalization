using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Plugin._121ResxLocalization;

using Xamarin.Forms;

namespace TestLocalizationNuget
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                Boolean checkLocal = Cross_121ResxLocalization.IsSupported;
                I_121ResxLocalization iService = Cross_121ResxLocalization.Current;

                
                
                //I_121ResxLocalization iService = DependencyService.Get<I_121ResxLocalization>();
                // determine the correct, supported .NET culture
                var ci = iService.GetCurrentCultureInfo();
                Plugin._121ResxLocalization.TranslateExtension trE = new Plugin._121ResxLocalization.TranslateExtension(ci);
                Plugin._121ResxLocalization.TranslateExtension.resourceBasename = "TestLocalizationNuget.Resx.AppResources";
                Plugin._121ResxLocalization.TranslateExtension.resourceAssembly = IntrospectionExtensions.GetTypeInfo(typeof(Plugin._121ResxLocalization.TranslateExtension)).Assembly;
                Resx.AppResources.Culture = ci; // set the RESX for resource localization
                iService.SetLocale(ci); // set the Thread for locale-aware methods
            }

            MainPage = new TestLocalizationNuget.MainPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
