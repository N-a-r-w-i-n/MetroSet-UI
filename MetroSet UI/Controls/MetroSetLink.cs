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
	[ToolboxBitmap(typeof(MetroSetLink), "Bitmaps.LinkLabel.bmp")]
	[Designer(typeof(MetroSetLinkDesigner))]
	[DefaultProperty("Text")]
	[ComVisible(true)]
	public class MetroSetLink : LinkLabel, IMetroSetControl
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

		#endregion Internal Vars

		#region Constructors

		public MetroSetLink()
		{
			SetStyle(
				ControlStyles.ResizeRedraw |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.SupportsTransparentBackColor, true
				);
			UpdateStyles();
			base.Font = MetroSetFonts.Light(10);
			base.Cursor = Cursors.Hand;
			_utl = new Utilites();
			_style = Style.Dark;
			ApplyTheme();
			LinkBehavior = LinkBehavior.HoverUnderline;
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
					ForeColor = Color.Black;
					BackColor = Color.Transparent;
					ActiveLinkColor = Color.FromArgb(85, 197, 245);
					LinkColor = Color.FromArgb(65, 177, 225);
					VisitedLinkColor = Color.FromArgb(45, 157, 205);
					ThemeAuthor = "Narwin";
					ThemeName = "MetroLite";
					UpdateProperties();
					break;

				case Style.Dark:
					ForeColor = Color.FromArgb(170, 170, 170);
					BackColor = Color.Transparent;
					ActiveLinkColor = Color.FromArgb(85, 197, 245);
					LinkColor = Color.FromArgb(65, 177, 225);
					VisitedLinkColor = Color.FromArgb(45, 157, 205);
					ThemeAuthor = "Narwin";
					ThemeName = "MetroDark";
					UpdateProperties();
					break;

				case Style.Custom:
					if (StyleManager != null)
						foreach (var varkey in StyleManager.LinkLabelDictionary)
						{
							switch (varkey.Key)
							{
								case "ForeColor":
									ForeColor = _utl.HexColor((string)varkey.Value);
									break;

								case "BackColor":
									BackColor = _utl.HexColor((string)varkey.Value);
									break;

								case "LinkColor":
									LinkColor = _utl.HexColor((string)varkey.Value);
									break;

								case "ActiveLinkColor":
									ActiveLinkColor = _utl.HexColor((string)varkey.Value);
									break;

								case "VisitedLinkColor":
									VisitedLinkColor = _utl.HexColor((string)varkey.Value);
									break;

								case "LinkBehavior":
									switch ((string)varkey.Value)
									{
										case "HoverUnderline":
											LinkBehavior = LinkBehavior.HoverUnderline;
											break;
										case "AlwaysUnderline":
											LinkBehavior = LinkBehavior.AlwaysUnderline;
											break;
										case "NeverUnderline":
											LinkBehavior = LinkBehavior.NeverUnderline;
											break;
										case "SystemDefault":
											LinkBehavior = LinkBehavior.SystemDefault;
											break;
									}
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

		#region Events

		/// <summary>
		/// Here we set the smooth mouse hand.
		/// </summary>
		/// <param name="m"></param>
		protected override void WndProc(ref Message m)
		{
			_utl.SmoothCursor(ref m);

			base.WndProc(ref m);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets ForeColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the form forecolor.")]
		public override Color ForeColor { get; set; } = Color.Black;

		/// <summary>
		/// Gets or sets the form BackColor.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the form backcolor.")]
		public override Color BackColor { get; set; } = Color.Transparent;

		/// <summary>
		/// Gets or sets LinkColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets LinkColor used by the control.")]
		public new Color LinkColor { get; set; } = Color.FromArgb(65, 177, 225);

		/// <summary>
		/// Gets or sets ActiveLinkColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets ActiveLinkColor used by the control.")]
		public new Color ActiveLinkColor { get; set; } = Color.FromArgb(85, 197, 245);

		/// <summary>
		/// Gets or sets VisitedLinkColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets VisitedLinkColor used by the control.")]
		public new Color VisitedLinkColor { get; set; } = Color.FromArgb(45, 157, 205);

		/// <summary>
		/// Gets or sets LinkBehavior used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets LinkBehavior used by the control.")]
		public new LinkBehavior LinkBehavior { get; set; }

		/// <summary>
		/// Gets or sets DisabledLinkColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets DisabledLinkColor used by the control.")]
		public new Color DisabledLinkColor { get; set; } = Color.FromArgb(133, 133, 133);

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

	}
}