using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

//TODO: Make form resizable.
// Details:
// - 3 Pixels large control (which control?)
// - onHover = Cursor
// - onMouseDown = you know

namespace WinNumix
{
    partial class Form1
    {
        // -- Control box --
        PictureBox btnCloseButton = new PictureBox();
        PictureBox btnMinimizeButton = new PictureBox();
        PictureBox btnMaximizeButton = new PictureBox();
        // -- Titlebar --
        PictureBox TitleBar = new PictureBox();
        Label TitleBarText = new Label();
        PictureBox TitleBarIcon = new PictureBox();
        // -- Borders --
        PictureBox FormBorderLeft = new PictureBox();
        PictureBox FormBorderRight = new PictureBox();
        PictureBox FormBorderTop = new PictureBox();
        PictureBox FormBorderBottom = new PictureBox();

        /// <summary>
        /// Get or set the text of the current form.
        /// </summary>
        /// <remarks>
        /// This overrides the Form.Text property.
        /// </remarks>
        new string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text =
                    TitleBarText.Text =
                    value;
            }
        }

        /// <summary>
        /// Gets or sets the size of the client area of the form.
        /// </summary>
        /// <remarks>
        /// This overrides the Form.ClientSize property.
        /// With no border styles, Size is the same size as ClientSize.
        /// </remarks>
        new Size ClientSize
        {
            get
            {
                return new Size(Width,
                    Height - NumixRessources.TitleBarHeight);
            }
            set
            {
                base.ClientSize = new Size(value.Width, 
                    value.Height - NumixRessources.TitleBarHeight);
            }
        }

        /// <summary>
        /// Places the Numix theme on the current form.
        /// </summary>
        void InitializeNumixTheme()
        {
            SuspendLayout();

            // Initial button position: FormWidth - IconWidth - Padding
            int InitialButtonPos = Width - NumixRessources.TitleBarHeight - 3;

            #region btnCloseButton
            btnCloseButton.Size = new Size(24, 24);
            btnCloseButton.SizeMode = PictureBoxSizeMode.StretchImage;
            btnCloseButton.Location = new Point(InitialButtonPos, 0);
            btnCloseButton.Image = NumixRessources.CloseActive;
            btnCloseButton.Anchor = AnchorStyles.Right;
            // Events
            btnCloseButton.MouseEnter += CloseButton_MouseEnter;
            btnCloseButton.MouseLeave += CloseButton_MouseLeave;
            btnCloseButton.MouseDown += CloseButton_MouseDown;
            btnCloseButton.MouseUp += CloseButton_MouseUp;
            #endregion

            #region btnMaximizeButton
            btnMaximizeButton.Size = new Size(24, 24);
            btnMaximizeButton.SizeMode = PictureBoxSizeMode.StretchImage;
            btnMaximizeButton.Location = new Point(InitialButtonPos - btnMaximizeButton.Width, 0);
            btnMaximizeButton.Image = NumixRessources.MaximizeActive;
            btnMaximizeButton.Anchor = AnchorStyles.Right;
            // Events
            btnMaximizeButton.MouseEnter += BtnMaximizeButton_MouseEnter;
            btnMaximizeButton.MouseLeave += BtnMaximizeButton_MouseLeave;
            btnMaximizeButton.MouseDown += BtnMaximizeButton_MouseDown;
            btnMaximizeButton.MouseUp += BtnMaximizeButton_MouseUp;
            #endregion

            #region btnMinimizeButton
            btnMinimizeButton.Size = new Size(24, 24);
            btnMinimizeButton.SizeMode = PictureBoxSizeMode.StretchImage;
            btnMinimizeButton.Location = new Point(InitialButtonPos - (btnMaximizeButton.Size.Width + btnMinimizeButton.Width), 0);
            btnMinimizeButton.Image = NumixRessources.MinimizeActice;
            btnMinimizeButton.Anchor = AnchorStyles.Right;
            // Events
            btnMinimizeButton.MouseEnter += BtnMinimizeButton_MouseEnter;
            btnMinimizeButton.MouseLeave += BtnMinimizeButton_MouseLeave;
            btnMinimizeButton.MouseDown += BtnMinimizeButton_MouseDown;
            btnMinimizeButton.MouseUp += BtnMinimizeButton_MouseUp;
            #endregion

            #region Titlebar
            TitleBar.Size = new Size(Width, 24);
            TitleBar.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            TitleBar.BackColor = Color.FromArgb(45, 45, 45);
            // Events
            TitleBar.MouseDown += TitleBar_MouseDown;
            TitleBar.MouseMove += TitleBar_MouseMove;
            TitleBar.MouseUp += TitleBar_MouseUp;
            TitleBar.DoubleClick += TitleBar_DoubleClick;
            #endregion

            #region Borders
            // -- Left --
            FormBorderLeft.Size = new Size(NumixRessources.FormBorderThickness, ClientSize.Height - NumixRessources.FormBorderThickness);
            FormBorderLeft.Location = new Point(0, Size.Height - ClientSize.Height);
            FormBorderLeft.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
            // Events
            FormBorderLeft.MouseDown += FormBorderLeft_MouseDown;
            FormBorderLeft.MouseMove += FormBorderLeft_MouseMove;
            FormBorderLeft.MouseUp += FormBorderLeft_MouseUp;

            // -- Right --
            FormBorderRight.Size = new Size(NumixRessources.FormBorderThickness, ClientSize.Height - NumixRessources.FormBorderThickness);
            FormBorderRight.Location = new Point(Size.Width - NumixRessources.FormBorderThickness, Size.Height - ClientSize.Height);
            FormBorderRight.Anchor = AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            // Events
            FormBorderRight.MouseDown += FormBorderRight_MouseDown;
            FormBorderRight.MouseMove += FormBorderRight_MouseMove;
            FormBorderRight.MouseUp += FormBorderRight_MouseUp;

            // -- Top --
            FormBorderTop.Size = new Size(Size.Width, NumixRessources.FormBorderThickness);
            FormBorderTop.Location = new Point(0, 0);
            FormBorderTop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            // Events
            FormBorderTop.MouseDown += FormBorderTop_MouseDown;
            FormBorderTop.MouseMove += FormBorderTop_MouseMove;
            FormBorderTop.MouseUp += FormBorderTop_MouseUp;

            // -- Bottom --
            FormBorderBottom.Size = new Size(Size.Width, NumixRessources.FormBorderThickness);
            FormBorderBottom.Location = new Point(0, Size.Height - NumixRessources.FormBorderThickness);
            FormBorderBottom.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            // Events
            FormBorderBottom.MouseDown += FormBorderBottom_MouseDown;
            FormBorderBottom.MouseMove += FormBorderBottom_MouseMove;
            FormBorderBottom.MouseUp += FormBorderBottom_MouseUp;

            FormBorderBottom.Cursor =
                FormBorderTop.Cursor =
                Cursors.SizeNS;
            FormBorderLeft.Cursor =
                FormBorderRight.Cursor =
                Cursors.SizeWE;
            FormBorderLeft.BackColor =
                FormBorderBottom.BackColor =
                FormBorderRight.BackColor =
                Color.FromArgb(45, 45, 45);
            #endregion

            #region Text
            TitleBarText.AutoSize = true;
            TitleBarText.TextAlign = ContentAlignment.MiddleCenter;
            TitleBarText.Font = new Font("*", 8, FontStyle.Bold);
            TitleBarText.Location = new Point
            (
                (TitleBar.Width / 2) - (TitleBarText.Width / 2),
                //TODO: Fix label height for Mono and .NET compability
                ((TitleBar.Height / 2) - (TitleBarText.Height / 2)) + 4
            );
            TitleBarText.Anchor = AnchorStyles.Top;
            TitleBarText.ForeColor = Color.White;
            // Events
            TitleBarText.MouseDown += TitleBarText_MouseDown;
            TitleBarText.MouseMove += TitleBarText_MouseMove;
            TitleBarText.MouseUp += TitleBarText_MouseUp;
            TitleBarText.DoubleClick += TitleBarText_DoubleClick;
            #endregion

            #region Icon (User defined)
            TitleBarIcon.Size =
                new Size(NumixRessources.TitleBarHeight - NumixRessources.FormBorderThickness,
                NumixRessources.TitleBarHeight - NumixRessources.FormBorderThickness);
            TitleBarIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            TitleBarIcon.Location = new Point(NumixRessources.FormBorderThickness,
                NumixRessources.FormBorderThickness);
            TitleBarIcon.Image = Icon.ToBitmap();
            #endregion

            TitleBar.Controls.Add(FormBorderTop);
            TitleBar.Controls.Add(btnMinimizeButton);
            TitleBar.Controls.Add(btnMaximizeButton);
            TitleBar.Controls.Add(btnCloseButton);
            TitleBar.Controls.Add(TitleBarIcon);
            TitleBar.Controls.Add(TitleBarText);
            Controls.Add(TitleBar);

            Controls.Add(FormBorderLeft);
            Controls.Add(FormBorderRight);
            Controls.Add(FormBorderBottom);

            ResumeLayout(false);
        }
        
        // -- Events --

        #region Titlebar
        private void TitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            NumixRessources.formMouseDown = false;
        }

        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            NumixRessources.formMouseDown = true;
            NumixRessources.lastLocation = e.Location;
        }

        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (NumixRessources.formMouseDown)
            {
                Location =
                    new Point((Location.X - NumixRessources.lastLocation.X) + e.X,
                    (Location.Y - NumixRessources.lastLocation.Y) + e.Y);

                Update();
            }
        }

        private void TitleBar_DoubleClick(object sender, EventArgs e)
        {
            ToggleMaximize();
        }
        #endregion

        #region Text (Label)
        private void TitleBarText_MouseUp(object sender, MouseEventArgs e)
        {
            NumixRessources.formMouseDown = false;
        }

        private void TitleBarText_MouseDown(object sender, MouseEventArgs e)
        {
            NumixRessources.formMouseDown = true;
            NumixRessources.lastLocation = e.Location;
        }

        private void TitleBarText_MouseMove(object sender, MouseEventArgs e)
        {
            if (NumixRessources.formMouseDown)
            {
                Location =
                    new Point((Location.X - NumixRessources.lastLocation.X) + e.X,
                    (Location.Y - NumixRessources.lastLocation.Y) + e.Y);

                Update();
            }
        }

        private void TitleBarText_DoubleClick(object sender, EventArgs e)
        {
            ToggleMaximize();
        }
        #endregion

        #region Close Button
        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            btnCloseButton.Image = NumixRessources.CloseHover;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            btnCloseButton.Image = NumixRessources.CloseActive;
        }

        private void CloseButton_MouseDown(object sender, MouseEventArgs e)
        {
            btnCloseButton.Image = NumixRessources.ClosePressed;
        }

        private void CloseButton_MouseUp(object sender, MouseEventArgs e)
        {
            Close();
        }
        #endregion

        #region Maximize Button
        private void BtnMaximizeButton_MouseEnter(object sender, EventArgs e)
        {
            btnMaximizeButton.Image = NumixRessources.MaximizeHover;
        }

        private void BtnMaximizeButton_MouseLeave(object sender, EventArgs e)
        {
            btnMaximizeButton.Image = NumixRessources.MaximizeActive;
        }

        private void BtnMaximizeButton_MouseDown(object sender, MouseEventArgs e)
        {
            btnMaximizeButton.Image = NumixRessources.MaximizePressed;
        }

        private void BtnMaximizeButton_MouseUp(object sender, MouseEventArgs e)
        {
            ToggleMaximize();
        }
        #endregion

        #region Minimize Button
        private void BtnMinimizeButton_MouseEnter(object sender, EventArgs e)
        {
            btnMinimizeButton.Image = NumixRessources.MinimizeHover;
        }

        private void BtnMinimizeButton_MouseLeave(object sender, EventArgs e)
        {
            btnMinimizeButton.Image = NumixRessources.MinimizeActice;
        }

        private void BtnMinimizeButton_MouseDown(object sender, MouseEventArgs e)
        {
            btnMinimizeButton.Image = NumixRessources.MinimizePressed;
        }

        private void BtnMinimizeButton_MouseUp(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region Left border
        private void FormBorderLeft_MouseDown(object sender, MouseEventArgs e)
        {
            NumixRessources.formMouseDown = true;
        }

        private void FormBorderLeft_MouseUp(object sender, MouseEventArgs e)
        {
            NumixRessources.formMouseDown = false;
        }

        private void FormBorderLeft_MouseMove(object sender, MouseEventArgs e)
        {
            if (NumixRessources.formMouseDown)
            {
                Size =
                    new Size(Size.Width - e.X,
                    Size.Height);
                Location =
                    new Point(Location.X + e.X,
                    Location.Y);

                Update();
            }
        }
        #endregion

        #region Right border
        private void FormBorderRight_MouseUp(object sender, MouseEventArgs e)
        {
            NumixRessources.formMouseDown = false;
        }

        private void FormBorderRight_MouseDown(object sender, MouseEventArgs e)
        {
            NumixRessources.formMouseDown = true;
        }

        private void FormBorderRight_MouseMove(object sender, MouseEventArgs e)
        {
            if (NumixRessources.formMouseDown)
            {
                Size =
                    new Size(Size.Width + e.X,
                    Size.Height);

                Update();
            }
        }
        #endregion

        #region Top border
        private void FormBorderTop_MouseUp(object sender, MouseEventArgs e)
        {
            NumixRessources.formMouseDown = false;
        }

        private void FormBorderTop_MouseDown(object sender, MouseEventArgs e)
        {
            NumixRessources.formMouseDown = true;
        }

        private void FormBorderTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (NumixRessources.formMouseDown)
            {
                Size =
                    new Size(Size.Width,
                    Size.Height - e.Y);
                Location =
                    new Point(Location.X,
                    Location.Y + e.Y);

                Update();
            }
        }
        #endregion

        #region Bottom border
        private void FormBorderBottom_MouseUp(object sender, MouseEventArgs e)
        {
            NumixRessources.formMouseDown = false;
        }

        private void FormBorderBottom_MouseDown(object sender, MouseEventArgs e)
        {
            NumixRessources.formMouseDown = true;
        }

        private void FormBorderBottom_MouseMove(object sender, MouseEventArgs e)
        {
            if (NumixRessources.formMouseDown)
            {
                Size =
                    new Size(Size.Width,
                    Size.Height + e.Y);

                Update();
            }
        }
        #endregion

        // -- Form related methods --

        void ToggleMaximize()
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;

            TitleBarText.Location = new Point
            (
                (TitleBar.Width / 2) - (TitleBarText.Width / 2),
                TitleBarText.Location.Y
            );
        }
    }

    class NumixRessources
    {
        // Used when dragging the form around.
        static internal bool formMouseDown;
        static internal Point lastLocation;

        // Settings
        internal const int TitleBarHeight = 24;
        internal const int FormBorderThickness = 3;

        // Assembly-related properties
        static readonly string AssemblyName = Assembly.GetExecutingAssembly().GetName().Name;

        #region Close icons
        static public Image CloseActive = GetImageFromAssemblyStream($"{AssemblyName}.Numix.close-active.png");
        static public Image CloseInactive = GetImageFromAssemblyStream($"{AssemblyName}.Numix.close-inactive.png");
        static public Image CloseHover = GetImageFromAssemblyStream($"{AssemblyName}.Numix.close-prelight.png");
        static public Image ClosePressed = GetImageFromAssemblyStream($"{AssemblyName}.Numix.close-pressed.png");
        #endregion

        #region Maximize icons
        static public Image MaximizeActive = GetImageFromAssemblyStream($"{AssemblyName}.Numix.maximize-active.png");
        static public Image MaximizeInactive = GetImageFromAssemblyStream($"{AssemblyName}.Numix.maximize-inactive.png");
        static public Image MaximizeHover = GetImageFromAssemblyStream($"{AssemblyName}.Numix.maximize-prelight.png");
        static public Image MaximizePressed = GetImageFromAssemblyStream($"{AssemblyName}.Numix.maximize-pressed.png");
        #endregion

        #region Minimize icons
        static public Image MinimizeActice = GetImageFromAssemblyStream($"{AssemblyName}.Numix.hide-active.png");
        static public Image MinimizeInactive = GetImageFromAssemblyStream($"{AssemblyName}.Numix.hide-inactive.png");
        static public Image MinimizeHover = GetImageFromAssemblyStream($"{AssemblyName}.Numix.hide-prelight.png");
        static public Image MinimizePressed = GetImageFromAssemblyStream($"{AssemblyName}.Numix.hide-pressed.png");
        #endregion

        static Image GetImageFromAssemblyStream(string pAssemblyPath)
        {
            return
                 Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream(pAssemblyPath));
        }
    }
}
