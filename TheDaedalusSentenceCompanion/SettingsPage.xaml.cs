using System;

using Xamarin.Forms;

namespace TheDaedalusSentenceCompanion
{
	public partial class SettingsPage : ContentPage
	{
		private GameSettings GameSettings { get; set; }
		
		public SettingsPage(GameSettings gameSettings)
		{
			InitializeComponent();

			GameSettings = gameSettings;

			BindingContext = GameSettings;
		}

		void OnButtonClicked(object sender, EventArgs args)
		{
			var button = (Button)sender;

			Page gamePage = new GamePage(GameSettings);

			button.Navigation.PushModalAsync(gamePage);
		}

		void OnBackButtonClicked(object sender, EventArgs args)
		{
			var button = (Button)sender;

			button.Navigation.PopModalAsync();
		}
	}
}

