using System.Reflection;
using Xamarin.Forms;

namespace TheDaedalusSentenceCompanion
{
	public class App : Application
	{
		public ISoundProvider AudioManager { get; set; } = DependencyService.Get<ISoundProvider>();

		public App ()
		{
			// The root page of your application
			MainPage = new HomePage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
			AudioManager.PlayBackgroundMusic("BackgroundMusic.mp3");
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
			AudioManager.SuspendBackgroundMusic();
			AudioManager.DeactivateAudioSession();
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
			AudioManager.ReactivateAudioSession();
			AudioManager.RestartBackgroundMusic();
		}
	}
}

