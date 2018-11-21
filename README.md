# 121ResxLocalization
Xamarin Resx Localization

This plugin is to ease developer to implement resx localization in xamarin

Note: All of your projects under a solution must be using Xamarin Forms 3.3.0.912540 or later

      All projects must be using the same version of Xamarin Forms if not, there will be token error

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

5) From the sample page, you can also find out how to change language during runtime on the button click:

'''

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
'''

6) To use localization programmatically or from code, for example from the sample you can find these lines:



''' 

		//set localization from coding
		public void setLocalizationFromCode()
		{
		    // create UI controls
		    var myLabel = new Label();
		    var myEntry = new Entry();
		    var myButton = new Button();
		    var myPicker = new Picker();
		    myPicker.Items.Add("0");
		    myPicker.Items.Add("1");
		    myPicker.Items.Add("2");
		    myPicker.Items.Add("3");
		    myPicker.Items.Add("4");

		    // apply translated resources
		    myLabel.Text = AppResources.NotesLabel;
		    myEntry.Placeholder = AppResources.NotesPlaceholder;
		    myPicker.Title = AppResources.PickerName;
		    myButton.Text = AppResources.AddButton;

		    // button shows an alert, also translated
		    myButton.Clicked += async (sender, e) =>
		    {
			AppResources.Culture = CultureInfo.CurrentCulture;
			var message = AppResources.AddMessageN;
			if (myPicker.SelectedIndex <= 0)
			{
			    message = AppResources.AddMessage0;
			}
			else if (myPicker.SelectedIndex == 1)
			{
			    message = AppResources.AddMessage1;
			}
			else
			{
			    message = String.Format(message, myPicker.Items[myPicker.SelectedIndex]);
			}
			await DisplayAlert(message, message, AppResources.CancelButton);
		    };

		    // add to screen
		    stack1.VerticalOptions = LayoutOptions.Center;
		    stack1.HorizontalOptions = LayoutOptions.FillAndExpand;
		    stack1.Children.Add(myLabel);
		    stack1.Children.Add(myEntry);
		    stack1.Children.Add(myButton);
		    stack1.Children.Add(myPicker);

		}
		
'''
