using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using Foundation;
using UIKit;

namespace TheDaedalusSentenceCompanion.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public iOSSoundProvider AudioManager { get; set; } = new iOSSoundProvider();

		public override bool FinishedLaunching (UIApplication uiApplication, NSDictionary launchOptions)
		{
			Contract.Requires(launchOptions != null);
			global::Xamarin.Forms.Forms.Init ();

			LoadApplication (new App ());
			UIApplication.SharedApplication.IdleTimerDisabled = true;

			return base.FinishedLaunching (uiApplication, launchOptions);
		}
	}
}

