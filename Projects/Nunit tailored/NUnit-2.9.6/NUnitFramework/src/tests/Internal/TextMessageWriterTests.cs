// ***********************************************************************
// Copyright (c) 2007 Charlie Poole
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System;
using System.Threading;
using System.Globalization;

namespace NUnit.Framework.Internal
{
    [TestFixture]
    public class TextMessageWriterTests : AssertionHelper
    {
        private static readonly string NL = NUnit.Env.NewLine;

        private TextMessageWriter writer;

		[SetUp]
		public void SetUp()
        {
            writer = new TextMessageWriter();
        }

        [Test]
        public void DisplayStringDifferences()
        {
            string s72 = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string exp = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXY...";

            writer.DisplayStringDifferences(s72, "abcde", 5, false, true);
            string message = writer.ToString();
            Expect(message, EqualTo(
                TextMessageWriter.Pfx_Expected + Q(exp) + NL +
                TextMessageWriter.Pfx_Actual + Q("abcde") + NL +
                "  ----------------^" + NL));
        }

        [Test]
        public void DisplayStringDifferences_NoClipping()
        {
            string s72 = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

            writer.DisplayStringDifferences(s72, "abcde", 5, false, false);
            string message = writer.ToString();
            Expect(message, EqualTo(
                TextMessageWriter.Pfx_Expected + Q(s72) + NL +
                TextMessageWriter.Pfx_Actual + Q("abcde") + NL +
                "  ----------------^" + NL));
        }

        private string Q(string s)
        {
            return "\"" + s + "\"";
        }
    }
}
