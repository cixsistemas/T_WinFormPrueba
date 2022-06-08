using Embarque_Escritorio.Clases;
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
    public partial class FrmDesencriptar : Form
    {
        public FrmDesencriptar()
        {
            InitializeComponent();
        }
        CsSeguridad _CsSeguridad = new CsSeguridad();
        private void button1_Click(object sender, EventArgs e)
        {
            TxtResultado.Text = (_CsSeguridad.Desencripta(textBox1.Text, cboClave.Text.Trim()));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
