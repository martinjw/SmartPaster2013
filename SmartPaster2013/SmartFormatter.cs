using System;
using System.IO;
using System.Text;

namespace SmartPaster2013
{
    public static class SmartFormatter
    {
        private const string Quote = "\"";

        /// <summary>
        /// Stringinizes text passed to it for use in C#
        /// </summary>
        /// <param name="txt">Text to be Stringinized</param>
        /// <returns>C# Stringinized text</returns>
        public static string StringinizeInCs(string txt)
        {
            //sb to work with
            var sb = new StringBuilder(txt);

            //escape appropriately
            //escape the quotes with ""
            sb.Replace(Quote, Quote + Quote);

            //insert " at beginning and end
            sb.Insert(0, "@" + Quote);
            sb.Append(Quote);
            return sb.ToString();
        }

        /// <summary>
        /// Literallies the text in C#.
        /// </summary>
        /// <param name="txt">Literally, the text to be literallized.</param>
        /// <returns></returns>
        public static string LiterallyInCs(string txt)
        {
            //escape appropriately
            //escape the quotes with ""
            txt = txt.Trim() //ignore leading and trailing blank lines
                .Replace(Quote, "\\\"") //escape quotes
                .Replace("\t", "\\t") //escape tabs
                .Replace("\r", "\\r") //cr
                .Replace("\n", "\\n") //lf
                .Replace("\\r\\n", "\" + Environment.NewLine + \r\n\"") //escaped crlf to Env.NewLine
                .Replace("\"\" + ", ""); //"" + 

            return Quote + txt + Quote;
        }

        /// <summary>
        /// Stringinizes the text in vb (VB14 verbatim).
        /// </summary>
        /// <param name="txt">The text.</param>
        /// <returns></returns>
        public static string StringinizeInVb(string txt)
        {
            //double-up internal quotes
            txt = txt.Replace(Quote, Quote + Quote);

            //vb14 (vs2015+) supports verbatim strings
            return Quote + txt + Quote;
        }

        /// <summary>
        /// Literal VB (actually in C#, wat...).  
        /// </summary>
        /// <param name="txt">The text.</param>
        /// <returns></returns>
        public static string LiterallyInVb(string txt)
        {
            //double-up internal quotes
            txt = txt.Replace(Quote, Quote + Quote);
            txt = txt.Trim() //ignore leading and trailing blank lines
                .Replace(Quote, Quote + Quote) //escape quotes
                .Replace("\t", "\" & vbTab & \"") //explicit Tabs
                .Replace("\r", "\" & vbCr & \"") //cr
                .Replace("\n", "\" & vbLf & \"") //lf
                .Replace("\" & vbCr & \"\" & vbLf & \"", "\" & vbCrLf & _\r\n\"") //escaped cr + lf to CrLf + vb line continuation
                .Replace("\"\" & ", ""); //"" & 

            return Quote + txt + Quote;
        }

        public static string CDataizeInVb(string txt)
        {
            const string cdataStart = "<![CDATA[";

            const string cdataEnd = "]]>.Value";
            var sb = new StringBuilder();

            sb.AppendLine(cdataStart);
            sb.AppendLine(txt);
            sb.AppendLine(cdataEnd);

            //add the dec statement
            sb.Insert(0, "Dim s As String = " + Environment.NewLine);

            //and return
            return sb.ToString();
        }

        /// <summary>
        /// Commentizes text passed to it for use in C#
        /// </summary>
        /// <param name="txt">Text to be Stringinized</param>
        /// <returns>C# Commentized text</returns>
        public static string CommentizeInCs(string txt)
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

        public static string CommentizeInVb(string txt)
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

        public static string StringbuilderizeInCs(string txt, string sbName)
        {
            //sb to work with
            var sb = new StringBuilder(txt);

            //escape \,", and {}
            sb.Replace(Quote, Quote + Quote);

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
                    sb.Append("@" + Quote);
                    sb.Append(line);
                    sb.AppendLine(Quote + ");");
                }
            }

            //TODO: Better '@"" + ' replacement to not cover inside strings
            sb.Replace("@" + Quote + Quote + " + ", "");

            //add the dec statement
            sb.Insert(0, "var " + sbName + " = new System.Text.StringBuilder(" + txt.Length + ");" + Environment.NewLine);

            //and return
            return sb.ToString();
        }

        public static string StringbuilderizeInVb(string txt, string sbName)
        {
            //sb to work with
            var sb = new StringBuilder(txt);

            //escape
            sb.Replace(Quote, Quote + Quote);

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
                    sb.Append(Quote);
                    sb.Append(line);
                    sb.Append(Quote);
                    sb.AppendLine(")");
                }
            }

            //add the dec statement
            sb.Insert(0, "Dim " + sbName + " As New System.Text.StringBuilder(" + txt.Length + ")" + Environment.NewLine);

            //and return
            return sb.ToString();
        }

    }
}
