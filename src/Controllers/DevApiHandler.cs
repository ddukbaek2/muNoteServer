using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace muNoteServer
{
	/// <summary>
	/// 개발용 API 핸들러.
	/// </summary>
	[Route("/dev")]
	public class DevApiHandler : BaseApiHandler
	{
	}
}