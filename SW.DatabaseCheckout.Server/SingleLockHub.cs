using Microsoft.AspNet.SignalR;
using SW.DatabaseCheckout.Server.Properties;
using System.Threading.Tasks;

namespace SW.DatabaseCheckout.Server
{
	public class SingleLockHub : Hub
	{
		static bool Locked = false;
		static string LockedBy = string.Empty;
		public void SetLock(string userName)
		{
			if (!Locked)
			{
				Locked = true;
				LockedBy = userName;
				Clients.All.setLock(userName);
				Util.Util.PostMessageToSlack(string.Format(Resources.XTrabModelo, userName));
			}
		}

		public void SetUnlock(string userName)
		{
			if (Locked)
			{
				if (userName == LockedBy)
				{
					Locked = false;
					LockedBy = string.Empty;
					Clients.All.setUnlock(userName);
					Util.Util.PostMessageToSlack(string.Format(Resources.XNaoTrabModelo, userName));
				}
			}
		}

		public override Task OnConnected()
		{
			return base.OnConnected();
		}
		public override Task OnDisconnected(bool stopCalled)
		{
			return base.OnDisconnected(stopCalled);
		}
	}
}