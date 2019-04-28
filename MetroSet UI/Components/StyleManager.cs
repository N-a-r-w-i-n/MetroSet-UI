/**
 * MetroSet UI - MetroSet UI Framewrok
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
using MetroSet_UI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml;

namespace MetroSet_UI
{
    [DefaultProperty("Style")]
    [Designer(typeof(StyleManagerDesigner))]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(StyleManager), "Style.bmp")]
    public class StyleManager : Component
    {

        #region Constructor

        public StyleManager(System.Windows.Forms.Form ownerForm)
        {
            MetroForm = ownerForm;
        }

        public StyleManager()
        {
            _style = Style.Light;
            if (_customTheme == null)
            {
                var themeFile = Properties.Settings.Default.ThemeFile;
                _customTheme = File.Exists(themeFile) ? themeFile : ThemeFilePath(themeFile);
            }
            EvaluateDicts();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The Method to update the form with the style manager style and it's controls.
        /// </summary>
        private void UpdateForm()
        {
            switch (MetroForm)
            {
                case null:
                    return;
                case iForm form when CustomTheme != null:
                    form.Style = Style;
                    form.ThemeAuthor = ThemeAuthor;
                    form.ThemeName = ThemeName;
                    form.StyleManager = this;
                    break;
            }

            if (MetroForm.Controls.Count > 0)
                UpdateControls(MetroForm.Controls);

            MetroForm.Invalidate();
        }

        /// <summary>
        /// The Method to update controls with the style manager style.
        /// </summary>
        private void UpdateControls(Control.ControlCollection controls)
        {
            if (controls == null) throw new ArgumentNullException(nameof(controls));
            foreach (Control ctrl in controls)
            {
                var control = ctrl as iControl;
                if (control != null && CustomTheme != null)
                {
                    control.Style = Style;
                    control.ThemeAuthor = ThemeAuthor;
                    control.ThemeName = ThemeName;
                    control.StyleManager = this;
                }
                if (control is TabControl tabControl)
                {
                    foreach (TabPage c in tabControl.TabPages)
                    {
                        if (c is iControl)
                        {
                            control.Style = Style;
                            control.StyleManager = this;
                            control.ThemeAuthor = ThemeAuthor;
                            control.ThemeName = ThemeName;
                        }
                        UpdateControls(c.Controls);
                    }
                }

                foreach (Control child in ctrl.Controls)
                {
                    if (!(child is iControl)) continue;
                    ((iControl)child).Style = Style;
                    ((iControl)child).StyleManager = this;
                    ((iControl)child).ThemeAuthor = ThemeAuthor;
                    ((iControl)child).ThemeName = ThemeName;

                }
            }
        }

        /// <summary>
        /// The Method to apply the style manager style to the added controls.
        /// </summary>
        private void ControlAdded(object sender, ControlEventArgs e)
        {
            if (e.Control is iControl control && CustomTheme != null)
            {
                control.Style = Style;
                control.ThemeAuthor = ThemeAuthor;
                control.ThemeName = ThemeName;
                control.StyleManager = this;
            }
            else
            {
                UpdateForm();
            }
        }

        #endregion

        #region Internal Vars

        private Style _style;
        private Form _metroForm;
        private string _customTheme;

        #endregion Internal Vars

        #region Properties

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
        /// Gets or sets the form (MetroForm) to Apply themes for.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the form (MetroForm) to Apply themes for.")]
        public Form MetroForm
        {
            get => _metroForm;
            set
            {
                if (_metroForm != null) return;
                _metroForm = value;
                _metroForm.ControlAdded += ControlAdded;
                UpdateForm();
            }
        }

        /// <summary>
        /// Gets or sets the style for the button.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the style.")]
        public Style Style
        {
            get => _style;
            set
            {
                _style = value;
                switch (value)
                {
                    case Style.Light:
                        ThemeAuthor = "Narwin";
                        ThemeName = "MetroLite";
                        break;
                    case Style.Dark:
                        ThemeAuthor = "Narwin";
                        ThemeName = "MetroDark";
                        break;
                }

                UpdateForm();
            }
        }


        /// <summary>
        /// Gets or sets the custom theme file controls.
        /// </summary>
        [Editor(typeof(FileNamesEditor), typeof(UITypeEditor)), Category("MetroSet Framework"), Description("Gets or sets the custom theme file.")]
        public string CustomTheme
        {
            get => _customTheme;
            set
            {
                if (Style == Style.Custom)
                {
                    Properties.Settings.Default.ThemeFile = value;
                    Properties.Settings.Default.Save();
                    ControlProperties(value);
                }
                _customTheme = value;
            }
        }

        #endregion Properties

        #region Open Theme

        /// <summary>
        /// The Method to excute the FileNamesEditor and open the dialog of importing the custom theme.
        /// </summary>
        public void OpenTheme()
        {
            Style = Style.Custom;
            using (var ofd = new OpenFileDialog { Filter = "Xml File (*.xml)|*.xml" })
            {
                if (ofd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                CustomTheme = ofd.FileName;
            }
        }

        /// <summary>
        /// The Method for setting the custom theme up.
        /// </summary>
        /// <param name="path">The Custom theme file path.</param>
        public void SetTheme(string path)
        {
            Style = Style.Custom;
            CustomTheme = path;
        }

        /// <summary>
        /// The Method to write the them file from resources to templates folder.
        /// </summary>
        /// <param name="str">the theme content</param>
        /// <returns>The Sorted theme path in templates folder.</returns>
        private string ThemeFilePath(string str)
        {
            var path = $"{Environment.GetFolderPath(Environment.SpecialFolder.Templates) + @"\ThemeFile.xml"}";
            File.WriteAllText(path, str);
            return path;
        }

        #endregion Open Theme

        #region Dictionaries

        #region Declartions

        /// <summary>
        /// The Button properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> ButtonDictionary;

        /// <summary>
        /// The DefaultButton properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> DefaultButtonDictionary;

        /// <summary>
        /// The Label properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> LabelDictionary;

        /// <summary>
        /// The LinkLabel properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> LinkLabelDictionary;

        /// <summary>
        /// The TextBox properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> TextBoxDictionary;

        /// <summary>
        /// The RichTextBox properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> RichTextBoxDictionary;

        /// <summary>
        /// The ComboBox properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> ComboBoxDictionary;

        /// <summary>
        /// The Form properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> FormDictionary;

        /// <summary>
        /// The Badge properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> BadgeDictionary;

        /// <summary>
        /// The Divider properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> DividerDictionary;

        /// <summary>
        /// The CheckBox properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> CheckBoxDictionary;

        /// <summary>
        /// The RadioButton properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> RadioButtonDictionary;

        /// <summary>
        /// The Switch properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> SwitchBoxDictionary;

        /// <summary>
        /// The ToolTip properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> ToolTipDictionary;

        /// <summary>
        /// The ToolTip properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> NumericDictionary;

        /// <summary>
        /// The ToolTip properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> EllipseDictionary;

        /// <summary>
        /// The Tile properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> TileDictionary;

        /// <summary>
        /// The Tile properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> ProgressDictionary;

        /// <summary>
        /// The ControlBox properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> ControlBoxDictionary;

        /// <summary>
        /// The TabControl properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> TabControlDictionary;

        /// <summary>
        /// The ScrollBar properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> ScrollBarDictionary;

        /// <summary>
        /// The Panel properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> PanelDictionary;

        /// <summary>
        /// The TrackBar properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> TrackBarDictionary;

        /// <summary>
        /// The ContextMenuStrip properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> ContextMenuDictionary;

        /// <summary>
        /// The ListBox properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> ListBoxDictionary;


        #endregion

        #region Methods 

        private void Clear()
        {
            ButtonDictionary.Clear();
            DefaultButtonDictionary.Clear();
            FormDictionary.Clear();
            LabelDictionary.Clear();
            TextBoxDictionary.Clear();
            LabelDictionary.Clear();
            LinkLabelDictionary.Clear();
            BadgeDictionary.Clear();
            DividerDictionary.Clear();
            CheckBoxDictionary.Clear();
            RadioButtonDictionary.Clear();
            SwitchBoxDictionary.Clear();
            ToolTipDictionary.Clear();
            RichTextBoxDictionary.Clear();
            ComboBoxDictionary.Clear();
            NumericDictionary.Clear();
            EllipseDictionary.Clear();
            TileDictionary.Clear();
            ProgressDictionary.Clear();
            ControlBoxDictionary.Clear();
            TabControlDictionary.Clear();
            ScrollBarDictionary.Clear();
            PanelDictionary.Clear();
            TrackBarDictionary.Clear();
            ContextMenuDictionary.Clear();
            ListBoxDictionary.Clear();
        }

        #endregion

        #region Evaluate

        private void EvaluateDicts()
        {
            ButtonDictionary = new Dictionary<string, object>();
            DefaultButtonDictionary = new Dictionary<string, object>();
            LabelDictionary = new Dictionary<string, object>();
            LinkLabelDictionary = new Dictionary<string, object>();
            TextBoxDictionary = new Dictionary<string, object>();
            RichTextBoxDictionary = new Dictionary<string, object>();
            FormDictionary = new Dictionary<string, object>();
            BadgeDictionary = new Dictionary<string, object>();
            DividerDictionary = new Dictionary<string, object>();
            CheckBoxDictionary = new Dictionary<string, object>();
            RadioButtonDictionary = new Dictionary<string, object>();
            SwitchBoxDictionary = new Dictionary<string, object>();
            ToolTipDictionary = new Dictionary<string, object>();
            ComboBoxDictionary = new Dictionary<string, object>();
            NumericDictionary = new Dictionary<string, object>();
            EllipseDictionary = new Dictionary<string, object>();
            TileDictionary = new Dictionary<string, object>();
            ProgressDictionary = new Dictionary<string, object>();
            ControlBoxDictionary = new Dictionary<string, object>();
            TabControlDictionary = new Dictionary<string, object>();
            ScrollBarDictionary = new Dictionary<string, object>();
            PanelDictionary = new Dictionary<string, object>();
            TrackBarDictionary = new Dictionary<string, object>();
            ContextMenuDictionary = new Dictionary<string, object>();
            ListBoxDictionary = new Dictionary<string, object>();
        }

        #endregion

        #endregion

        #region Reader

        /// <summary>
        /// Reads the theme file and put elements properties to dictionaries.
        /// </summary>
        /// <param name="path">The File path.</param>
        private void ControlProperties(string path)
        {
            // We clear every dictionary for avoid the "the key is already exist in dictionary" exception.

            Clear();

            // Here we refill the dictionaries with information we get in custom theme.

            FormDictionary = GetValues(path, "Form");

            ButtonDictionary = GetValues(path, "Button");

            DefaultButtonDictionary = GetValues(path, "DefaultButton");

            LabelDictionary = GetValues(path, "Label");

            LinkLabelDictionary = GetValues(path, "LinkLabel");

            BadgeDictionary = GetValues(path, "Badge");

            DividerDictionary = GetValues(path, "Divider");

            CheckBoxDictionary = GetValues(path, "CheckBox");

            RadioButtonDictionary = GetValues(path, "RadioButton");

            SwitchBoxDictionary = GetValues(path, "SwitchBox");

            ToolTipDictionary = GetValues(path, "ToolTip");

            TextBoxDictionary = GetValues(path, "TextBox");

            RichTextBoxDictionary = GetValues(path, "RichTextBox");

            ComboBoxDictionary = GetValues(path, "ComboBox");

            NumericDictionary = GetValues(path, "Numeric");

            EllipseDictionary = GetValues(path, "Ellipse");

            TileDictionary = GetValues(path, "Tile");

            ProgressDictionary = GetValues(path, "Progress");

            ControlBoxDictionary = GetValues(path, "ControlBox");

            TabControlDictionary = GetValues(path, "TabControl");

            ScrollBarDictionary = GetValues(path, "ScrollBar");

            PanelDictionary = GetValues(path, "Panel");

            TrackBarDictionary = GetValues(path, "TrackBar");

            ContextMenuDictionary = GetValues(path, "ContextMenu");

            ListBoxDictionary = GetValues(path, "ListBox");

            ThemeDetailsReader(path);

            UpdateForm();

        }


        /// <summary>
        /// The Method get the custom theme name and author.
        /// </summary>
        /// <param name="path">The Path of the custom theme file.</param>
        private void ThemeDetailsReader(string path)
        {
            foreach (var item in GetValues(path, "Theme"))
            {
                if (item.Key == "Name")
                    ThemeName = item.Value.ToString();
                else if (item.Key == "Author")
                {
                    ThemeAuthor = item.Value.ToString();
                }
            }
        }


        /// <summary>
        /// The Method to load the custom xml theme file and add a childnodes from a specific node into a dectionary. 
        /// </summary>
        /// <param name="path">The Path of custom theme file (XML file).</param>
        /// <param name="nodename">The Node name to get the childnodes from.</param>
        /// <returns>The Dictionary of childnodes names and values of a specific node.</returns>
        private Dictionary<string, object> GetValues(string path, string nodename)
        {
            try
            {
                var dict = new Dictionary<string, object>();
                var doc = new XmlDocument();
                if (File.Exists(path))
                    doc.Load(path);
                if (doc.DocumentElement == null) { return null; }
                var xmlNode = doc.SelectSingleNode($"/MetroSetTheme/{nodename}");
                if (xmlNode == null) return dict;
                foreach (XmlNode node in xmlNode.ChildNodes)
                    dict.Add(node.Name, node.InnerText);

                return dict;
            }
            catch
            {
                return null;
            }
        }


        #endregion Reader

        #region UITypeEditor

        /// <summary>
        /// Dialog Type For Opening the theme.
        /// </summary>
        public class FileNamesEditor : UITypeEditor
        {
            private OpenFileDialog _ofd;

            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                return UITypeEditorEditStyle.Modal;
            }

            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                if (context == null || provider == null) return base.EditValue(context, provider, value);
                var editorService =
                    (IWindowsFormsEditorService)
                    provider.GetService(typeof(IWindowsFormsEditorService));
                if (editorService == null) return base.EditValue(context, provider, value);
                _ofd = new OpenFileDialog
                {
                    Filter = "Xml File (*.xml)|*.xml",
                };
                return _ofd.ShowDialog() == DialogResult.OK ? _ofd.FileName : base.EditValue(context, provider, value);
            }
        }

        #endregion

    }
}