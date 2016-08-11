using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TheDaedalusSentenceCompanion
{
	public partial class HomePage : ContentPage
	{
		public HomePage()
		{
			InitializeComponent();
		}

		void OnSecondTimeButtonClicked(object sender, EventArgs args)
		{
			Page settingsPage = new SettingsPage(new GameSettings(GameModes.SecondTimeThrough));

			var button = (Button)sender;
			button.Navigation.PushModalAsync(settingsPage);
		}

		void OnVeteranButtonClicked(object sender, EventArgs args)
		{
			Page settingsPage = new SettingsPage(new GameSettings(GameModes.Veteran));

			var button = (Button)sender;
			button.Navigation.PushModalAsync(settingsPage);
		}

		void OnExpertButtonClicked(object sender, EventArgs args)
		{
			Page settingsPage = new SettingsPage(new GameSettings(GameModes.Expert));

			var button = (Button)sender;
			button.Navigation.PushModalAsync(settingsPage);
		}

		void OnHellInSpaceButtonClicked(object sender, EventArgs args)
		{
			Page settingsPage = new SettingsPage(new GameSettings(GameModes.HellInSpace));

			var button = (Button)sender;
			button.Navigation.PushModalAsync(settingsPage);
		}

		void OnCustomButtonClicked(object sender, EventArgs args)
		{
			Page settingsPage = new SettingsPage(new GameSettings(GameModes.Custom));

			var button = (Button)sender;
			button.Navigation.PushModalAsync(settingsPage);
		}
	}
}

