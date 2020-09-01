using Cycling_demo.Data;
using Cycling_demo.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.OpenApi.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cycling_demo.Hubs
{
	// Enkel nodig in deze class
	internal enum SignalRMethod
	{
		[Display(Name = "updateRiderCount")]
		UpdateRiderCount = 1,

		[Display(Name = "ListUpdated")]
		ListUpdated = 2
	}

	public enum Tab
	{
		Tour = 1,
		Paris = 2
	}

	public class UpdateHub : Hub
	{
		private readonly IRidersDB _ridersDB;

		public UpdateHub(IRidersDB ridersDB)
		{
			_ridersDB = ridersDB;
		}

		// Custom methods
		public async Task OnConnect()
		{
			await Clients.Client(Context.ConnectionId).SendAsync(SignalRMethod.UpdateRiderCount.GetDisplayName(), new RidersCount(_ridersDB.GetTourRiders().Count(), _ridersDB.GetParisRiders().Count()));
		}

		public async Task UpdateRiderCount()
		{
			await Clients.All.SendAsync(SignalRMethod.UpdateRiderCount.GetDisplayName(), new RidersCount(_ridersDB.GetTourRiders().Count(), _ridersDB.GetParisRiders().Count()));
		}

		public async Task AlertGroupListUpdated(Tab tab)
		{
			await Clients.GroupExcept(tab.GetDisplayName(), Context.ConnectionId).SendAsync(SignalRMethod.ListUpdated.GetDisplayName());
		}

		// Join Group and Leave Group
		public async Task JoinGroup(Tab tab)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, tab.GetDisplayName());
		}
		public async Task LeaveGroup(Tab tab)
		{
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, tab.GetDisplayName());
		}
	}
}
