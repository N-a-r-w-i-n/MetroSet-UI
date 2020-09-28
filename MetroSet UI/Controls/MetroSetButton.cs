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

using MetroSet_UI.Design;
using MetroSet_UI.Enums;
using MetroSet_UI.Extensions;
using MetroSet_UI.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MetroSet_UI.Controls
{
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(MetroSetButton), "Bitmaps.Button.bmp")]
	[Designer(typeof(MetroSetButtonDesigner))]
	[DefaultEvent("Click")]
	[DefaultProperty("Text")]
	[ComVisible(true)]
	public class MetroSetButton : Control, iControl
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
		
		private Color _normalColor;
		private Color _normalBorderColor;
		private Color _normalTextColor;
		private Color _hoverColor;
		private Color _hoverBorderColor;
		private Color _hoverTextColor;
		private Color _pressColor;
		private Color _pressBorderColor;
		private Color _pressTextColor;
		private Color _disabledBackColor;
		private Color _disabledForeColor;
		private Color _disabledBorderColor;

		#endregion Internal Vars

		#region Constructors

		public MetroSetButton()
		{
			SetStyle(
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.SupportsTransparentBackColor, true);
			UpdateStyles();
			Font = MetroSetFonts.Light(10);
			_utl = new Utilites();
			_mth = new Methods();

			ApplyTheme();
		}

		#endregion Constructors

		#region Draw Control

		protected override void OnPaint(PaintEventArgs e)
		{
			var g = e.Graphics;
			var r = new Rectangle(0, 0, Width - 1, Height - 1);
			g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			switch (_state)
			{
				case MouseMode.Normal:

					using (var bg = new SolidBrush(NormalColor))
					using (var p = new Pen(NormalBorderColor))
					using (var tb = new SolidBrush(NormalTextColor))
					{
						g.FillRectangle(bg, r);
						g.DrawRectangle(p, r);
						g.DrawString(Text, Font, tb, r, _mth.SetPosition());
					}

					break;

				case MouseMode.Hovered:

					Cursor = Cursors.Hand;
					using (var bg = new SolidBrush(HoverColor))
					using (var p = new Pen(HoverBorderColor))
					using (var tb = new SolidBrush(HoverTextColor))
					{
						g.FillRectangle(bg, r);
						g.DrawRectangle(p, r);
						g.DrawString(Text, Font, tb, r, _mth.SetPosition());
					}

					break;

				case MouseMode.Pushed:

					using (var bg = new SolidBrush(PressColor))
					using (var p = new Pen(PressBorderColor))
					using (var tb = new SolidBrush(PressTextColor))
					{
						g.FillRectangle(bg, r);
						g.DrawRectangle(p, r);
						g.DrawString(Text, Font, tb, r, _mth.SetPosition());
					}

					break;

				case MouseMode.Disabled:

					using (var bg = new SolidBrush(DisabledBackColor))
					using (var p = new Pen(DisabledBorderColor))
					using (var tb = new SolidBrush(DisabledForeColor))
					{
						g.FillRectangle(bg, r);
						g.DrawRectangle(p, r);
						g.DrawString(Text, Font, tb, r, _mth.SetPosition());
					}
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		#endregion Draw Control

		#region ApplyTheme

		/// <summary>
		/// Gets or sets the style provided by the user.
		/// </summary>
		/// <param name="style">The Style.</param>
		/// <param name="path">The path of the custom theme.</param>
		private void ApplyTheme(Style style = Style.Light)
		{
			switch (style)
			{
				case Style.Light:
					NormalColor = Color.FromArgb(65, 177, 225);
					NormalBorderColor = Color.FromArgb(65, 177, 225);
					NormalTextColor = Color.White;
					HoverColor = Color.FromArgb(95, 207, 255);
					HoverBorderColor = Color.FromArgb(95, 207, 255);
					HoverTextColor = Color.White;
					PressColor = Color.FromArgb(35, 147, 195);
					PressBorderColor = Color.FromArgb(35, 147, 195);
					PressTextColor = Color.White;
					DisabledBackColor = Color.FromArgb(120, 65, 177, 225);
					DisabledBorderColor = Color.FromArgb(120, 65, 177, 225);
					DisabledForeColor = Color.Gray;
					ThemeAuthor = "Narwin";
					ThemeName = "MetroLite";
					break;

				case Style.Dark:
					NormalColor = Color.FromArgb(65, 177, 225);
					NormalBorderColor = Color.FromArgb(65, 177, 225);
					NormalTextColor = Color.White;
					HoverColor = Color.FromArgb(95, 207, 255);
					HoverBorderColor = Color.FromArgb(95, 207, 255);
					HoverTextColor = Color.White;
					PressColor = Color.FromArgb(35, 147, 195);
					PressBorderColor = Color.FromArgb(35, 147, 195);
					PressTextColor = Color.White;
					DisabledBackColor = Color.FromArgb(120, 65, 177, 225);
					DisabledBorderColor = Color.FromArgb(120, 65, 177, 225);
					DisabledForeColor = Color.Gray;
					ThemeAuthor = "Narwin";
					ThemeName = "MetroDark";
					break;

				case Style.Custom:
					if (StyleManager != null)
						foreach (var varkey in StyleManager.ButtonDictionary)
						{
							if ((varkey.Key == null) || varkey.Key == null)
							{
								return;
							}

							switch (varkey.Key)
							{
								case "NormalColor":
									NormalColor = _utl.HexColor((string)varkey.Value);
									break;
								case "NormalBorderColor":
									NormalBorderColor = _utl.HexColor((string)varkey.Value);
									break;
								case "NormalTextColor":
									NormalTextColor = _utl.HexColor((string)varkey.Value);
									break;
								case "HoverColor":
									HoverColor = _utl.HexColor((string)varkey.Value);
									break;
								case "HoverBorderColor":
									HoverBorderColor = _utl.HexColor((string)varkey.Value);
									break;
								case "HoverTextColor":
									HoverTextColor = _utl.HexColor((string)varkey.Value);
									break;
								case "PressColor":
									PressColor = _utl.HexColor((string)varkey.Value);
									break;
								case "PressBorderColor":
									PressBorderColor = _utl.HexColor((string)varkey.Value);
									break;
								case "PressTextColor":
									PressTextColor = _utl.HexColor((string)varkey.Value);
									break;
								case "DisabledBackColor":
									DisabledBackColor = _utl.HexColor((string)varkey.Value);
									break;
								case "DisabledBorderColor":
									DisabledBorderColor = _utl.HexColor((string)varkey.Value);
									break;
								case "DisabledForeColor":
									DisabledForeColor = _utl.HexColor((string)varkey.Value);
									break;
							}
						}
					Invalidate();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(style), style, null);
			}
		}

		#endregion Theme Changing

		#region Properties

		/// <summary>
		/// I make BackColor inaccessible cause we have not use of it. 
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor => Color.Transparent;

		/// <summary>
		/// Handling Control Enable state to detect the disability state.
		/// </summary>
		[Category("MetroSet Framework")]
		public new bool Enabled
		{
			get => base.Enabled;
			set
			{
				base.Enabled = value;
				_state = value ? MouseMode.Normal : MouseMode.Disabled;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the button background color in normal mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the button background color in normal mouse sate.")]
		public Color NormalColor
		{
			get { return _normalColor; }
			set
			{
				_normalColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the button border color in normal mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the button border color in normal mouse sate.")]
		public Color NormalBorderColor
		{
			get { return _normalBorderColor; }
			set
			{
				_normalBorderColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the button Text color in normal mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the button Text color in normal mouse sate.")]
		public Color NormalTextColor
		{
			get { return _normalTextColor; }
			set
			{
				_normalTextColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the button background color in hover mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the button background color in hover mouse sate.")]
		public Color HoverColor
		{
			get { return _hoverColor; }
			set
			{
				_hoverColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the button border color in hover mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the button border color in hover mouse sate.")]
		public Color HoverBorderColor
		{
			get { return _hoverBorderColor; }
			set
			{
				_hoverBorderColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the button Text color in hover mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the button Text color in hover mouse sate.")]
		public Color HoverTextColor
		{
			get { return _hoverTextColor; }
			set
			{
				_hoverTextColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the button background color in pushed mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the button background color in pushed mouse sate.")]
		public Color PressColor
		{
			get { return _pressColor; }
			set
			{
				_pressColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the button border color in pushed mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the button border color in pushed mouse sate.")]
		public Color PressBorderColor
		{
			get { return _pressBorderColor; }
			set
			{
				_pressBorderColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the button Text color in pushed mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the button Text color in pushed mouse sate.")]
		public Color PressTextColor
		{
			get { return _pressTextColor; }
			set
			{
				_pressTextColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets BackColor used by the control while disabled.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets backcolor used by the control while disabled.")]
		public Color DisabledBackColor
		{
			get { return _disabledBackColor; }
			set
			{
				_disabledBackColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the ForeColor of the control whenever while disabled.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the forecolor of the control whenever while disabled.")]
		public Color DisabledForeColor
		{
			get { return _disabledForeColor; }
			set
			{
				_disabledForeColor = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets the border color of the control while disabled.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the border color of the control while disabled.")]
		public Color DisabledBorderColor
		{
			get { return _disabledBorderColor; }
			set
			{
				_disabledBorderColor = value;
				Refresh();
			}
		}

		#endregion

		#region Events

		/// <summary>
		/// Handling mouse up event of the control.
		/// </summary>
		/// <param name="e">MouseEventArgs</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			_state = MouseMode.Hovered;
			Invalidate();
		}

		/// <summary>
		/// Handling mouse down event of the control.
		/// </summary>
		/// <param name="e">MouseEventArgs</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			_state = MouseMode.Pushed;
			Invalidate();
		}

		/// <summary>
		/// Handling mouse entering event of the control.
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			_state = MouseMode.Hovered;
			Invalidate();
		}

		/// <summary>
		/// Handling mouse leave event of the control.
		/// </summary>
		/// <param name="e">MouseEventArgs</param>
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseEnter(e);
			_state = MouseMode.Normal;
			Invalidate();
		}

		#endregion Events
	}
}