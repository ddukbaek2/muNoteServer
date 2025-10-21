using System.Threading.Tasks;


namespace muNoteServer
{
	/// <summary>
	/// 데이터베이스 핸들러.
	/// </summary>
	public class DatabaseHandler : BaseDatabaseHandler
	{
		/// <summary>
		/// 사용자 테이블 생성.
		/// </summary>
		public const string CreateUserTableString =
			@"CREATE TABLE IF NOT EXISTS User (
				UserId TEXT PRIMARY KEY,
				Name TEXT NOT NULL,
				Email TEXT NOT NULL,
				Password TEXT NOT NULL,
				CreateAt TEXT NOT NULL DEFAULT (datetime('now')),
				UpdateTime TEXT NOT NULL DEFAULT (datetime('now'))
			);";

		/// <summary>
		/// 작업공간 테이블 생성.
		/// </summary>
		public const string CreateWorkspaceTableString =
			@"CREATE TABLE IF NOT EXISTS Workspace (
				WorkspaceId TEXT PRIMARY KEY,
				OwnerUserId TEXT NOT NULL,
				Name TEXT NOT NULL,
				CreateAt TEXT NOT NULL DEFAULT (datetime('now')),
				UpdateTime TEXT NOT NULL DEFAULT (datetime('now'))
			);";

		/// <summary>
		/// 노트 테이블 생성.
		/// </summary>
		public const string CreateNoteTableString =
			@"CREATE TABLE IF NOT EXISTS Note (
				NoteId TEXT PRIMARY KEY,
				OwnerUserId TEXT TEXT NOT NULL,
				OwnerWorksapceId TEXT NOT NULL,
				OwnerNoteId TEXT NULL,
				Type TEXT NULL,
				Property TEXT NULL,
				Title TEXT NULL,
				Content TEXT NULL,
				CreateAt TEXT NOT NULL DEFAULT (datetime('now')),
				UpdateTime TEXT NOT NULL DEFAULT (datetime('now'))
			);";

		/// <summary>
		/// 노트 테이블에 레코드 추가.
		/// </summary>
		public const string InsertNoteTableString =
			@"INSERT INTO Note (
				NoteId,
				OwnerUserId,
				OwnerWorkspaceId,
				OwnerNoteId,
				Type,
				Property,
				Title,
				Content,
				CreateTime,
				UpdateTime
			) VALUES (
				@NoteId,
				@UserId,
				@WorkspaceId,
				@OwnerNoteId,
				@Type,
				@Property,
				@Title,
				@Content,
				@CreateTime,
				@UpdateTime
			);";

		/// <summary>
		/// 노트 검색.
		/// </summary>
		public const string SearchNoteString =
			@"WITH RECURSIVE Tree (NoteId, Title, OwnerNoteId) AS (
				SELECT NoteId, Title, OwnerNoteId FROM Note WHERE NoteId = @root
				UNION ALL
				SELECT n.NoteId, n.Title, n.OwnerNoteId FROM Note n
				JOIN tree t ON n.OwnerNoteId = t.NoteId
			)
			SELECT * FROM Tree;";

		/// <summary>
		/// 생성됨.
		/// </summary>
		public DatabaseHandler() : base("muNoteServer.db")
		{
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
			base.OnDispose(explicitDisposing);
		}

		/// <summary>
		/// 사용자 테이블 생성.
		/// </summary>
		public async Task CreateUserTableAsync()
		{
			using var connection = await ConnectAsync();
			await RequestQueryAsync(connection, DatabaseHandler.CreateUserTableString);
		}

		/// <summary>
		/// 작업공간 테이블 생성.
		/// </summary>
		public async Task CreateWorkspaceTableAsync()
		{
			using var connection = await ConnectAsync();
			await RequestQueryAsync(connection, DatabaseHandler.CreateWorkspaceTableString);
		}

		/// <summary>
		/// 노트 테이블 생성.
		/// </summary>
		public async Task CreateNoteTableAsync()
		{
			using var connection = await ConnectAsync();
			await RequestQueryAsync(connection, DatabaseHandler.CreateNoteTableString);
		}

		/// <summary>
		/// 노트 목록 반환.
		/// </summary>
		public async Task GetNotesAsync()
		{
			using var connection = await ConnectAsync();
			await RequestQueryAsync(connection, DatabaseHandler.CreateNoteTableString);
		}

		/// <summary>
		/// 노트 레코드 생성.
		/// </summary>
		public async Task InsertNoteTableAsync(string noteId, string ownerUserId, string ownerWorkspaceId, string ownerNoteId, string property, string type, string timestamp)
		{
			using var connection = await ConnectAsync();
			using var command = connection.CreateCommand();
			command.CommandText = DatabaseHandler.InsertNoteTableString;
			command.Parameters.AddWithValue("@NoteId", noteId);
			command.Parameters.AddWithValue("@OwnerUserId", ownerUserId);
			command.Parameters.AddWithValue("@OwnerWorkspaceId", ownerWorkspaceId);
			command.Parameters.AddWithValue("@OwnerNoteId", ownerNoteId);
			command.Parameters.AddWithValue("@Property", property);
			command.Parameters.AddWithValue("@Type", type);
			command.Parameters.AddWithValue("@Title", string.Empty);
			command.Parameters.AddWithValue("@Content", string.Empty);
			command.Parameters.AddWithValue("@CreateTime", timestamp);
			command.Parameters.AddWithValue("@UpdateTime", timestamp);

			try
			{
				await command.ExecuteNonQueryAsync();
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// 노트 레코드 수정.
		/// </summary>
		public async Task UpdateNoteAsync()
		{
			using var connection = await ConnectAsync();
		}

		/// <summary>
		/// 노트 레코드 삭제.
		/// </summary>
		public async Task DeleteNoteAsync()
		{
			using var connection = await ConnectAsync();
		}
	}
}