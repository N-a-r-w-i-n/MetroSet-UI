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
using System.Drawing.Text;
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
	[ToolboxBitmap(typeof(MetroSetTile), "Bitmaps.Button.bmp")]
	[Designer(typeof(MetroSetTileDesigner))]
	[DefaultEvent("Click")]
	[DefaultProperty("Text")]
	[ComVisible(true)]
	public class MetroSetTile : Control, IMetroSetControl
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

		private readonly Utilites _utl;

		#endregion Global Vars

		#region Internal Vars

		private MouseMode _state;
		private Style _style;
		private StyleManager _styleManager;

		private TileAlign _textAlign;
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

		public MetroSetTile()
		{
			SetStyle(
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.SupportsTransparentBackColor, true);
			UpdateStyles();
			base.Font = MetroSetFonts.Light(10);
			_utl = new Utilites();
			ApplyTheme();
		}

		#endregion Constructors

		#region Draw Control

		protected override void OnPaint(PaintEventArgs e)
		{
			var g = e.Graphics;
			var r = new Rectangle(1, 1, Width - 2, Height - 2);
			g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

			StringFormat sf;

			switch (TileAlign)
			{
				case TileAlign.BottomLeft:
					sf = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Far };
					break;
				case TileAlign.BottomRight:
					sf = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Far };
					break;
				case TileAlign.TopLeft:
					sf = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near };
					break;
				case TileAlign.TopRight:
					sf = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Near };
					break;
				case TileAlign.TopCenter:
					sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near };
					break;
				case TileAlign.BottomCenter:
					sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far };
					break;
				case TileAlign.MiddleLeft:
					sf = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
					break;
				case TileAlign.MiddleRight:
					sf = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };
					break;
				case TileAlign.MiddleCenter:
					sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			switch (_state)
			{
				case MouseMode.Normal:

					using (var bg = new SolidBrush(NormalColor))
					{
						using (var p = new Pen(NormalBorderColor, 2))
						{
							using (var tb = new SolidBrush(NormalTextColor))
							{
								if (BackgroundImage != null)
								{
									g.DrawImage(BackgroundImage, r);
								}
								else
								{
									g.FillRectangle(bg, r);
									g.DrawRectangle(p, r);
								}
								g.DrawString(Text, Font, tb, r, sf);
							}
						}
					}
					break;

				case MouseMode.Hovered:

					Cursor = Cursors.Hand;
					using (var bg = new SolidBrush(HoverColor))
					{
						using (var p = new Pen(HoverBorderColor, 2))
						{
							using (var tb = new SolidBrush(HoverTextColor))
							{
								if (BackgroundImage != null)
								{
									g.DrawImage(BackgroundImage, r);
								}
								else
								{
									g.FillRectangle(bg, r);
								}
								g.DrawString(Text, Font, tb, r, sf);
								g.DrawRectangle(p, r);
							}
						}
					}
					break;

				case MouseMode.Pushed:

					using (var bg = new SolidBrush(PressColor))
					{
						using (var p = new Pen(PressBorderColor, 2))
						{
							using (var tb = new SolidBrush(PressTextColor))
							{
								if (BackgroundImage != null)
								{
									g.DrawImage(BackgroundImage, r);
								}
								else
								{
									g.FillRectangle(bg, r);
								}
								g.DrawString(Text, Font, tb, r, sf);
								g.DrawRectangle(p, r);

							}
						}
					}
					break;

				case MouseMode.Disabled:
					using (var bg = new SolidBrush(DisabledBackColor))
					{
						using (var p = new Pen(DisabledBorderColor))
						{
							using (var tb = new SolidBrush(DisabledForeColor))
							{
								g.FillRectangle(bg, r);
								g.DrawString(Text, Font, tb, r, sf);
								g.DrawRectangle(p, r);
							}
						}
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
		private void ApplyTheme(Style style = Style.Light)
		{
			if (!IsDerivedStyle)
				return;

			switch (style)
			{
				case Style.Light:
					NormalColor = Color.FromArgb(65, 177, 225);
					NormalBorderColor = Color.FromArgb(65, 177, 225);
					NormalTextColor = Color.White;
					HoverColor = Color.FromArgb(65, 177, 225);
					HoverBorderColor = Color.FromArgb(230, 230, 230);
					HoverTextColor = Color.White;
					PressColor = Color.FromArgb(65, 177, 225);
					PressBorderColor = Color.FromArgb(65, 177, 225);
					PressTextColor = Color.White;
					DisabledBackColor = Color.FromArgb(204, 204, 204);
					DisabledBorderColor = Color.FromArgb(155, 155, 155);
					DisabledForeColor = Color.FromArgb(136, 136, 136);
					ThemeAuthor = "Narwin";
					ThemeName = "MetroLite";
					break;

				case Style.Dark:
					NormalColor = Color.FromArgb(65, 177, 225);
					NormalBorderColor = Color.FromArgb(65, 177, 225);
					NormalTextColor = Color.White;
					HoverColor = Color.FromArgb(65, 177, 225);
					HoverBorderColor = Color.FromArgb(102, 102, 102);
					HoverTextColor = Color.White;
					PressColor = Color.FromArgb(65, 177, 225);
					PressBorderColor = Color.FromArgb(51, 51, 51);
					PressTextColor = Color.White;
					DisabledBackColor = Color.FromArgb(80, 80, 80);
					DisabledBorderColor = Color.FromArgb(109, 109, 109);
					DisabledForeColor = Color.FromArgb(109, 109, 109);
					ThemeAuthor = "Narwin";
					ThemeName = "MetroDark";
					break;

				case Style.Custom:
					if (StyleManager != null)
						foreach (var varkey in StyleManager.TileDictionary)
						{
							if ((varkey.Key == null) || varkey.Key == null)
							{
								return;
							}

							if (varkey.Key == "NormalColor")
							{
								NormalColor = _utl.HexColor((string)varkey.Value);
							}
							else if (varkey.Key == "NormalBorderColor")
							{
								NormalBorderColor = _utl.HexColor((string)varkey.Value);
							}
							else if (varkey.Key == "NormalTextColor")
							{
								NormalTextColor = _utl.HexColor((string)varkey.Value);
							}
							else if (varkey.Key == "HoverColor")
							{
								HoverColor = _utl.HexColor((string)varkey.Value);
							}
							else if (varkey.Key == "HoverBorderColor")
							{
								HoverBorderColor = _utl.HexColor((string)varkey.Value);
							}
							else if (varkey.Key == "HoverTextColor")
							{
								HoverTextColor = _utl.HexColor((string)varkey.Value);
							}
							else if (varkey.Key == "PressColor")
							{
								PressColor = _utl.HexColor((string)varkey.Value);
							}
							else if (varkey.Key == "PressBorderColor")
							{
								PressBorderColor = _utl.HexColor((string)varkey.Value);
							}
							else if (varkey.Key == "PressTextColor")
							{
								PressTextColor = _utl.HexColor((string)varkey.Value);
							}
							else if (varkey.Key == "DisabledBackColor")
							{
								DisabledBackColor = _utl.HexColor((string)varkey.Value);
							}
							else if (varkey.Key == "DisabledBorderColor")
							{
								DisabledBorderColor = _utl.HexColor((string)varkey.Value);
							}
							else if (varkey.Key == "DisabledForeColor")
							{
								DisabledForeColor = _utl.HexColor((string)varkey.Value);
							}
						}
					Refresh();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(style), style, null);
			}
		}

		#endregion Theme Changing

		#region Properties

		/// <summary>
		/// Gets the background color.
		/// </summary>
		[Browsable(false)]
		public override Color BackColor => Color.Transparent;

		/// <summary>
		/// Gets or sets a value indicating whether the control can respond to user interaction.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets a value indicating whether the control can respond to user interaction.")]
		public new bool Enabled
		{
			get => base.Enabled;
			set
			{
				base.Enabled = value;
				if (value == false)
				{
					_state = MouseMode.Disabled;
				}
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the BackgroundImage associated with the control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the BackgroundImage associated with the control.")]
		public override Image BackgroundImage { get => base.BackgroundImage; set => base.BackgroundImage = value; }

		/// <summary>
		/// Gets or sets the TileAlign associated with the control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the TileAlign associated with the control.")]
		[DefaultValue(TileAlign.MiddleCenter)]
		public TileAlign TileAlign
		{
			get { return _textAlign; }
			set
			{
				_textAlign = value;
				Refresh();
			}
		}
		/// <summary>
		/// Gets or sets the control background color in normal mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the control background color in normal mouse sate.")]
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
		/// Gets or sets the control border color in normal mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the control border color in normal mouse sate.")]
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
		/// Gets or sets the control Text color in normal mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the control Text color in normal mouse sate.")]
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
		/// Gets or sets the control background color in hover mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the control background color in hover mouse sate.")]
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
		/// Gets or sets the control border color in hover mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the control border color in hover mouse sate.")]
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
		/// Gets or sets the control Text color in hover mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the control Text color in hover mouse sate.")]
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
		/// Gets or sets the control background color in pushed mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the control background color in pushed mouse sate.")]
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
		/// Gets or sets the control border color in pushed mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the control border color in pushed mouse sate.")]
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
		/// Gets or sets the control Text color in pushed mouse sate.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the control Text color in pushed mouse sate.")]
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
		/// Gets or sets back color used by the control while disabled.
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
		/// Gets or sets the fore color of the control whenever while disabled.
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
		/// <param name="e">MouseEventArgs</param>
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			_state = MouseMode.Hovered;
			Invalidate();
		}

		/// <summary>
		/// Handling mouse leave event of the control.
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseEnter(e);
			_state = MouseMode.Normal;
			Invalidate();
		}

		#endregion Events

	}

}
