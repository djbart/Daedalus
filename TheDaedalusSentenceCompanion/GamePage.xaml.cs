using System;

using Xamarin.Forms;

namespace TheDaedalusSentenceCompanion
{
	public partial class GamePage : ContentPage
	{
		private GameSettings GameSettings { get; set; }
		private DateTime GameStartTime { get; set; }

		public GamePage(GameSettings gameSettings)
		{
			InitializeComponent();

			GameSettings = gameSettings;

			BindingContext = GameSettings;

			if (GameSettings.GameTimerEnabled)
			{
				GameStartTime = DateTime.Now;
				UpdateGameTimer();

				Device.StartTimer(TimeSpan.FromSeconds(1), UpdateGameTimer);
			}
		}

		void OnQuitButtonClicked(object sender, EventArgs args)
		{
			var button = (Button)sender;

			button.Navigation.PopModalAsync();
		}

		bool UpdateGameTimer()
		{
			var currentTime = DateTime.Now;
			var elapsedTime = currentTime - GameStartTime;

			var totalSecondsRemaining = (GameSettings.GameTimerInMinutes * 60) - Math.Round(elapsedTime.TotalSeconds);

			var hours = (short)(totalSecondsRemaining / 3600);
			var minutes = (short)(totalSecondsRemaining % 3600 / 60);
			var seconds = (short)(totalSecondsRemaining % 3600 % 60);

			GameTimerRemainingLabel.Text = string.Format("{0}:{1}:{2}", hours.ToString("D2"), minutes.ToString("D2"), seconds.ToString("D2"));

			return true;
	    }
	}
}

