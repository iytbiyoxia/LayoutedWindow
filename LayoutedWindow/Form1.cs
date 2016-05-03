using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LayoutedWindow
{
    public partial class Form1 : Form
    {
        DXLayeredImageWindow layer;

        public Form1()
        {
            InitializeComponent();

        }

        protected override void OnLoad(EventArgs e)
        {            
            base.OnLoad(e);

            var image = LayoutedWindow.Properties.Resources.cabafb090da77acf051dbe56adb15be1;
            layer = new DXLayeredImageWindow(image, this.panel1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            layer.Show(MousePosition);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //protected void BringRibbonToFront() {
            this.panel1.BringToFront();
            //DestroyImageContainer();
            if (layer != null && layer.IsActive)
            {
                layer.Close(1000);
            }
            //}
        }
    }
}
