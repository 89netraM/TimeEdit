namespace MoreTec.TimeEditApi
{
	public readonly struct SearchItem
	{
		public int Id { get; }
		public int TypeId { get; }
		public string Name { get; }

		public SearchItem(int id, int typeId, string name)
		{
			Id = id;
			TypeId = typeId;
			Name = name;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			if (!(obj is SearchItem other))
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
			return Id + TypeId + Name.GetHashCode();
		}

		public static bool operator ==(SearchItem left, SearchItem right)
		{
			return left.Id == right.Id && left.TypeId == right.TypeId && left.Name == right.Name;
		}

		public static bool operator !=(SearchItem left, SearchItem right)
		{
			return !(left == right);
		}
	}
}