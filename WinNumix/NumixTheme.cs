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
        //TODO: Move ressources out of this class.
        // ..Includes formMouseDown, lastLocation, and AssemblyNameString

        bool formMouseDown;
        Point lastLocation;

        static readonly string AssemblyNameString = Assembly.GetExecutingAssembly().GetName().Name;

        // Close button
        PictureBox btnCloseButton = new PictureBox();
        Image imgCloseActive = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"{AssemblyNameString}.Numix.close-active.png"));
        Image imgCloseInactive = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"{AssemblyNameString}.Numix.close-inactive.png"));
        Image imgCloseHover = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"{AssemblyNameString}.Numix.close-prelight.png"));
        Image imgClosePressed = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"{AssemblyNameString}.Numix.close-pressed.png"));

        // Minimize button
        PictureBox btnMinimizeButton = new PictureBox();
        Image imgMinimizeActice = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"{AssemblyNameString}.Numix.hide-active.png"));
        Image imgMinimizeInactive = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"{AssemblyNameString}.Numix.hide-inactive.png"));
        Image imgMinimizeHover = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"{AssemblyNameString}.Numix.hide-prelight.png"));
        Image imgMinimizePressed = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"{AssemblyNameString}.Numix.hide-pressed.png"));

        // Maximize button
        PictureBox btnMaximizeButton = new PictureBox();
        Image imgMaximizeActive = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"{AssemblyNameString}.Numix.maximize-active.png"));
        Image imgMaximizeInactive = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"{AssemblyNameString}.Numix.maximize-inactive.png"));
        Image imgMaximizeHover = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"{AssemblyNameString}.Numix.maximize-prelight.png"));
        Image imgMaximizePressed = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream($"{AssemblyNameString}.Numix.maximize-pressed.png"));

        PictureBox TitleBar = new PictureBox();

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
            btnCloseButton.SizeMode = PictureBoxSizeMode.AutoSize;
            btnCloseButton.Location = new Point(InitialButtonPos, 0);
            btnCloseButton.Image = imgCloseActive;
            // Events
            btnCloseButton.MouseEnter += CloseButton_MouseEnter;
            btnCloseButton.MouseLeave += CloseButton_MouseLeave;
            btnCloseButton.MouseDown += CloseButton_MouseDown;
            btnCloseButton.MouseUp += CloseButton_MouseUp;
            #endregion

            #region btnMaximizeButton
            btnMaximizeButton.Size = new Size(24, 24);
            btnMaximizeButton.SizeMode = PictureBoxSizeMode.AutoSize;
            btnMaximizeButton.Location = new Point(InitialButtonPos - btnMaximizeButton.Width, 0);
            btnMaximizeButton.Image = imgMaximizeActive;
            // Events
            btnMaximizeButton.MouseEnter += BtnMaximizeButton_MouseEnter;
            btnMaximizeButton.MouseLeave += BtnMaximizeButton_MouseLeave;
            btnMaximizeButton.MouseDown += BtnMaximizeButton_MouseDown;
            btnMaximizeButton.MouseUp += BtnMaximizeButton_MouseUp;
            #endregion

            #region btnMinimizeButton
            btnMinimizeButton.Size = new Size(24, 24);
            btnMinimizeButton.SizeMode = PictureBoxSizeMode.AutoSize;
            btnMinimizeButton.Location = new Point(InitialButtonPos - (btnMaximizeButton.Size.Width + btnMinimizeButton.Width), 0);
            btnMinimizeButton.Image = imgMinimizeActice;
            // Events
            btnMinimizeButton.MouseEnter += BtnMinimizeButton_MouseEnter;
            btnMinimizeButton.MouseLeave += BtnMinimizeButton_MouseLeave;
            btnMinimizeButton.MouseDown += BtnMinimizeButton_MouseDown;
            btnMinimizeButton.MouseUp += BtnMinimizeButton_MouseUp;
            #endregion

            TitleBar.Controls.Add(btnMinimizeButton);
            TitleBar.Controls.Add(btnMaximizeButton);
            TitleBar.Controls.Add(btnCloseButton);
            Controls.Add(TitleBar);

            ResumeLayout(false);
        }

        #region Titlebar
        private void TitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            formMouseDown = false;
        }

        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            formMouseDown = true;
            lastLocation = e.Location;
        }

        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (formMouseDown)
            {
                Location = 
                    new Point((Location.X - lastLocation.X) + e.X, (Location.Y - lastLocation.Y) + e.Y);

                Update();
            }
        }
        #endregion

        #region Close Button
        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            btnCloseButton.Image = imgCloseHover;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            btnCloseButton.Image = imgCloseActive;
        }

        private void CloseButton_MouseDown(object sender, MouseEventArgs e)
        {
            btnCloseButton.Image = imgClosePressed;
        }

        private void CloseButton_MouseUp(object sender, MouseEventArgs e)
        {
            //CloseButton.Image = CloseHover;

            Close();
        }
        #endregion

        #region Maximize Button
        private void BtnMaximizeButton_MouseEnter(object sender, EventArgs e)
        {
            btnMaximizeButton.Image = imgMaximizeHover;
        }

        private void BtnMaximizeButton_MouseLeave(object sender, EventArgs e)
        {
            btnMaximizeButton.Image = imgMaximizeActive;
        }

        private void BtnMaximizeButton_MouseDown(object sender, MouseEventArgs e)
        {
            btnMaximizeButton.Image = imgMaximizePressed;
        }

        private void BtnMaximizeButton_MouseUp(object sender, MouseEventArgs e)
        {
            //TODO: Maximize function
        }
        #endregion

        #region Minimize Button
        private void BtnMinimizeButton_MouseEnter(object sender, EventArgs e)
        {
            btnMinimizeButton.Image = imgMinimizeHover;
        }

        private void BtnMinimizeButton_MouseLeave(object sender, EventArgs e)
        {
            btnMinimizeButton.Image = imgMinimizeActice;
        }

        private void BtnMinimizeButton_MouseDown(object sender, MouseEventArgs e)
        {
            btnMinimizeButton.Image = imgMinimizePressed;
        }

        private void BtnMinimizeButton_MouseUp(object sender, MouseEventArgs e)
        {
            //TODO: Minimize function
        }
        #endregion
    }
}
