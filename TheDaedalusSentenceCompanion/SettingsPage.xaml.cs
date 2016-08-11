using System;
using System.Collections.Generic;

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
			Button button = (Button)sender;

			Page gamePage = new GamePage();

			button.Navigation.PushModalAsync(gamePage);
		}
	}
}

