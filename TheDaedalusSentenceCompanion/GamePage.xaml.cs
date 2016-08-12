using System;

using Xamarin.Forms;

namespace TheDaedalusSentenceCompanion
{
	public partial class GamePage : ContentPage
	{
		GameSettings GameSettings { get; set; }
		DateTime GameStartTime { get; set; }
		DateTime RoundStartTime { get; set; }
		bool RoundActive { get; set; } = false;
		const string NoTimeRemaining = "00:00:00";

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

			RoundTimerRemainingLabel.Text = string.Format("00:00:{0}", GameSettings.RoundTimerInSeconds.ToString("D2"));
		}

		void OnQuitButtonClicked(object sender, EventArgs args)
		{
			var button = (Button)sender;

			button.Navigation.PopModalAsync();
		}

		void OnStartRoundButtonClicked(object sender, EventArgs args)
		{
			SwitchRound();
		}

		bool UpdateGameTimer()
		{
			GameTimerRemainingLabel.Text = GetTimeRemaining(GameSettings.GameTimerInMinutes * 60, GameStartTime);

			return true;
	    }

		bool UpdateRoundTimer()
		{
			if (RoundActive)
			{
				string timeRemaining = GetTimeRemaining(GameSettings.RoundTimerInSeconds, RoundStartTime);

				if (timeRemaining == NoTimeRemaining)
				{
					SwitchRound();
				}
				else
				{
					RoundTimerRemainingLabel.Text = timeRemaining;
				}
			}

			return RoundActive;
		}

		string GetTimeRemaining(int totalSeconds, DateTime startTime)
		{
			var currentTime = DateTime.Now;
			var elapsedTime = currentTime - startTime;

			var totalSecondsRemaining = totalSeconds - Math.Round(elapsedTime.TotalSeconds);

			if (totalSecondsRemaining > 0)
			{
				var hours = (short)(totalSecondsRemaining / 3600);
				var minutes = (short)(totalSecondsRemaining % 3600 / 60);
				var seconds = (short)(totalSecondsRemaining % 3600 % 60);

				return string.Format("{0}:{1}:{2}", hours.ToString("D2"), minutes.ToString("D2"), seconds.ToString("D2"));
			}
			else
			{
				return NoTimeRemaining;
			}
		}

		void SwitchRound()
		{
			if (!RoundActive)
			{
				RoundStartTime = DateTime.Now;

				Device.StartTimer(TimeSpan.FromSeconds(1), UpdateRoundTimer);

				StartRoundButton.Text = "End round " + GameSettings.CurrentRoundNumber;
			}
			else
			{
				GameSettings.CurrentRoundNumber++;
				StartRoundButton.Text = "Start round " + GameSettings.CurrentRoundNumber;
				RoundTimerRemainingLabel.Text = string.Format("00:00:{0}", GameSettings.RoundTimerInSeconds.ToString("D2"));
			}

			RoundActive = !RoundActive;
		}
	}
}