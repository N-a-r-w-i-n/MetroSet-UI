using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroSet_UI.Components;
using MetroSet_UI.Enums;
using MetroSet_UI.Extensions;
using MetroSet_UI.Interfaces;

namespace MetroSet_UI.Controls
{
	public class MetroSetDataGrid : DataGridView, iControl
	{

		#region Interfaces

		/// <summary>
		/// Gets or sets the style associated with the control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the style associated with the control.")]
		public Style Style
		{
			get => StyleManager?.Style ?? _style;
			set
			{
				_style = value;
				switch (value)
				{
					case Style.Light:
						//ApplyTheme();
						break;

					case Style.Dark:
						// ApplyTheme(Style.Dark);
						break;

					case Style.Custom:
						// ApplyTheme(Style.Custom);
						break;

					default:
						// ApplyTheme();
						break;
				}

				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the The Author name associated with the theme.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the The Author name associated with the theme.")]
		public string ThemeAuthor { get; set; }

		/// <summary>
		/// Gets or sets the The Theme name associated with the theme.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the The Theme name associated with the theme.")]
		public string ThemeName { get; set; }

		/// <summary>
		/// Gets or sets the Style Manager associated with the control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the Style Manager associated with the control.")]
		public StyleManager StyleManager
		{
			get => _styleManager;
			set
			{
				_styleManager = value;
				Invalidate();
			}
		}

		#endregion Interfaces

		#region Global Vars

		private readonly Methods _mth;
		private readonly Utilites _utl;

		#endregion Global Vars

		#region Internal Vars

		private MouseMode _state;
		private Style _style;
		private StyleManager _styleManager;

		private MetroSetScrollBar hScroll;
		private MetroSetScrollBar vScroll;

		#endregion

		#region Constructors

		public MetroSetDataGrid()
		{

			BackgroundColor = Color.White;
			GridColor = Color.Blue;
			base.Font = MetroSetFonts.UIRegular(10);
			base.DoubleBuffered = true;


			hScroll = new MetroSetScrollBar { Orientation = ScrollOrientate.Horizontal };
			vScroll = new MetroSetScrollBar { Orientation = ScrollOrientate.Vertical };
		}

		#endregion

		

	}
}
