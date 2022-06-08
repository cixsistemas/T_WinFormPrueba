using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AxZKFPEngXControl;
using Embarque_Escritorio.Listar;

namespace Embarque_Escritorio
{
    public partial class Trabajador_Huella2 : Form
    {
        Utilitarios Utilitarios_ = new Utilitarios();
        private String Ruta = System.Configuration.ConfigurationManager.AppSettings.Get("CadenaConeccion");
        policia.clsaccesodatos servidor = new policia.clsaccesodatos();
        string __mesajeerror = "";
        public String Nivel  = "";
        public DataTable Dt_Trabajador2;

        private AxZKFPEngX ZkFprint = new AxZKFPEngX();
        //private bool Check;
        public int Id_Trabajador = -1;
        string Template;
        byte[] imageData;

        //string Calidad;

        public Trabajador_Huella2()
        {
            InitializeComponent();
        }

        
        private void Trabajador_Huella2_Load(object sender, EventArgs e)
        {
            Controls.Add(ZkFprint);
            InitialAxZkfp();

            if (Nivel == "M")
            {
                listar_(TxtTrabajador.Text);
                //imageData = (byte[])Dt_Trabajador2.Rows[0].ItemArray[1];
                Template = Convert.ToString(Dt_Trabajador2.Rows[0].ItemArray[2]);

                //=========================================================================
                //CAPTURAR HUELLA 
                imageData = (byte[])Dt_Trabajador2.Rows[0].ItemArray[1];
                Image newImage;
                //Read image data into a memory stream
                using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length)){
                    ms.Write(imageData, 0, imageData.Length);
                    //Set image variable value using memory stream.
                    newImage = Image.FromStream(ms, true);
                }
                //set picture
                fpicture.Image = newImage;
                //=========================================================================
            }
        }

       
        

        private void BtnTrabajador_Click(object sender, EventArgs e)
        {
            
            try
            {
                ListarTrabajador f = new ListarTrabajador();
                f.TxtBusca.Focus();
                f.ShowDialog();
                //if (Utilitarios_.indice >= 0)
                //this.TxtOrigen.Text = f.DgvLista["Origen", Utilitarios_.indice+1].Value.ToString();
                f.DgvLista.Rows[0].Selected = true;
                Id_Trabajador = Convert.ToInt32(f.DgvLista.Rows[f.DgvLista.CurrentRow.Index - 1].Cells[0].Value.ToString());
                this.TxtTrabajador.Text = f.DgvLista.Rows[f.DgvLista.CurrentRow.Index - 1].Cells[1].Value.ToString();

                //this.TxtOrigen.Text = Convert.ToString(f.DgvLista[0, f.DgvLista.CurrentRow.Index - 1].Value);                        
            }
            catch (Exception)
            {
                MessageBox.Show("Seleccione Registro.", "Atencion");
                this.TxtTrabajador.Focus();
            }
            //// indice = -1
            this.btnRegister.Focus();
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            Boolean ok;
            ok = TxtTrabajador.Text != "";
            if ((ok == false))
            {
                MessageBox.Show("Seleccione nombre de trabajador por favor.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                BtnTrabajador.Focus();
                return;
            }

            ok = txtTemplate.Text != "";
            if ((ok == false))
            {
                MessageBox.Show("Genere Huella.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btnRegister.Focus();
                return;
            }

            ok = TxtCalidad.Text != "";
            if ((ok == false))
            {
                MessageBox.Show("Genere Huella.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btnRegister.Focus();
                return;
            }

            ok = Convert.ToDouble(TxtCalidad.Text) > 84;
            if ((ok == false))
            {
                MessageBox.Show("La calidad de Huella, es insuficiente.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btnRegister.Focus();
                return;
            }


           
                if (Nivel == "N")
                {
                    if (MessageBox.Show("¿Desea Guardar Registro?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                    //==============================================================
                    // Stream usado como buffer
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    // Se guarda la imagen en el buffer
                    fpicture.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    //string Calidad = Convert.ToString(ZkFprint.LastQuality);
                    //==============================================================

                    policia.clsaccesodatos servidor = new policia.clsaccesodatos();
                    servidor.cadenaconexion = Ruta;
                    if (servidor.abrirconexiontrans() == true)
                    {
                        servidor.ejecutar("[dbo].[pa_mantenimiento_Trabajador_Biometrico]",
                            false
                            , Id_Trabajador
                            , ms.GetBuffer()
                            , TxtCalidad.Text.Trim()
                            , txtTemplate.Text.Trim()
                            , "N", 0, "");
                        if (servidor.getRespuesta() == 1)
                        {
                            servidor.cerrarconexiontrans();
                            __mesajeerror = servidor.getMensaje();
                            MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Hide();
                        }
                        else
                        {
                            servidor.cancelarconexiontrans();
                            __mesajeerror = servidor.getMensaje();
                            MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        __mesajeerror = servidor.getMensaje();
                        servidor.cerrarconexiontrans();
                        MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

           
                //CUANDO SE GUARDA Y NO SE HA MODIFICADO IMAGEN
                if (Nivel == "M" && Template == txtTemplate.Text.Trim())
                {
                    if (MessageBox.Show("¿Desea Modificar Registro.?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                    policia.clsaccesodatos servidor = new policia.clsaccesodatos();
                    servidor.cadenaconexion = Ruta;
                    if (servidor.abrirconexiontrans() == true)
                    {
                        servidor.ejecutar("[dbo].[pa_mantenimiento_Trabajador_Biometrico]",
                            false
                            , Id_Trabajador
                            , imageData
                            , TxtCalidad.Text.Trim()
                            , txtTemplate.Text.Trim()
                            , "M", 0, "");
                        if (servidor.getRespuesta() == 1)
                        {
                            servidor.cerrarconexiontrans();
                            __mesajeerror = servidor.getMensaje();
                            MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Hide();
                        }
                        else
                        {
                            servidor.cancelarconexiontrans();
                            __mesajeerror = servidor.getMensaje();
                            MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        __mesajeerror = servidor.getMensaje();
                        servidor.cerrarconexiontrans();
                        MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            
                //CUANDO SE GUARDA Y SE HA MODIFICADO IMAGEN
                if (Nivel == "M" && Template != txtTemplate.Text.Trim())
                {
                    if (MessageBox.Show("¿Desea Modificar Registro..?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                    //==============================================================
                    // Stream usado como buffer
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    // Se guarda la imagen en el buffer
                    fpicture.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    //string Calidad = Convert.ToString(ZkFprint.LastQuality);
                    //==============================================================

                    policia.clsaccesodatos servidor = new policia.clsaccesodatos();
                    servidor.cadenaconexion = Ruta;
                    if (servidor.abrirconexiontrans() == true)
                    {
                        servidor.ejecutar("[dbo].[pa_mantenimiento_Trabajador_Biometrico]",
                            false
                            , Id_Trabajador
                            , ms.GetBuffer()
                            , TxtCalidad.Text.Trim()
                            , txtTemplate.Text.Trim()
                            , "M", 0, "");
                        if (servidor.getRespuesta() == 1)
                        {
                            servidor.cerrarconexiontrans();
                            __mesajeerror = servidor.getMensaje();
                            MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Hide();
                        }
                        else
                        {
                            servidor.cancelarconexiontrans();
                            __mesajeerror = servidor.getMensaje();
                            MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        __mesajeerror = servidor.getMensaje();
                        servidor.cerrarconexiontrans();
                        MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
               
            }
        //}


        public void listar_(string fechainicio)
        {
            Trabajador_Huella2 f = new Trabajador_Huella2();
            //try
            //{
            //this.__mesajeerror.ForeColor = Color.Blue;
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexion() == true)
            {
                Dt_Trabajador2 = servidor.consultar("[dbo].[pa_Listar_Trabajador_Biometrico]", fechainicio).Tables[0];
                if (Dt_Trabajador2 == null)
                {
                    __mesajeerror = servidor.getMensageError();
                    servidor.cerrarconexion();
                    MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    int NroFilas = Dt_Trabajador2.Rows.Count;
                    if (NroFilas == 0)
                    {
                        servidor.cerrarconexion();
                    }
                    else
                    {
                        //=========================================================================
                        //byte[] imageData = (byte[])Dt_Trabajador2.Rows[0].ItemArray[1];
                        //textBox1.Text = Convert.ToString(Dt_Trabajador2.Rows[0].ItemArray[0]);
                        ////Read image data into a memory stream
                        //using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                        //{
                        //    ms.Write(imageData, 0, imageData.Length);
                        //    //Set image variable value using memory stream.
                        //    newImage = Image.FromStream(ms, true);
                        //}
                        ////set picture
                        ////f.fpicture.Image = newImage;
                        //pictureBox1.Image = newImage;

                        servidor.cerrarconexion();
                        ////=========================================================================
                        ////txtTemplate.Text = Dt.Rows[0].ItemArray[1].ToString();

                    }
                }
            }
            else
            {
                __mesajeerror = servidor.getMensageError();
                servidor.cerrarconexion();
                MessageBox.Show(__mesajeerror, "Balanza de Camiones", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Ingrese Fecha a Buscar", "Balanza de Camiones", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    //dtpfin.Focus();
            //}

        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Sensor
        private void InitialAxZkfp()
        {
            try
            {
                ZkFprint.OnImageReceived += zkFprint_OnImageReceived;
                ZkFprint.OnFeatureInfo += zkFprint_OnFeatureInfo;
                //zkFprint.OnFingerTouching 
                //zkFprint.OnFingerLeaving
                ZkFprint.OnEnroll += zkFprint_OnEnroll;

                if (ZkFprint.InitEngine() == 0)
                {
                    ZkFprint.FPEngineVersion = "9";
                    ZkFprint.EnrollCount = 3;
                    deviceSerial.Text += " " + ZkFprint.SensorSN + " Count: " + ZkFprint.SensorCount.ToString()
                    + " Index: " + ZkFprint.SensorIndex.ToString();
                    ShowHintInfo("Sensor conectado correctamente");
                }
            }
            catch (Exception ex)
            {
                ShowHintInfo("Verifique conexion de Sensor: " + ex.Message);
            }
        }
        private void zkFprint_OnImageReceived(object sender, IZKFPEngXEvents_OnImageReceivedEvent e)
        {
            Graphics g = fpicture.CreateGraphics();
            Bitmap bmp = new Bitmap(fpicture.Width, fpicture.Height);
            g = Graphics.FromImage(bmp);
            int dc = g.GetHdc().ToInt32();
            ZkFprint.PrintImageAt(dc, 0, 0, bmp.Width, bmp.Height);
            g.Dispose();
            fpicture.Image = bmp;
        }

        private void zkFprint_OnFeatureInfo(object sender, IZKFPEngXEvents_OnFeatureInfoEvent e)
        {
            String strTemp = string.Empty;
            if (ZkFprint.EnrollIndex != 1)
            {
                if (ZkFprint.IsRegister)
                {
                    if (ZkFprint.EnrollIndex - 1 > 0)
                    {
                        int eindex = ZkFprint.EnrollIndex - 1;
                        strTemp = "Please scan again ..." + eindex + "..." + ZkFprint.LastQuality;
                    }
                }
            }
            ShowHintInfo(strTemp);
        }

        private void zkFprint_OnEnroll(object sender, IZKFPEngXEvents_OnEnrollEvent e)
        {
            if (RbCal90.Checked == true)
            {
                if (ZkFprint.LastQuality > 89)
                {
                    if (e.actionResult)
                    {
                        string template = ZkFprint.EncodeTemplate1(e.aTemplate);
                        txtTemplate.Text = template;
                        ShowHintInfo("Huella tiene la calidad correcta");
                        //btnRegister.Enabled = false;
                        //btnVerify.Enabled = true;
                        TxtCalidad.Text = Convert.ToString(ZkFprint.LastQuality);
                        BtnAceptar.Focus();
                    }
                }
                else
                {
                    ShowHintInfo("Calidad de la huella" + " " + Convert.ToString(ZkFprint.LastQuality));
                    MessageBox.Show("Huella no ha superado la calidad esperada, Vuelva a registrar", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.btnRegister.Focus();
                    //this.btnRegister_Click(sender, e);
                }
            }

            if (RbCal85.Checked == true)
            {
                if (ZkFprint.LastQuality > 84)
                {
                    if (e.actionResult)
                    {
                        string template = ZkFprint.EncodeTemplate1(e.aTemplate);
                        txtTemplate.Text = template;
                        ShowHintInfo("Huella tiene la calidad correcta");
                        //btnRegister.Enabled = false;
                        //btnVerify.Enabled = true;
                        TxtCalidad.Text = Convert.ToString(ZkFprint.LastQuality);
                        BtnAceptar.Focus();
                    }
                }
                else
                {
                    ShowHintInfo("Calidad de la huella" + " " + Convert.ToString(ZkFprint.LastQuality));
                    MessageBox.Show("Huella no ha superado la calidad esperada, Vuelva a registrar", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.btnRegister.Focus();
                    //this.btnRegister_Click(sender, e);
                }
            }
            
        }

        //private void btnRegister_Click(object sender, IZKFPEngXEvents_OnEnrollEvent e)
        //{
        //    throw new NotImplementedException();
        //}

        //private void zkFprint_OnCapture(object sender, IZKFPEngXEvents_OnCaptureEvent e)
        //{
        //    string template = ZkFprint.EncodeTemplate1(e.aTemplate);
        //    //txtTemplate2.Text = template;

        //    if (ZkFprint.VerFingerFromStr(ref template, txtTemplate.Text, false, ref Check))
        //    {
        //        ShowHintInfo("Verified");
        //    }
        //    else
        //        ShowHintInfo("Not Verified");
        //}

        private void ShowHintInfo(String s)
        {
            prompt.Text = s;
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            ZkFprint.CancelEnroll();
            ZkFprint.EnrollCount = 1;
            ZkFprint.BeginEnroll();
            ShowHintInfo("Por favor, presione huella en sensor");
        }
        #endregion
    }
}
