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

		public void PlaySound(string fileName)
		{
			_soundProvider.PlaySound(fileName);
		}
	}
}