using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace MetroSet_Theme_Maker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("uxtheme", CharSet = CharSet.Unicode)]
        public static extern Int32 SetWindowTheme
        (IntPtr hWnd, String textSubAppName, String textSubIdList);

        private void Form1_Load(object sender, EventArgs e)
        {
            SetWindowTheme(listView1.Handle, "explorer", null);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "XML File (*.xml)|*.xml" })
            {
                if (openFileDialog.ShowDialog() != DialogResult.OK) { return; }
                listView1.Items.Clear();
                XmlDocument doc = new XmlDocument();
                doc.Load(openFileDialog.FileName);
                foreach (XmlNode node in doc.DocumentElement)
                {
                    string name = node.Name;
                    ListViewGroup lgGroup = new ListViewGroup(name, name);
                    listView1.Groups.Add(lgGroup);

                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        ListViewItem l = new ListViewItem
                        {
                            Text = childNode.Name,
                            Group = lgGroup,
                            Font = new Font("Segoe UI", 10)
                        };
                        l.SubItems.Add(childNode.InnerText);

                        if (listView1.Items.Count % 2 != 0)
                        {
                            l.BackColor = Color.GhostWhite;
                            l.ForeColor = Color.DimGray;
                        }
                        else
                        {
                            l.BackColor = Color.White;
                        }
                        listView1.Items.Add(l);
                    }
                }
            }
        }

        private string itemname = default(string);
        private bool isColor = default(bool);
        private bool isFont = default(bool);

        private void listView1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0) { return; }
            ListViewItem lvItem = listView1.SelectedItems[0];
            txtValue.Text = lvItem.SubItems[1].Text;
            itemname = lvItem.Text;
            if (lvItem.Text.Contains("Color"))
            {
                isColor = true;
            }
            else if (lvItem.Text == "Font")
            {
                isFont = true;
            }
            else
            {
                isColor = false;
                isFont = false;
            }
            btnColor.Enabled = isColor;
            btnFont.Enabled = isFont;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem l in listView1.Items)
            {
                if (l.Text == itemname)
                {
                    l.SubItems[1].Text = txtValue.Text;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog svDialog = new SaveFileDialog() { Filter = "XML File (*.xml)|*.xml" })
            {
                if (svDialog.ShowDialog() == DialogResult.OK)
                {
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    settings.OmitXmlDeclaration = true;
                    using (XmlWriter writer = XmlWriter.Create(svDialog.FileName, settings))
                    {
                        writer.WriteStartElement("MetroSetTheme");

                        foreach (ListViewGroup lvGroup in listView1.Groups)
                        {
                            writer.WriteStartElement(lvGroup.Name);

                            foreach (ListViewItem item in listView1.Items)
                            {
                                if (item.Group.Name == lvGroup.Name)
                                {
                                    writer.WriteStartElement(item.Text);
                                    writer.WriteString(item.SubItems[1].Text);
                                    writer.WriteEndElement();
                                }
                            }

                            writer.WriteEndElement();
                        }

                        writer.WriteEndElement();

                        MessageBox.Show($"Theme Successfully Saved : {svDialog.FileName}");

                    }
                }
            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog() { AllowFullOpen = true, FullOpen = true, AnyColor = true })
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    Color C = colorDialog.Color;
                    txtValue.Text = $"{C.R}, {C.G}, {C.B}";
                }
            }
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            using (FontDialog fontDialog = new FontDialog() { FontMustExist = true })
            {
                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    using (Font f = fontDialog.Font)
                    {
                        txtValue.Text = f.Name;
                    }
                }
            }
        }
    }
}