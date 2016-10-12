using System.Linq;
using System.Diagnostics;
using System.Configuration;

namespace SW.DatabaseCheckout.Util
{
	public static class Util
	{
		public static bool IsProcessRunning(string processName)
		{
			Process[] processlist = Process.GetProcesses();
			return processlist.Any(p => p.ProcessName == processName);
		}
		public static void PostMessageToSlack(string message)
		{
			string webhookUrl = ConfigurationManager.AppSettings["WebhookURL"];
			if (!string.IsNullOrEmpty(webhookUrl))
			{
				SlackClient client = new SlackClient(webhookUrl);
				client.PostMessage(text: message);
			}
		}
	}
}