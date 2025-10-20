namespace muNoteServer
{
	/// <summary>
	/// 응답 핸들러.
	/// </summary>
	public class BaseResponseModel<TResult> : BaseModel
	{
		/// <summary>
		/// 결과 프로퍼티.
		/// </summary>
		public TResult Result { set; get; }

		/// <summary>
		/// 오류 메시지 프로퍼티.
		/// </summary>
		public string Error { set; get; }
	}
}