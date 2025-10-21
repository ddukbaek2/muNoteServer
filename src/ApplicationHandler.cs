using Crockhead.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace muNoteServer
{
	/// <summary>
	/// 애플리케이션 핸들러.
	/// </summary>
	public class ApplicationHandler : BaseApplicationHandler<ApplicationHandler>
	{
		/// <summary>
		/// 데이터베이스 프로퍼티.
		/// </summary>
		public static DatabaseHandler DatabaseHandler => SharedInstances.Get<DatabaseHandler>();

		/// <summary>
		/// 초기화됨.
		/// </summary>
		protected override void OnInitialize(WebApplicationBuilder builder)
		{
			// 데이터베이스 설정.
			var databaseHandler = new DatabaseHandler();

			// 웹 애플리케이션 설정.
			builder.Services.AddSingleton(databaseHandler);
			builder.Services.AddControllers();

			// 웹 애플리케이션 초기화.
			base.OnInitialize(builder);

			// 공유 인스턴스 설정.
			SharedInstances.Set(databaseHandler);

			// 컨트롤러 맵핑.
			WebApplication.MapControllers();
		}

		/// <summary>
		/// 시작됨.
		/// </summary>
		protected override async Task OnWillStartAsync()
		{
			await base.OnWillStartAsync();
			
			// 최초 실행시 테이블 미리 생성.
			await DatabaseHandler.CreateUserTableAsync();
			await DatabaseHandler.CreateWorkspaceTableAsync();
			await DatabaseHandler.CreateNoteTableAsync();
		}
	}
}