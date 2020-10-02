/*
* MetroSet UI - MetroSet UI Framework
* 
* The MIT License (MIT)
* Copyright (c) 2017 Narwin, https://github.com/N-a-r-w-i-n
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy of 
* this software and associated documentation files (the "Software"), to deal in the 
* Software without restriction, including without limitation the rights to use, copy, 
* modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
* and to permit persons to whom the Software is furnished to do so, subject to the 
* following conditions:
* 
* The above copyright notice and this permission notice shall be included in 
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
* INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
* PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
* HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
* CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
* OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MetroSet_UI.Components;
using MetroSet_UI.Design;
using MetroSet_UI.Enums;
using MetroSet_UI.Extensions;
using MetroSet_UI.Interfaces;

namespace MetroSet_UI.Controls
{
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(MetroSetDivider), "Bitmaps.Divider.bmp")]
	[Designer(typeof(MetroSetDividerDesigner))]
	[DefaultProperty("Orientation")]
	[ComVisible(true)]
	public class MetroSetDivider : Control, IMetroSetControl
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
						ApplyTheme();
						break;

					case Style.Dark:
						ApplyTheme(Style.Dark);
						break;

					case Style.Custom:
						ApplyTheme(Style.Custom);
						break;

					default:
						ApplyTheme();
						break;
				}
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the Style Manager associated with the control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the Style Manager associated with the control.")]
		public StyleManager StyleManager
		{
			get => _styleManager;
			set { _styleManager = value; Invalidate(); }
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

		#endregion Interfaces

		#region Global Vars

		private readonly Utilites _utl;

		#endregion Global Vars

		#region Internal Vars

		private Style _style;
		private StyleManager _styleManager;

		private DividerStyle _orientation;
		private int _thickness;

		#endregion Internal Vars

		#region Constructors

		public MetroSetDivider()
		{
			SetStyle(
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.SupportsTransparentBackColor, true);
			UpdateStyles();
			_utl = new Utilites();
			ApplyTheme();
			Orientation = DividerStyle.Horizontal;
		}

		#endregion Constructors

		#region ApplyTheme

		/// <summary>
		/// Gets or sets the style provided by the user.
		/// </summary>
		/// <param name="style">The Style.</param>
		private void ApplyTheme(Style style = Style.Light)
		{
			if (!IsDerivedStyle)
				return;

			switch (style)
			{
				case Style.Light:
					Thickness = 1;
					ForeColor = Color.Black;
					ThemeAuthor = "Narwin";
					ThemeName = "MetroLite";
					UpdateProperties();
					break;

				case Style.Dark:
					Thickness = 1;
					ForeColor = Color.FromArgb(170, 170, 170);
					ThemeAuthor = "Narwin";
					ThemeName = "MetroDark";
					UpdateProperties();
					break;

				case Style.Custom:
					if (StyleManager != null)
						foreach (var varkey in StyleManager.DividerDictionary)
						{
							switch (varkey.Key)
							{
								case "Orientation":
									if ((string)varkey.Value == "Horizontal")
									{
										Orientation = DividerStyle.Horizontal;
									}
									else if ((string)varkey.Value == "Vertical")
									{
										Orientation = DividerStyle.Vertical;
									}
									break;

								case "Thickness":
									Thickness = ((int)varkey.Value);
									break;

								case "ForeColor":
									ForeColor = _utl.HexColor((string)varkey.Value);
									break;

								default:
									return;
							}
						}
					UpdateProperties();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(style), style, null);
			}
		}

		private void UpdateProperties()
		{
			Invalidate();
		}

		#endregion ApplyTheme

		#region Draw Control

		protected override void OnPaint(PaintEventArgs e)
		{
			var g = e.Graphics;
			using (var p = new Pen(ForeColor, Thickness))
			{
				if (Orientation == DividerStyle.Horizontal)
					g.DrawLine(p, 0, Thickness, Width, Thickness);
				else
					g.DrawLine(p, Thickness, 0, Thickness, Height);
			}
		}

		#endregion Draw Control

		#region Properties

		/// <summary>
		/// Gets or sets the style associated with the control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets Orientation of the control.")]
		public DividerStyle Orientation
		{
			get { return _orientation; }
			set
			{
				_orientation = value;
				Refresh();
			}
		}


		/// <summary>
		/// Gets or sets the divider thickness.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the divider thickness.")]
		public int Thickness
		{
			get { return _thickness; }
			set
			{
				_thickness = value;
				Refresh();
			}
		}


		/// <summary>
		/// I make BackColor inaccessible cause I want it to be just transparent and I used another property for the same job in following properties. 
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor => Color.Transparent;

		/// <summary>
		/// Gets or sets ForeColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the form forecolor.")]
		public override Color ForeColor { get; set; }

		private bool _isDerivedStyle = true;

		/// <summary>
		/// Gets or sets the whether this control reflect to parent form style.
		/// Set it to false if you want the style of this control be independent. 
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the whether this control reflect to parent(s) style. \n " +
					 "Set it to false if you want the style of this control be independent. ")]
		public bool IsDerivedStyle
		{
			get { return _isDerivedStyle; }
			set
			{
				_isDerivedStyle = value;
				Refresh();
			}
		}

		#endregion Properties

		#region Events

		/// <summary>
		/// Here we handle the width and height while resizing.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (Orientation == DividerStyle.Horizontal)
			{
				Height = Thickness + 3;
			}
			else
			{
				Width = Thickness + 3;
			}
		}

		#endregion Events
	}
}