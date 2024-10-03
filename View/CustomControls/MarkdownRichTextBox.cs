using ML3DInstaller.Presenter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ML3DInstaller.View.CustomControls
{
    internal class MarkdownRichTextBox : RichTextBoxEx
    {
        private Dictionary<string,string> LinkTextToUrl = new Dictionary<string,string>();
        public MarkdownRichTextBox() 
        {
            this.DetectUrls = false; // do not remove (related to RichTextBoxEx)

            this.LinkClicked += MarkdownRichTextBox_LinkClicked;
        }

        private void MarkdownRichTextBox_LinkClicked(object? sender, LinkClickedEventArgs e)
        {
            string text = e.LinkText;
            if ( text != null)
            {
                string url = LinkTextToUrl[text];
                if (url != null)
                {
                    Utils.OpenUrl(url);
                }
            }
        }

        public void SetText(string content)
        {
            if (content == null)
            {
                return;
            }
            var lines = content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            Font defaultFont = this.Font;
            FontFamily fontFamily = defaultFont.FontFamily;

            
            Font title1 = new Font(fontFamily, defaultFont.Size+8, FontStyle.Bold); //# test
            Font title2 = new Font(fontFamily, defaultFont.Size+6, FontStyle.Bold); // ## test
            Font title3 = new Font(fontFamily, defaultFont.Size+4, FontStyle.Bold); // ### test
            Font title4 = new Font(fontFamily, defaultFont.Size+2, FontStyle.Bold); // #### test

            // Clear the RichTextBox before adding new content
            this.Clear();

            foreach (var line in lines)
            {
                string newLine;
                if (line.StartsWith("# "))
                {
                    this.SelectionFont = title1;
                    newLine = ReplaceFirst(line, "# ", "");
                    this.AppendText(newLine + "\n");
                }
                else if (line.StartsWith("## "))
                {
                    this.SelectionFont = title2;
                    newLine = ReplaceFirst(line, "## ", "");
                    this.AppendText(newLine + "\n");
                }
                else if (line.StartsWith("### "))
                {
                    this.SelectionFont = title3;
                    newLine = ReplaceFirst(line, "### ", "");
                    this.AppendText(newLine + "\n");
                }
                else if (line.StartsWith("#### "))
                {
                    this.SelectionFont = title4;
                    newLine = ReplaceFirst(line, "#### ", "");
                    this.AppendText(newLine + "\n");
                }
                else
                {
                    newLine = line;
                    if (line.StartsWith("* "))
                    {
                        newLine = ReplaceFirst(line, "* ", "   • ");
                    }
                    if (line.StartsWith("- "))
                    {
                        newLine = ReplaceFirst(line, "- ", "   • ");
                    }
                    this.SelectionFont = defaultFont;
                    parseLine(newLine);
                    
                }

                

            }

        }

        /// <summary>
        /// Do italic and bold
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private void parseLine(string line)
        {
            Font defaultFont = this.Font;
            FontFamily fontFamily = defaultFont.FontFamily;
            Font boldFont = new Font(fontFamily, defaultFont.Size, FontStyle.Bold); // **text**
            Font italicFont = new Font(fontFamily, defaultFont.Size, FontStyle.Italic); // *text*
            Font boldItalicFont = new Font(fontFamily, defaultFont.Size, FontStyle.Bold | FontStyle.Italic); // ** and *
            Font currentFont = defaultFont;

            // Variables to keep track of formatting
            bool isBold = false;
            bool isItalic = false;

            int i = 0;
            while (i < line.Length)
            {
                // Check for markdown-style hyperlink [text](url)
                if (line[i] == '[')
                {
                    int linkTextStart = i + 1;
                    int linkTextEnd = line.IndexOf(']', linkTextStart);
                    if (linkTextEnd != -1 && linkTextEnd + 1 < line.Length && line[linkTextEnd + 1] == '(')
                    {
                        int urlStart = linkTextEnd + 2;
                        int urlEnd = line.IndexOf(')', urlStart);
                        if (urlEnd != -1)
                        {
                            // Extract the link text and URL
                            string linkText = line.Substring(linkTextStart, linkTextEnd - linkTextStart);
                            string url = line.Substring(urlStart, urlEnd - urlStart);

                            // Handle punctuation at the end of the link text
                            char punctuation = '\0';
                            if (linkText.Length > 0 && (linkText.EndsWith("?") || linkText.EndsWith("!") || linkText.EndsWith(".")))
                            {
                                punctuation = linkText[linkText.Length - 1];
                                linkText = linkText.Substring(0, linkText.Length - 1);
                            }

                            // Insert any preceding text before the link
                            if (i > 0)
                            {
                                string precedingText = line.Substring(0, i);
                                AppendFormattedText(precedingText, isBold, isItalic, defaultFont, boldFont, italicFont, boldItalicFont);
                            }

                            // Insert the link
                            SetSelectionFont(isBold, isItalic, defaultFont, boldFont, italicFont, boldItalicFont);
                            this.InsertLink(linkText, url);
                            LinkTextToUrl[linkText] = url;

                            // Insert punctuation if any
                            if (punctuation != '\0')
                            {
                                this.AppendText(punctuation.ToString());
                            }

                            // Move the index past the hyperlink
                            i = urlEnd + 1;

                            // Continue with the rest of the line
                            line = line.Substring(i);
                            i = 0;
                            continue;
                        }
                    }
                }

                char c = line[i];

                if (c == '\\')
                {
                    // Escape character, skip to next character
                    if (i + 1 < line.Length)
                    {
                        i++;
                        c = line[i];
                        // Output the escaped character
                        SetSelectionFont(isBold, isItalic, defaultFont, boldFont, italicFont, boldItalicFont);
                        this.AppendText(c.ToString());
                    }
                    i++;
                    continue;
                }

                // Check for '**' for bold
                if (c == '*' && i + 1 < line.Length && line[i + 1] == '*')
                {
                    // Toggle bold
                    isBold = !isBold;
                    i += 2; // Skip both asterisks
                    continue;
                }
                else if (c == '*')
                {
                    // Toggle italic
                    isItalic = !isItalic;
                    i++; // Skip the asterisk
                    continue;
                }
                else
                {
                    // Normal character
                    SetSelectionFont(isBold, isItalic, defaultFont, boldFont, italicFont, boldItalicFont);
                    this.AppendText(c.ToString());
                    i++;
                }
            }

            // Append a newline at the end of the line
            this.AppendText("\n");
        }

        private void AppendFormattedText(string text, bool isBold, bool isItalic, Font defaultFont, Font boldFont, Font italicFont, Font boldItalicFont)
        {
            SetSelectionFont(isBold, isItalic, defaultFont, boldFont, italicFont, boldItalicFont);
            this.AppendText(text);
        }


        private void SetSelectionFont(bool isBold, bool isItalic, Font defaultFont, Font boldFont, Font italicFont, Font boldItalicFont)
        {
            if (isBold && isItalic)
            {
                this.SelectionFont = boldItalicFont;
            }
            else if (isBold)
            {
                this.SelectionFont = boldFont;
            }
            else if (isItalic)
            {
                this.SelectionFont = italicFont;
            }
            else
            {
                this.SelectionFont = defaultFont;
            }
        }

        private bool IsAdjacentToLetter(string text, int index, int asteriskCount)
        {
            // Check character before the asterisks
            bool beforeIsLetter = false;
            if (index > 0)
            {
                char beforeChar = text[index - 1];
                beforeIsLetter = char.IsLetter(beforeChar);
            }

            // Check character after the asterisks
            bool afterIsLetter = false;
            int afterIndex = index + asteriskCount;
            if (afterIndex < text.Length)
            {
                char afterChar = text[afterIndex];
                afterIsLetter = char.IsLetter(afterChar);
            }

            return beforeIsLetter || afterIsLetter;
        }


        string ReplaceFirst(string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }


    }
}
