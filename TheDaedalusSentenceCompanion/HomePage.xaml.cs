using System;

using Xamarin.Forms;

namespace TheDaedalusSentenceCompanion
{
	public partial class HomePage : ContentPage
	{
		ISoundProvider AudioManager { get; set; } = DependencyService.Get<ISoundProvider>();

		public HomePage()
		{
			InitializeComponent();
		}

		void OnSecondTimeButtonClicked(object sender, EventArgs args)
		{
			LaunchSettingsPage(GameModes.SecondTimeThrough);
		}

		void OnVeteranButtonClicked(object sender, EventArgs args)
		{
			LaunchSettingsPage(GameModes.Veteran);
		}

		void OnExpertButtonClicked(object sender, EventArgs args)
		{
			LaunchSettingsPage(GameModes.Expert);
		}

		void OnHellInSpaceButtonClicked(object sender, EventArgs args)
		{
			LaunchSettingsPage(GameModes.HellInSpace);
		}

		void OnCustomButtonClicked(object sender, EventArgs args)
		{
			LaunchSettingsPage(GameModes.Custom);
		}

		void LaunchSettingsPage(GameModes gameMode)
		{
			AudioManager.PlayClick();
			Page settingsPage = new SettingsPage(new GameSettings(gameMode));
			Navigation.PushModalAsync(settingsPage);
		}
	}
}

