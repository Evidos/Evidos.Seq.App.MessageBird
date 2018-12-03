using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MessageBird.API.Messages
{
	public class TypeDetails
	{
		[JsonProperty("udh")]
		public string UDH { get; set; }

		public string Keyword { get; set; }

		public int Shortcode { get; set; }

		public int Tariff { get; set; }

		public bool Member { get; set; }

		public int Mid { get; set; }
	}
}
