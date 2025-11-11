using System.Drawing;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Accounts_Module.Class_Components
{
    public class ProfileMenuPainter
    {
        public void Draw(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int textX = 47; // move text right of the avatar
            int textY = 5;  // top line
            int spacing = 14; // space between name and role

            // Draw Name
            using (Font nameFont = new Font("Lexend Semibold", 8f))
            using (SolidBrush nameBrush = new SolidBrush(Color.Black))
            {
                g.DrawString("Richard F.", nameFont, nameBrush, textX, textY);
            }

            // Draw Role
            using (Font roleFont = new Font("Lexend Light", 8f))
            using (SolidBrush roleBrush = new SolidBrush(Color.Gray))
            {
                g.DrawString("Store Manager", roleFont, roleBrush, textX, textY + spacing);
            }

           
        }
    }
}
