using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TheDaedalusSentenceCompanion
{
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage()
		{
			InitializeComponent();
		}

		void OnButtonClicked(object sender, EventArgs args)
		{
			Button button = (Button)sender;

			Page gamePage = new GamePage();

			button.Navigation.PushModalAsync(gamePage);
		}
	}
}

