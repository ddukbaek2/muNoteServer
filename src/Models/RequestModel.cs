using System.ComponentModel.DataAnnotations;

namespace muNoteServer
{
	/// <summary>
	/// 요청 모델.
	/// </summary>
	public class RequestModel : BaseModel
	{
		/// <summary>
		/// 사용자 고유 식별자. (uid)
		/// </summary>
		[Required]
		public string UserId { set; get; }

		/// <summary>
		/// 작업공간 고유 식별자. (wid)
		/// </summary>
		[Required]
		public string WorkspaceId { set; get; }
	}
}