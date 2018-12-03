using System;
using System.Collections.Concurrent;

namespace Evidos.Seq.App.Messagebird
{
	public class MessageSuppressions
	{
		private readonly ConcurrentDictionary<uint, DateTime> _lastSeen = new ConcurrentDictionary<uint, DateTime>();
		private readonly int suppressionMinutes;

		public MessageSuppressions(int suppressionMinutes)
		{
			this.suppressionMinutes = suppressionMinutes;
		}

		public bool ShouldSuppressAt(uint eventType, DateTime utcNow)
		{
			var added = false;
			var lastSeen = _lastSeen.GetOrAdd(eventType, k => { added = true; return DateTime.UtcNow; });
			if (!added)
			{
				if (lastSeen > utcNow.AddMinutes(-suppressionMinutes))
					return true;

				_lastSeen[eventType] = utcNow;
			}

			return false;
		}
	}
}