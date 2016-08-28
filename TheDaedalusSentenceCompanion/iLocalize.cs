using System;
using System.Globalization;

namespace TheDaedalusSentenceCompanion
{
	public interface ILocalize
	{
		CultureInfo GetCurrentCultureInfo();
	}
}