using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Presentacion.Reutilizable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Logica
{
    public class CsExportar
    {
        //https://csharp.hotexamples.com/es/site/file?hash=0xbab5051b9c51f352d29ebb755d6921b2ec910baae6ca7f8d04ba9f66c6e06569&fullName=Form1.cs&project=Warrior6021/ReportesItextSharp

        CsReutilizar _CsReutlizar = new CsReutilizar();

        //=====================================================================================
        #region EXPORTAR DESDE WINFORM
        //========================================================================
        //EXPORTAR A EXCEL DESDE CLOSE XML
        #region EXPORTAR A EXCEL
        public void ExportarExcelDataGridView(DataGridView dgv, string NombreArchivo)
        {
            try
            {
                string Fecha = DateTime.Now.ToString("dd-MM-yyyy");
                string Hora = DateTime.Now.ToString("hhmmss");
                DataTable dt = new DataTable();

                //Adding the Columns  
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    dt.Columns.Add(column.HeaderText, column.ValueType);
                }
                //Adding the Rows  
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    dt.Rows.Add();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                    }
                }
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xlsx)|*.xlsx";
                //sfd.FileName = "export.xlsx";
                sfd.FileName = Convert.ToString(Fecha) + Hora + NombreArchivo + ".xlsx";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (!(dgv.RowCount == 0))
                    {
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(dt, "Sheet1");
                            wb.SaveAs(sfd.FileName);
                            wb.Dispose();
                            _CsReutlizar.MensajeCorrecto("Exportacion realizada correctamente");
                            if (MessageBox.Show("¿Desea abrir archivo?", "Sistema", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Exclamation) == DialogResult.Yes)
                            {
                                Process.Start(sfd.FileName);
                            }
                        }
                    }
                    else
                    {
                        _CsReutlizar.MensajeExclamacion("No hay datos para exportar");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }
        }
        #endregion
        //========================================================================

        //========================================================================
        //EXPORTAR A EXCEL DESDE ITEXSHARP
        #region EXPORTAR A PDF DESDE DATATABLE
        public void ExportarPDFDatatable(DataTable dgv, string NombreArchivo, string Titulo
         , string Descripcion, string Orientacion)
        {
            if (dgv.Rows.Count > 0)
            {
                string Fecha = DateTime.Now.ToString("dd-MM-yyyy");
                string Hora = DateTime.Now.ToString("hhmmss");
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = Convert.ToString(Fecha) + Hora + NombreArchivo + ".pdf";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {

                            PdfPTable pdfTable = new PdfPTable(dgv.Columns.Count);
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;


                            for (int k = 0; k < dgv.Columns.Count; k++)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(dgv.Columns[k].ColumnName));

                                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                                cell.BackgroundColor = new iTextSharp.text.BaseColor(51, 102, 102);

                                pdfTable.AddCell(cell);
                            }

                            // Add values of DataTable in pdf file
                            for (int i = 0; i < dgv.Rows.Count; i++)
                            {
                                for (int j = 0; j < dgv.Columns.Count; j++)
                                {
                                    PdfPCell cell = new PdfPCell(new Phrase(dgv.Rows[i][j].ToString()));

                                    //Align the cell in the center
                                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;

                                    pdfTable.AddCell(cell);
                                }
                            }

                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {

                                Document pdfDoc = new Document(PageSize.LETTER);


                                if (Orientacion == "Vertical")
                                {
                                    pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);
                                }
                                else if (Orientacion == "Horizontal")
                                {
                                    pdfDoc = new Document(PageSize.A4.Rotate());
                                }



                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                //==================================================
                                //AGREGAR TITULO Y OTRAS DESCRIPCIONES
                                Paragraph paragraph = new Paragraph(); //se crea el parrafo
                                paragraph.Font = new Font(FontFactory.GetFont("Tahoma", 15, BaseColor.BLACK));
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                paragraph.Add(Titulo);//agregar texto
                                pdfDoc.Add(paragraph);//lo metes al documento
                                paragraph.Clear();//limpiar el parrago para agregar mas texto
                                paragraph.Add(Descripcion);
                                pdfDoc.Add(paragraph);
                                paragraph.Clear();
                                paragraph.Add(" ");//aquia gregas un parrafo vacio ANTES DE METER TU TABLA
                                pdfDoc.Add(paragraph);
                                paragraph.Clear();
                                //==================================================

                                pdfDoc.Add(pdfTable);
                                pdfDoc.Close();
                                stream.Close();
                            }
                            _CsReutlizar.MensajeCorrecto("Exportacion realizada correctamente");
                            if (MessageBox.Show("¿Desea abrir archivo?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                            {
                                Process.Start(sfd.FileName);
                            }

                        }
                        catch (Exception ex)
                        {
                            //Respuesta = 0;
                            //Mensaje = "Error" + ex;
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                _CsReutlizar.MensajeExclamacion("No hay datos para exportar");
                //Respuesta = 0;
                //Mensaje = ("No hay datos para exportar");
            }
        }
        #endregion
        //========================================================================

        //========================================================================
        //EXPORTAR A EXCEL DESDE ITEXSHARP
        #region EXPORTAR A PDF
        public void ExportarPDF(DataGridView dgv, string NombreArchivo, string Titulo
            , string Descripcion, string Orientacion)
        {
            if (dgv.Rows.Count > 0)
            {
                string Fecha = DateTime.Now.ToString("dd-MM-yyyy");
                string Hora = DateTime.Now.ToString("hhmmss");
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = Convert.ToString(Fecha) + Hora + NombreArchivo + ".pdf";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            PdfPTable pdfTable = new PdfPTable(dgv.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            float[] headerwidths = GetTamañoColumnas(dgv);
                            pdfTable.SetWidths(headerwidths);
                            pdfTable.WidthPercentage = 100;
                            //pdfTable.DefaultCell.BorderWidth = 2;
                            pdfTable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in dgv.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                //cell.Column.ro
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dgv.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                { pdfTable.AddCell(cell.Value.ToString());  }
                            }

                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.LETTER);

                                // Add meta information to the document  
                                pdfDoc.AddAuthor("ANC");
                                //pdfDoc.AddCreator("Pdf ");
                                //pdfDoc.AddKeywords("PDF tutorial education");
                                //pdfDoc.AddSubject("Document subject - Describing the steps creating a PDF document");
                                pdfDoc.AddTitle(Titulo);

                                //===============================================
                                //ORIENTACION DE PDF 
                                if (Orientacion == "Vertical")
                                { pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f); }
                                else if (Orientacion == "Horizontal")
                                { pdfDoc = new Document(PageSize.A4.Rotate()); }
                                //===============================================

                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                //==================================================
                                //AGREGAR TITULO Y OTRAS DESCRIPCIONES
                                Paragraph paragraph = new Paragraph(); //se crea el parrafo
                                paragraph.Font = new Font(FontFactory.GetFont("Tahoma", 15, BaseColor.RED));
                                paragraph.Alignment = Element.ALIGN_LEFT;
                                paragraph.Add(Titulo);//agregar texto
                                pdfDoc.Add(paragraph);//lo metes al documento
                                paragraph.Clear();//limpiar el parrago para agregar mas texto
                                paragraph.Add(Descripcion);
                                pdfDoc.Add(paragraph);
                                paragraph.Clear();
                                paragraph.Add(" ");//aquia gregas un parrafo vacio ANTES DE METER TU TABLA
                                pdfDoc.Add(paragraph);
                                paragraph.Clear();
                                //==================================================

                                pdfDoc.Add(pdfTable);
                                //pdfDoc.Add(new Paragraph("______________________________________________", FontFactory.GetFont("ARIAL", 20, iTextSharp.text.Font.BOLD)));
                                //pdfDoc.Add(new Paragraph("Firma", FontFactory.GetFont("ARIAL", 20, iTextSharp.text.Font.BOLD)));

                                pdfDoc.Close();
                                stream.Close();
                            }
                            _CsReutlizar.MensajeCorrecto("Exportacion realizada correctamente");
                            if (MessageBox.Show("¿Desea abrir archivo?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                            {
                                Process.Start(sfd.FileName);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                _CsReutlizar.MensajeExclamacion("No hay datos para exportar");
            }
        }
        #endregion

        public float[] GetTamañoColumnas(DataGridView dg)
        {
            float[] values = new float[dg.ColumnCount];
            for (int i = 0; i < dg.ColumnCount; i++)
            {
                values[i] = (float)dg.Columns[i].Width;
            }
            return values;
        }
        //=====================================================================================
        #endregion





    }
}
