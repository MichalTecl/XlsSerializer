using System;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace XlsSerializer.Core.Attributes.Contract
{
    public abstract class CellStyleAttributeBase : Attribute
    {
        private float? m_fontSize;
        private FontStyle? m_fontStyle;
        private Color? m_color;
        private ExcelVerticalAlignment? m_verticalAlignment;
        private ExcelHorizontalAlignment? m_horizontalAlignment;
        private Color? m_backgroundColor;
        private bool? m_locked;

        public float FontSize
        {
            get => m_fontSize ?? 11f;
            set => m_fontSize = value;
        }

        public string Color
        {
            get => ColorTranslator.ToHtml(m_color ?? System.Drawing.Color.Black);
            set => m_color = ColorTranslator.FromHtml(value);
        }

        public string BackgroundColor
        {
            get => ColorTranslator.ToHtml(m_backgroundColor ?? System.Drawing.Color.Transparent);
            set => m_backgroundColor = ColorTranslator.FromHtml(value);
        }

        public FontStyle FontStyle
        {
            get => m_fontStyle ?? Attributes.FontStyle.Normal;
            set => m_fontStyle = value;
        }

        public ExcelVerticalAlignment VerticalAlignment
        {
            get => m_verticalAlignment ?? ExcelVerticalAlignment.Top;
            set => m_verticalAlignment = value;
        }

        public ExcelHorizontalAlignment HorizontalAlignment
        {
            get => m_horizontalAlignment ?? ExcelHorizontalAlignment.General;
            set => m_horizontalAlignment = value;
        }

        public bool Locked
        {
            get => m_locked == true;
            set => m_locked = value;
        }

        protected void Apply(ExcelRange cell)
        {
            if (m_fontSize != null)
            {
                cell.Style.Font.Size = m_fontSize.Value;
            }

            if (m_color != null)
            {
                cell.Style.Font.Color.SetColor(m_color.Value);
            }

            if (m_fontStyle != null)
            {
                var s = m_fontStyle.Value;

                cell.Style.Font.Bold = (s & Attributes.FontStyle.Bold) == Attributes.FontStyle.Bold;
                cell.Style.Font.UnderLine = (s & Attributes.FontStyle.Underline) == Attributes.FontStyle.Underline;
                cell.Style.Font.Strike = (s & Attributes.FontStyle.Strike) == Attributes.FontStyle.Strike;
                cell.Style.Font.Italic = (s & Attributes.FontStyle.Italic) == Attributes.FontStyle.Italic;
            }

            if (m_verticalAlignment != null)
            {
                cell.Style.VerticalAlignment = m_verticalAlignment.Value;
            }

            if (m_horizontalAlignment != null)
            {
                cell.Style.HorizontalAlignment = m_horizontalAlignment.Value;
            }

            if (m_backgroundColor != null)
            {
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(m_backgroundColor.Value);
            }

            if (m_locked != null)
            {
                cell.Style.Locked = m_locked.Value;
            }
        }
    }
}
