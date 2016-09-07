using System;
using System.ComponentModel;

namespace TheDaedalusSentenceCompanion
{
	public enum GameModes
	{
		SecondTimeThrough,
		Veteran,
		Expert,
		HellInSpace,
		Custom
	}

	public class GameSettings: INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public GameSettings(GameModes gameMode)
		{
			setGameMode(gameMode);
		}

		public int CurrentRoundNumber { get; set; } = 1;

		private int gameTimerInMinutes = 60;
		public int GameTimerInMinutes
		{
			get
			{
				return gameTimerInMinutes;
			}
			set
			{
				if (gameTimerInMinutes != value)
				{
					gameTimerInMinutes = value;
					OnPropertyChanged("GameTimerInMinutes");
					OnPropertyChanged("GameTimerInMinutesText");
				}
			}
		}

		public string GameTimerInMinutesText
		{
			get
			{
				return String.Format("{0} {1}", GameTimerInMinutes, AppResources.Minutes);
			}
		}

		private bool gameTimerEnabled = true;
		public bool GameTimerEnabled
		{
			get
			{
				return gameTimerEnabled;
			}
			set
			{
				if (gameTimerEnabled != value)
				{
					gameTimerEnabled = value;
					OnPropertyChanged("GameTimerEnabled");
				}
			}
		}

		private int roundTimerInSeconds = 25;
		public int RoundTimerInSeconds
		{
			get
			{
				return roundTimerInSeconds;
			}
			set
			{
				if (roundTimerInSeconds != value)
				{
					roundTimerInSeconds = value;
					OnPropertyChanged("RoundTimerInSeconds");
					OnPropertyChanged("RoundTimerInSecondsText");
					OnPropertyChanged("RoundTimerCountdownText");
				}
			}
		}

		public string RoundTimerInSecondsText
		{
			get
			{
				return String.Format("{0} {1}", RoundTimerInSeconds, AppResources.Seconds);
			}
		}

		public string RoundTimerCountdownText
		{
			get
			{
				string minutes = (RoundTimerInSeconds / 60).ToString("D2");
				string seconds = (RoundTimerInSeconds % 60).ToString("D2");
				return string.Format("00:{0}:{1}", minutes, seconds);
			}
		}

		private bool roundTimerEnabled = true;
		public bool RoundTimerEnabled
		{
			get
			{
				return roundTimerEnabled;
			}
			set
			{
				if (roundTimerEnabled != value)
				{
					roundTimerEnabled = value;

					if (value == true)
					{
						this.RoundTimerDieEnabled = false;
					}

					OnPropertyChanged("RoundTimerEnabled");
				}
			}
		}

		private bool disabledLocationDieEnabled = false;
		public bool DisabledLocationDieEnabled
		{
			get
			{
				return disabledLocationDieEnabled;
			}
			set
			{
				if (disabledLocationDieEnabled != value)
				{
					disabledLocationDieEnabled = value;
					OnPropertyChanged("DisabledLocationDieEnabled");
				}
			}
		}

		private bool roundTimerDieEnabled = false;
		public bool RoundTimerDieEnabled
		{
			get
			{
				return roundTimerDieEnabled;
			}
			set
			{
				if (roundTimerDieEnabled != value)
				{
					roundTimerDieEnabled = value;

					if (value == true)
					{
						this.RoundTimerEnabled = false;
					}

					OnPropertyChanged("RoundTimerDieEnabled");
				}
			}
		}

		public bool RoundTimerVisible
		{
			get
			{
				return RoundTimerEnabled || RoundTimerDieEnabled;
			}
		}

		private bool theseusDieEnabled = false;
		public bool TheseusDieEnabled
		{
			get
			{
				return theseusDieEnabled;
			}
			set
			{
				if (theseusDieEnabled != value)
				{
					theseusDieEnabled = value;
					OnPropertyChanged("TheseusDieEnabled");
				}
			}
		}

		private void setGameMode(GameModes gameMode)
		{
			switch (gameMode)
			{
				case GameModes.SecondTimeThrough:
					useDifficultyEnhancer1();
					useDifficultyEnhancer3();
					break;
				case GameModes.Veteran:
					useDifficultyEnhancer1();
					useDifficultyEnhancer5();
					useDifficultyEnhancer6();
					break;
				case GameModes.Expert:
					useDifficultyEnhancer1();
					useDifficultyEnhancer3();
					useDifficultyEnhancer4();
					break;
				case GameModes.HellInSpace:
					useDifficultyEnhancer1();
					useDifficultyEnhancer3();
					useDifficultyEnhancer4();
					useDifficultyEnhancer5();
					break;
				case GameModes.Custom:
					useDifficultyEnhancer3();
					break;
			}
		}

		private void useDifficultyEnhancer1() {
			TheseusDieEnabled = true;
		}

		private void useDifficultyEnhancer3()
		{
			GameTimerEnabled = true;
			GameTimerInMinutes = 60;
		}

		private void useDifficultyEnhancer4()
		{
			RoundTimerDieEnabled = true;
		}

		private void useDifficultyEnhancer5()
		{
			DisabledLocationDieEnabled = true;
		}

		private void useDifficultyEnhancer6()
		{
			RoundTimerEnabled = true;
			RoundTimerInSeconds = 25;
		}

			protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this,
					new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}