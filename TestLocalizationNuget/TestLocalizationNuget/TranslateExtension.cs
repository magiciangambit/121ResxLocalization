using Plugin._121ResxLocalization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestLocalizationNuget
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        readonly CultureInfo ci = null;
        //Type myType = typeof(Resx.AppResources);
        //string namSpace = (string)myType.Namespace;

        //const string ResourceId = "TestLocalizationNuget.Resx.AppResources";

        //static readonly Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(() => new ResourceManager(ResourceId, IntrospectionExtensions.GetTypeInfo(typeof(TranslateExtension)).Assembly));

        
        //ResourceManager rscMgr = new ResourceManager(check, IntrospectionExtensions.GetTypeInfo(typeof(TranslateExtension)).Assembly);

        public string Text { get; set; }

        public static string GetCurrentNamespace()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().EntryPoint.DeclaringType.Namespace;
        }

        public TranslateExtension()
        {
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                ci = DependencyService.Get<I_121ResxLocalization>().GetCurrentCultureInfo();
            }
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            var resourceTypeInfo = typeof(Label).GetTypeInfo();
            var resourceBasename = resourceTypeInfo.FullName;
            var resourceAssembly = resourceTypeInfo.Assembly;

            var ResMgr = new ResourceManager(resourceBasename, resourceAssembly);

            //var translation = resourceManager.GetString(Text, cultureInfo);
            if (Text == null)
                return string.Empty;

            var translation = ResMgr.GetString(Text, ci);
            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException(
                    string.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Text, resourceBasename, ci.Name),
                    "Text");
#else
				translation = Text; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#endif
            }
            return translation;
        }
    }
}
