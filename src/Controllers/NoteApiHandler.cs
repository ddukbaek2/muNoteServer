using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System;
using System.Threading.Tasks;


namespace muNoteServer
{
	/// <summary>
	/// 노트 API 핸들러.
	/// </summary>
	[Route("/note")]
	public class NoteApiHandler : BaseApiHandler
	{
		/// <summary>
		/// 노트 조회 요청 받음.
		/// </summary>
		[HttpGet("/{noteId}")]
		public async Task<IResult> GetNoteAsync(string noteId)
		{
			await Task.CompletedTask;
			return Results.Ok();
		}

		/// <summary>
		/// 노트 목록 조회 요청 받음.
		/// </summary>
		[HttpGet]
		public async Task<IResult> GetNoteAllAsync(string ownerUserId, string ownerWorkspaceId)
		{
			await Task.CompletedTask;
			return Results.Ok();
		}
		/// <summary>
		/// 노트 목록 검색 요청 받음.
		/// </summary>
		[HttpGet("/find")]
		public async Task<IResult> FindNoteAllAsync(string ownerUserId, string ownerWorkspaceId)
		{
			await Task.CompletedTask;
			return Results.Ok();
		}

		/// <summary>
		/// 노트 생성 요청 받음.
		/// </summary>
		[HttpPost]
		public async Task<IResult> CreateNoteAsync([FromBody] NoteCreateRequestModel requestModel)
		{
			var noteId = DatabaseHandler.CreateUniqueIdentifier();
			var ownerUserId = requestModel.UserId;
			var ownerWorkspaceId = requestModel.WorkspaceId;
			var ownerNoteId = requestModel.OwnerNoteId;
			var property = requestModel.Property.ToString();
			var type = requestModel.Type.ToString();
			var title = requestModel.Title;
			var timestamp = DatabaseHandler.CreateTimestamp();

			try
			{
				await ApplicationHandler.DatabaseHandler.InsertNoteTableAsync(noteId, ownerUserId, ownerWorkspaceId, ownerNoteId, property, type, timestamp);

				// 응답.
				var responseModel = new NoteCreateResponseModel();
				responseModel.RequestModel = requestModel;
				responseModel.NoteId = noteId;
				responseModel.CreateAt = timestamp;
				return Results.Created($"/note/{responseModel.NoteId}", responseModel);
			}
			catch (SqliteException exception)
			{
				Console.WriteLine(exception);
				return Results.Problem($"Database Error: {exception}");
			}
		}

		/// <summary>
		/// 노트 삭제 요청 받음.
		/// </summary>
		[HttpDelete("{noteId}")]
		public async Task<IResult> DeleteNoteAsync(string ownerUserId, string noteId)
		{
			await Task.CompletedTask;
			return Results.Ok();
		}
	}
}