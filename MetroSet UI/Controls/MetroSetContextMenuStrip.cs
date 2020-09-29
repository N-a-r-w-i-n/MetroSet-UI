﻿/*
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
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MetroSet_UI.Components;
using MetroSet_UI.Enums;
using MetroSet_UI.Extensions;
using MetroSet_UI.Interfaces;

namespace MetroSet_UI.Controls
{
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(MetroSetContextMenuStrip), "Bitmaps.ContextMenu.bmp")]
	[DefaultEvent("Opening")]
	[ComVisible(true)]
	public class MetroSetContextMenuStrip : ContextMenuStrip, iControl
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
		private ToolStripItemClickedEventArgs _clickedEventArgs;

		#endregion Internal Vars

		#region Constructors

		public MetroSetContextMenuStrip()
		{
			_utl = new Utilites();
			ApplyTheme();
			Renderer = new MetroSetToolStripRender();
		}

		#endregion Constructors

		#region ApplyTheme

		/// <summary>
		/// Gets or sets the style provided by the user.
		/// </summary>
		/// <param name="style">The Style.</param>
		private void ApplyTheme(Style style = Style.Light)
		{
			switch (style)
			{
				case Style.Light:
					ForegroundColor = Color.FromArgb(170, 170, 170);
					BackgroundColor = Color.White;
					ArrowColor = Color.Gray;
					SelectedItemBackColor = Color.FromArgb(65, 177, 225);
					SelectedItemColor = Color.White;
					SeparatorColor = Color.LightGray;
					DisabledForeColor = Color.Silver;
					ThemeAuthor = "Narwin";
					ThemeName = "MetroLite";
					UpdateProperties();
					break;

				case Style.Dark:
					ForegroundColor = Color.FromArgb(170, 170, 170);
					BackgroundColor = Color.FromArgb(30, 30, 30);
					ArrowColor = Color.Gray;
					SelectedItemBackColor = Color.FromArgb(65, 177, 225);
					SelectedItemColor = Color.White;
					SeparatorColor = Color.Gray;
					DisabledForeColor = Color.Silver;
					ThemeAuthor = "Narwin";
					ThemeName = "MetroDark";
					UpdateProperties();
					break;

				case Style.Custom:
					if (StyleManager != null)
						foreach (var varkey in StyleManager.ContextMenuDictionary)
						{
							switch (varkey.Key)
							{
								case "ForeColor":
									ForegroundColor = _utl.HexColor((string)varkey.Value);
									break;

								case "BackColor":
									BackgroundColor = _utl.HexColor((string)varkey.Value);
									break;

								case "ArrowColor":
									ArrowColor = _utl.HexColor((string)varkey.Value);
									break;

								case "SeparatorColor":
									SeparatorColor = _utl.HexColor((string)varkey.Value);
									break;

								case "SelectedItemColor":
									SelectedItemColor = _utl.HexColor((string)varkey.Value);
									break;

								case "SelectedItemBackColor":
									SelectedItemBackColor = _utl.HexColor((string)varkey.Value);
									break;

								case "DisabledForeColor":
									DisabledForeColor = _utl.HexColor((string)varkey.Value);
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

		#endregion Theme Changing

		#region Properties

		/// <summary>
		/// Gets or sets ForegroundColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets forecolor used by the control.")]
		[DisplayName("ForeColor")]
		public static Color ForegroundColor { get; set; }

		/// <summary>
		/// Gets or sets BackgroundColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets backcolor used by the control.")]
		[DisplayName("BackColor")]
		public static Color BackgroundColor { get; set; }

		/// <summary>
		/// Gets or sets separator color used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets separator color used by the control.")]
		public static Color SeparatorColor { get; set; }

		/// <summary>
		/// Gets or sets Arrow color used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets arrowcolor used by the control.")]
		public static Color ArrowColor { get; set; }

		/// <summary>
		/// Gets or sets SelectedItem color used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets selecteditem color used by the control.")]
		public static Color SelectedItemColor { get; set; }

		/// <summary>
		/// Gets or sets SelectedItem BackColor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets selecteditem backcolor used by the control.")]
		public static Color SelectedItemBackColor { get; set; }

		/// <summary>
		/// Gets or sets disabled forecolor used by the control
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets disabled forecolor used by the control.")]
		public static Color DisabledForeColor { get; set; }

		public static new Font Font => MetroSetFonts.UIRegular(10);

		#endregion

		#region Events

		public event ClickedEventHandler Clicked;
		public delegate void ClickedEventHandler(object sender);

		/// <summary>
		/// Here we handle whenever and item clicked.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
		{
			if ((e.ClickedItem == null) || e.ClickedItem is ToolStripSeparator)
				return;
			if (ReferenceEquals(e, _clickedEventArgs))
				OnItemClicked(e);
			else
			{
				_clickedEventArgs = e;
				Clicked?.Invoke(this);
			}
		}

		/// <summary>
		/// Here we handle mouse hover event.
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnMouseHover(EventArgs e)
		{
			base.OnMouseHover(e);
			Cursor = Cursors.Hand;
			Invalidate();
		}

		/// <summary>
		/// Here we handle mouse up event
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			Cursor = Cursors.Hand;
			Invalidate();
		}

		#endregion

		#region Child

		private sealed class MetroSetToolStripRender : ToolStripProfessionalRenderer
		{

			#region Drawing Text

			/// <summary>
			/// Here we draw item text.
			/// </summary>
			/// <param name="e"></param>
			protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
			{
				e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
				var textRect = new Rectangle(25, e.Item.ContentRectangle.Y, e.Item.ContentRectangle.Width - (24 + 16), e.Item.ContentRectangle.Height - 4);
				using (var b = new SolidBrush(e.Item.Enabled ? e.Item.Selected ? SelectedItemColor : ForegroundColor : DisabledForeColor))
				{
					e.Graphics.DrawString(e.Text, Font, b, textRect);
				}
			}

			#endregion Drawing Text

			#region Drawing Backgrounds

			/// <summary>
			/// Here we draw toolstrip background.
			/// </summary>
			/// <param name="e"></param>
			protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
			{
				base.OnRenderToolStripBackground(e);
				e.Graphics.Clear(BackgroundColor);
			}

			/// <summary>
			/// Here we draw toolstrip menu item background.
			/// </summary>
			/// <param name="e"></param>
			protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
			{
				e.Graphics.InterpolationMode = InterpolationMode.High;
				e.Graphics.Clear(BackgroundColor);
				var r = new Rectangle(0, e.Item.ContentRectangle.Y - 2, e.Item.ContentRectangle.Width + 4, e.Item.ContentRectangle.Height + 3);
				using (var b = new SolidBrush(e.Item.Selected && e.Item.Enabled ? SelectedItemBackColor : BackgroundColor))
				{
					e.Graphics.FillRectangle(b, r);
				}
			}

			#endregion Drawing Backgrounds

			#region Set Image Margin

			protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
			{
				//MyBase.OnRenderImageMargin(e)
				//I Make above line comment which makes users to be able to add images to ToolStrips
			}

			#endregion Set Image Margin

			#region Drawing Seperators & Borders

			/// <summary>
			/// Here we draw toolstrip separators.
			/// </summary>
			/// <param name="e"></param>
			protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
			{
				using (var p = new Pen(SeparatorColor))
				{
					e.Graphics.DrawLine(p, new Point(e.Item.Bounds.Left, e.Item.Bounds.Height / 2), new Point(e.Item.Bounds.Right - 5, e.Item.Bounds.Height / 2));
				}
			}

			#endregion Drawing Seperators & Borders

			#region Drawing DropDown Arrows

			/// <summary>
			/// Here we draw the toolstrip arrows.
			/// </summary>
			/// <param name="e">ToolStripArrowRenderEventArgs</param>
			protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
			{
				var arrowX = e.ArrowRectangle.X + e.ArrowRectangle.Width / 2;
				var arrowY = e.ArrowRectangle.Y + e.ArrowRectangle.Height / 2;
				var arrowPoints = new[]
				{
				new Point(arrowX - 5, arrowY - 5),
				new Point(arrowX, arrowY),
				new Point(arrowX - 5, arrowY + 5)
				};

				using (var arrowBrush = new SolidBrush(e.Item.Enabled ? ArrowColor : DisabledForeColor))
				{
					e.Graphics.FillPolygon(arrowBrush, arrowPoints);
				}
			}

			#endregion Drawing DropDown Arrows
		}

		#endregion

	}
}