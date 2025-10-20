using Crockhead.Core;
using Microsoft.Data.Sqlite;
using System;
using System.Threading.Tasks;


namespace muNoteServer
{
	/// <summary>
	/// 데이터베이스 기반 클래스.
	/// </summary>
	public class BaseDatabaseHandler : Disposable
	{
		/// <summary>
		/// 데이터베이스 접속 문자열.
		/// </summary>
		private string m_ConnectionString;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public BaseDatabaseHandler(string databaseFileName = "local.db") : base()
		{
			if (string.IsNullOrWhiteSpace(databaseFileName))
				databaseFileName = "local.db";

			m_ConnectionString = $"Data Source={databaseFileName};Cache=Shared";
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
		}

		/// <summary>
		/// 파일로부터 데이터베이스 열기.
		/// </summary>
		protected async Task<SqliteConnection> ConnectAsync()
		{
			try
			{
				var connection = new SqliteConnection(m_ConnectionString);
				await connection.OpenAsync();
				return connection;
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// 데이터베이스에 쿼리 송신.
		/// </summary>
		protected async Task<int> RequestQueryAsync(SqliteCommand command)
		{
			try
			{
				return await command.ExecuteNonQueryAsync();
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// 데이터베이스에 쿼리 송신.
		/// </summary>
		protected async Task<int> RequestQueryAsync(SqliteConnection connection, string sql)
		{
			try
			{
				using var command = connection.CreateCommand();
				command.CommandText = sql;
				return await RequestQueryAsync(command);
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// 데이터베이스에 쿼리 송신.
		/// </summary>
		protected async Task<int> RequestQueryAsync(string sql)
		{
			try
			{
				var connection = await ConnectAsync();
				return await RequestQueryAsync(connection, sql);
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// 고유 식별자 생성.
		/// </summary>
		public static string CreateUniqueIdentifier()
		{
			var guid = Guid.NewGuid();
			var uniqueIdentifier = guid.ToString("N");
			return uniqueIdentifier;
		}

		/// <summary>
		/// 타임스탬프 생성.
		/// </summary>
		public static string CreateTimestamp()
		{
			var dateTime = DateTime.UtcNow;
			var timestamp = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
			return timestamp;
		}
	}
}