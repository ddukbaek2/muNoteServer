using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace muNoteServer
{
	/// <summary>
	/// 사용자 API 핸들러.
	/// </summary>
	[Route("/user")]
	public class UserApiHandler : BaseApiHandler
	{
		/// <summary>
		/// 유저 생성 요청 받음.
		/// </summary>
		[HttpPost]
		public async Task<IResult> CreateUserAsync(string email, string password, string name)
		{
			//ApplicationHandler.DatabaseHandler.cREATE
			await Task.CompletedTask;
			return Results.Ok();
		}
	}
}