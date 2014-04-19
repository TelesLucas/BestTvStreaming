/*
 * Criado por Lucas Teles
 * Data: 17/04/2014
 * Hora: 11:44
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace BestTvStreaming
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		//Método da API
		[DllImport("wininet.dll")]
		private extern static Boolean InternetGetConnectedState(out int Description, int ReservedValue);

		// Um método que verifica se esta conectado com a internet
		public static Boolean IsConnected()
		{
			int Description;
			return InternetGetConnectedState(out Description, 0);
		}
		
		//Localizar diretorio de execução do programa
		string diretorio = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);

		//Metodo para abrir o canal		
        public void AbrirCanal(object sender, EventArgs e)
		{
            string canal = "data/" + (sender as ToolStripItem).Text + ".html";
            lbl_canal.Text = (sender as ToolStripItem).Text;
			string LocalCanal = Path.Combine(diretorio, canal);
			Navegador.Navigate(new Uri(LocalCanal));
		}

		public MainForm()
		{
			InitializeComponent();
		}
		

        private void sair(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void travou(object sender, EventArgs e)
        {
            Navegador.Refresh();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (IsConnected())
            {
                Navegador.Navigate(new Uri("https://googledrive.com/host/0B5evPZjGs3qeNFkwTm80OWg0em8/d.html"));
            }
            else
            {
                MessageBox.Show("Não foi possivel conectar com a internet! verifique sua conexão!",
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
	}
}
