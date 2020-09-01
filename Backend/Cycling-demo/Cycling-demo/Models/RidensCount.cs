using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cycling_demo.Models
{
	public class RidersCount
	{
		public int TourRiders { get; set; }
		public int ParisRiders { get; set; }

		public RidersCount(int tourRiders, int parisRiders)
		{
			this.TourRiders = tourRiders;
			this.ParisRiders = parisRiders;
		}
	}
}
