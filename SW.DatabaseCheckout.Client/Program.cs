﻿using System;
using System.Windows.Forms;

namespace SW.DatabaseCheckout.Client
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new WinFormsClient());
		}
	}
}