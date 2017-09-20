using MetroSet_UI.Design;
using MetroSet_UI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml;

namespace MetroSet_UI
{
    [DefaultProperty("Style")]
    [Designer(typeof(StyleManagerDesigner))]
    public class StyleManager : Component
    {

        #region Constructor

        public StyleManager(Form ownerForm)
        {
            MetroForm = ownerForm;
        }

        public StyleManager()
        {
            style = Style.Light;
            if (_CustomTheme == null)
            {
                var themeFile = Properties.Settings.Default.ThemeFile;
                _CustomTheme = File.Exists(themeFile) ? themeFile : ThemeFilePath(themeFile);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The Method to update the form with the style manager style and it's controls.
        /// </summary>
        public void UpdateForm()
        {
            if (MetroForm == null)
                return;

            var form = MetroForm as iForm;
            if (form != null && CustomTheme != null)
            {
                form.Style = Style;
                form.ThemeAuthor = ThemeAuthor;
                form.ThemeName = ThemeName;
                form.StyleManager = this;
            }

            if (MetroForm.Controls.Count > 0)
                UpdateControls(MetroForm.Controls);

            MetroForm.Refresh();
        }

        /// <summary>
        /// The Method to update controls with the style manager style.
        /// </summary>
        private void UpdateControls(Control.ControlCollection controls)
        {
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
            }
        }

        /// <summary>
        /// The Method to apply the style manager style to the added controls.
        /// </summary>
        private void ControlAdded(object sender, ControlEventArgs e)
        {
            var control = e.Control as iControl;
            if (control != null && CustomTheme != null)
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

        private Style style;
        private Form _MetroForm;
        private string _CustomTheme;

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
            get { return _MetroForm; }
            set
            {
                if (_MetroForm == null)
                {
                    _MetroForm = value;
                    _MetroForm.ControlAdded += ControlAdded;
                    UpdateForm();
                }
            }
        }


        /// <summary>
        /// Gets or sets the style for the button.
        /// </summary>
        [Category("MetroSet Framework"), Description("Gets or sets the style.")]
        public Style Style
        {
            get { return style; }
            set
            {
                style = value;
                UpdateForm();
            }
        }


        /// <summary>
        /// Gets or sets the custom theme file controls.
        /// </summary>
        [Editor(typeof(FileNamesEditor), typeof(UITypeEditor)), Category("MetroSet Framework"), Description("Gets or sets the custom theme file.")]
        public string CustomTheme
        {
            get { return _CustomTheme; }
            set
            {
                if (Style == Style.Custom)
                {
                    Properties.Settings.Default.ThemeFile = value;
                    Properties.Settings.Default.Save();
                    ControlProperties(value);
                }
                else
                {
                    Style = Style.Custom;
                    Properties.Settings.Default.ThemeFile = value;
                    Properties.Settings.Default.Save();
                    ControlProperties(value);
                }
                _CustomTheme = value;
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
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Xml File (*.xml)|*.xml" })
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
        public string ThemeFilePath(string str)
        {
            string content = str;
            string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.Templates) + "\\ThemeFile.xml"}";
            File.WriteAllText(path, content);
            return path;
        }

        #endregion Open Theme

        #region Dictionaries

        /// <summary>
        /// The Button properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> ButtonDictionary = new Dictionary<string, object>();

        /// <summary>
        /// The Label properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> LabelDictionary = new Dictionary<string, object>();

        /// <summary>
        /// The TextBox properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> TextBoxDictionary = new Dictionary<string, object>();

        /// <summary>
        /// The Form properties from custom theme will be stored into this dictionary.
        /// </summary>
        public Dictionary<string, object> FormDictionary = new Dictionary<string, object>();

        #endregion

        #region Reader
        
        /// <summary>
        /// Reads the theme file and put elements properties to dictionaries.
        /// </summary>
        /// <param name="path">The File path.</param>
        public void ControlProperties(string path)
        {
            // We clear every dictionary for avoid the "the key is already exist in dictionary" exception.
            ButtonDictionary.Clear();
            FormDictionary.Clear();
            LabelDictionary.Clear();
            TextBoxDictionary.Clear();

            // Here we will load the theme contents that we get in Custom Theme Dialog property
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            if (doc.DocumentElement != null)
                foreach (XmlNode node in doc.DocumentElement)
                {
                    string nodeName = node.Name;
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        switch (nodeName)
                        {
                            case "Button":
                                ButtonDictionary.Add(childNode.Name, childNode.InnerText);
                                break;

                            case "Label":
                                LabelDictionary.Add(childNode.Name, childNode.InnerText);
                                break;

                            case "TextBox":
                                TextBoxDictionary.Add(childNode.Name, childNode.InnerText);
                                break;

                            case "Form":
                                FormDictionary.Add(childNode.Name, childNode.InnerText);
                                break;

                            case "Theme":
                                if (childNode.Name == "Name")
                                {
                                    ThemeName = childNode.InnerText;
                                }
                                else if (childNode.Name == "Author")
                                {
                                    ThemeAuthor = childNode.InnerText;
                                }
                                break;
                        }
                    }
                    UpdateForm();
                }
        }

        #endregion Reader

        #region UITypeEditor

        /// <summary>
        /// Dialog Type For Opening the theme.
        /// </summary>
        public class FileNamesEditor : UITypeEditor
        {
            private OpenFileDialog ofd;

            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                return UITypeEditorEditStyle.Modal;
            }

            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                if ((context == null) || (provider == null)) return base.EditValue(context, provider, value);
                IWindowsFormsEditorService editorService =
                    (IWindowsFormsEditorService)
                    provider.GetService(typeof(IWindowsFormsEditorService));
                if (editorService != null)
                {
                    ofd = new OpenFileDialog
                    {
                        Filter = "Xml File (*.xml)|*.xml",
                        FileName = ""
                    };
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        return ofd.FileName;
                    }
                }
                return base.EditValue(context, provider, value);
            }
        }

#endregion
    }
}