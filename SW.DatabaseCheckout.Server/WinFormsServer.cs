using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SW.DatabaseCheckout.Server
{
	public partial class WinFormsServer : Form
	{
		private IDisposable SignalR { get; set; }
		const string ServerURI = "http://localhost:8080";

		internal WinFormsServer()
		{
			InitializeComponent();
		}

		private void ButtonStart_Click(object sender, EventArgs e)
		{
			WriteToConsole("Starting server...");
			ButtonStart.Enabled = false;
			Task.Run(() => StartServer());
		}

		private void ButtonStop_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void StartServer()
		{
			try
			{
				SignalR = WebApp.Start(ServerURI);
			}
			catch (TargetInvocationException)
			{
				WriteToConsole("Server failed to start. A server is already running on " + ServerURI);
				this.Invoke((Action)(() => ButtonStart.Enabled = true));
				return;
			}
			this.Invoke((Action)(() => ButtonStop.Enabled = true));
			WriteToConsole("Server started at " + ServerURI);
		}

		internal void WriteToConsole(String message)
		{
			if (RichTextBoxConsole.InvokeRequired)
			{
				this.Invoke((Action)(() =>
					WriteToConsole(message)
				));
				return;
			}
			RichTextBoxConsole.AppendText(message + Environment.NewLine);
		}

		private void WinFormsServer_FormClosing(object sender, FormClosingEventArgs e)
		{

			if (SignalR != null)
			{
				SignalR.Dispose();
			}
		}

		private void WinFormsServer_Load(object sender, EventArgs e)
		{
			StartServer();
		}
	}

	class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.UseCors(CorsOptions.AllowAll);
			app.MapSignalR();
		}
	}
}