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

			RerollDice();
		}

		void RerollDice()
		{
			if (GameSettings.DisabledLocationDieEnabled)
			{
				RollDisabledLocationDie(DisabledLocationDieImage);
			}

			if (GameSettings.RoundTimerDieEnabled)
			{
				RollRoundDie(RoundTimerDieImage);
			}

			if (GameSettings.TheseusDieEnabled)
			{
				RollTheseusDie(TheseusDieImage);
			}
		}

		void OnQuitButtonClicked(object sender, EventArgs args)
		{
			Navigation.PopModalAsync();
		}

		async void OnMadeItButtonClicked(object sender, EventArgs args)
		{
			var answer = await DisplayAlert("Congratulations", "You escaped the space station!", "Back to home screen","Cancel");

			if (answer == true)
			{
				await Navigation.PopModalAsync(false);
				await Navigation.PopModalAsync();
			}
		}

		void OnStartRoundButtonClicked(object sender, EventArgs args)
		{
			SwitchRound();
		}

		bool UpdateGameTimer()
		{
			string timeRemaining = GetTimeRemaining(GameSettings.GameTimerInMinutes * 60, GameStartTime);

			if (timeRemaining == NoTimeRemaining)
			{
				GameTimerEnded();
			}
			else
			{
				GameTimerRemainingLabel.Text = timeRemaining;
			}

			return true;
	    }

		async void GameTimerEnded()
		{
			await DisplayAlert("Time's up", "You failed to escape the space prison", "Back to home screen");
			await Navigation.PopModalAsync(false);
			await Navigation.PopModalAsync();
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
				RerollDice();
			}

			RoundActive = !RoundActive;

			if (RoundActive)
			{
				DisabledLocationDieImage.Opacity = 0.5;
				RoundTimerDieImage.Opacity = 0.5;
				TheseusDieImage.Opacity = 0.5;
			}
			else
			{
				DisabledLocationDieImage.Opacity = 1;
				RoundTimerDieImage.Opacity = 1;
				TheseusDieImage.Opacity = 1;
			}

		}


		void OnTapDisabledLocationDie(object sender, EventArgs args)
		{
			if (!RoundActive)
			{
				RollDisabledLocationDie((Image)sender);
			}
		}

		void OnTapRoundTimerDie(object sender, EventArgs args)
		{
			if (!RoundActive)
			{
				RollRoundDie((Image)sender);
			}
		}

		void OnTapTheseusDie(object sender, EventArgs args)
		{
			if (!RoundActive)
			{
				RollTheseusDie((Image)sender);
			}
		}

		async void RollDisabledLocationDie(Image imageToUpdate)
		{
			var rnd = new Random();

			await imageToUpdate.FadeTo(0, 250, Easing.Linear);
			imageToUpdate.Source = string.Format("diespecial{0}.png", rnd.Next(1, 7));
			await imageToUpdate.FadeTo(1, 250, Easing.Linear);
		}

		async void RollRoundDie(Image imageToUpdate)
		{
			var rnd = new Random();
			var diceroll = rnd.Next(1, 7);

			await imageToUpdate.FadeTo(0, 250, Easing.Linear);
			imageToUpdate.Source = string.Format("dietimer{0}.png", diceroll);
			GameSettings.RoundTimerInSeconds = 10 + (5 * diceroll);
			await imageToUpdate.FadeTo(1, 250, Easing.Linear);
		}

		async void RollTheseusDie(Image imageToUpdate)
		{
			var rnd = new Random();

			await imageToUpdate.FadeTo(0, 250, Easing.Linear);
			imageToUpdate.Source = string.Format("dietheseus{0}.png", rnd.Next(1, 7));
			await imageToUpdate.FadeTo(1, 250, Easing.Linear);	
		}
	}
}