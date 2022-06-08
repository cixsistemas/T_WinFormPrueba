using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Embarque_Escritorio.BDPasajes
{
    public partial class FrmMenuPasajes : Form
    {
        public FrmMenuPasajes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmHojaRuta1 frm = new FrmHojaRuta1();
            //frm.Id_UsuarioGlobal = Convert.ToInt32(Id_UsuarioGlobal);
            
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmDesencriptar frm = new FrmDesencriptar();
            //frm.Id_UsuarioGlobal = Convert.ToInt32(Id_UsuarioGlobal);

            frm.ShowDialog();
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            FrmHojaRuta2 frm = new FrmHojaRuta2();
            //frm.Id_UsuarioGlobal = Convert.ToInt32(Id_UsuarioGlobal);
            frm.ShowDialog();
        }
    }
}
