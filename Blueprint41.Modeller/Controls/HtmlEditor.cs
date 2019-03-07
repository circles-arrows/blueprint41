using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using mshtml;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;
using System.Threading;
using System.Net.Mail;
using System.Net.Mime;

namespace Blueprint41.Modeller
{
    public partial class HtmlEditor : UserControl
    {
        private IHTMLDocument2 doc;
        private bool updatingFontSize = false;
        private bool updatingFontColor = false;
        private bool setup = false;
        private bool init_timer = false;
        private Color DEFAULT_COLOR = Color.FromArgb(51, 51, 51);

        public delegate void TickDelegate();

        public class EnterKeyEventArgs : EventArgs
        {
            private bool _cancel = false;

            public bool Cancel
            {
                get { return _cancel; }
                set { _cancel = value; }
            }

        }

        public event TickDelegate Tick;
        
        public event WebBrowserNavigatedEventHandler Navigated;

        public event EventHandler<EnterKeyEventArgs> EnterKeyEvent;

        public HtmlEditor()
        {
            Load += new EventHandler(Editor_Load);
            InitializeComponent();
            SetupEvents();
            SetupTimer();
            SetupBrowser();
            SetupFontSizeComboBox();
            SetupFontColorComboBox();
        }
        
        private void Editor_Load(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void ParentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer.Stop();
            ParentForm.FormClosed -= new FormClosedEventHandler(ParentForm_FormClosed);
        }

        /// <summary>
        /// Setup navigation and focus event handlers.
        /// </summary>
        private void SetupEvents()
        {
            webBrowser1.Navigated += new WebBrowserNavigatedEventHandler(webBrowser1_Navigated);
            webBrowser1.GotFocus += new EventHandler(webBrowser1_GotFocus);
            if (webBrowser1.Version.Major >= 9)
                webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
        }

        public string GetHTMLAsText()
        {
            if (this.BodyText == null)
                return string.Empty;

            return webBrowser1.Document.Body.InnerHtml;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //webBrowser1.Document.Write(webBrowser1.DocumentText);
            //doc.designMode = "On";

            if(webBrowser1.Document.Body != null)
                webBrowser1.Document.Body.SetAttribute("contentEditable", "true");
        }

        /// <summary>
        /// When this control receives focus, it transfers focus to the 
        /// document body.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void webBrowser1_GotFocus(object sender, EventArgs e)
        {
            SuperFocus();
        }

        /// <summary>
        /// This is called when the initial html/body framework is set up, 
        /// or when document.DocumentText is set.  At this point, the 
        /// document is editable.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">navigation args</param>
        void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            SetBackgroundColor(BackColor);
            if (Navigated != null)
            {
                Navigated(this, e);
            }
        }

        /// <summary>
        /// Setup timer with 200ms interval
        /// </summary>
        private void SetupTimer()
        {
            timer.Interval = 200;
            timer.Tick += new EventHandler(timer_Tick);
        }

        /// <summary>
        /// Add document body, turn on design mode on the whole document, 
        /// and overred the context menu
        /// </summary>
        public void SetupBrowser()
        {
            webBrowser1.DocumentText = "<html><body></body></html>";

            doc = webBrowser1.Document.DomDocument as IHTMLDocument2;
            doc.designMode = "On";
            webBrowser1.Document.ContextMenuShowing += new HtmlElementEventHandler(Document_ContextMenuShowing);
        }

        /// <summary>
        /// Set the focus on the document body.  
        /// </summary>
        private void SuperFocus()
        {
            if (webBrowser1.Document != null &&
                webBrowser1.Document.Body != null)
                webBrowser1.Document.Body.Focus();
        }

