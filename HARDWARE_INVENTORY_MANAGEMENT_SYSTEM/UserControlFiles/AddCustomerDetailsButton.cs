using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.UserControlFiles
{
    public partial class AddCustomerDetailsButton : UserControl
    {
        public AddCustomerDetailsButton()
        {
            InitializeComponent();
            MakeButtonRound();
        }

        private void MakeButtonRound()
        {
            // Apply circular shape to the button
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, button1.Width, button1.Height);
            button1.Region = new Region(path);

            // Optional: add styling
            button1.BackColor = Color.RoyalBlue;
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
        }

        private void button1_Resize(object sender, EventArgs e)
        {
            MakeButtonRound(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
