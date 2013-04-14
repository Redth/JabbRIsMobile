using System;
using Cirrious.MvvmCross;
using JabbR.Client;
using System.Collections.Generic;

namespace JabbRIsMobile.Common.Services
{
	public class JabbRService
	{
		List<JabbRClient> clients = new List<JabbRClient>();

		public IEnumerable<JabbRClient> Clients
		{
			get { return clients; }
		}

		public void AddClient(string url)
		{
			clients.Add(new JabbRClient(url));
		}

		
	}
	
}