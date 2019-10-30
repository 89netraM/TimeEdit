using System;
using System.Collections.Immutable;

namespace TimeEdit
{
	public readonly struct ScheduleEntry
	{
		public string Id { get; }
		public DateTime StartTime { get; }
		public DateTime EndTime { get; }
		public IImmutableDictionary<string, string[]> Columns { get; }

		public ScheduleEntry(string id, DateTime startTime, DateTime endTime, IImmutableDictionary<string, string[]> columns)
		{
			Id = id;
			StartTime = startTime;
			EndTime = endTime;
			Columns = columns;
		}
	}
}