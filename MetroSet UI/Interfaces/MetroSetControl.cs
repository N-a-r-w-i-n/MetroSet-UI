using MetroSet_UI.Components;
using MetroSet_UI.Enums;

namespace MetroSet_UI.Interfaces
{
	public interface MetroSetControl
	{
		/// <summary>
		///
		/// </summary>
		Style Style { get; set; }

		/// <summary>
		///
		/// </summary>
		StyleManager StyleManager { get; set; }

		string ThemeAuthor { get; set; }
		string ThemeName { get; set; }
	}


}