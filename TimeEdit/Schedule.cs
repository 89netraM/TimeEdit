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
	}
}