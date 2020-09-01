using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cycling_demo.Data;
using Cycling_demo.Hubs;
using Cycling_demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Cycling_demo.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class RidersController : Controller
	{
		private readonly IRidersDB _ridersDB;
		private readonly IHubContext<UpdateHub> _hubContext;

		public RidersController(IRidersDB ridersDB, IHubContext<UpdateHub> hubContext)
		{
			_ridersDB = ridersDB;
			_hubContext = hubContext;
		}

		[HttpGet]
		[Route("tour")]
		public List<Rider> GetTour()
		{
			return _ridersDB.GetTourRiders();
		}

		[HttpGet]
		[Route("paris")]
		public List<Rider> GetParis()
		{
			return _ridersDB.GetParisRiders();
		}

		[HttpGet]
		[Route("add/tour")]
		public List<Rider> AddTour()
		{
			return _ridersDB.AddTourRider();
		}

		[HttpGet]
		[Route("add/paris")]
		public List<Rider> AddParis()
		{
			return _ridersDB.AddParisRider();
		}
	}
}
