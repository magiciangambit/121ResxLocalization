# 121ResxLocalization
Xamarin Resx Localization

This plugin is to ease developer to implement resx localization in xamarin


After installing the plugin into your project from nuget: https://www.nuget.org/packages/Plugin._121ResxLocalization/1.0.0#

1) Create a Resx folder inside your main project
2) Create new resource file inside Resx folder, for English -> AppResources.resx, for Spanish -> AppResources.es.resx, and for others you can refer to this page: https://msdn.microsoft.com/en-us/library/cc233982.aspx
3) Open your App.cs and insert lines below, please note that you need to change resourceBaseName and resourceAssembly to refer to your project:

            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                Boolean checkLocal = Cross_121ResxLocalization.IsSupported;
                I_121ResxLocalization iService = Cross_121ResxLocalization.Current;
                
                // determine the correct, supported .NET culture
                var ci = iService.GetCurrentCultureInfo();
                Plugin._121ResxLocalization.TranslateExtension trE = new Plugin._121ResxLocalization.TranslateExtension(ci);
                 Plugin._121ResxLocalization.TranslateExtension.resourceBasename = "<project name>.Resx.AppResources";
                Plugin._121ResxLocalization.TranslateExtension.resourceAssembly = IntrospectionExtensions.GetTypeInfo(typeof(< project name>.<any class>)).Assembly;
                Resx.AppResources.Culture = ci; // set the RESX for resource localization
                iService.SetLocale(ci); // set the Thread for locale-aware methods
            }
            
 For example:
 
 public App ()
		{
			InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                Boolean checkLocal = Cross_121ResxLocalization.IsSupported;
                I_121ResxLocalization iService = Cross_121ResxLocalization.Current;
                
                // determine the correct, supported .NET culture
                var ci = iService.GetCurrentCultureInfo();
                Plugin._121ResxLocalization.TranslateExtension trE = new Plugin._121ResxLocalization.TranslateExtension(ci);
                 Plugin._121ResxLocalization.TranslateExtension.resourceBasename = "<project name>.Resx.AppResources";
                Plugin._121ResxLocalization.TranslateExtension.resourceAssembly = IntrospectionExtensions.GetTypeInfo(typeof(< project name>.<any class>)).Assembly;
                Resx.AppResources.Culture = ci; // set the RESX for resource localization
                iService.SetLocale(ci); // set the Thread for locale-aware methods
            }

            MainPage = new TestLocalizationNuget.MainPage();
		}
		
4) To apply localization in a page:

Please take a look at the sample: https://github.com/magiciangambit/121ResxLocalization/blob/121ResxLocalization/TestLocalizationNuget/TestLocalizationNuget/MainPage.xaml
