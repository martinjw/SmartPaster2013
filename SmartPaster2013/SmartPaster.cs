using System;
using System.Windows.Forms; //clipboard
using EnvDTE;
using EnvDTE80;

namespace SmartPaster2013
{
    /// <summary>
    /// Class responsible for doing the pasting/manipulating of clipdata.
    /// </summary>
    internal sealed class SmartPaster
    {
        /// <summary>
        ///  Convient property to retrieve the clipboard text from the clipboard
        /// </summary>
        private static string ClipboardText
        {
            get
            {
                IDataObject iData = Clipboard.GetDataObject();
                if (iData == null) return string.Empty;
                //is it Unicode? Then we use that
                if (iData.GetDataPresent(DataFormats.UnicodeText))
                    return Convert.ToString(iData.GetData(DataFormats.UnicodeText));
                //otherwise ANSI
                if (iData.GetDataPresent(DataFormats.Text))
                    return Convert.ToString(iData.GetData(DataFormats.Text));
                return string.Empty;
            }
        }


        /// <summary>
        /// Inserts text at current cursor location in the application
        /// </summary>
        /// <param name="application">application with activewindow</param>
        /// <param name="text">text to insert</param>
        private static void Paste(DTE2 application, string text)
        {
            //get the text document
            var txt = (TextDocument)application.ActiveDocument.Object("TextDocument");

            //get an edit point
            EditPoint ep = txt.Selection.ActivePoint.CreateEditPoint();

            //get a start point
            EditPoint sp = txt.Selection.ActivePoint.CreateEditPoint();

            //open the undo context
            bool isOpen = application.UndoContext.IsOpen;
            if (!isOpen)
                application.UndoContext.Open("SmartPaster");

            //clear the selection
            if (!txt.Selection.IsEmpty)
                txt.Selection.Delete();

            //insert the text
            //ep.Insert(Indent(text, ep.LineCharOffset))
            ep.Insert(text);

            //smart format
            sp.SmartFormat(ep);

            //close the context
            if (!isOpen)
                application.UndoContext.Close();
        }

        private static bool IsVb(DTE2 application)
        {
            return application.ActiveWindow.Caption.EndsWith(".vb", StringComparison.OrdinalIgnoreCase);
        }
        private static bool IsCs(DTE2 application)
        {
            return application.ActiveWindow.Caption.EndsWith(".cs", StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsCxx(DTE2 application)
        {
            return application.ActiveDocument.Language == "C/C++";
        }
        #region "Paste As ..."

        /// <summary>
        /// Public method to paste and format clipboard text as string the cursor
        /// location for the configured or active window's langage .
        /// </summary>
        /// <param name="application">application to insert</param>
        public void PasteAsString(DTE2 application)
        {
            string text;
            if (IsVb(application))
                text = SmartFormatter.LiterallyInVb(ClipboardText);
            else if (IsCs(application))
                text = SmartFormatter.LiterallyInCs(ClipboardText);
            else if (IsCxx(application))
                text = SmartFormatter.LiterallyInCxx(ClipboardText);
            else
                text = ClipboardText;
            Paste(application, text);
        }

        /// <summary>
        /// Pastes as verbatim string.
        /// </summary>
        /// <param name="application">The application.</param>
        public void PasteAsVerbatimString(DTE2 application)
        {
            if (IsVb(application))
            {
                //vb14 has verbatim strings, otherwise do the CData trick
                int version;
                var appVersion = application.Version;
                var p = appVersion.IndexOf('.'); //12.0 in VS2013, but MSDN says dp is optional
                if (p > 0) appVersion = appVersion.Substring(0, p);

                int.TryParse(appVersion, out version);

                Paste(application,
                    version < 14
                        ? SmartFormatter.CDataizeInVb(ClipboardText)
                        : SmartFormatter.StringinizeInVb(ClipboardText));
                return;
            }
            //c#
            Paste(application, SmartFormatter.StringinizeInCs(ClipboardText));
        }


        /// <summary>
        /// Public method to paste and format clipboard text as comment the cursor 
        /// location for the configured or active window's langage .
        /// </summary>
        /// <param name="application">application to insert</param>
        public void PasteAsComment(DTE2 application)
        {
            Paste(application, IsVb(application) ?
                SmartFormatter.CommentizeInVb(ClipboardText) :
                SmartFormatter.CommentizeInCs(ClipboardText));
        }


        /// <summary>
        /// Public method to paste format clipboard text into a specified region
        /// </summary>
        /// <param name="application">application to insert</param>
        public void PasteAsRegion(DTE2 application)
        {
            //get the region name
            const string region = "myRegion";

            //it's so simple, we really don't need a function
            string csRegionized = "#region " + region + Environment.NewLine + ClipboardText + Environment.NewLine + "#endregion";

            //and paste
            Paste(application, csRegionized);
        }

        /// <summary>
        /// Public method to paste and format clipboard text as stringbuilder the cursor 
        /// location for the configured or active window's langage .
        /// </summary>
        /// <param name="application">application to insert</param>
        public void PasteAsStringBuilder(DTE2 application)
        {
            const string stringbuilder = "sb";
            Paste(application, IsVb(application) ?
                SmartFormatter.StringbuilderizeInVb(ClipboardText, stringbuilder) :
                SmartFormatter.StringbuilderizeInCs(ClipboardText, stringbuilder));
        }

        #endregion
    }
}
