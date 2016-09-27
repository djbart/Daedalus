using System;

using Xamarin.Forms;

namespace TheDaedalusSentenceCompanion
{
	public partial class GamePage : ContentPage
	{
		GameSettings GameSettings { get; set; }
		DateTime GameStartTime { get; set; }
		DateTime RoundStartTime { get; set; }
		enum RoundState
		{
			Ready,
			Active,
			Finished
		}
		RoundState CurrentRoundState { get; set; } = RoundState.Ready;
		const string NoTimeRemaining = "00:00:00";
		bool isTapped = false;

		ISoundProvider AudioManager { get; set; } = DependencyService.Get<ISoundProvider>();

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
			if (GameSettings.DisabledLocationDieEnabled
				|| GameSettings.RoundTimerDieEnabled
				|| GameSettings.TheseusDieEnabled)
			{
				AudioManager.PlaySound("DiceRoll.wav");
			}

			if (GameSettings.DisabledLocationDieEnabled)
			{
				RollDisabledLocationDie(DisabledLocationDieImage, false);
			}

			if (GameSettings.RoundTimerDieEnabled)
			{
				RollRoundDie(RoundTimerDieImage, false);
			}

			if (GameSettings.TheseusDieEnabled)
			{
				RollTheseusDie(TheseusDieImage, false);
			}
		}

		void OnQuitButtonClicked(object sender, EventArgs args)
		{
			AudioManager.PlayClick();
			Navigation.PopModalAsync();
		}

		async void OnMadeItButtonClicked(object sender, EventArgs args)
		{
			AudioManager.PlayClick();
			var answer = await DisplayAlert(AppResources.Escaped, AppResources.EscapedDescription, AppResources.BackToHome, AppResources.Cancel);

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
			await DisplayAlert(AppResources.Captured, AppResources.CapturedDescription, AppResources.BackToHome);
			await Navigation.PopModalAsync(false);
			await Navigation.PopModalAsync();
		}

		bool UpdateRoundTimer()
		{
			if (CurrentRoundState == RoundState.Active)
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

			return CurrentRoundState == RoundState.Active;
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
			//Advance round state
			switch (CurrentRoundState)
			{
				case RoundState.Active:
					if (GameSettings.DisabledLocationDieEnabled
						|| GameSettings.RoundTimerDieEnabled
						|| GameSettings.TheseusDieEnabled)
					{
						CurrentRoundState = RoundState.Finished;
					}
					else
					{
						AudioManager.RestartBackgroundMusic();
						AudioManager.StopSound();
						RoundTimerRemainingLabel.Text = GameSettings.RoundTimerCountdownText;
						CurrentRoundState = RoundState.Ready;
					}
					break;
				case RoundState.Finished:
					CurrentRoundState = RoundState.Ready;
					break;
				case RoundState.Ready:
					CurrentRoundState = RoundState.Active;
					break;
			}

			switch (CurrentRoundState)
			{
				case RoundState.Active:
					RoundStartTime = DateTime.Now;
					Device.StartTimer(TimeSpan.FromSeconds(1), UpdateRoundTimer);
					AudioManager.SuspendBackgroundMusic();
					AudioManager.PlaySound("Countdown1.mp3", true, (double)(GameSettings.RoundTimerInSeconds+5));
					StartRoundButton.Text = AppResources.EndRound + " " + GameSettings.CurrentRoundNumber;
					break;
				case RoundState.Ready:
					RerollDice();
					GameSettings.CurrentRoundNumber++;
					StartRoundButton.Text = AppResources.StartRound + " " + GameSettings.CurrentRoundNumber;
					break;
				case RoundState.Finished:
					AudioManager.RestartBackgroundMusic();
					AudioManager.StopSound();
					RoundTimerRemainingLabel.Text = NoTimeRemaining;
					StartRoundButton.Text = AppResources.RerollDice;
					break;
			}

			if (CurrentRoundState != RoundState.Ready)
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
			if (CurrentRoundState == RoundState.Ready)
			{
				RollDisabledLocationDie((Image)sender, true);
			}
		}

		void OnTapRoundTimerDie(object sender, EventArgs args)
		{
			if (CurrentRoundState == RoundState.Ready)
			{
				RollRoundDie((Image)sender, true);
			}
		}

		void OnTapTheseusDie(object sender, EventArgs args)
		{
			if (CurrentRoundState == RoundState.Ready)
			{
				RollTheseusDie((Image)sender, true);
			}
		}

		async void RollDisabledLocationDie(Image imageToUpdate, bool interactive)
		{
			if (!isTapped)
			{
				if (interactive)
				{
					isTapped = true;
					AudioManager.PlaySound("DiceRoll.wav");
				}

				var rnd = new Random();
				await imageToUpdate.FadeTo(0, 250, Easing.Linear);
				imageToUpdate.Source = string.Format("diespecial{0}.png", rnd.Next(1, 7));
				await imageToUpdate.FadeTo(1, 250, Easing.Linear);
				if (interactive) isTapped = false;
			}
		}



		async void RollRoundDie(Image imageToUpdate, bool interactive)
		{
			if (!isTapped)
			{
				if (interactive)
				{
					isTapped = true;
					AudioManager.PlaySound("DiceRoll.wav");
				}
				var rnd = new Random();
				var diceroll = rnd.Next(1, 7);

				await imageToUpdate.FadeTo(0, 250, Easing.Linear);
				imageToUpdate.Source = string.Format("dietimer{0}.png", diceroll);
				GameSettings.RoundTimerInSeconds = 10 + (5 * diceroll);
				await imageToUpdate.FadeTo(1, 250, Easing.Linear);
				if (interactive) isTapped = false;
			}
		}

		async void RollTheseusDie(Image imageToUpdate, bool interactive)
		{
			if (!isTapped)
			{
				if (interactive)
				{
					isTapped = true;
					AudioManager.PlaySound("DiceRoll.wav");
				}

				var rnd = new Random();

				await imageToUpdate.FadeTo(0, 250, Easing.Linear);
				imageToUpdate.Source = string.Format("dietheseus{0}.png", rnd.Next(1, 7));
				await imageToUpdate.FadeTo(1, 250, Easing.Linear);
				if (interactive) isTapped = false;
			}
		}
	}
}