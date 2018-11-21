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
			setLocalizationFromCode();
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
	}
}
