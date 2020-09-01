using Cycling_demo.Data;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cycling_demo.Hubs
{
	public class ridersCount
	{
		public int TourRiders { get; set; }
		public int ParisRiders { get; set; }

		public ridersCount(int tourRiders, int parisRiders)
		{
			this.TourRiders = tourRiders;
			this.ParisRiders = parisRiders;
		}
	}
	public class UpdateHub : Hub
	{
		private readonly IRidersDB _ridersDB;

		public UpdateHub(IRidersDB ridersDB)
		{
			_ridersDB = ridersDB;
		}

		public async Task OnConnect()
		{
			await Clients.Client(Context.ConnectionId).SendAsync("updateRiderCount", new ridersCount(_ridersDB.GetTourRiders().Count(), _ridersDB.GetParisRiders().Count()));
		}

		public async Task UpdateRiderCount()
		{
			await Clients.All.SendAsync("updateRiderCount", new ridersCount(_ridersDB.GetTourRiders().Count(), _ridersDB.GetParisRiders().Count()));
		}

		public async Task alertGroupListUpdated(string tab)
		{
			await Clients.GroupExcept(tab, Context.ConnectionId).SendAsync("listUpdated");
		}

		public async Task joinGroup(string tab)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, tab); 
		}
	}
}
