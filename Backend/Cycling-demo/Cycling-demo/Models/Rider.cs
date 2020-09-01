using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cycling_demo.Models
{
	public class Rider
	{
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Age { get; set; }
		public string Team { get; set; }

		public Rider(string firstname, string lastname, string age, string team)
		{
			this.Firstname = firstname;
			this.Lastname = lastname;
			this.Age = age;
			this.Team = team;
		}
	}
}
