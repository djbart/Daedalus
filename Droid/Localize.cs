using System;
using Xamarin.Forms;

[assembly:Dependency(typeof(TheDaedalusSentenceCompanion.Droid.Localize))]

namespace TheDaedalusSentenceCompanion.Droid
{
	public class Localize : TheDaedalusSentenceCompanion.ILocalize
	{
		public System.Globalization.CultureInfo GetCurrentCultureInfo()
		{
			var androidLocale = Java.Util.Locale.Default;
			var netLanguage = androidLocale.ToString().Replace("_", "-");
			return new System.Globalization.CultureInfo(netLanguage);
		}
	}
}