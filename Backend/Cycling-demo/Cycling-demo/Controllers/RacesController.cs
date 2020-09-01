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

	// THIS CONTROLLER IS ONLY FOR SWAGGER!
	// THIS CONTROLLER IS ONLY FOR SWAGGER!
	// THIS CONTROLLER IS ONLY FOR SWAGGER!
	// THIS CONTROLLER IS ONLY FOR SWAGGER!
	// THIS CONTROLLER IS ONLY FOR SWAGGER!
	// THIS CONTROLLER IS ONLY FOR SWAGGER!
	// THIS CONTROLLER IS ONLY FOR SWAGGER!

	[ApiController]
	[Route("[controller]")]
	public class RacesController : Controller
	{
		private readonly IRidersDB _ridersDB;

		public RacesController(IRidersDB ridersDB)
		{
			_ridersDB = ridersDB;
		}

		[HttpGet]
		[Route("get")]
		public List<Race> GetRaces()
		{
			return new List<Race>();
		}
	}
}
