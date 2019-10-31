using System.Collections.Immutable;

namespace TimeEdit
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