using System;
using System.IO;
using System.Text;
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
                if (iData.GetDataPresent(DataFormats.Text))
                    return Convert.ToString(iData.GetData(DataFormats.Text));
                return string.Empty;
            }
        }

        #region "Stringinize"
        /// <summary>
        /// Stringinizes text passed to it for use in C#
        /// </summary>
        /// <param name="txt">Text to be Stringinized</param>
        /// <returns>C# Stringinized text</returns>
        private static string StringinizeInCs(string txt)
        {
            //c# quote character -- really just a "
            const string qChr = "\"";

            //sb to work with
            var sb = new StringBuilder(txt);

            //escape appropriately
            //escape the quotes with ""
            sb.Replace(qChr, qChr + qChr);

            //insert " at beginning and end
            sb.Insert(0, "@" + qChr);
            sb.Append(qChr);
            return sb.ToString();
        }

        private static string StringinizeInVb(string txt)
        {
            //quote character
            const string qChr = "\"";

            //double-up internal quotes
            txt = txt.Replace(qChr, qChr + qChr);

            var firstLine = true;
            var sb = new StringBuilder();
            //read line by line (may only be one) and quote
            using (var stringReader = new StringReader(txt))
            {
                string line;
                while ((line = stringReader.ReadLine()) != null)
                {
                    if (firstLine)
                        firstLine = false;
                    else
                        sb.AppendLine(" & Environment.NewLine & _");
                    sb.Append(qChr + line + qChr);
                }
            }

            return sb.ToString();
        }

        #endregion

        #region "Commentize"
        /// <summary>
        /// Commentizes text passed to it for use in C#
        /// </summary>
        /// <param name="txt">Text to be Stringinized</param>
        /// <returns>C# Commentized text</returns>
        private static string CommentizeInCs(string txt)
        {
            const string cmtChar = "//";

            var sb = new StringBuilder(txt.Length);

            //process the passed string (txt), one line at a time
            //the original was horrible WTF code
            using (var reader = new StringReader(txt))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    sb.AppendLine(cmtChar + line);
                }
            }

            return sb.ToString();
        }

        private static string CommentizeInVb(string txt)
        {
            const string cmtChar = "'";

            var sb = new StringBuilder(txt.Length);

            //process the passed string (txt), one line at a time
            using (var reader = new StringReader(txt))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    sb.AppendLine(cmtChar + line);
                }
            }

            return sb.ToString();
        }
        #endregion

        #region "Stringbuilderize"
        private static string StringbuilderizeInCs(string txt, string sbName)
        {
            //c# quote character -- really just a "
            const string qChr = "\"";

            //sb to work with
            var sb = new StringBuilder(txt);

            //escape \,", and {}
            sb.Replace(qChr, qChr + qChr);

            //process the passed string (txt), one line at a time

            //dump the stringbuilder into a temp string
            string fullString = sb.ToString();
            sb.Clear(); //lovely .net 4 - sb.Remove(0, sb.Length);

            //the original was horrible
            using (var reader = new StringReader(fullString))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    sb.Append(sbName + ".AppendLine(");
                    sb.Append("@" + qChr);
                    sb.Append(line.Replace("\t", "\\t"));
                    sb.AppendLine(qChr + ");");
                }
            }

            //TODO: Better '@"" + ' replacement to not cover inside strings
            sb.Replace("@" + qChr + qChr + " + ", "");

            //add the dec statement
            sb.Insert(0, "var " + sbName + " = new System.Text.StringBuilder(" + txt.Length + ");" + Environment.NewLine);

            //and return
            return sb.ToString();
        }

        private static string StringbuilderizeInVb(string txt, string sbName)
        {
            //c# quote character -- really just a "
            const string qChr = "\"";

            //sb to work with
            var sb = new StringBuilder(txt);

            //escape
            sb.Replace(qChr, qChr + qChr);

            //dump the stringbuilder into a temp string
            var fullString = sb.ToString();
            sb.Clear();

            //read it line by line
            using (var reader = new StringReader(fullString))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    sb.Append(sbName + ".AppendLine(");
                    sb.Append(qChr);
                    sb.Append(line);
                    sb.Append(qChr);
                    sb.AppendLine(")");
                }
            }

            //add the dec statement
            sb.Insert(0, "Dim " + sbName + " As New System.Text.StringBuilder(" + txt.Length + ")" + Environment.NewLine);

            //and return
            return sb.ToString();
        }

        #endregion

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

        #region "Paste As ..."

        /// <summary>
        /// Public method to paste and format clipboard text as string the cursor 
        /// location for the configured or active window's langage .
        /// </summary>
        /// <param name="application">application to insert</param>
        public void PasteAsString(DTE2 application)
        {
            Paste(application,
                IsVb(application) ? StringinizeInVb(ClipboardText) : StringinizeInCs(ClipboardText));
        }


        /// <summary>
        /// Public method to paste and format clipboard text as comment the cursor 
        /// location for the configured or active window's langage .
        /// </summary>
        /// <param name="application">application to insert</param>
        public void PasteAsComment(DTE2 application)
        {
            Paste(application,
                IsVb(application) ? CommentizeInVb(ClipboardText) : CommentizeInCs(ClipboardText));
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
            Paste(application,
                IsVb(application) ? StringbuilderizeInVb(ClipboardText, stringbuilder) : StringbuilderizeInCs(ClipboardText, stringbuilder));
        }

        #endregion
    }
}
