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
	public class iOSSoundProvider : NSObject, ISoundProvider
	{
		private AVAudioPlayer _player;

		public Task<bool> PlaySoundAsync(string filename)
		{
			var tcs = new TaskCompletionSource<bool>();

			var mp3URL = new NSUrl(filename, false);
			NSError err;

			_player = new AVAudioPlayer(mp3URL, "mp3", out err);

			_player.FinishedPlaying += (object sender, AVStatusEventArgs e) =>
			{
				_player = null;
				tcs.SetResult(true);
			};

			_player.Play();

			return tcs.Task;
		}
	}
}