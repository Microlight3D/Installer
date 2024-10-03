using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ML3DInstaller.View.CustomControls
{
    internal class MarkdownRichTextBox : RichTextBox
    {
        public MarkdownRichTextBox() 
        {
            
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
                        newLine = ReplaceFirst(line, "* ", "   ●");
                    }
                    if (line.StartsWith("- "))
                    {
                        newLine = ReplaceFirst(line, "- ", "   ●");
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
                    // Check if adjacent to letters
                    bool isValid = IsAdjacentToLetter(line, i, 2);

                    if (isValid)
                    {
                        // Toggle bold
                        isBold = !isBold;
                        i += 2; // Skip both asterisks
                        continue;
                    }
                    else
                    {
                        // Not a valid bold marker, treat as normal characters
                        SetSelectionFont(isBold, isItalic, defaultFont, boldFont, italicFont, boldItalicFont);
                        this.AppendText("**");
                        i += 2;
                        continue;
                    }
                }
                else if (c == '*')
                {
                    // Check if adjacent to letters
                    bool isValid = IsAdjacentToLetter(line, i, 1);

                    if (isValid)
                    {
                        // Toggle italic
                        isItalic = !isItalic;
                        i++; // Skip the asterisk
                        continue;
                    }
                    else
                    {
                        // Not a valid italic marker, treat as normal character
                        SetSelectionFont(isBold, isItalic, defaultFont, boldFont, italicFont, boldItalicFont);
                        this.AppendText("*");
                        i++;
                        continue;
                    }
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
