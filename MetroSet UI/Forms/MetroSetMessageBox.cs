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

using MetroSet_UI.Controls;
using MetroSet_UI.Design;
using MetroSet_UI.Extensions;
using MetroSet_UI.Property;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MetroSet_UI.Forms
{
    public class MetroSetMessageBox : MetroSetForm
    {        

        #region Internal vars

        private Size buttonSize;
        private MetroSetButton okButton;
        private MetroSetButton yesButton;
        private MetroSetButton noButton;
        private MetroSetButton cancelButton;
        private MetroSetButton retryButton;
        private MetroSetButton abortButton;
        private MetroSetButton ignoreButton;

        #endregion

        #region Properties

        /// <summary>
        /// Get or sets the parent form.
        /// </summary>
        private Form OwnerForm { get; set; }

        /// <summary>
        /// Gets or sets the content of the message.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the title of the content
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Gets or sets the MessageBoxButtons.
        /// </summary> 
        public MessageBoxButtons Buttons { get; set; }

        /// <summary>
        /// Gets or sets the MessageBoxIcon.
        /// </summary>
        public new MessageBoxIcon Icon { get; set; }

        /// <summary>
        /// Gets or sets the BackgroundColor
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private static Color BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the BorderColor
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private static Color BorderColor { get; set; }

        /// <summary>
        /// Gets or sets the ForegroundColor
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private static Color ForegroundColor { get; set; }


        #endregion

        #region Constructor

        /// <summary>
        /// The Constructor.
        /// </summary>
        public MetroSetMessageBox() : base()
        {
            Font = MetroSetFonts.Regular(9.5f);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            buttonSize = new Size(95, 32);
            ApplyTheme();
            EvaluateControls();
            AddControls();
            //HideControls();
        }

        /// <summary>
        /// Here we set the buttons properties value.
        /// </summary>
        private void EvaluateControls()
        {
            EvaluateOkeyButton();

            EvaluateYesButton();

            EvaluateNoButton();

            EvaluateCancelButton();

            EvaluateRetryButton();

            EvaluateAbortButton();

            EvaluateIgnoreButton();
        }

        /// <summary>
        /// Adding the controls just to be exist in form but we don't need them all at the moment.
        /// </summary>
        private void AddControls()
        {
            Controls.Add(okButton);
            Controls.Add(yesButton);
            Controls.Add(noButton);
            Controls.Add(cancelButton);
            Controls.Add(retryButton);
            Controls.Add(abortButton);
            Controls.Add(ignoreButton);
        }

        /// <summary>
        /// Set the required properties values and click event of retry button.
        /// </summary>
        private void EvaluateRetryButton()
        {
            retryButton = new MetroSetButton
            {
                Text = "Retry",
                Size = buttonSize,
                Visible = false
            };
            retryButton.Click += RetryButton_Click;
        }

        /// <summary>
        /// Set the required properties values and click event of cancel button.
        /// </summary>
        private void EvaluateCancelButton()
        {
            cancelButton = new MetroSetButton
            {
                Text = "Cancel",
                Size = buttonSize,
                Visible = false
            };
            cancelButton.Click += CancelButton_Click;
        }

        /// <summary>
        /// Set the required properties values and click event of no button.
        /// </summary>
        private void EvaluateNoButton()
        {
            noButton = new MetroSetButton
            {
                Text = "No",
                Size = buttonSize,
                Visible = false
            };
            noButton.Click += NoButton_Click;
        }

        /// <summary>
        /// Set the required properties values and click event of yes button.
        /// </summary>
        private void EvaluateYesButton()
        {
            yesButton = new MetroSetButton
            {
                Text = "Yes",
                Size = buttonSize,
                Visible = false
            };
            yesButton.Click += YesButton_Click;
        }

        /// <summary>
        /// Set the required properties values and click event of ok button.
        /// </summary>
        private void EvaluateOkeyButton()
        {
            okButton = new MetroSetButton
            {
                Text = "Ok",
                Size = buttonSize,
                Visible = false
            };
            okButton.Click += OkButton_Click;
        }

        /// <summary>
        /// Set the required properties values and click event of abort button.
        /// </summary>
        private void EvaluateAbortButton()
        {
            abortButton = new MetroSetButton
            {
                Text = "Abort",
                Size = buttonSize,
                Visible = false

            };
            abortButton.Click += AbortButton_Click;
        }

        /// <summary>
        /// Set the required properties values and click event of ignore button.
        /// </summary>
        private void EvaluateIgnoreButton()
        {
            ignoreButton = new MetroSetButton
            {
                Text = "Ignore",
                Size = buttonSize,
                Visible = false
            };
            ignoreButton.Click += IgnoreButton_Click;
        }

        #endregion

        #region Events

        /// <summary>
        /// Handling the retry button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void RetryButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Retry;
        }

        /// <summary>
        /// Handling the cancel button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Handling the no button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void NoButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        /// <summary>
        /// Handling the yes button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void YesButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        /// <summary>
        /// Handling the okey button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Handling the abort button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void AbortButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
        }

        /// <summary>
        /// Handling the ignore button click.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void IgnoreButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Ignore;
        }

        /// <summary>
        /// When the user just provides the content of message to appear.
        /// </summary>
        /// <param name="form">The Form that messagebox will be showed from.</param>
        /// <param name="content">The Content of the message.</param>
        /// <returns>The MessageBox with just the content and an ok button.</returns>
        public static DialogResult Show(MetroSetForm form, string content)
        {
            return Show(form, content, form.Text, MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        /// <summary>
        ///  When the user provides the content of message and the message title to appear.
        /// </summary>
        /// <param name="form">The Form that messagebox will be showed from.</param>
        /// <param name="content">The Content of the message.</param>
        /// <param name="caption">The MesageBox title.</param>
        /// <returns>The MessageBox with the content and title and an ok button.</returns>
        public static DialogResult Show(MetroSetForm form, string content, string caption)
        {
            return Show(form, content, caption, MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        /// <summary>
        /// When the user provides the content of message and the message title and also which type of buttons to appear.
        /// </summary>
        /// <param name="form">The Form that messagebox will be showed from.</param>
        /// <param name="content">The Content of the message.</param>
        /// <param name="caption">The MesageBox title.</param>
        /// <param name="buttons">The Type of buttons to appear.</param>
        /// <returns>The MessageBox with the content and title and provided button(s) type.</returns>
        public static DialogResult Show(MetroSetForm form, string content, string caption, MessageBoxButtons buttons)
        {
            return Show(form, content, caption, buttons, MessageBoxIcon.None);
        }

        /// <summary>
        /// When the user provides the content of message and the message title and also which type message and buttons to appear.
        /// </summary>
        /// <param name="form">The Form that messagebox will be showed from.</param>
        /// <param name="content">The Content of the message.</param>
        /// <param name="caption">The MesageBox title.</param>
        /// <param name="buttons">The Type of buttons to appear.</param>
        /// <param name="icon">The MessageBox type.</param>
        /// <returns>The MessageBox with the content and title and provided button(s) and type.</returns>
        public static DialogResult Show(MetroSetForm form, string content, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            MetroSetMessageBox msgBox = new MetroSetMessageBox
            {
                OwnerForm = form ?? throw new ArgumentNullException("MetroSetMessageBox requires a form, use 'this' as the first parameter in the place you use MetroSetMessageBox."),
                Content = content,
                Caption = caption,
                Buttons = buttons,
                Size = new Size(form.Width - 2, (form.Height / 3) - 1),
                Location = new Point(form.Location.X, (form.Height / 2) - 1)
            };

            if (icon == MessageBoxIcon.Error || icon == MessageBoxIcon.Stop)
            {
                BackgroundColor = Color.FromArgb(210, 50, 45);
                BorderColor = Color.FromArgb(210, 50, 45);
                ForegroundColor = Color.Black;
            }

            else if (icon == MessageBoxIcon.Information) 
            {
                BackgroundColor = Color.FromArgb(60, 180, 218);
                BorderColor = Color.FromArgb(60, 180, 218);
                ForegroundColor = Color.White;
            }

            else if (icon == MessageBoxIcon.Question)
            {
                BackgroundColor = Color.FromArgb(70, 165, 70);
                BorderColor = Color.FromArgb(70, 165, 70);
                ForegroundColor = Color.Black;
            }

            else if (icon == MessageBoxIcon.Exclamation || icon == MessageBoxIcon.Warning)
            {
                BackgroundColor = Color.FromArgb(237, 156, 40);
                BorderColor = Color.FromArgb(237, 156, 40);
                ForegroundColor = Color.Black;
            }

            else if (icon == MessageBoxIcon.None || icon == MessageBoxIcon.Asterisk || icon == MessageBoxIcon.Hand)
            {
                BackgroundColor = Color.White;
                BorderColor = Color.FromArgb(65, 177, 225);
                ForegroundColor = Color.Black;
            }
            
            return msgBox.ShowDialog();
        }

        /// <summary>
        /// Here we handle the user provided buttons appearance.
        /// </summary>
        /// <returns>The MessageBox with provided buttons.</returns>
        protected new DialogResult ShowDialog()
        {

            int buttonHeight = Height - 45;
            int firstButton = (Width - buttonSize.Width) - 10;
            int secondButoon = (Width - (buttonSize.Width * 2)) - 20;
            switch (Buttons)
            {
                case MessageBoxButtons.OK:
                    okButton.Location = new Point(firstButton, buttonHeight);
                    okButton.Visible = true;
                    break;

                case MessageBoxButtons.OKCancel:
                    okButton.Location = new Point(secondButoon, buttonHeight);
                    okButton.Visible = true;
                    cancelButton.Location = new Point(firstButton, buttonHeight);
                    cancelButton.Visible = true;
                    break;

                case MessageBoxButtons.YesNo:
                    yesButton.Location = new Point(secondButoon, buttonHeight);
                    yesButton.Visible = true;
                    noButton.Location = new Point(firstButton, buttonHeight);
                    noButton.Visible = true;
                    break;

                case MessageBoxButtons.YesNoCancel:
                    yesButton.Location = new Point((Width - (buttonSize.Width * 3)) - 30, buttonHeight);
                    yesButton.Visible = true;
                    noButton.Location = new Point(secondButoon, buttonHeight);
                    noButton.Visible = true;
                    cancelButton.Location = new Point(firstButton, buttonHeight);
                    cancelButton.Visible = true;
                    break;

                case MessageBoxButtons.RetryCancel:
                    retryButton.Location = new Point(secondButoon, buttonHeight);
                    retryButton.Visible = true;
                    cancelButton.Location = new Point(firstButton, buttonHeight);
                    cancelButton.Visible = true;
                    break;

                case MessageBoxButtons.AbortRetryIgnore:
                    abortButton.Location = new Point((Width - (buttonSize.Width * 3)) - 30, buttonHeight);
                    abortButton.Visible = true;
                    retryButton.Location = new Point(secondButoon, buttonHeight);
                    retryButton.Visible = true;
                    ignoreButton.Location = new Point(firstButton, buttonHeight);
                    ignoreButton.Visible = true;
                    break;

                default:
                    okButton.Location = new Point(firstButton, buttonHeight);
                    okButton.Visible = true;
                    break;
            }
            return base.ShowDialog();
        }

        #endregion

        #region Draw Dialog

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle rect = new Rectangle(0, ((OwnerForm.Height - (OwnerForm.Height / 2)) / 250), OwnerForm.Width - 3, (OwnerForm.Height / 3) - 2);

            using (SolidBrush BG = new SolidBrush(BackgroundColor))
            {
                using (SolidBrush CTNT = new SolidBrush(ForegroundColor))
                {
                    using (Pen P = new Pen(BorderColor))
                    {
                        G.FillRectangle(BG, rect);
                        G.DrawString(Caption, Font, CTNT, new PointF(rect.X + 10, rect.Y + 10));
                        G.DrawString(Content, Font, CTNT, new PointF(rect.X + 10, rect.Y + 40));
                        G.DrawRectangle(P, rect);
                    }
                }
            }
        }

        #endregion

    }
}
