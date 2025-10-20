using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace muNoteServer
{
	/// <summary>
	/// 기본 API 핸들러.
	/// </summary>
	[Route("/")]
	public class DefaultApiHandler : BaseApiHandler
	{
		/// <summary>
		/// 기본 요청 받음.
		/// </summary>
		[HttpGet]
		public IResult DefaultAsync()
		{
			return Results.Ok();
		}

		/// <summary>
		/// 간단한 요청 테스트용 요청 받음.
		/// </summary>
		[HttpGet("/ping")]
		private IResult Ping()
		{
			return Results.Ok();
		}
	}
}