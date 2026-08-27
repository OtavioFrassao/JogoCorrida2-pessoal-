using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JogoCorridaWinFormsApp
{
    public partial class TelaEstatisticaJogoCorrida : Form
    {
        public TelaEstatisticaJogoCorrida()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            if (panelPrincipal3 != null)
            {
                panelPrincipal3.Dock = DockStyle.None;
                panelPrincipal3.Anchor = AnchorStyles.None;
                int centroX = (this.ClientSize.Width - panelPrincipal3.Width) / 2;
                int centroY = (this.ClientSize.Height - panelPrincipal3.Height) / 2;
                panelPrincipal3.Location = new Point(centroX, centroY);
            }
        }
    }
}
