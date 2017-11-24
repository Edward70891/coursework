using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace restSharpTesting
{
	class restAPI
	{
		const string clientID = "EdwardDe-schoolwo-SBX-bb7edec1b-55d6f224";
		const string appID = "SBX-b7edec1b0639-43d4-48dc-bdda-d964";
		RestClient client;
		public restAPI(string username, string password)
		{
			client = new RestClient();
			client.BaseUrl = new Uri("https://api.sandbox.ebay.com");
			client.Authenticator = new HttpBasicAuthenticator(username, password);
		}
	}
}
