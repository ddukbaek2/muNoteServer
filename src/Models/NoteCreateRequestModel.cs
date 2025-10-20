using System.ComponentModel.DataAnnotations;

namespace muNoteServer
{
	/// <summary>
	/// 노트 생성 요청 모델.
	/// </summary>
	public sealed class NoteCreateRequestModel : RequestModel
	{
		/// <summary>
		/// 노트 이름. (제목)
		/// </summary>
		[Required]
		public string Title { set; get; }

		/// <summary>
		/// 노트 속성.
		/// </summary>
		[Required]
		public NoteProperty Property { set; get; }

		/// <summary>
		/// 노트 종류.
		/// </summary>
		[Required]
		public NoteType Type { set; get; }

		/// <summary>
		/// 상위 노트 식별자. (nid)
		/// </summary>
		public string OwnerNoteId { set; get; }
	}
}