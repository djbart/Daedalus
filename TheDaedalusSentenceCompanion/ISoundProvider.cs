using System.Threading.Tasks;

namespace TheDaedalusSentenceCompanion
{
	public interface ISoundProvider
	{
		float BackgroundMusicVolume { get; set; }

		bool MusicOn { get; set; }
		float MusicVolume { get; set; }

		bool EffectsOn { get; set; }
		float EffectsVolume { get; set; }

		void ActivateAudioSession();
		void DeactivateAudioSession();
		void ReactivateAudioSession();

		void PlayBackgroundMusic(string filename);
		void StopBackgroundMusic();
		void SuspendBackgroundMusic();
		void RestartBackgroundMusic();

		void PlaySound(string fileName);
		void PlaySound(string filename, bool fadeIn, double secondsToPlay);
		void StopSound();
		void PlayClick();
	}
}

