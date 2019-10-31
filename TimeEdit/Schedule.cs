using System.Collections.Immutable;

namespace MoreTec.TimeEditApi
{
	public readonly struct Schedule
	{
		public IImmutableList<string> ColumnNames { get; }
		public IImmutableList<ScheduleEntry> Entries { get; }

		public Schedule(IImmutableList<string> columnNames, IImmutableList<ScheduleEntry> entries)
		{
			ColumnNames = columnNames;
			Entries = entries;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			if (!(obj is Schedule other))
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
			return ColumnNames.GetHashCode() + Entries.GetHashCode();
		}

		public static bool operator ==(Schedule left, Schedule right)
		{
			return left.ColumnNames.Equals(right.ColumnNames) && left.Entries.Equals(right.Entries);
		}

		public static bool operator !=(Schedule left, Schedule right)
		{
			return !(left == right);
		}
	}
}