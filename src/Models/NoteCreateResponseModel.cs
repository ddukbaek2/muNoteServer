using System;


namespace muNoteServer
{
	/// <summary>
	/// 노트 생성 응답 모델.
	/// </summary>
	public sealed class NoteCreateResponseModel : BaseModel
	{
		/// <summary>
		/// 요청.
		/// </summary>
		public NoteCreateRequestModel RequestModel { set; get; }

		/// <summary>
		/// 노트의 고유 식별자. (nid)
		/// </summary>
		public string NoteId { set; get; }

		/// <summary>
		/// 생성일.
		/// </summary>
		public string CreateAt { set; get; }
	}
}