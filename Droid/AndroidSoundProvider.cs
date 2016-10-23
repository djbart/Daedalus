using System.Threading.Tasks;
using Android.Media;
using TheDaedalusSentenceCompanion.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidSoundProvider))]
namespace TheDaedalusSentenceCompanion.Droid
{
	public class AndroidSoundProvider : ISoundProvider
	{
		#region Private Variables
		private MediaPlayer backgroundMusic;
		private MediaPlayer soundEffect;
		private string backgroundSong = "";
		#endregion

		#region Computed Properties
		public float BackgroundMusicVolume
		{
			get { return 1; }
			set { backgroundMusic.SetVolume(value, value); }
		}

		public bool MusicOn { get; set; } = true;
		public float MusicVolume { get; set; } = 0.5f;

		public bool EffectsOn { get; set; } = true;
		public float EffectsVolume { get; set; } = 1.0f;
		#endregion

		#region Constructors
		public AndroidSoundProvider()
		{
			// Initialize
			ActivateAudioSession();
		}
		#endregion

		#region Public Methods
		public void ActivateAudioSession()
		{
			// Initialize Audio
		}

		public void DeactivateAudioSession()
		{
		}

		public void ReactivateAudioSession()
		{
		}

		public void PlayBackgroundMusic(string filename)
		{
			// Music enabled?
			if (!MusicOn) return;

			// Any existing background music?
			if (backgroundMusic != null && backgroundMusic.Handle != System.IntPtr.Zero)
			{
				//Stop and dispose of any background music
				backgroundMusic.Stop();
				backgroundMusic.Dispose();
			}

			// Initialize background music
			var fd = Forms.Context.Assets.OpenFd(filename);
			//MediaPlayer.Create(global::Android.App.Application.Context, Resource.Raw.BackgroundMusic);

			backgroundMusic = new MediaPlayer();
			backgroundMusic.Prepared += (s, e) =>
			{
				backgroundMusic.Start();
			};
			backgroundMusic.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length);
			backgroundMusic.SetVolume(MusicVolume, MusicVolume);
			backgroundMusic.Looping = true;

			backgroundMusic.Prepare();
		}

		public void StopBackgroundMusic()
		{

			// If any background music is playing, stop it
			backgroundSong = "";
			if (backgroundMusic != null && backgroundMusic.Handle != System.IntPtr.Zero)
			{
				backgroundMusic.Stop();
				backgroundMusic.Dispose();
			}
		}

		public void SuspendBackgroundMusic()
		{

			// If any background music is playing, stop it
			if (backgroundMusic != null && backgroundMusic.Handle != System.IntPtr.Zero)
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
			// Music enabled?
			if (!EffectsOn) return;

			StopSound();

			// Initialize background music
			var fd = Forms.Context.Assets.OpenFd(filename);

			soundEffect = new MediaPlayer();
			soundEffect.Prepared += (s, e) =>
			{
				if (secondsToPlay > 0f && soundEffect.Duration > (secondsToPlay * 1000))
				{
					soundEffect.SeekTo((soundEffect.Duration - ((int)secondsToPlay) * 1000));
				}

				soundEffect.Start();
			};
			soundEffect.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length);

			soundEffect.SetVolume(EffectsVolume, EffectsVolume);
			soundEffect.Looping = false;

			soundEffect.Prepare();
		}

		public void StopSound()
		{
			// Any existing sound effect?
			if (soundEffect != null && soundEffect.Handle != System.IntPtr.Zero)
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

		public void PlayBleep()
		{
			PlaySound("Bleep.wav");
		}

		#endregion
	}
}