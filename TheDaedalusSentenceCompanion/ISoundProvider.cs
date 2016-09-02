using System.Threading.Tasks;

namespace TheDaedalusSentenceCompanion
{
	public interface ISoundProvider
	{
		Task<bool> PlaySoundAsync(string filename);
	}
}

