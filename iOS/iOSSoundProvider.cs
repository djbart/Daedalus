using System;
using System.IO;
using System.Threading.Tasks;
using AudioToolbox;
using AVFoundation;
using Foundation;
using TheDaedalusSentenceCompanion;
using TheDaedalusSentenceCompanion.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof (iOSSoundProvider))]
namespace TheDaedalusSentenceCompanion.iOS
{
	public class iOSSoundProvider : NSObject, ISoundProvider, IAVAudioPlayerDelegate
	{
		#region Private Variables
		private AVAudioPlayer backgroundMusic;
		private AVAudioPlayer soundEffect;
		private string backgroundSong = "";
		#endregion

		#region Computed Properties
		public float BackgroundMusicVolume
		{
			get { return backgroundMusic.Volume; }
			set { backgroundMusic.Volume = value; }
		}

		public bool MusicOn { get; set; } = true;
		public float MusicVolume { get; set; } = 0.5f;

		public bool EffectsOn { get; set; } = true;
		public float EffectsVolume { get; set; } = 1.0f;
		#endregion

		#region Constructors
		public iOSSoundProvider()
		{
			// Initialize
			ActivateAudioSession();
		}
		#endregion

		#region Public Methods
		public void ActivateAudioSession()
		{
			// Initialize Audio
			var session = AVAudioSession.SharedInstance();
			session.SetCategory(AVAudioSessionCategory.Ambient);
			session.SetActive(true);
		}

		public void DeactivateAudioSession()
		{
			var session = AVAudioSession.SharedInstance();
			session.SetActive(false);
		}

		public void ReactivateAudioSession()
		{
			var session = AVAudioSession.SharedInstance();
			session.SetActive(true);
		}

		public void PlayBackgroundMusic(string filename)
		{
			NSUrl songURL;

			// Music enabled?
			if (!MusicOn) return;

			// Any existing background music?
			if (backgroundMusic != null)
			{
				//Stop and dispose of any background music
				backgroundMusic.Stop();
				backgroundMusic.Dispose();
			}

			// Initialize background music
			songURL = new NSUrl("Sounds/" + filename);
			NSError err;
			backgroundMusic = new AVAudioPlayer(songURL, "mp3", out err);
			backgroundMusic.Volume = MusicVolume;
			backgroundMusic.FinishedPlaying += delegate
			{
				// backgroundMusic.Dispose(); 
				backgroundMusic = null;
			};
			backgroundMusic.NumberOfLoops = -1;
			backgroundMusic.Play();
			backgroundSong = filename;

		}

		public void StopBackgroundMusic()
		{

			// If any background music is playing, stop it
			backgroundSong = "";
			if (backgroundMusic != null)
			{
				backgroundMusic.Stop();
				backgroundMusic.Dispose();
			}
		}

		public void SuspendBackgroundMusic()
		{

			// If any background music is playing, stop it
			if (backgroundMusic != null)
			{
				backgroundMusic.Stop();
				backgroundMusic.Dispose();
			}
		}

		public void RestartBackgroundMusic()
		{

			// Music enabled?
			if (!MusicOn) return;

			// Was a song previously playing?
			if (backgroundSong == "") return;

			// Restart song to fix issue with wonky music after sleep
			PlayBackgroundMusic(backgroundSong);
		}

		public void PlaySound(string filename)
		{
			PlaySound(filename, false, 0);
		}

		public void PlaySound(string filename, bool fadeIn, double secondsToPlay)
		{
			NSUrl songURL;

			// Music enabled?
			if (!EffectsOn) return;

			StopSound();

			// Initialize background music
			songURL = new NSUrl("Sounds/" + filename);
			NSError err;
			soundEffect = new AVAudioPlayer(songURL, Path.GetExtension(filename), out err);
			soundEffect.Volume = EffectsVolume;
			soundEffect.FinishedPlaying += delegate
			{
				soundEffect = null;
			};
			soundEffect.NumberOfLoops = 0;
			if (secondsToPlay > 0f && soundEffect.Duration > secondsToPlay)
			{
				soundEffect.CurrentTime = soundEffect.Duration - secondsToPlay;
			}

			if (fadeIn)
			{
				FadeIn();
			}
			else
			{
				soundEffect.Play();
			}
		}

		public void StopSound()
		{
			// Any existing sound effect?
			if (soundEffect != null)
			{
				//Stop and dispose of any sound effect
				soundEffect.Stop();
				soundEffect.Dispose();
			}
		}

		public void PlayClick()
		{
			PlaySound("Click.wav");
		}

		public async void FadeIn()
		{
			soundEffect.Volume = 0f;
			soundEffect.Play();
			await Task.Delay(200);
			await Task.Run(() => { soundEffect.Volume = 0.2f; });
			await Task.Delay(200);
			await Task.Run(() => { soundEffect.Volume = 0.4f; });	
			await Task.Delay(200);
			await Task.Run(() => { soundEffect.Volume = 0.6f; });
			await Task.Delay(200);
			await Task.Run(() => { soundEffect.Volume = 0.8f; });
			await Task.Delay(200);
			await Task.Run(() => { soundEffect.Volume = 1.0f; });
		}

		#endregion
	}
}