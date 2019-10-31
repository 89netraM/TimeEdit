using System;
using System.Collections.Immutable;

namespace MoreTec.TimeEditApi
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

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			if (!(obj is ScheduleEntry other))
			{
				return false;
			}
			else
			{
				return this == other;
			}
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public static bool operator ==(ScheduleEntry left, ScheduleEntry right)
		{
			return left.Id == right.Id && left.StartTime == right.StartTime &&
				left.EndTime == right.EndTime && left.Columns.Equals(right.Columns);
		}

		public static bool operator !=(ScheduleEntry left, ScheduleEntry right)
		{
			return !(left == right);
		}
	}
}