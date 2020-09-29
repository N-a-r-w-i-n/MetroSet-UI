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
	[ToolboxBitmap(typeof(MetroSetRichTextBox), "Bitmaps.RitchTextBox.bmp")]
	[Designer(typeof(MetroSetRichTextBoxDesigner))]
	[DefaultProperty("Text")]
	[DefaultEvent("TextChanged")]
	[ComVisible(true)]
	public class MetroSetRichTextBox : Control, iControl
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

		private Utilites _utl;

		#endregion Global Vars

		#region Internal Vars

		private Style _style;
		private StyleManager _styleManager;
		private int _maxLength;
		private bool _readOnly;
		private MouseMode _state;
		private bool _wordWrap;
		private bool _autoWordSelection;
		private string[] _lines;
		private Color _foreColor;
		private Color _backColor;
		private Color _borderColor;
		private Color _hoverColor;

		private Color _disabledBackColor = Color.FromArgb(204, 204, 204);
		private Color _disabledForeColor = Color.FromArgb(136, 136, 136);
		private Color _disabledBorderColor = Color.FromArgb(155, 155, 155);

		#region Base RichTextBox

		private RichTextBox _richTextBox = new RichTextBox();

		#endregion Base RichTextBox

		#endregion Internal Vars

		#region Constructors

		public MetroSetRichTextBox()
		{
			SetStyle(
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.OptimizedDoubleBuffer |
				ControlStyles.SupportsTransparentBackColor, true);
			UpdateStyles();
			VirtualCalls();
			EvaluateVars();
			ApplyTheme();
			T_Defaults();
		}

		private void VirtualCalls()
		{
			Font = MetroSetFonts.Regular(10);
		}

		private void EvaluateVars()
		{
			_utl = new Utilites();
		}

		private void T_Defaults()
		{
			_wordWrap = true;
			_autoWordSelection = false;
			_foreColor = Color.FromArgb(20, 20, 20);

			_borderColor = Color.FromArgb(155, 155, 155);
			_hoverColor = Color.FromArgb(102, 102, 102);
			_backColor = Color.FromArgb(238, 238, 238);
			_richTextBox.BackColor = BackColor;
			_richTextBox.ForeColor = ForeColor;
			_readOnly = false;
			_maxLength = 32767;
			_state = MouseMode.Normal;
			_lines = null;
			_richTextBox.Cursor = Cursors.IBeam;
			_richTextBox.BorderStyle = BorderStyle.None;
			_richTextBox.Location = new Point(7, 8);
			_richTextBox.Font = Font;
			_richTextBox.Size = new Size(Width, Height);

			_richTextBox.MouseHover += T_MouseHover;
			_richTextBox.MouseUp += T_MouseUp;
			_richTextBox.Leave += T_Leave;
			_richTextBox.Enter += T_Enter;
			_richTextBox.KeyDown += T_KeyDown;
			_richTextBox.TextChanged += T_TextChanged;

		}

		#endregion Constructors

		#region Draw Control

		protected override void OnPaint(PaintEventArgs e)
		{
			var g = e.Graphics;
			var rect = new Rectangle(0, 0, Width - 1, Height - 1);
			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

			if (Enabled)
			{
				using (var bg = new SolidBrush(BackColor))
				{
					using (var p = new Pen(BorderColor))
					{
						using (var ph = new Pen(HoverColor))
						{
							g.FillRectangle(bg, rect);
							switch (_state)
							{
								case MouseMode.Normal:
									g.DrawRectangle(p, rect);
									break;
								case MouseMode.Hovered:
									g.DrawRectangle(ph, rect);
									break;
							}

							_richTextBox.BackColor = BackColor;
							_richTextBox.ForeColor = ForeColor;
						}
					}
				}
			}
			else
			{
				using (var bg = new SolidBrush(DisabledBackColor))
				{
					using (var p = new Pen(DisabledBorderColor))
					{
						g.FillRectangle(bg, rect);
						g.DrawRectangle(p, rect);
						_richTextBox.BackColor = DisabledBackColor;
						_richTextBox.ForeColor = DisabledForeColor;
					}
				}
			}

			_richTextBox.Location = new Point(7, 4);
			_richTextBox.Width = Width - 10;

		}

		#endregion Draw Control

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
					ForeColor = Color.FromArgb(20, 20, 20);
					BackColor = Color.FromArgb(238, 238, 238);
					HoverColor = Color.FromArgb(102, 102, 102);
					BorderColor = Color.FromArgb(155, 155, 155);
					DisabledBackColor = Color.FromArgb(204, 204, 204);
					DisabledBorderColor = Color.FromArgb(155, 155, 155);
					DisabledForeColor = Color.FromArgb(136, 136, 136);
					ThemeAuthor = "Narwin";
					ThemeName = "MetroLite";
					UpdateProperties();
					break;

				case Style.Dark:
					ForeColor = Color.FromArgb(204, 204, 204);
					BackColor = Color.FromArgb(34, 34, 34);
					HoverColor = Color.FromArgb(170, 170, 170);
					BorderColor = Color.FromArgb(110, 110, 110);
					DisabledBackColor = Color.FromArgb(80, 80, 80);
					DisabledBorderColor = Color.FromArgb(109, 109, 109);
					DisabledForeColor = Color.FromArgb(109, 109, 109);
					ThemeAuthor = "Narwin";
					ThemeName = "MetroDark";
					UpdateProperties();
					break;

				case Style.Custom:
					if (StyleManager != null)
						foreach (var varkey in StyleManager.RichTextBoxDictionary)
						{
							switch (varkey.Key)
							{
								case "ForeColor":
									ForeColor = _utl.HexColor((string)varkey.Value);
									break;

								case "BackColor":
									BackColor = _utl.HexColor((string)varkey.Value);
									break;

								case "HoverColor":
									HoverColor = _utl.HexColor((string)varkey.Value);
									break;

								case "BorderColor":
									BorderColor = _utl.HexColor((string)varkey.Value);
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

		/// <summary>
		/// Updating properties after changing in style.
		/// </summary>
		private void UpdateProperties()
		{
			Invalidate();
			_richTextBox.Invalidate();
		}

		#endregion ApplyTheme

		#region Events

		public new event TextChangedEventHandler TextChanged;

		public delegate void TextChangedEventHandler(object sender);

		public event SelectionChangedEventHandler SelectionChanged;

		public delegate void SelectionChangedEventHandler(object sender, EventArgs e);

		public event LinkClickedEventHandler LinkClicked;

		public delegate void LinkClickedEventHandler(object sender, EventArgs e);

		public event ProtectedEventHandler Protected;

		public delegate void ProtectedEventHandler(object sender, EventArgs e);


		/// <summary>
		/// Handling richtextbox selection changed event and raising the same event here.
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">EventArgs</param>
		private void T_SelectionChanged(object sender, EventArgs e)
		{
			SelectionChanged?.Invoke(sender, e);
		}

		/// <summary>
		/// Handling richtextbox link clicked event  and raising the same event here.
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">EventArgs</param>
		private void T_LinkClicked(object sender, EventArgs e)
		{
			LinkClicked?.Invoke(sender, e);
		}

		/// <summary>
		/// Handling textbox link clicked event and raising the same event here.
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">EventArgs</param>
		private void T_Protected(object sender, EventArgs e)
		{
			Protected?.Invoke(sender, e);
		}

		/// <summary>
		/// Handling richtextbox leave event and raising the same event here.
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">EventArgs</param>
		public void T_Leave(object sender, EventArgs e)
		{
			base.OnMouseLeave(e);
			Invalidate();
		}

		/// <summary>
		/// Handling mouse leave event of the control.
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			_state = MouseMode.Normal;
			Invalidate();
		}

		/// <summary>
		/// Handling mouse up event of the control.
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			_state = MouseMode.Hovered;
			Invalidate();
		}

		/// <summary>
		/// Handling mouse up event
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">EventArgs</param>
		public void T_MouseUp(object sender, MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (e.Button == MouseButtons.Right)
			{
				if (ContextMenuStrip != null)
					ContextMenuStrip.Show(_richTextBox, new Point(e.X, e.Y));
			}
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
		/// Handling mouse hover event of the control.
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnMouseHover(EventArgs e)
		{
			base.OnMouseHover(e);
			_state = MouseMode.Hovered;
			Invalidate();
		}

		/// <summary>
		/// Handling the mouse hover event on textbox control.
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">EventArgs</param>
		public void T_MouseHover(object sender, EventArgs e)
		{
			base.OnMouseHover(e);
			Invalidate();
		}

		/// <summary>
		/// Handling the richtextbox size while resizing the control.
		/// </summary>
		/// <param name="e">EventArgs</param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			_richTextBox.Size = new Size(Width - 10, Height - 10);
		}

		/// <summary>
		/// Raises the Control.Enter event.
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">EventArgs</param>
		public void T_Enter(object sender, EventArgs e)
		{
			base.OnMouseEnter(e);
			Invalidate();
		}

		/// <summary>
		/// Handling Keydown event of thextbox control.
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">KeyEventArgs</param>
		private void T_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.A)
				e.SuppressKeyPress = true;
			if (e.Control && e.KeyCode == Keys.C)
			{
				_richTextBox.Copy();
				e.SuppressKeyPress = true;
			}
		}

		/// <summary>
		/// An System.EventArgs that contains the event data.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void T_TextChanged(object sender, EventArgs e)
		{
			Text = _richTextBox.Text;
			TextChanged?.Invoke(this);
			Invalidate();
		}

		/// <summary>
		/// override the control creating , here we add the base textbox to the main control.
		/// </summary>
		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			if (!Controls.Contains(_richTextBox))
				Controls.Add(_richTextBox);
		}

		/// <summary>
		/// Appends text to the current text of a text box.
		/// </summary>
		/// <param name="text"></param>
		public void AppendText(string text)
		{
			if (_richTextBox != null)
			{
				_richTextBox.AppendText(text);
			}
		}

		/// <summary>
		/// Undoes the last edit operation in the text box.
		/// </summary>
		public void Undo()
		{
			if (_richTextBox != null)
			{
				if (_richTextBox.CanUndo)
				{
					_richTextBox.Undo();
				}
			}
		}

		/// <summary>
		/// Retrieves the line number from the specified character position within the text of the control.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public int GetLineFromCharIndex(int index)
		{
			if (_richTextBox != null)
			{
				return _richTextBox.GetLineFromCharIndex(index);
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		/// Retrieves the location within the control at the specified character index.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public Point GetPositionFromCharIndex(int index)
		{
			return _richTextBox.GetPositionFromCharIndex(index);
		}

		/// <summary>
		/// Retrieves the index of the character nearest to the specified location.
		/// </summary>
		/// <param name="pt"></param>
		/// <returns></returns>
		public int GetCharIndexFromPosition(Point pt)
		{
			if (_richTextBox == null)
				return 0;
			return _richTextBox.GetCharIndexFromPosition(pt);
		}

		/// <summary>
		/// Clears information about the most recent operation from the undo buffer of the text box.
		/// </summary>
		public void ClearUndo()
		{
			if (_richTextBox == null)
				return;
			_richTextBox.ClearUndo();
		}

		/// <summary>
		/// Copies the current selection in the text box to the Clipboard.
		/// </summary>
		public void Copy()
		{
			if (_richTextBox == null)
				return;
			_richTextBox.Copy();
		}

		/// <summary>
		/// Moves the current selection in the text box to the Clipboard.
		/// </summary>
		public void Cut()
		{
			if (_richTextBox == null)
				return;
			_richTextBox.Cut();
		}

		/// <summary>
		/// Selects all text in the text box.
		/// </summary>
		public void SelectAll()
		{
			if (_richTextBox == null)
				return;
			_richTextBox.SelectAll();
		}

		/// <summary>
		/// Specifies that the value of the TextBoxBase.SelectionLength property is zero so that no characters are selected in the control.
		/// </summary>
		public void DeselectAll()
		{
			if (_richTextBox == null)
				return;
			_richTextBox.DeselectAll();
		}

		/// <summary>
		/// Selects a range of text in the text box.
		/// </summary>
		/// <param name="start">The position of the first character in the current text selection within the text box.</param>
		/// <param name="length">The number of characters to select.</param>
		public void Select(int start, int length)
		{
			if (_richTextBox == null)
				return;
			_richTextBox.Select(start, length);
		}

		/// <summary>
		/// Pastes the contents of the Clipboard in the specified Clipboard format.
		/// </summary>
		/// <param name="clipFormat">The Clipboard format in which the data should be obtained from the Clipboard.</param>
		public void Paste(DataFormats.Format clipFormat)
		{
			if (_richTextBox == null)
				return;
			_richTextBox.Paste(clipFormat);
		}

		/// <summary>
		/// Loads a rich text format (RTF) or standard ASCII text file into the control.
		/// </summary>
		/// <param name="path">The name and location of the file to load into the control.</param>
		public void LoadFile(string path)
		{
			if (_richTextBox == null)
				return;
			_richTextBox.LoadFile(path);
		}

		/// <summary>
		/// Loads a specific type of file into the control.
		/// </summary>
		/// <param name="path">The name and location of the file to load into the control.</param>
		/// <param name="fileType">One of the System.Windows.Forms.RichTextBoxStreamType values.</param>
		public void LoadFile(string path, RichTextBoxStreamType fileType)
		{
			if (_richTextBox == null)
				return;
			_richTextBox.LoadFile(path, fileType);
		}

		/// <summary>
		/// Loads the contents of an existing data stream into the System.Windows.Forms.RichTextBox control.
		/// </summary>
		/// <param name="data">TA stream of data to load into the control.</param>
		/// <param name="fileType">One of the control StreamType values.</param>
		public void LoadFile(System.IO.Stream data, RichTextBoxStreamType fileType)
		{
			if (_richTextBox == null)
				return;
			_richTextBox.LoadFile(data, fileType);
		}

		/// <summary>
		/// Saves the contents of the control to a rich text format (RTF) file.
		/// </summary>
		/// <param name="path">The name and location of the file to save.</param>
		public void SaveFile(string path)
		{
			if (_richTextBox == null)
				return;
			_richTextBox.SaveFile(path);
		}

		/// <summary>
		/// Saves the contents of the control to a specific type of file.
		/// </summary>
		/// <param name="path">The name and location of the file to save.</param>
		/// <param name="fileType">One of the control StreamType values.</param>
		public void SaveFile(string path, RichTextBoxStreamType fileType)
		{
			if (_richTextBox == null)
				return;
			_richTextBox.SaveFile(path, fileType);
		}

		/// <summary>
		/// Saves the contents of the control to an open data stream.
		/// </summary>
		/// <param name="data">The data stream that contains the file to save to.</param>
		/// <param name="fileType">One of the control StreamType values.</param>
		public void SaveFile(System.IO.Stream data, RichTextBoxStreamType fileType)
		{
			if (_richTextBox == null)
				return;
			_richTextBox.SaveFile(data, fileType);
		}

		/// <summary>
		/// Determines whether you can paste information from the Clipboard in the specified data format.
		/// </summary>
		/// <param name="clipFormat">clipFormat: One of the System.Windows.Forms.DataFormats.Format values.</param>
		/// <returns>true if you can paste data from the Clipboard in the specified data format; otherwise, false.</returns>
		public bool CanPaste(DataFormats.Format clipFormat)
		{
			return _richTextBox.CanPaste(clipFormat);
		}


		/// <summary>
		/// Searches the text of the control for the first instance of a character from a list of characters.
		/// </summary>
		/// <param name="characterSet">The array of characters to search for.</param>
		/// <returns>The location within the control where the search characters were found or -1 if the search characters are not found or an empty search character set is specified in the char parameter.</returns>
		public int Find(char[] characterSet)
		{
			if (_richTextBox == null)
				return 0;
			return _richTextBox.Find(characterSet);
		}

		/// <summary>
		/// Searches the text of the control, at a specific starting point, for the first instance of a character from a list of characters.
		/// </summary>
		/// <param name="characterSet">The array of characters to search for.</param>
		/// <param name="start">The location within the control's text at which to begin searching.</param>
		/// <returns>The location within the control where the search characters are found.</returns>
		public int Find(char[] characterSet, int start)
		{
			if (_richTextBox == null)
				return 0;
			return _richTextBox.Find(characterSet, start);
		}

		/// <summary>
		/// Searches a range of text in the control for the first instance of a character from a list of characters.
		/// </summary>
		/// <param name="characterSet">The array of characters to search for.</param>
		/// <param name="start">The location within the control's text at which to begin searching.</param>
		/// <param name="ends">The location within the control's text at which to end searching.</param>
		/// <returns>The location within the control where the search characters are found.</returns>
		public int Find(char[] characterSet, int start, int ends)
		{
			if (_richTextBox == null)
				return 0;
			return _richTextBox.Find(characterSet, start, ends);
		}

		/// <summary>
		/// Searches the text in the control for a string.
		/// </summary>
		/// <param name="str">The text to locate in the control.</param>
		/// <returns>The location within the control where the search text was found or -1 if the search string is not found or an empty search string is specified in the str parameter.</returns>
		public int Find(string str)
		{
			if (_richTextBox == null)
				return 0;
			return _richTextBox.Find(str);
		}

		/// <summary>
		/// Searches the text in the control for a string within a range of text within the control and with specific options applied to the search.
		/// </summary>
		/// <param name="str">The text to locate in the control.</param>
		/// <param name="start">The location within the control's text at which to begin searching.</param>
		/// <param name="ends">The location within the control's text at which to end searching.</param>
		/// <param name="options">A bitwise combination of the control values.</param>
		/// <returns>The location within the control where the search text was found.</returns>
		public int Find(string str, int start, int ends, RichTextBoxFinds options)
		{
			if (_richTextBox == null)
				return 0;
			return _richTextBox.Find(str, start, ends, options);
		}

		/// <summary>
		/// Searches the text in the control for a string at a specific location within the control and with specific options applied to the search.
		/// </summary>
		/// <param name="str">The text to locate in the control.</param>
		/// <param name="options">A bitwise combination of the control values.</param>
		/// <returns>The location within the control where the search text was found.</returns>
		public int Find(string str, RichTextBoxFinds options)
		{
			if (_richTextBox == null)
				return 0;
			return _richTextBox.Find(str, options);
		}

		/// <summary>
		/// Searches the text in the control for a string with specific options applied to the search.
		/// </summary>
		/// <param name="str">The text to locate in the control.</param>
		/// <param name="start">The location within the control's text at which to begin searching.</param>
		/// <param name="options">A bitwise combination of the control values.</param>
		/// <returns>The location within the control where the search text was found.</returns>
		public int Find(string str, int start, RichTextBoxFinds options)
		{
			if (_richTextBox == null)
				return 0;
			return _richTextBox.Find(str, start, options);
		}

		#endregion Events

		#region Properties

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public BorderStyle BorderStyle => BorderStyle.None;

		/// <summary>
		/// Gets or sets how text is aligned in a TextBox control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets how text is aligned in a RichTextBox control.")]
		public int MaxLength
		{
			get => _maxLength;
			set
			{
				_maxLength = value;
				if (_richTextBox != null)
				{
					_richTextBox.MaxLength = value;
				}
				Invalidate();
			}
		}



		/// <summary>
		/// Gets or sets the background color of the control.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the background color of the control.")]
		public override Color BackColor
		{
			get => _backColor;
			set
			{
				_backColor = value;
				_richTextBox.BackColor = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the color of the control whenever hovered.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the color of the control whenever hovered.")]
		public Color HoverColor
		{
			get => _hoverColor;
			set
			{
				_hoverColor = value;
				Invalidate();
			}
		}


		/// <summary>
		/// Gets or sets the border color of the control.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the border color of the control.")]
		public Color BorderColor
		{
			get => _borderColor;
			set
			{
				_borderColor = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the foreground color of the control.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets the foreground color of the control.")]
		[Browsable(false)]
		public override Color ForeColor
		{
			get => _foreColor;
			set
			{
				_foreColor = value;
				_richTextBox.ForeColor = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether text in the text box is read-only.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets a value indicating whether text in the RichTextBox is read-only.")]
		public bool ReadOnly
		{
			get => _readOnly;
			set
			{
				_readOnly = value;
				if (_richTextBox != null)
				{
					_richTextBox.ReadOnly = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the background image.
		/// </summary>
		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Image BackgroundImage => null;

		/// <summary>
		/// Gets or sets the current text in the TextBox.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the current text in the RichTextBox.")]
		public override string Text
		{
			get => _richTextBox.Text;
			set
			{
				base.Text = value;
				if (_richTextBox != null)
				{
					_richTextBox.Text = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the font of the text displayed by the control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the font of the text displayed by the control.")]
		public override Font Font
		{
			get => base.Font;
			set
			{
				base.Font = value;
				if (_richTextBox == null)
					return;
				_richTextBox.Font = value;
				_richTextBox.Location = new Point(5, 5);
				_richTextBox.Width = Width - 8;
			}
		}

		/// <summary>
		/// Indicates whether a multiline text box control automatically wraps words to the beginning of the next line when necessary.
		/// </summary>
		[Category("MetroSet Framework"), Description("Indicates whether a multiline text box control automatically wraps words to the beginning of the next line when necessary.")]
		public bool WordWrap
		{
			get => _wordWrap;
			set
			{
				_wordWrap = value;
				if (_richTextBox != null)
				{
					_richTextBox.WordWrap = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether automatic word selection is enabled.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets a value indicating whether automatic word selection is enabled.")]
		public bool AutoWordSelection
		{
			get => _autoWordSelection;
			set
			{
				_autoWordSelection = value;
				if (_richTextBox != null)
				{
					_richTextBox.AutoWordSelection = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the lines of text in the control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the lines of text in the control.")]
		public string[] Lines
		{
			get => _lines;
			set
			{
				_lines = value;
				if (_richTextBox != null)
					_richTextBox.Lines = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets the ContextMenuStrip associated with this control.
		/// </summary>
		[Category("MetroSet Framework"), Description("Gets or sets the ContextMenuStrip associated with this control.")]
		public override ContextMenuStrip ContextMenuStrip
		{
			get => base.ContextMenuStrip;
			set
			{
				base.ContextMenuStrip = value;
				if (_richTextBox == null)
					return;
				_richTextBox.ContextMenuStrip = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Gets or sets back color used by the control while disabled.
		/// </summary>
		[Category("MetroSet Framework")]
		[Description("Gets or sets back color used by the control while disabled.")]
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
		[Description("Gets or sets the fore color of the control whenever while disabled")]
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


		#endregion Properties

	}
}