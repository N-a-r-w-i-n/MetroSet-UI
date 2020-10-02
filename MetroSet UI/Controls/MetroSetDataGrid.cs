using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MetroSet_UI.Components;
using MetroSet_UI.Enums;
using MetroSet_UI.Extensions;
using MetroSet_UI.Interfaces;

namespace MetroSet_UI.Controls
{
	[ToolboxItem(true)]
	[ComplexBindingProperties(nameof(DataSource), nameof(DataMember))]
	[Docking(DockingBehavior.Ask)]
	[DefaultEvent(nameof(CellContentClick))]
	public class MetroSetDataGrid : DataGridView, IMetroSetControl
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
					default:
						ApplyTheme();
						break;

					case Style.Dark:
						ApplyTheme(Style.Dark);
						break;

					case Style.Custom:
						ApplyTheme(Style.Custom);
						break;
				}

				Refresh();
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

		private MetroSetScrollBar _hSetScroll;
		private MetroSetScrollBar _vSetScroll;

		#endregion

		#region Constructors

		public MetroSetDataGrid()
		{

			BackgroundColor = Color.White;
			GridColor = Color.Blue;
			base.Font = MetroSetFonts.UIRegular(10);
			base.DoubleBuffered = true;

			_hSetScroll = new MetroSetScrollBar { Orientation = ScrollOrientate.Horizontal };
			_vSetScroll = new MetroSetScrollBar { Orientation = ScrollOrientate.Vertical };

			SetScrollDefaults(_hSetScroll);
			SetScrollDefaults(_vSetScroll);
			SetDataGridDefaults();

			SetBarPosition();

			AutoGenerateColumns = false;

			AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

			ColumnHeadersDefaultCellStyle.ApplyStyle(ColumnStyle);

		}

		private DataGridViewCellStyle ColumnStyle
		{
			get
			{
				return new DataGridViewCellStyle
				{
					BackColor = Color.FromArgb(65, 177, 225),
					Alignment = DataGridViewContentAlignment.MiddleCenter,
					Font = new Font("Segoe UI", 20),
					ForeColor = Color.White,
					WrapMode = DataGridViewTriState.True,
					SelectionBackColor = Color.Green
				};
			}
		}
		#endregion

		#region DrawControl

		protected override void OnPaint(PaintEventArgs e)
		{
			var g = e.Graphics;
			g.Clear(BackColor);
			var r = new Rectangle(0, 0, Width - 1, Height - 1);

			if (!ShowBorder)
				return;

			using (var p = new Pen(Enabled ? BorderColor : DisabledBorderColor))
			{
				g.DrawRectangle(p, r);
			}

		}

		#endregion

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
				default:
					//BackgroundColor = Color.FromArgb(65, 177, 225);
					BackColor = Color.White;
					BorderColor = Color.FromArgb(65, 177, 225);
					ForeColor = Color.Gray;
					DisabledBorderColor = Color.FromArgb(120, 65, 177, 225);
					ShowBorder = true;
					ThemeAuthor = "Narwin";
					ThemeName = "MetroLite";


					//StripeEvenColor = uiColor.GridStripeEvenColor;
					//StripeOddColor = uiColor.GridStripeOddColor;

					//HBar.FillColor = VBar.FillColor = uiColor.GridStripeOddColor;
					//HBar.ForeColor = VBar.ForeColor = uiColor.PrimaryColor;

					break;

				case Style.Dark:
					BackColor = Color.FromArgb(65, 177, 225);
					BorderColor = Color.FromArgb(65, 177, 225);
					ForeColor = Color.Gray;
					DisabledBorderColor = Color.FromArgb(120, 65, 177, 225);
					ShowBorder = true;
					ThemeAuthor = "Narwin";
					ThemeName = "MetroDark";
					break;

				case Style.Custom:
					if (StyleManager != null)
					{
						foreach (var varkey in StyleManager.ButtonDictionary)
						{
							if (varkey.Key == null)
								return;

							switch (varkey.Key)
							{
								case "BackColor":
									BackColor = _utl.HexColor((string)varkey.Value);
									break;
								case "BorderColor":
									BorderColor = _utl.HexColor((string)varkey.Value);
									break;
								case "ForeColor":
									ForeColor = _utl.HexColor((string)varkey.Value);
									break;
								case "DisabledBorderColor":
									DisabledBorderColor = _utl.HexColor((string)varkey.Value);
									break;
							}
						}
					}

					Refresh();
					break;
			}
		}

		#endregion Theme Changing

		#region Properties

		private Color borderColor;
		private bool showBorder;
		private Color disabledBorderColor;
		private Color backColor = Color.White;
		private Color foreColor = Color.Gray;
		private bool _isDerivedStyle = true;

		private Color gridColor;

		[Category("MetroSet Framework")]
		[Description("Gets or sets background color of the control.")]
		[DefaultValue(typeof(Color), "50, 50, 50")]
		public new Color GridColor
		{
			get { return gridColor; }
			set
			{
				gridColor = value;
				Refresh();
			}
		}


		[Category("MetroSet Framework")]
		[Description("Gets or sets background color of the control.")]
		[Browsable(true)]
		public new Color BackColor
		{
			get { return backColor; }
			set
			{
				backColor = value;
				BackgroundColor = value;
				Refresh();
			}
		}

		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		private new Color BackgroundColor { get; set; }

		[Category("MetroSet Framework")]
		[Description("Gets or sets foreground color of the control.")]
		public new Color ForeColor
		{
			get { return foreColor; }
			set
			{
				foreColor = value;
				Refresh();
			}
		}

		[Category("MetroSet Framework")]
		[Description("Gets or sets border color.")]
		public Color BorderColor
		{
			get { return borderColor; }
			set
			{
				borderColor = value;
				Refresh();
			}
		}

		[Category("MetroSet Framework")]
		[Description("Gets or sets border color while the control is disabled.")]
		public Color DisabledBorderColor
		{
			get { return disabledBorderColor; }
			set
			{
				disabledBorderColor = value;
				Refresh();
			}
		}

		[Category("MetroSet Framework")]
		[Description("Gets or sets whether to show the border around datagrid.")]
		public bool ShowBorder
		{
			get { return showBorder; }
			set
			{
				showBorder = value;
				Refresh();
			}
		}

		[Category("MetroSet Framework")]
		public new bool Enabled
		{
			get => base.Enabled;
			set
			{
				base.Enabled = value;
				Refresh();
			}
		}

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

		private Color columnBackColor = Color.DodgerBlue;

		[Category("MetroSet Framework")]
		[Description("Gets or sets column headers background color.")]
		public Color ColumnBackColor
		{
			get { return columnBackColor; }
			set
			{
				columnBackColor = value;
				Refresh();
			}
		}

		private Color columnForeColor = Color.White;

		[Category("MetroSet Framework")]
		[Description("Gets or sets column headers foreground color.")]
		public Color ColumnForeColor
		{
			get { return columnForeColor; }
			set
			{
				columnForeColor = value;
				Refresh();
			}
		}

		private DataGridViewCellStyle columnCellStyle;

		public DataGridViewCellStyle ColumnCellStyle
		{
			get { return columnCellStyle; }
			set
			{
				columnCellStyle = value;
				Refresh();
			}
		}



		#endregion

		#region Events

		private void SetScrollDefaults(MetroSetScrollBar setScrollBar)
		{
			setScrollBar.Parent = this;
			setScrollBar.StyleManager = StyleManager;
		}

		private void Init()
		{
			BorderStyle = BorderStyle.None;
			CellBorderStyle = DataGridViewCellBorderStyle.None;
			EnableHeadersVisualStyles = false;
			SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			//BackColor = MetroPaint.BackColor.Form(Theme);
			//BackgroundColor = MetroPaint.BackColor.Form(Theme);
			//GridColor = MetroPaint.BackColor.Form(Theme);
			//ForeColor = MetroPaint.ForeColor.Button.Disabled(Theme);
			Font = new Font("Segoe UI", 11f, FontStyle.Regular, GraphicsUnit.Pixel);

			RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			AllowUserToResizeRows = false;

			ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
			ColumnHeadersDefaultCellStyle.BackColor = BackColor;
			ColumnHeadersDefaultCellStyle.ForeColor = ForeColor;

			RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
			RowHeadersDefaultCellStyle.BackColor = BackColor;
			RowHeadersDefaultCellStyle.ForeColor = ForeColor;

			DefaultCellStyle.BackColor = BackColor;

			DefaultCellStyle.SelectionBackColor = Color.Green;
			DefaultCellStyle.SelectionForeColor = Color.White;

			RowHeadersDefaultCellStyle.SelectionBackColor = BackColor;
			RowHeadersDefaultCellStyle.SelectionForeColor = Color.White;

			//ColumnHeadersDefaultCellStyle.SelectionBackColor = ControlPaint.Light(MetroPaint.GetStyleColor(Style), _offset);
			//ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(17, 17, 17);
		}

		private void SetDataGridDefaults()
		{
			EnableHeadersVisualStyles = false;
			ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
			ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			ColumnHeadersDefaultCellStyle.BackColor = ColumnBackColor;
			ColumnHeadersDefaultCellStyle.ForeColor = ColumnForeColor;
			ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
			ColumnHeadersHeight = 32;
			RowTemplate.Height = 29;
			RowTemplate.MinimumHeight = 29;
			AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
		}

		private int ColumnsCount()
		{
			return Columns.Cast<DataGridViewColumn>().Count();
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			if (!_vSetScroll.Visible)
				return;
			if (e.Delta > 10)
			{
				_vSetScroll.Value -= _vSetScroll.Maximum / 20;
			}
			else if (e.Delta < -10)
			{
				_vSetScroll.Value += _vSetScroll.Maximum / 20;
			}
		}

		protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
		{
			base.OnColumnAdded(e);
			//e.Column.DefaultCellStyle = ColumnCellStyle;
			if (ColumnHeadersHeightSizeMode == DataGridViewColumnHeadersHeightSizeMode.AutoSize)
			{
				ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			}
		}

		public DataGridViewColumn AddColumn(string columnName, string dataPropertyName, int fillWeight = 100,
											DataGridViewContentAlignment alignment =
												DataGridViewContentAlignment.MiddleCenter, bool readOnly = true)
		{
			DataGridViewColumn column = new DataGridViewTextBoxColumn
			{
				HeaderText = columnName,
				DataPropertyName = dataPropertyName,
				Name = columnName,
				ReadOnly = readOnly,
				FillWeight = fillWeight,
				SortMode = DataGridViewColumnSortMode.NotSortable
			};
			column.DefaultCellStyle.Alignment = alignment;
			Columns.Add(column);
			return column;
		}

		private void SetBarPosition()
		{

			var vScrollRect = new Rectangle(Width - 11, 1, 10, Height - 2);

			SetScrollPosition(_vSetScroll, vScrollRect);

			var hScrollRect = new Rectangle(1, Height - 10, Width - 3, 10);

			SetScrollPosition(_hSetScroll, hScrollRect);

			//_vSetScroll.Left = Width - 2;
			//_vSetScroll.Top = 1;
			//_vSetScroll.Width = 10;
			//_vSetScroll.Height = Height - 2;
			//_vSetScroll.BringToFront();

			//_hSetScroll.Left = Height - 2;
			//_hSetScroll.Height = 10;
			//_hSetScroll.Width = Width - (_vSetScroll.Visible ? _vSetScroll.Width : 0) - 2;
			//_hSetScroll.Top = Height - _hSetScroll.Height - 2;
			//_hSetScroll.BringToFront();
		}

		private void SetScrollPosition(MetroSetScrollBar setScrollBar, Rectangle rect)
		{
			setScrollBar.Left = rect.X;
			setScrollBar.Width = rect.Width;
			setScrollBar.Top = rect.Y;
			setScrollBar.Height = rect.Height;
			setScrollBar.BringToFront();
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			SetScrollDefaults(_hSetScroll);
			SetScrollDefaults(_vSetScroll);
			SetBarPosition();
		}

		protected override void OnDataSourceChanged(EventArgs e)
		{
			base.OnDataSourceChanged(e);
			SetScrollDefaults(_vSetScroll);
			SetScrollDefaults(_hSetScroll);
		}

		#endregion

	}
}
