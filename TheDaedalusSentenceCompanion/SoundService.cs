using System.Threading.Tasks;
using Xamarin.Forms;

namespace TheDaedalusSentenceCompanion
{
	public class SoundService
	{
		private readonly ISoundProvider _soundProvider;
		public SoundService()
		{
			_soundProvider = DependencyService.Get<ISoundProvider>();
		}

		public Task<bool> PlaySoundAsync(string filename)
		{
			return _soundProvider.PlaySoundAsync(filename);
		}
	}
}