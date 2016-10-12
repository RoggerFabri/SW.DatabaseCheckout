using Microsoft.AspNet.SignalR.Client;
using System;
using System.Net.Http;
using System.Windows.Forms;
using SW.DatabaseCheckout.Client.Properties;
using System.Threading;
using System.Configuration;

namespace SW.DatabaseCheckout.Client
{
	public partial class WinFormsClient : Form
	{
		private string UserName { get; set; }
		private bool Locked { get; set; }
		private bool Connected { get; set; }
		private IHubProxy HubProxy { get; set; }
		private string ServerURI = string.Empty;
		private ContextMenu contextMenu;
		private MenuItem menuItem;
		private MenuItem menuItemConnect;

		private HubConnection Connection { get; set; }

		internal WinFormsClient()
		{
			ServerURI = ConfigurationManager.AppSettings["SignalRServer"];
			InitializeComponent();
		}

		private async void ConnectAsync()
		{
			Connection = new HubConnection(ServerURI);
			Connection.Closed += Connection_Closed;
			HubProxy = Connection.CreateHubProxy("SingleLockHub");

			HubProxy.On<string>("SetLock", (userName) =>
				this.Lock(userName)
			);

			HubProxy.On<string>("SetUnlock", (userName) =>
				this.Unlock(userName)
			);

			try
			{
				await Connection.Start();
			}
			catch (HttpRequestException)
			{
				var errorTooltipText = Resources.ServerUnreachableMessage;
				trayIcon.ShowBalloonTip(TimeSpan.FromSeconds(15).Milliseconds, Resources.TooltipTitle, errorTooltipText, ToolTipIcon.Error);
				return;
			}

			Connected = true;
			menuItemConnect.Enabled = false;
			var tooltipText = Resources.TooltipTitle;
			trayIcon.Text = "Connected";
			trayIcon.ShowBalloonTip(TimeSpan.FromSeconds(3).Milliseconds, Resources.TooltipTitle, trayIcon.Text, ToolTipIcon.Info);
		}

		private void Lock(string userName)
		{
			var tooltipText = string.Format(Resources.XLockMessage, userName);
			trayIcon.Text = string.Format(Resources.XTrabalhandoNoModelo, userName);
			trayIcon.Icon = Resources.database_delete;
			trayIcon.ShowBalloonTip(TimeSpan.FromSeconds(15).Milliseconds, Resources.TooltipTitle, tooltipText, ToolTipIcon.Warning);
		}

		private void Unlock(string userName)
		{
			var tooltipText = string.Format(Resources.XUnlockMessage, userName);
			trayIcon.Text = string.Format(Resources.XTrabalhandoNoModelo, Resources.Ninguem);
			trayIcon.Icon = Resources.database_add;
			trayIcon.ShowBalloonTip(TimeSpan.FromSeconds(15).Milliseconds, Resources.TooltipTitle, tooltipText, ToolTipIcon.Warning);
		}

		private void Connection_Closed()
		{
			trayIcon.Visible = false;
			Application.Exit();
		}

		private void WinFormsClient_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (Connection != null)
			{
				Connection.Stop();
				Connection.Dispose();
			}

			trayIcon.Visible = false;
		}

		private void WinFormsClient_Load(object sender, EventArgs e)
		{
			contextMenu = new ContextMenu();
			menuItem = new MenuItem();
			menuItemConnect = new MenuItem();

			contextMenu.MenuItems.AddRange(new MenuItem[] { this.menuItem, this.menuItemConnect });

			menuItem.Index = 1;
			menuItem.Text = "E&xit";
			menuItem.Click += new EventHandler(menuItem_Click);

			menuItemConnect.Index = 0;
			menuItemConnect.Text = "C&onnect";
			menuItemConnect.Click += new EventHandler(menuItemConnect_Click);

			trayIcon.ContextMenu = contextMenu;
			trayIcon.Text = string.Format(Resources.XTrabalhandoNoModelo, Resources.Ninguem);
			UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
			Thread.Sleep(TimeSpan.FromSeconds(5).Milliseconds);

			ConnectAsync();
		}

		private void menuItem_Click(object sender, EventArgs e)
		{
			if (Connection != null)
			{
				Connection.Stop();
				Connection.Dispose();
			}

			trayIcon.Visible = false;
			Application.Exit();
		}

		private void menuItemConnect_Click(object sender, EventArgs e)
		{
			ConnectAsync();
		}

		private void timerProcessWatcher_Tick(object sender, EventArgs e)
		{
			if (Connected)
			{
				if (!Locked)
				{
					if (Util.Util.IsProcessRunning("pdshell15"))
					{
						Locked = true;
						HubProxy.Invoke("SetLock", UserName);
					}
				}
				else
				{
					if (!Util.Util.IsProcessRunning("pdshell15"))
					{
						Locked = false;
						HubProxy.Invoke("SetUnlock", UserName);
					}
				}
			}
		}
	}
}