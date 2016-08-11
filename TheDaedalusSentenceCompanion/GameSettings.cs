using System;
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

	public class GameSettings
	{
		public GameSettings(GameModes gameMode)
		{
			setGameMode(gameMode);
		}

		public int CurrentRoundNumber { get; set; } = 1;

		public int GameTimerInSeconds { get; set; } = 3600;
		public bool GameTimerEnabled { get; set; } = true;

		public int RoundTimerInSeconds { get; set; } = 25;
		public bool RoundTimerEnabled { get; set; } = true;

		public bool DisabledLocationDieEnabled { get; set; } = false;
		public bool RoundTimerDieEnabled { get; set; } = false;
		public bool TheseusDieEnabled { get; set; } = false;

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
			GameTimerInSeconds = 3600;
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
	}
}