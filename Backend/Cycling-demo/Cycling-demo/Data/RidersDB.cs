using Cycling_demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cycling_demo.Data
{
	public interface IRidersDB
	{
		List<Rider> GetTourRiders();
		List<Rider> GetParisRiders();
		List<Rider> AddTourRider();
		List<Rider> AddParisRider();
	}

	public class RidersDB: IRidersDB
	{
		List<Rider> RidersTour = new List<Rider>()
		{
			new Rider("Chris", "Froome", "31", "Ineos"),
			new Rider("Adam", "Yates", "30", "Lotto Jumbo"),
			new Rider("Tom", "Dumolin", "32", "Lotto Jumbo"),
			new Rider("Peter", "Sagan", "31", "Bora Hansgrohe")
		};

		List<Rider> RidersParis = new List<Rider>()
		{
			new Rider("Philippe", "Gilbert", "31", "Lotto Soedal"),
			new Rider("Peter", "Sagan", "30", "Quickstep"),
			new Rider("Zdenek", "Stybar", "32", "Quickstep"),
			new Rider("Fabian", "Cancellara", "31", "Treck Segafredo")
		};

		List<Rider> RidersRandom = new List<Rider>()
		{
			new Rider("Tom", "Boonen", "31", "Quickstep"),
			new Rider("Nikki", "Terpstra", "30", "CCC"),
			new Rider("Hector", "Carretero", "31", "Movistar"),
			new Rider("Greg", "Van Avermaet", "35", "CCC"),
			new Rider("Rafał", "Majka", "30", "Bora Hansgrohe"),
			new Rider("Michał", "Kwiatkowski", "30", "Ineos"),
			new Rider("Rui", "Costa", "30", "nvt"),
			new Rider("Simon", "Gerrans", "30", "nvt"),
			new Rider("Matti", "Breschel", "30", "nvt"),
			new Rider("Cadel", "Evans", "30", "nvt")
		};

		public RidersDB()
		{

		}

		public List<Rider> GetTourRiders()
		{
			return RidersTour;
		}
		public List<Rider> GetParisRiders()
		{
			return RidersParis;
		}

		public List<Rider> AddTourRider()
		{
			Random rnd = new Random();
			int r = rnd.Next(RidersRandom.Count);
			RidersTour.Add(RidersRandom[r]);
			return RidersTour;
		}

		public List<Rider> AddParisRider()
		{
			Random rnd = new Random();
			int r = rnd.Next(RidersRandom.Count);
			RidersParis.Add(RidersRandom[r]);
			return RidersParis;
		}
	}
}
