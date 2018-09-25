using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsaderoVivaMexicoPV
{
    public partial class Expander : UserControl
    {
        public Expander()
        {
            InitializeComponent();
            Expanded = true;
        }
        #region Events

        public event EventHandler StateChanged;
        public event CancelEventHandler StateChanging;

        #endregion

        #region Properties

        public bool Expanded { get; private set; }

        public Control Header
        {
            get { return header; }

            set
            {
                if (header != null)
                    Controls.Remove(header);

                header = value;
                header.Dock = DockStyle.Top;
                Controls.Add(header);
                Controls.SetChildIndex(header, Controls.Count > 1 ? 1 : 0);
            }
        }

        public Control Content
        {
            get { return content; }

            set
            {
                if (content != null)
                    Controls.Remove(content);

                content = value;
                Size = new Size(Width, header.Height + content.Height);
                content.Top = header.Height;

                Controls.Add(content);
                Controls.SetChildIndex(content, 0);
            }
        }

        #endregion

        #region Public methods

        public void Expand()
        {
            if (Expanded)
                return;

            if (StateChanging != null)
            {
                CancelEventArgs args = new CancelEventArgs();
                StateChanging(this, args);
                if (args.Cancel)
                    return;
            }

            Expanded = true;
            ArrangeLayout();

            StateChanged?.Invoke(this, null);
        }

        public void Collapse()
        {
            if (!Expanded)
                return;

            if (StateChanging != null)
            {
                CancelEventArgs args = new CancelEventArgs();
                StateChanging(this, args);
                if (args.Cancel)
                    return;
            }

            if (Content != null)
                contentHeight = Content.Height;
            Expanded = false;
            ArrangeLayout();

            StateChanged?.Invoke(this, null);
        }

        public void Toggle()
        {
            if (Expanded)
                Collapse();
            else
                Expand();
        }

        #endregion

        #region Private methods

        private void ArrangeLayout()
        {
            int h = 0;
            if (header != null)
                h += header.Height;
            if (Expanded && content != null)
                h += content.Height;
            Size = new Size(Width, h);
        }

        #endregion

        #region Priate fields

        private Control header;
        private Control content;
        private int contentHeight = 0;

        #endregion
    }

    public static class ExpanderHelper
    {
        public static Label CreateLabelHeader(Expander expander, string text, Color backColor, Image collapsedImage = null, Image expandedImage = null, int height = 25, Font font = null)
        {
            Label headerLabel = new Label();
            headerLabel.Text = text;
            headerLabel.AutoSize = false;
            headerLabel.Height = height;
            if (font != null)
                headerLabel.Font = font;
            headerLabel.TextAlign = ContentAlignment.MiddleLeft;
            if (collapsedImage != null && expandedImage != null)
            {
                headerLabel.Image = collapsedImage;
                headerLabel.ImageAlign = ContentAlignment.MiddleRight;
            }
            headerLabel.BackColor = backColor;

            if (collapsedImage != null && expandedImage != null)
            {
                expander.StateChanged += delegate { headerLabel.Image = expander.Expanded ? collapsedImage : expandedImage; };
            }

            headerLabel.Click += delegate { expander.Toggle(); };

            expander.Header = headerLabel;

            return headerLabel;
        }
    }
}
