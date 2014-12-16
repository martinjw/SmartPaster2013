using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartPaster2013;

namespace SmartPaster2013_UnitTests
{
    [TestClass]
    public class SmartFormatterTests
    {
        private const string Original = "我给你一本书 。\r\nThis has a \t tab and \" quotes but this \\t is not an escape";

        [TestMethod]
        public void TestVerbatimStringCs()
        {
            var result = SmartFormatter.StringinizeInCs(Original);

            //The line break is included, the double-quotes are doubled
            //In verbatim, tabs can't be escaped, so it's just included as a raw tab
            //a VS reformat may convert the tab to spaces
            Assert.AreEqual(@"@""我给你一本书 。
This has a " + "\t tab and \"\" quotes but this \\t is not an escape\"", result);
        }

        [TestMethod]
        public void TestVerbatimStringVb()
        {
            var result = SmartFormatter.StringinizeInVb(Original);

            //No verbatim in VB, so we just use literals with Environment.NewLine and line continuation
            //Arguably tab could be vbTab
            //You could use xml literals...
            Assert.AreEqual(@"""我给你一本书 。"" & Environment.NewLine & _
""This has a 	 tab and """" quotes but this \t is not an escape""", result);
        }

        [TestMethod]
        public void TestCommentCs()
        {
            var result = SmartFormatter.CommentizeInCs(Original);

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
