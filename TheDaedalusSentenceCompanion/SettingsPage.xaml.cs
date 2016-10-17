using System;

using Xamarin.Forms;

namespace TheDaedalusSentenceCompanion
{
	public partial class SettingsPage : ContentPage
	{
		ISoundProvider AudioManager { get; set; } = DependencyService.Get<ISoundProvider>();

		GameSettings GameSettings { get; set; }
		bool Loaded { get; set; } = false;
		
		public SettingsPage(GameSettings gameSettings)
		{
			InitializeComponent();

			GameSettings = gameSettings;

			BindingContext = GameSettings;

			Loaded = true;
		}

		void OnButtonClicked(object sender, EventArgs args)
		{
			AudioManager.PlayClick();
			AudioManager.SuspendBackgroundMusic();
			Page gamePage = new GamePage(GameSettings);
			Navigation.PushModalAsync(gamePage);
		}

		void OnBackButtonClicked(object sender, EventArgs args)
		{
			AudioManager.PlayClick();
			Navigation.PopModalAsync();
		}

		void SwitchToggleSound(object sender, EventArgs args)
		{
			if (Loaded)
			{
				AudioManager.PlayBleep();
			}
		}
	}
}