using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Embarque_Escritorio.Contabilidad
{
    public partial class PruebaConta : Form
    {
        public PruebaConta()
        {
            InitializeComponent();
        }
        string Vsecreto;
        int xv=1;
        private void button1_Click(object sender, EventArgs e)
        {
            desencripta();
            textBox1.Text = Vsecreto;
        }

        private void PruebaConta_Load(object sender, EventArgs e)
        {

        }

        void desencripta()
        {
            string cadena = txtClave.Text.Trim();
            //int cadena2 = cadena.Length;
            for (xv = 1; xv <= ((txtClave.TextLength)); xv++)
                Vsecreto = Vsecreto +
                  Convert.ToChar(
                    (
                    (Convert.ToInt32((Asc(cadena.Substring(xv - 1, 1)) * 4) / 10))));

            //Vsecreto = Vsecreto + 
            //    ((char)
            //    (Convert.ToString(Convert.ToInt32(((Mid(cadena, xv, 1)) * 4) / (double)10))));
            if ((lblUsuario.Text.Trim()) != Vsecreto)
            {
                MessageBox.Show("Clave Incorrecta ....!");
                //Interaction.MsgBox("Clave Incorrecta ....!", Constants.vbCritical, "Error...!");
                //txtClave.Text = "";
                txtClave.Enabled = true;
                txtClave.Focus();
            }
            else
            {
                MessageBox.Show("Clave Correcta ....!");
            }
        }

        static int Asc(char c)
        {
            int converted = c;
            if (converted >= 0x80)
            {
                byte[] buffer = new byte[2];
                // if the resulting conversion is 1 byte in length, just use the value
                if (System.Text.Encoding.Default.GetBytes(new char[] { c }, 0, 1, buffer, 0) == 1)
                {
                    converted = buffer[0];
                }
                else
                {
                    // byte swap bytes 1 and 2;
                    converted = buffer[0] << 16 | buffer[1];
                }
            }
            return converted;
        }

        public static int Asc(string String)
        {
            if (String == null || String.Length == 0)
                throw new ArgumentException(Utils.GetResourceString("xxx", new string[1]
                {
      "String"
                }));
            return Strings.Asc(String[0]);
        }

        public static int AscInt(char String)
        {
            int num1 = Convert.ToInt32(String);
            if (num1 < 128)
                return num1;
            try
            {
                Encoding fileIoEncoding = GetFileIOEncoding();
                char[] chars = new char[1]
                {
        String
                };
                if (fileIoEncoding.IsSingleByte)
                {
                    byte[] bytes = new byte[1];
                    fileIoEncoding.GetBytes(chars, 0, 1, bytes, 0);
                    return (int)bytes[0];
                }
                byte[] bytes1 = new byte[2];
                if (fileIoEncoding.GetBytes(chars, 0, 1, bytes1, 0) == 1)
                    return (int)bytes1[0];
                if (BitConverter.IsLittleEndian)
                {
                    byte num2 = bytes1[0];
                    bytes1[0] = bytes1[1];
                    bytes1[1] = num2;
                }
                return (int)BitConverter.ToInt16(bytes1, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static Encoding GetFileIOEncoding()
        {
            return Encoding.Default;
        }

        internal static int GetLocaleCodePage()
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ANSICodePage;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        string VSecreto;
        public void Encripta(string Clave)
        {
            string cadena = Clave.Trim();
            for (xv = 1; xv <= ((cadena.Length)); xv++)
                VSecreto += Convert.ToChar(
                    ((Convert.ToInt32((Asc(cadena.Substring(xv - 1, 1)) * 4) / 10))));
        }
    }
}
