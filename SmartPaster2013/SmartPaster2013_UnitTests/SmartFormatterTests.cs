using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartPaster2013;

namespace SmartPaster2013_UnitTests
{
    [TestClass]
    public class SmartFormatterTests
    {
        /*
我给你一本书 。
This has a   tab and " quotes but this \t is not an escape
        */
        //我给你一本书 。
        //This has a   tab and " quotes but this \t is not an escape

        private const string Original = "我给你一本书 。\r\nThis has a \t tab and \" quotes but this \\t is not an escape";

        [TestMethod]
        public void TestVerbatimStringCs()
        {
            var result = SmartFormatter.StringinizeInCs(Original);

            //should look like this
            var s = @"我给你一本书 。
This has a   tab and "" quotes but this \t is not an escape";

            //The line break is included, the double-quotes are doubled
            //In verbatim, tabs can't be escaped, so it's just included as a raw tab
            //a VS reformat may convert the tab to spaces
            Assert.AreEqual(@"@""我给你一本书 。
This has a " + "\t tab and \"\" quotes but this \\t is not an escape\"", result);
        }

        [TestMethod]
        public void TestLiteralStringCs()
        {
            var result = SmartFormatter.LiterallyInCs(Original);

            //should look like this
            var s = "我给你一本书 。" + Environment.NewLine +
                    "This has a   tab and \" quotes but this \t is not an escape";

            //Tab and quote is escaped, line is turned into NewLine
            Assert.AreEqual(@"""我给你一本书 。"" + Environment.NewLine + 
""This has a \t tab and \"" quotes but this \\t is not an escape""", result);
        }

        [TestMethod]
        public void TestLiteralStringCsPath()
        {
            var path = @"C:\x\y\x";
            var result = SmartFormatter.LiterallyInCs(path);
            Assert.AreEqual("\"C:\\\\x\\\\y\\\\x\"", result);
        }

        [TestMethod]
        public void TestLiteralStringCxxPath()
        {
            var path = @"C:\x\y\x";
            var result = SmartFormatter.LiterallyInCxx(path);
            Assert.AreEqual("\"C:\\\\x\\\\y\\\\x\"", result);
        }

        [TestMethod]
        public void TestLiteralStringWithEmptyLinesCs()
        {
            var result = SmartFormatter.LiterallyInCs(@"
1

2
");

            //should look like this
            var s = "1" + Environment.NewLine +
Environment.NewLine +
"2";

            //Tab and quote is escaped, line is turned into NewLine
            Assert.AreEqual("\"1\" + Environment.NewLine + \r\n\"\" + Environment.NewLine + \r\n\"2\"", result);
        }

        [TestMethod]
        public void TestLiteralWithLineBreak()
        {
            var s = @"select ""Id"", ""max"", ""Prefs""
 from rs.""table"";";
            var result = SmartFormatter.LiterallyInCs(s);

            Assert.AreEqual(
                "\"select \\\"Id\\\", \\\"max\\\", \\\"Prefs\\\"\" + Environment.NewLine + \r\n\" from rs.\\\"table\\\";\"", result);
        }


        [TestMethod]
        public void TestVerbatimStringVb()
        {
            var result = SmartFormatter.StringinizeInVb(Original);

            //Verbatim in VB 14. Double quoted, otherwise unchanged.
            Assert.AreEqual("\"我给你一本书 。\r\nThis has a \t tab and \"\" quotes but this \\t is not an escape\"", result);
        }


        [TestMethod]
        public void TestLiteralStringVb()
        {
            var result = SmartFormatter.LiterallyInVb(Original);

            //No verbatim in VB up to 14, so we just use literals with vbCrLf and line continuation
            Assert.AreEqual("\"我给你一本书 。\" & vbCrLf & _\r\n\"This has a \" & vbTab & \" tab and \"\"\"\" quotes but this \\t is not an escape\"", result);
        }


        [TestMethod]
        public void TestLiteralStringWithEmptyLinesVb()
        {
            var result = SmartFormatter.LiterallyInVb(@"
1

2
");

            //No verbatim in VB up to 14, so we just use literals with vbCrLf and line continuation
            //Arguably tab could be vbTab
            //You could use xml literals...
            Assert.AreEqual("\"1\" & vbCrLf & _\r\nvbCrLf & _\r\n\"2\"", result);
        }


        [TestMethod]
        public void TestCommentCs()
        {
            var result = SmartFormatter.CommentizeInCs(Original);

            //should look like this
            //我给你一本书 。
            //This has a   tab and " quotes but this \t is not an escape

            //don't double quotes, just use line comment prefix
            //there's a trailing line break too
            Assert.AreEqual(@"//我给你一本书 。
//This has a " + "\t tab and \" quotes but this \\t is not an escape\r\n", result);
        }

        [TestMethod]
        public void TestCommentVb()
        {
            var result = SmartFormatter.CommentizeInVb(Original);

            //don't double quotes, just use line comment prefix
            //there's a trailing line break too
            Assert.AreEqual(@"'我给你一本书 。
'This has a " + "\t tab and \" quotes but this \\t is not an escape\r\n", result);
        }

        [TestMethod]
        public void TestStringBuilderCs()
        {
            var result = SmartFormatter.StringbuilderizeInCs(Original, "sb");

            //should look like this
            var sb = new System.Text.StringBuilder(68);
            sb.AppendLine(@"我给你一本书 。");
            sb.AppendLine(@"This has a   tab and "" quotes but this \t is not an escape");

            //the single " becomes doubled, which becomes 4 here
            Assert.AreEqual(@"var sb = new System.Text.StringBuilder(68);
sb.AppendLine(@""我给你一本书 。"");
sb.AppendLine(@""This has a 	 tab and """" quotes but this \t is not an escape"");
", result);
        }


        [TestMethod]
        public void TestStringBuilderVb()
        {
            var result = SmartFormatter.StringbuilderizeInVb(Original, "sb");

            //the single " becomes doubled, which becomes 4 here
            Assert.AreEqual(@"Dim sb As New System.Text.StringBuilder(68)
sb.AppendLine(""我给你一本书 。"")
sb.AppendLine(""This has a 	 tab and """" quotes but this \t is not an escape"")
", result);
        }


    }
}
