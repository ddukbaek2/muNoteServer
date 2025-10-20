using System;


namespace muNoteServer
{
	/// <summary>
	/// 노트 속성. (조합)
	/// </summary>
	[Flags]
	public enum NoteProperty : ulong
	{
		/// <summary>
		/// 없음. (기본)
		/// </summary>
		Nothing = 0,

		/// <summary>
		/// 노트의 내용을 보여줄 지 여부.
		/// </summary>
		VisibleContent = 1 << 0,

		/// <summary>
		/// 계층을 따라 내용을 보여줄 지 여부. (VisibleContent가 우선)
		/// </summary>
		VisibleContentOnHierarchy = 1 << 1,
	}
}