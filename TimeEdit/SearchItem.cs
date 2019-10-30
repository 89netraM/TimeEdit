namespace TimeEdit
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
	}
}