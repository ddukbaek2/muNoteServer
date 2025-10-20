using Crockhead.Core;


namespace muNoteServer
{
	/// <summary>
	/// 노트 내용.
	/// </summary>
	public class NoteContent : Disposable
	{
		/// <summary>
		/// 값.
		/// </summary>
		public string Value { set; get; }

		/// <summary>
		/// 생성됨.
		/// </summary>
		public NoteContent() : base()
		{
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
		}
	}
}