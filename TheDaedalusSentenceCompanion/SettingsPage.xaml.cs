using System;

using Xamarin.Forms;

namespace TheDaedalusSentenceCompanion
{
	public partial class SettingsPage : ContentPage
	{
		ISoundProvider AudioManager { get; set; } = DependencyService.Get<ISoundProvider>();

		GameSettings GameSettings { get; set; }
		
		public SettingsPage(GameSettings gameSettings)
		{
			InitializeComponent();

			GameSettings = gameSettings;

			BindingContext = GameSettings;
		}

		void OnButtonClicked(object sender, EventArgs args)
		{
			AudioManager.PlayClick();
			Page gamePage = new GamePage(GameSettings);
			Navigation.PushModalAsync(gamePage);
		}

		void OnBackButtonClicked(object sender, EventArgs args)
		{
			AudioManager.PlayClick();
			Navigation.PopModalAsync();
		}
	}
}