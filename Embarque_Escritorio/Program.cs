using Embarque_Escritorio.BDPasajes;
using Embarque_Escritorio.SqlDependency;
using Embarque_Escritorio.Sunat.TipoCambio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Embarque_Escritorio.SubirExcel;
//using Embarque_Escritorio.Listar;



namespace Embarque_Escritorio
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmMenuPasajes());
            Application.Run(new FrmExcel());
        }
    }
}
