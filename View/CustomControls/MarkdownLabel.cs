using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ML3DInstaller.View.CustomControls
{
    public class MarkdownLabel : Control
    {
        private string _markdownText;
        private List<MarkdownLine> _parsedLines;

        public string MarkdownText
        {
            get { return _markdownText; }
            set
            {
                _markdownText = value;
                ParseMarkdown();
                Invalidate(); // Redraw the control
            }
        }

        public MarkdownLabel()
        {
            this.DoubleBuffered = true;
            _parsedLines = new List<MarkdownLine>();
        }

        private void ParseMarkdown()
        {
            _parsedLines.Clear();

            if (string.IsNullOrEmpty(_markdownText))
                return;

            var lines = _markdownText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            foreach (var line in lines)
            {
                Font lineFont = this.Font;
                string content = line;

                if (line.StartsWith("# "))
                {
                    // Header level 1
                    content = line.Substring(2).Trim();
                    lineFont = new Font(this.Font.FontFamily, this.Font.Size + 8, FontStyle.Bold);
                }
                else if (line.StartsWith("## "))
                {
                    // Header level 2
                    content = line.Substring(3).Trim();
                    lineFont = new Font(this.Font.FontFamily, this.Font.Size + 4, FontStyle.Bold);
                }
                else
                {
                    // Normal text
                    content = line;
                    lineFont = this.Font;
                }

                var segments = ParseLine(content, lineFont);

                _parsedLines.Add(new MarkdownLine
                {
                    Segments = segments,
                });
            }
        }

        private List<MarkdownSegment> ParseLine(string line, Font baseFont)
        {
            List<MarkdownSegment> segments = new List<MarkdownSegment>();

            // Regex pattern to match **bold**, *italic*, or normal text
            string pattern = @"(\*\*[^*]+\*\*|\*[^*]+\*|[^*]+|\*)";

            MatchCollection matches = Regex.Matches(line, pattern);

            foreach (Match match in matches)
            {
                string text = match.Value;
                Font font = baseFont;

                if (text.StartsWith("**") && text.EndsWith("**") && text.Length > 4)
                {
                    // Bold text
                    text = text.Substring(2, text.Length - 4);
                    font = new Font(baseFont, baseFont.Style | FontStyle.Bold);
                }
                else if (text.StartsWith("*") && text.EndsWith("*") && text.Length > 2)
                {
                    // Italic text
                    text = text.Substring(1, text.Length - 2);
                    font = new Font(baseFont, baseFont.Style | FontStyle.Italic);
                }
                else if (text == "*" || text == "**")
                {
                    // Single asterisk or double asterisks, treat as normal text
                    font = baseFont;
                }
                // Else normal text

                segments.Add(new MarkdownSegment
                {
                    Text = text,
                    Font = font
                });
            }

            return segments;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            float y = 0;
            float maxWidth = this.ClientSize.Width;

            foreach (var line in _parsedLines)
            {
                float x = 0;
                float lineHeight = 0;

                foreach (var segment in line.Segments)
                {
                    // Measure the size required for the text
                    SizeF size = e.Graphics.MeasureString(segment.Text, segment.Font, new SizeF(maxWidth - x, this.ClientSize.Height - y));

                    // Draw the text
                    e.Graphics.DrawString(segment.Text, segment.Font, new SolidBrush(this.ForeColor), new PointF(x, y));

                    x += size.Width;
                    if (size.Height > lineHeight)
                        lineHeight = size.Height;

                    // Handle wrapping (basic implementation)
                    if (x >= maxWidth)
                    {
                        x = 0;
                        y += lineHeight;
                        lineHeight = 0;
                    }
                }

                y += lineHeight;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate(); // Redraw on resize to adjust word wrapping
        }

        private class MarkdownLine
        {
            public List<MarkdownSegment> Segments { get; set; }
        }

        private class MarkdownSegment
        {
            public string Text { get; set; }
            public Font Font { get; set; }
        }
    }


}
