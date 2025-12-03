using System;
using System.Drawing;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM
{
    public static class PopupHost
    {
        /// <summary>
        /// Shows a UserControl in a borderless centered dialog, and optionally runs a handler on close.
        /// </summary>
        public static void ShowUserControlDialog(UserControl control, Size size, FormClosedEventHandler closedHandler)
        {
            if (control == null) throw new ArgumentNullException("control");

            Form popup = new Form();
            popup.FormBorderStyle = FormBorderStyle.None;
            popup.StartPosition = FormStartPosition.CenterScreen;
            popup.BackColor = Color.White;
            popup.Size = size;

            control.Dock = DockStyle.Fill;
            popup.Controls.Add(control);

            if (closedHandler != null)
            {
                popup.FormClosed += closedHandler;
            }

            popup.ShowDialog();
        }
    }
}