        /// <summary>
        /// Get/Set the background color of the editor.
        /// Note that if this is called before the document is rendered and 
        /// complete, the navigated event handler will set the body's 
        /// background color based on the state of BackColor.
        /// </summary>
        [Browsable(true)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                if (ReadyState == ReadyState.Complete)
                {
                    SetBackgroundColor(value);
                }
            }
        }

        /// <summary>
        /// Set the background color of the body by setting it's CSS style
        /// </summary>
        /// <param name="value">the color to use for the background</param>
        private void SetBackgroundColor(Color value)
        {
            if (webBrowser1.Document != null &&
                webBrowser1.Document.Body != null)
                webBrowser1.Document.Body.Style =
                    string.Format("background-color: {0}", value.Name);
        }

        /// <summary>
        /// Clear the contents of the document, leaving the body intact.
        /// </summary>
        public void Clear()
        {
            if (webBrowser1.Document.Body != null)
                webBrowser1.Document.Body.InnerHtml = "";
        }

        /// <summary>
        /// Get the web browser component's document
        /// </summary>
        public HtmlDocument Document
        {
            get { return webBrowser1.Document; }
        }

        /// <summary>
        /// Document text should be used to load/save the entire document, 
        /// including html and body start/end tags.
        /// </summary>
        [Browsable(false)]
        public string DocumentText
        {
            get
            {
                string html = webBrowser1.DocumentText;
                if (html != null)
                {
                    html = ReplaceFileSystemImages(html);
                }
                return html;
            }
            set
            {
                webBrowser1.DocumentText = value;
            }
        }

        /// <summary>
        /// Get the html document title from document.
        /// </summary>
        [Browsable(false)]
        public string DocumentTitle
        {
            get
            {
                return webBrowser1.DocumentTitle;
            }
        }

        /// <summary>
        /// Get/Set the contents of the document Body, in html.
        /// </summary>
        [Browsable(false)]
        public string BodyHtml
        {
            get
            {
                if (webBrowser1.Document != null &&
                    webBrowser1.Document.Body != null)
                {
                    string html = webBrowser1.Document.Body.InnerHtml;
                    if (html != null)
                    {
                        html = ReplaceFileSystemImages(html);
                    }
                    return html;
                }
                else
                    return string.Empty;
            }
            set
            {
                if (webBrowser1.Document.Body != null)
                {
                    webBrowser1.DocumentText = "<html><body>" + value + "</body></html>";
                }
            }
        }

        public MailMessage ToMailMessage()
        {
            if (webBrowser1.Document != null &&
                webBrowser1.Document.Body != null)
            {
                string html = webBrowser1.Document.Body.InnerHtml;
                if (html != null)
                {
                    return LinkImages(html);
                }
                var msg = new MailMessage();
                msg.IsBodyHtml = true;
                return msg;
            }
            else
            {
                var msg = new MailMessage();
                msg.IsBodyHtml = true;
                msg.Body = string.Empty;
                return msg;
            }
        }

        private MailMessage LinkImages(string html)
        {
            var msg = new MailMessage();
            msg.IsBodyHtml = true;
            var matches = Regex.Matches(html, @"<img[^>]*?src\s*=\s*([""']?[^'"">]+?['""])[^>]*?>", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            var img_list = new List<LinkedResource>();
            var cid = 1;
            foreach (Match match in matches)
            {
                string src = match.Groups[1].Value;
                src = src.Trim('\"');
                if (File.Exists(src))
                {
                    var ext = Path.GetExtension(src);
                    if (ext.Length > 0)
                    {
                        ext = ext.Substring(1);
                        var res = new LinkedResource(src);
                        res.ContentId = string.Format("img{0}.{1}", cid++, ext);
                        res.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                        res.ContentType.MediaType = string.Format("image/{0}", ext);
                        res.ContentType.Name = res.ContentId;
                        img_list.Add(res);
                        src = string.Format("'cid:{0}'", res.ContentId);
                        html = html.Replace(match.Groups[1].Value, src);
                    }
                }
            }
            var view = AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html);
            foreach (var img in img_list)
            {
                view.LinkedResources.Add(img);
            }
            msg.AlternateViews.Add(view);
            return msg;
        }

        private string ReplaceFileSystemImages(string html)
        {
            var matches = Regex.Matches(html, @"<img[^>]*?src\s*=\s*([""']?[^'"">]+?['""])[^>]*?>", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            foreach (Match match in matches)
            {
                string src = match.Groups[1].Value;
                src = src.Trim('\"');
                if (File.Exists(src))
                {
                    var ext = Path.GetExtension(src);
                    if (ext.Length > 0)
                    {
                        ext = ext.Substring(1);
                        src = string.Format("'data:image/{0};base64,{1}'", ext, Convert.ToBase64String(File.ReadAllBytes(src)));
                        html = html.Replace(match.Groups[1].Value, src);
                    }
                }
            }
            return html;
        }

        /// <summary>
        /// Get/Set the documents body as text.
        /// </summary>
        [Browsable(false)]
        public string BodyText
        {
            get
            {
                if (webBrowser1.Document != null &&
                    webBrowser1.Document.Body != null)
                {
                    return webBrowser1.Document.Body.InnerText;
                }
                else
                    return string.Empty;
            }
            set
            {
                Document.OpenNew(false);
                if (webBrowser1.Document.Body != null)
                    webBrowser1.Document.Body.InnerText = HttpUtility.HtmlEncode(value);
            }
        }

        [Browsable(false)]
        public string Html
        {
            get
            {
                if (webBrowser1.Document != null &&
                    webBrowser1.Document.Body != null)
                {
                    return webBrowser1.Document.Body.InnerHtml;
                }
                else
                    return string.Empty;
            }
            set
            {
                Document.OpenNew(true);
                IHTMLDocument2 dom = Document.DomDocument as IHTMLDocument2;
                try
                {
                    if (value == null)
                        dom.clear();
                    else
                        dom.write(value);
                }
                finally
                {
                    dom.close();
                }
            }
        }

        /// <summary>
        /// Determine the status of the Undo command in the document editor.
        /// </summary>
        /// <returns>whether or not an undo operation is currently valid</returns>
        public bool CanUndo()
        {
            return doc.queryCommandEnabled("Undo");
        }

        /// <summary>
        /// Determine the status of the Redo command in the document editor.
        /// </summary>
        /// <returns>whether or not a redo operation is currently valid</returns>
        public bool CanRedo()
        {
            return doc.queryCommandEnabled("Redo");
        }

        /// <summary>
        /// Determine the status of the Cut command in the document editor.
        /// </summary>
        /// <returns>whether or not a cut operation is currently valid</returns>
        public bool CanCut()
        {
            return doc.queryCommandEnabled("Cut");
        }

        /// <summary>
        /// Determine the status of the Copy command in the document editor.
        /// </summary>
        /// <returns>whether or not a copy operation is currently valid</returns>
        public bool CanCopy()
        {
            return doc.queryCommandEnabled("Copy");
        }

        /// <summary>
        /// Determine the status of the Paste command in the document editor.
        /// </summary>
        /// <returns>whether or not a copy operation is currently valid</returns>
        public bool CanPaste()
        {
            return doc.queryCommandEnabled("Paste");
        }

        /// <summary>
        /// Determine the status of the Delete command in the document editor.
        /// </summary>
        /// <returns>whether or not a copy operation is currently valid</returns>
        public bool CanDelete()
        {
            return doc.queryCommandEnabled("Delete");
        }

        /// <summary>
        /// Determine whether the current block is left justified.
        /// </summary>
        /// <returns>true if left justified, otherwise false</returns>
        public bool IsJustifyLeft()
        {
            return doc.queryCommandState("JustifyLeft");
        }

        /// <summary>
        /// Determine whether the current block is right justified.
        /// </summary>
        /// <returns>true if right justified, otherwise false</returns>
        public bool IsJustifyRight()
        {
            return doc.queryCommandState("JustifyRight");
        }

        /// <summary>
        /// Determine whether the current block is center justified.
        /// </summary>
        /// <returns>true if center justified, false otherwise</returns>
        public bool IsJustifyCenter()
        {
            return doc.queryCommandState("JustifyCenter");
        }

        /// <summary>
        /// Determine whether the current block is full justified.
        /// </summary>
        /// <returns>true if full justified, false otherwise</returns>
        public bool IsJustifyFull()
        {
            return doc.queryCommandState("JustifyFull");
        }

        /// <summary>
        /// Determine whether the current selection is in Bold mode.
        /// </summary>
        /// <returns>whether or not the current selection is Bold</returns>
        public bool IsBold()
        {
            return doc.queryCommandState("Bold");
        }

        /// <summary>
        /// Determine whether the current selection is in Italic mode.
        /// </summary>
        /// <returns>whether or not the current selection is Italicized</returns>
        public bool IsItalic()
        {
            return doc.queryCommandState("Italic");
        }

        /// <summary>
        /// Determine whether the current selection is in Underline mode.
        /// </summary>
        /// <returns>whether or not the current selection is Underlined</returns>
        public bool IsUnderline()
        {
            return doc.queryCommandState("Underline");
        }

        /// <summary>
        /// Determine whether the current paragraph is an ordered list.
        /// </summary>
        /// <returns>true if current paragraph is ordered, false otherwise</returns>
        public bool IsOrderedList()
        {
            return doc.queryCommandState("InsertOrderedList");
        }

        /// <summary>
        /// Determine whether the current paragraph is an unordered list.
        /// </summary>
        /// <returns>true if current paragraph is ordered, false otherwise</returns>
        public bool IsUnorderedList()
        {
            return doc.queryCommandState("InsertUnorderedList");
        }

        /// <summary>
        /// Called when the editor context menu should be displayed.
        /// The return value of the event is set to false to disable the 
        /// default context menu.  A custom context menu (contextMenuStrip1) is 
        /// shown instead.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">HtmlElementEventArgs</param>
        private void Document_ContextMenuShowing(object sender, HtmlElementEventArgs e)
        {
            e.ReturnValue = false;
            cutToolStripMenuItem1.Enabled = CanCut();
            copyToolStripMenuItem2.Enabled = CanCopy();
            pasteToolStripMenuItem3.Enabled = CanPaste();
            deleteToolStripMenuItem.Enabled = CanDelete();
            cSSToolStripMenuItem.Enabled = SelectionType != SelectionType.None;
            contextMenuStrip1.Show(this, e.ClientMousePosition);
        }

        /// <summary>
        /// Populate the font size combobox.
        /// Add text changed and key press handlers to handle input and update 
        /// the editor selection font size.
        /// </summary>
        private void SetupFontSizeComboBox()
        {
            fontSizeComboBox.Items.Add("Regular");
            fontSizeComboBox.Items.Add("Medium");
            fontSizeComboBox.Items.Add("Large");
            fontSizeComboBox.TextChanged += new EventHandler(fontSizeComboBox_TextChanged);
            fontSizeComboBox.KeyPress += new KeyPressEventHandler(fontSizeComboBox_KeyPress);
        }

        /// <summary>
        /// Populate the font size combobox.
        /// Add text changed and key press handlers to handle input and update 
        /// the editor selection font size.
        /// </summary>
        private void SetupFontColorComboBox()
        {
            

            fontColorComboBox.Items.Add("Black");
            fontColorComboBox.Items.Add("Blue");
            fontColorComboBox.Items.Add("Dark Blue");
            fontColorComboBox.TextChanged += new EventHandler(fontColorComboBox_TextChanged);
        }

        /// <summary>
        /// Called when a key is pressed on the font size combo box.
        /// The font size in the boxy box is set to the key press value.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">KeyPressEventArgs</param>
        private void fontSizeComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar <= '7' && e.KeyChar > '0')
                    fontSizeComboBox.Text = e.KeyChar.ToString();
            }
            else if (!Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Set editor's current selection to the value of the font size combo box.
        /// Ignore if the timer is currently updating the font size to synchronize 
        /// the font size combo box with the editor's current selection.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void fontSizeComboBox_TextChanged(object sender, EventArgs e)
        {
            if (updatingFontSize) return;
            switch (fontSizeComboBox.Text.Trim())
            {
                case "Medium":
                    FontSize = FontSize.Medium;
                    break;
                case "Large":
                    FontSize = FontSize.Large;
                    break;
                default:
                    FontSize = FontSize.Regular;
                    break;
            }
        }
        
        private void fontColorComboBox_TextChanged(object sender, EventArgs e)
        {
            if (updatingFontColor) return;
            switch (fontColorComboBox.Text.Trim())
            {
                case "Blue":
                    EditorForeColor = FontColor.Blue;
                    break;
                case "Dark Blue":
                    EditorForeColor = FontColor.DarkBlue;
                    break;
                default:
                    EditorForeColor = FontColor.Black;
                    break;
            }
        }

        private void UpdateImageSizes()
        {
            foreach (HTMLImg image in doc.images)
            {
                if (image != null)
                {
                    if (image.height != image.style.pixelHeight && image.style.pixelHeight != 0)
                        image.height = image.style.pixelHeight;
                    if (image.width != image.style.pixelWidth && image.style.pixelWidth != 0)
                        image.width = image.style.pixelWidth;
                }
            }
        }

        private DateTime lastSplash = DateTime.Now;

        /// <summary>
        /// Called when the timer fires to synchronize the format buttons 
        /// with the text editor current selection.
        /// SetupKeyListener if necessary.
        /// Set bold, italic, underline and link buttons as based on editor state.
        /// Synchronize the font combo box and the font size combo box.
        /// Finally, fire the Tick event to allow external components to synchronize 
        /// their state with the editor.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void timer_Tick(object sender, EventArgs e)
        {
            if (!init_timer)
            {
                ParentForm.FormClosed += new FormClosedEventHandler(ParentForm_FormClosed);
                init_timer = true;
                lastSplash = DateTime.Now;
            }

            // don't process until browser is in ready state.
            if (ReadyState != ReadyState.Complete)
                return;

            SetupKeyListener();
            boldButton.Checked = IsBold();
            italicButton.Checked = IsItalic();
            underlineButton.Checked = IsUnderline();
            justifyLeftButton.Checked = IsJustifyLeft();
            justifyCenterButton.Checked = IsJustifyCenter();
            justifyRightButton.Checked = IsJustifyRight();
            justifyFullButton.Checked = IsJustifyFull();
            orderedListButton.Checked = IsOrderedList();
            unorderedListButton.Checked = IsUnorderedList();

            UpdateFontSizeComboBox();
            UpdateFontColorComboBox();
            UpdateImageSizes();

            if (Tick != null)
                Tick();
        }

        /// <summary>
        /// Update the font size combo box.
        /// Sets a flag to indicate that the combo box is updating, and should 
        /// not update the editor's selection.
        /// </summary>
        private void UpdateFontSizeComboBox()
        {
            if (!fontSizeComboBox.Focused)
            {
                string fontSize = "Regular";
                switch (FontSize)
                {
                    case FontSize.Medium:
                        fontSize = "Medium";
                        break;
                    case FontSize.Large:
                        fontSize = "Large";
                        break;
                    default:
                        break;
                }

                if (fontSize != fontSizeComboBox.Text)
                {
                    updatingFontSize = true;
                    fontSizeComboBox.Text = fontSize;
                    updatingFontSize = false;
                }
            }
        }

        private void UpdateFontColorComboBox()
        {
            if (!fontColorComboBox.Focused)
            {
                string fontColor = "Black";
                switch (EditorForeColor)
                {
                    case FontColor.Blue:
                        fontColor = "Blue";
                        break;
                    case FontColor.DarkBlue:
                        fontColor = "Dark Blue";
                        break;
                    default:
                        break;
                }

                if (fontColor != fontColorComboBox.Text)
                {
                    updatingFontColor = true;
                    fontColorComboBox.Text = fontColor;
                    updatingFontColor = false;
                }
            }
        }

        public Color BodyBackgroundColor
        {
            get
            {
                if (doc.body != null && doc.body.style != null && doc.body.style.backgroundColor != null)
                    return ConvertToColor(doc.body.style.backgroundColor.ToString());
                return Color.White;
            }
            set
            {
                if (ReadyState == ReadyState.Complete)
                {
                    if (doc.body != null && doc.body.style != null)
                    {
                        string colorstr =
                            string.Format("#{0:X2}{1:X2}{2:X2}", value.R, value.G, value.B);
                        doc.body.style.backgroundColor = colorstr;
                    }
                }
            }
        }

        /// <summary>
        /// Set up a key listener on the body once.
        /// The key listener checks for specific key strokes and takes 
        /// special action in certain cases.
        /// </summary>
        private void SetupKeyListener()
        {
            if (!setup)
            {
                webBrowser1.Document.Body.KeyDown += new HtmlElementEventHandler(Body_KeyDown);
                setup = true;
            }
        }

        /// <summary>
        /// If the user hits the enter key, and event will fire (EnterKeyEvent), 
        /// and the consumers of this event can cancel the projecessing of the 
        /// enter key by cancelling the event.
        /// This is useful if your application would like to take some action 
        /// when the enter key is pressed, such as a submission to a web service.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">HtmlElementEventArgs</param>
        private void Body_KeyDown(object sender, HtmlElementEventArgs e)
        {
            if (e.KeyPressedCode == 13 && !e.ShiftKeyPressed)
            {
                // handle enter code cancellation
                bool cancel = false;
                if (EnterKeyEvent != null)
                {
                    EnterKeyEventArgs args = new EnterKeyEventArgs();
                    EnterKeyEvent(this, args);
                    cancel = args.Cancel;
                }
                e.ReturnValue = !cancel;
            }
        }

        /// <summary>
        /// Embed a break at the current selection.
        /// This is a placeholder for future functionality.
        /// </summary>
        public void EmbedBr()
        {
            IHTMLTxtRange range =
                doc.selection.createRange() as IHTMLTxtRange;
            range.pasteHTML("<br/>");
            range.collapse(false);
            range.select();
        }

        /// <summary>
        /// Cut the current selection and place it in the clipboard.
        /// </summary>
        public void Cut()
        {
            webBrowser1.Document.ExecCommand("Cut", false, null);
        }

        /// <summary>
        /// Paste the contents of the clipboard into the current selection.
        /// </summary>
        public void Paste()
        {
            webBrowser1.Document.ExecCommand("Paste", false, null);
        }

        /// <summary>
        /// Copy the current selection into the clipboard.
        /// </summary>
        public void Copy()
        {
            webBrowser1.Document.ExecCommand("Copy", false, null);
        }

        /// <summary>
        /// Toggle the ordered list property for the current paragraph.
        /// </summary>
        public void OrderedList()
        {
            webBrowser1.Document.ExecCommand("InsertOrderedList", false, null);
        }

        /// <summary>
        /// Toggle the unordered list property for the current paragraph.
        /// </summary>
        public void UnorderedList()
        {
            webBrowser1.Document.ExecCommand("InsertUnorderedList", false, null);
        }

        /// <summary>
        /// Toggle the left justify property for the currnet block.
        /// </summary>
        public void JustifyLeft()
        {
            webBrowser1.Document.ExecCommand("JustifyLeft", false, null);
        }

        /// <summary>
        /// Toggle the right justify property for the current block.
        /// </summary>
        public void JustifyRight()
        {
            webBrowser1.Document.ExecCommand("JustifyRight", false, null);
        }

        /// <summary>
        /// Toggle the center justify property for the current block.
        /// </summary>
        public void JustifyCenter()
        {
            webBrowser1.Document.ExecCommand("JustifyCenter", false, null);
        }

        /// <summary>
        /// Toggle the full justify property for the current block.
        /// </summary>
        public void JustifyFull()
        {
            webBrowser1.Document.ExecCommand("JustifyFull", false, null);
        }

        /// <summary>
        /// Toggle bold formatting on the current selection.
        /// </summary>
        public void Bold()
        {
            webBrowser1.Document.ExecCommand("Bold", false, null);
        }

        /// <summary>
        /// Toggle italic formatting on the current selection.
        /// </summary>
        public void Italic()
        {
            webBrowser1.Document.ExecCommand("Italic", false, null);
        }

        /// <summary>
        /// Toggle underline formatting on the current selection.
        /// </summary>
        public void Underline()
        {
            webBrowser1.Document.ExecCommand("Underline", false, null);
        }

        /// <summary>
        /// Delete the current selection.
        /// </summary>
        public void Delete()
        {
            webBrowser1.Document.ExecCommand("Delete", false, null);
        }
        
        /// <summary>
        /// Get the ready state of the internal browser component.
        /// </summary>
        public ReadyState ReadyState
        {
            get
            {
                switch (doc.readyState.ToLower())
                {
                    case "uninitialized":
                        return ReadyState.Uninitialized;
                    case "loading":
                        return ReadyState.Loading;
                    case "loaded":
                        return ReadyState.Loaded;
                    case "interactive":
                        return ReadyState.Interactive;
                    case "complete":
                        return ReadyState.Complete;
                    default:
                        return ReadyState.Uninitialized;
                }
            }
        }

        /// <summary>
        /// Get the current selection type.
        /// </summary>
        public SelectionType SelectionType
        {
            get
            {
                var type = doc.selection.type.ToLower();
                switch (type)
                {
                    case "text":
                        return SelectionType.Text;
                    case "control":
                        return SelectionType.Control;
                    case "none":
                        return SelectionType.None;
                    default:
                        return SelectionType.None;
                }
            }
        }

        /// <summary>
        /// Get/Set the current font size.
        /// </summary>
        [Browsable(false)]
        public FontSize FontSize
        {
            get
            {
                if (ReadyState != ReadyState.Complete)
                    return FontSize.Regular;

                switch (doc.queryCommandValue("FontSize").ToString())
                {
                    case "3":
                        return FontSize.Regular;
                    case "4":
                        return FontSize.Medium;
                    case "5":
                        return FontSize.Large;
                    default:
                        return FontSize.Regular;
                }
            }
            set
            {
                string sz = "3";
                switch (value)
                {
                    case FontSize.Regular:
                        sz = "3";
                        break;
                    case FontSize.Medium:
                        sz = "4";
                        break;
                    case FontSize.Large:
                        sz = "5";
                        break;
                    default:
                        break;
                }
                webBrowser1.Document.ExecCommand("FontSize", false, sz);
            }
        }
        
        /// <summary>
        /// Get/Set the editor's foreground (text) color for the current selection.
        /// </summary>
        [Browsable(false)]
        public FontColor EditorForeColor
        {
            get
            {
                if (ReadyState != ReadyState.Complete)
                    return FontColor.Black;

                string clr = doc.queryCommandValue("ForeColor").ToString();

                if (string.IsNullOrEmpty(clr))
                    return FontColor.Black;

                switch (GetColorString(doc.queryCommandValue("ForeColor").ToString()))
                {
                    case "#333333":
                        return FontColor.Black;
                    case "#0000FF":
                        return FontColor.Blue;
                    case "#00008B":
                        return FontColor.DarkBlue;
                    default:
                        return FontColor.Black;
                }
            }
            set
            {
                Color clr = DEFAULT_COLOR;
                switch (value)
                {
                    case FontColor.Blue:
                        clr = Color.Blue;
                        break;
                    case FontColor.DarkBlue:
                        clr = Color.DarkBlue;
                        break;
                    default:
                        break;
                }
                string colorstr = 
                    string.Format("#{0:X2}{1:X2}{2:X2}", clr.R, clr.G, clr.B);
                webBrowser1.Document.ExecCommand("ForeColor", false, colorstr);
            }
        }

        /// <summary>
        /// Get/Set the editor's background color for the current selection.
        /// </summary>
        [Browsable(false)]
        public Color EditorBackColor
        {
            get
            {
                if (ReadyState != ReadyState.Complete)
                    return Color.White;
                return ConvertToColor(doc.queryCommandValue("BackColor").ToString());
            }
            set
            {
                string colorstr =
                    string.Format("#{0:X2}{1:X2}{2:X2}", value.R, value.G, value.B);
                webBrowser1.Document.ExecCommand("BackColor", false, colorstr);
            }
        }

        public void SelectBodyColor()
        {
            Color color = BodyBackgroundColor;
            if (ShowColorDialog(ref color))
                BodyBackgroundColor = color;
        }
        
        /// <summary>
        /// Convert the custom integer (B G R) format to a color object.
        /// </summary>
        /// <param name="clrs">the custorm color as a string</param>
        /// <returns>the color</returns>
        private static Color ConvertToColor(string clrs)
        {
            int red, green, blue;
            // sometimes clrs is HEX organized as (RED)(GREEN)(BLUE)
            if (clrs.StartsWith("#"))
            {
                int clrn = Convert.ToInt32(clrs.Substring(1), 16);
                red = (clrn >> 16) & 255;
                green = (clrn >> 8) & 255;
                blue = clrn & 255;
            }
            else // otherwise clrs is DECIMAL organized as (BlUE)(GREEN)(RED)
            {
                int clrn = Convert.ToInt32(clrs);
                red = clrn & 255;
                green = (clrn >> 8) & 255;
                blue = (clrn >> 16) & 255;
            }
            Color incolor = Color.FromArgb(red, green, blue);
            return incolor;
        }

        private static string GetColorString(string clrs)
        {
            Color clr = ConvertToColor(clrs);
            string clrStr = string.Format("#{0:X2}{1:X2}{2:X2}", clr.R, clr.G, clr.B);
            return clrStr;
        }

        /// <summary>
        /// Called when the bold button on the tool strip is pressed.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void boldButton_Click(object sender, EventArgs e)
        {
            Bold();
        }

        /// <summary>
        /// Called when the italic button on the tool strip is pressed.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void italicButton_Click(object sender, EventArgs e)
        {
            Italic();
        }

        /// <summary>
        /// Called when the underline button on the tool strip is pressed.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void underlineButton_Click(object sender, EventArgs e)
        {
            Underline();
        }

        /// <summary>
        /// Show the interactive Color dialog.
        /// </summary>
        /// <param name="color">the input and output color</param>
        /// <returns>true if dialog accepted, false if dialog cancelled</returns>
        private bool ShowColorDialog(ref Color color)
        {
            bool selected;
            using (ColorDialog dlg = new ColorDialog())
            {
                dlg.SolidColorOnly = true;
                dlg.AllowFullOpen = false;
                dlg.AnyColor = false;
                dlg.FullOpen = false;
                dlg.CustomColors = new int[] { ColorTranslator.ToOle(Color.FromArgb(51, 51, 51)), ColorTranslator.ToOle(Color.Blue), ColorTranslator.ToOle(Color.DarkBlue) };
                dlg.Color = color;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    selected = true;
                    color = dlg.Color;
                }
                else
                {
                    selected = false;
                }
            }
            return selected;
        }

        /// <summary>
        /// Called when the cut button is clicked on the editor context menu.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Cut();
        }

        /// <summary>
        /// Called when the copy button is clicked on the editor context menu.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void copyToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Copy();
        }

        /// <summary>
        /// Called when the paste button is clicked on the editor context menu.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void pasteToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Paste();
        }

        /// <summary>
        /// Called when the delete button is clicked on the editor context menu.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        /// <summary>
        /// Event handler for the left justify toolbar button.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void justifyLeftButton_Click(object sender, EventArgs e)
        {
            JustifyLeft();
        }

        /// <summary>
        /// Event handler for the center justify toolbar button.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void justifyCenterButton_Click(object sender, EventArgs e)
        {
            JustifyCenter();
        }

        /// <summary>
        /// Event handler for the right justify toolbar button.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void justifyRightButton_Click(object sender, EventArgs e)
        {
            JustifyRight();
        }

        /// <summary>
        /// Event handler for the full justify toolbar button.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void justifyFullButton_Click(object sender, EventArgs e)
        {
            JustifyFull();
        }

        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectBodyColor();
        }

        /// <summary>
        /// Event handler for the ordered list toolbar button
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void orderedListButton_Click(object sender, EventArgs e)
        {
            OrderedList();
        }

        /// <summary>
        /// Event handler for the unordered list toolbar button
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">EventArgs</param>
        private void unorderedListButton_Click(object sender, EventArgs e)
        {
            UnorderedList();
        }
    }

    /// <summary>
    /// Enumeration of possible font sizes for the Editor component
    /// </summary>
    public enum FontSize
    {
        Regular,
        Medium,
        Large
    }

    /// <summary>
    /// Enumeration of possible font colors for the Editor component
    /// </summary>
    public enum FontColor
    {
        Black,
        Blue,
        DarkBlue
    }

    public enum SelectionType
    {
        Text,
        Control,
        None
    }

    public enum ReadyState
    {
        Uninitialized,
        Loading,
        Loaded,
        Interactive,
        Complete
    }

}