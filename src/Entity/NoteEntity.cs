namespace muNoteServer
{
	/// <summary>
	/// 노트 엔티티.
	/// </summary>
	public class NoteEntity : BaseEntity
	{
		/// <summary>
		/// 작업공간 식별자.
		/// </summary>
		public string WorkspaceId { get; }
	}
}