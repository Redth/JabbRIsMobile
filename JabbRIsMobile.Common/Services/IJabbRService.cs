using System;
using Cirrious.MvvmCross;
using JabbR.Client;
using System.Collections.Generic;

namespace JabbRIsMobile.Common.Services
{
	public interface IJabbRService
	{
		IEnumerable<JabbRClient> Clients { get;set; }
	
		void AddClient(string host, string user, string pass);
		void RemoveClient(string host, string user, string pass);
	}
	
}