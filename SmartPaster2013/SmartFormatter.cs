using System;
using System.IO;
using System.Text;

namespace SmartPaster2013
{
    public static class SmartFormatter
    {

        /// <summary>
        /// Stringinizes text passed to it for use in C#
        /// </summary>
        /// <param name="txt">Text to be Stringinized</param>
        /// <returns>C# Stringinized text</returns>
        public static string StringinizeInCs(string txt)
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

        public static string StringinizeInVb(string txt)
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
                    sb.Append(line);
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

        public static string StringbuilderizeInVb(string txt, string sbName)
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

    }
}
