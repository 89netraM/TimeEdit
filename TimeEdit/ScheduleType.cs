namespace TimeEdit
{
	public readonly struct ScheduleType
	{
		public string Name { get; }
		public int Id { get; }

		public ScheduleType(string name, int id)
		{
			Name = name;
			Id = id;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			if (!(obj is ScheduleType other))
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
			return Name.GetHashCode() + Id;
		}

		public static bool operator ==(ScheduleType left, ScheduleType right)
		{
			return left.Name == right.Name && left.Id == right.Id;
		}

		public static bool operator !=(ScheduleType left, ScheduleType right)
		{
			return !(left == right);
		}
	}
}