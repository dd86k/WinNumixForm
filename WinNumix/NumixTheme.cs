using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

//TODO: Make form resizable.
// Details:
// - 3 Pixels large control (which?)
// - onHover = Cursor
// - onMouseDown = you know

namespace WinNumix
{
    partial class Form1
    {
        // -- Controls --
        PictureBox btnCloseButton = new PictureBox();
        PictureBox btnMinimizeButton = new PictureBox();
        PictureBox btnMaximizeButton = new PictureBox();
        PictureBox TitleBar = new PictureBox();
        Label TitleBarText = new Label();
        PictureBox TitleBarIcon = new PictureBox();

        /// <summary>
        /// Get or set the text of the current form.
        /// </summary>
        /// <remarks>
        /// This overrides and masks the Form.Text property.
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

        new Size ClientSize
        {
            get
            {
                return new Size(Width, Height - NumixRessources.Height);
            }
            set
            {
                base.ClientSize = new Size(value.Width, value.Height - NumixRessources.Height);
            }
        }

        /// <summary>
        /// Places the Numix theme on the current form.
        /// </summary>
        void InitializeNumixTheme()
        {
            SuspendLayout();
            
            // Initial button position: FormWidth - IconWidth - Padding
            int InitialButtonPos = Width - 24 - 3;

            #region Titlebar
            TitleBar.Size = new Size(Width, 24);
            TitleBar.Anchor = AnchorStyles.Right & AnchorStyles.Left;
            TitleBar.BackColor = Color.FromArgb(45, 45, 45);
            // Events
            TitleBar.MouseDown += TitleBar_MouseDown;
            TitleBar.MouseMove += TitleBar_MouseMove;
            TitleBar.MouseUp += TitleBar_MouseUp;
            #endregion

            #region btnCloseButton
            btnCloseButton.Size = new Size(24, 24);
            btnCloseButton.SizeMode = PictureBoxSizeMode.StretchImage;
            btnCloseButton.Location = new Point(InitialButtonPos, 0);
            btnCloseButton.Image = NumixRessources.CloseActive;
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
            // Events
            btnMinimizeButton.MouseEnter += BtnMinimizeButton_MouseEnter;
            btnMinimizeButton.MouseLeave += BtnMinimizeButton_MouseLeave;
            btnMinimizeButton.MouseDown += BtnMinimizeButton_MouseDown;
            btnMinimizeButton.MouseUp += BtnMinimizeButton_MouseUp;
            #endregion

            #region Text
            TitleBarText.AutoSize = true;
            TitleBarText.Font = new Font("*", 9, FontStyle.Bold);
            TitleBarText.Location = new Point
            {
                X = (TitleBar.Width / 2) - (TitleBarText.Width / 2),
                // Labels are weird in height ok
                Y = ((TitleBar.Height / 2) - (TitleBarText.Height / 2)) + 3
            };
            TitleBarText.Anchor = AnchorStyles.Right & AnchorStyles.Left;
            TitleBarText.ForeColor = Color.White;
            #endregion

            #region Icon (User defined)
            TitleBarIcon.Size = new Size(24, 24);
            TitleBarIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            TitleBarIcon.Location = new Point(3, 0);
            TitleBarIcon.Image = Icon.ToBitmap();
            #endregion

            TitleBar.Controls.Add(btnMinimizeButton);
            TitleBar.Controls.Add(btnMaximizeButton);
            TitleBar.Controls.Add(btnCloseButton);
            TitleBar.Controls.Add(TitleBarText);
            TitleBar.Controls.Add(TitleBarIcon);
            Controls.Add(TitleBar);

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
            //TODO: Maximize function
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
            //TODO: Minimize function
        }
        #endregion
    }

    class NumixRessources
    {
        internal const int Height = 24;

        static readonly string AssemblyName = Assembly.GetExecutingAssembly().GetName().Name;

        #region Close icons
        static public Image CloseActive = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"{AssemblyName}.Numix.close-active.png"));
        static public Image CloseInactive = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"{AssemblyName}.Numix.close-inactive.png"));
        static public Image CloseHover = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"{AssemblyName}.Numix.close-prelight.png"));
        static public Image ClosePressed = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"{AssemblyName}.Numix.close-pressed.png"));
        #endregion

        #region Maximize icons
        static public Image MaximizeActive = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"{AssemblyName}.Numix.maximize-active.png"));
        static public Image MaximizeInactive = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"{AssemblyName}.Numix.maximize-inactive.png"));
        static public Image MaximizeHover = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"{AssemblyName}.Numix.maximize-prelight.png"));
        static public Image MaximizePressed = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"{AssemblyName}.Numix.maximize-pressed.png"));
        #endregion

        #region Minimize icons
        static public Image MinimizeActice = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"{AssemblyName}.Numix.hide-active.png"));
        static public Image MinimizeInactive = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"{AssemblyName}.Numix.hide-inactive.png"));
        static public Image MinimizeHover = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"{AssemblyName}.Numix.hide-prelight.png"));
        static public Image MinimizePressed = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"{AssemblyName}.Numix.hide-pressed.png"));
        #endregion

        // Used when dragging the form around.
        static internal bool formMouseDown;
        static internal Point lastLocation;
    }
}
