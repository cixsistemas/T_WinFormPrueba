using Embarque_Escritorio.HojaRuta;
using Embarque_Escritorio.HojaRuta.Entities;
using Embarque_Escritorio.Reutilizable;
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
    public partial class FrmHojaRuta2 : Form
    {
        public FrmHojaRuta2()
        {
            InitializeComponent();
        }
        List<HojaRutaEntity> _lista;
        CsReutilizar _CsReutlizar = new CsReutilizar();
        String mensaje;
        int estado = 0;
        private void Listar(int EmpresaId, string FechaInicio, string FechaFinal
            , string Placa, string NroHojaRuta, int CODI_SUCURSAL, int Codi_Ruta, int Estado)
        {
            dgvLista.DataSource = null;
            HojaRutaEntity entidad = new HojaRutaEntity();
            try
            {
                HojaRutaLogic lp = new HojaRutaLogic();
                _lista = lp.LitsFinalizarHojaRuta(EmpresaId, FechaInicio, FechaFinal
                , Placa, NroHojaRuta, CODI_SUCURSAL, Codi_Ruta, Estado);



                if (_lista.Count == 0)
                {
                    lblMensajeError.Text = "No hay registros para mostrar.";
                    lblMensajeError.ForeColor = Color.Red;
                    //btnModificar.Enabled = false;
                    //btnEliminar.Enabled = false;
                }
                else
                {
                    //btnModificar.Enabled = true;
                    //btnEliminar.Enabled = true;

                    dgvLista.DataSource = _lista;
                    dgvLista.Columns["IdHojaRuta"].Visible = false;
                    dgvLista.Columns["Nro_Ruta"].Visible = false;
                    //dgvLista.Columns["VehiculoId"].Visible = false;
                    dgvLista.Columns["IdOrigen"].Visible = false;
                    dgvLista.Columns["IdDestino"].Visible = false;
                    dgvLista.Columns["St_Ok_manifiesto"].Visible = false;
                    dgvLista.Columns["St_Ok_Enviado"].Visible = false;
                    dgvLista.Columns["FechaOrigen"].Visible = false;
                    dgvLista.Columns["FechaOrigenStr"].Visible = false;
                    dgvLista.Columns["horaOrigen"].Visible = false;
                    dgvLista.Columns["FechaLlegada"].Visible = false;
                    dgvLista.Columns["FechaLlegadaStr"].Visible = false;
                    dgvLista.Columns["HoraLlegada"].Visible = false;
                    dgvLista.Columns["FechaFin"].Visible = false;
                    dgvLista.Columns["FechaFinStr"].Visible = false;
                    dgvLista.Columns["HoraFin"].Visible = false;
                    dgvLista.Columns["St_Ok_Enviado"].Visible = false;
                    dgvLista.Columns["St_Ok_Finalizado"].Visible = false;
                    dgvLista.Columns["usr_manifiesto"].Visible = false;
                    dgvLista.Columns["usr_envia"].Visible = false;
                    dgvLista.Columns["usr_finaliza"].Visible = false;
                    dgvLista.Columns["Fecha_Sys"].Visible = false;
                    dgvLista.Columns["Fecha_SysStr"].Visible = false;
                    dgvLista.Columns["Hora_Sys"].Visible = false;
                    dgvLista.Columns["St_Ok_Cancelado"].Visible = false;
                    dgvLista.Columns["Usr_cancelado"].Visible = false;
                    dgvLista.Columns["Fecha_cancel"].Visible = false;
                    dgvLista.Columns["RutaDes"].Visible = false;
                    dgvLista.Columns["Codi_Sucursal"].Visible = false;
                    dgvLista.Columns["Codi_ruta"].Visible = false;
                    dgvLista.Columns["Fecha_arribo"].Visible = false;

                    //RENOMBRAR NOMBRE DE COLUMNAS
                    //this.dgvLista.Columns["NombreGruConc"].HeaderText = "Grupo";
                    //this.dgvLista.Columns["UtilidadGruConc"].HeaderText = "Utilidad";
                    //================================================================

                    lblMensajeError.Text = "Sistema tiene " + _lista.Count.ToString() + " registros.";
                    lblMensajeError.ForeColor = Color.Blue;
                }
            }
            catch (Exception e)
            {
                //_CsReutlizar.MensajeError(entidad.MensajeBD() + (e) + ":(");
                // throw;
            }
        }
        private void FrmHojaRuta2_Load(object sender, EventArgs e)
        {

        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            
            if (rbFinalizados.Checked == true)
            { estado = 1; }
            else if (rbPendientes.Checked == true)
            { estado = 0; }

            Listar(1, Convert.ToString(dateTimePicker1.Value), Convert.ToString(dateTimePicker2.Value)
                , "", "", 0, 0, estado);
            //Listar(1, Convert.ToString("16/05/2022"), Convert.ToString("16/05/2022")
            //   , "", "", 0, 0, estado);
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _CsReutlizar.indice = e.RowIndex;
            if ((_CsReutlizar.indice == -1))
            {
                MessageBox.Show("Seleccione registro.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            txtNroHoja.Text = Convert.ToString(dgvLista.Rows[_CsReutlizar.indice].Cells["HojaRuta"].Value.ToString());
            txtFechaLlegada.Text = Convert.ToString(dgvLista.Rows[_CsReutlizar.indice].Cells["FechaLlegadaStr"].Value.ToString());
            txtHoraLlegada.Text = Convert.ToString(dgvLista.Rows[_CsReutlizar.indice].Cells["HoraLlegada"].Value.ToString());
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            HojaRutaLogic _HojaRutaLogic = new HojaRutaLogic();
            bool rpta;
            bool valida = _HojaRutaLogic.ValidarFinalizarMTC(txtFechaLlegada.Text.Trim(), txtHoraLlegada.Text.Trim());
            if (txtFechaLlegada.Text == "")
            {
                //MessageBox.Show(_HojaRutaLogic.MensajeBD(), "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //valida = false;
                mensaje = "FECHA LLEGADA INCORRECTA.";
                MessageBox.Show(mensaje, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtHoraLlegada.Text == "")
            {
                //MessageBox.Show(_HojaRutaLogic.MensajeBD(), "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //valida = false;
                mensaje = "HORA DE LLEGADA INCORRECTA.";
                MessageBox.Show(mensaje, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (valida == false)
            {
                MessageBox.Show(_HojaRutaLogic.MensajeBD(), "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            rpta = _HojaRutaLogic.FinalizarHojaRuta(1, txtNroHoja.Text.Trim(), txtFechaLlegada.Text.Trim()
                 , txtHoraLlegada.Text.Trim(), 1);
            if (rpta == true)
            {
                MessageBox.Show(_HojaRutaLogic.MensajeBD(), "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //__mensajeVale.Text = (_HojaRutaLogic.MensajeBD() + " :D").ToUpper();
                ////MessageBox.Show(_HojaRutaLogic.MensajeBD() + " :D");
                Listar(1, Convert.ToString(dateTimePicker1.Value), Convert.ToString(dateTimePicker2.Value)
                , "", "", 0, 0, estado);
            }
            else
            {
                MessageBox.Show(_HojaRutaLogic.MensajeBD(), "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //__mensajeVale.Text = (_HojaRutaLogic.MensajeBD() + " :(").ToUpper();
                //MessageBox.Show(_HojaRutaLogic.MensajeBD() + " :(");
            }
            //=======================================================

        }
    }
}
