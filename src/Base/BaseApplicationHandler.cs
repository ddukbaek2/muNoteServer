using Crockhead.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace muNoteServer
{
	/// <summary>
	/// 애플리케이션 기반 클래스.
	/// </summary>
	public class BaseApplicationHandler<TApplicationHandler> : Disposable where TApplicationHandler : BaseApplicationHandler<TApplicationHandler>, new()
	{
		/// <summary>
		/// 웹 애플리케이션 프로퍼티.
		/// </summary>
		public static WebApplication WebApplication => SharedInstances.Get<WebApplication>();

		/// <summary>
		/// 공유 인스턴스 프로퍼티.
		/// </summary>
		public static TApplicationHandler SharedInstance => SharedInstances.Get<TApplicationHandler>();

		/// <summary>
		/// 생성됨.
		/// </summary>
		public BaseApplicationHandler() : base()
		{
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected override void OnDispose(bool explicitDisposing)
		{
		}

		/// <summary>
		/// 초기화됨.
		/// </summary>
		protected virtual void OnInitialize(WebApplicationBuilder builder)
		{
			// 웹 애플리케이션 설정.
			builder.Services.AddSingleton((TApplicationHandler)this);

			// 웹 애플리케이션 생성.
			var application = builder.Build();

			// 공유 인스턴스 설정.
			SharedInstances.Clear();
			SharedInstances.Set(this);
			SharedInstances.Set(application);
		}

		/// <summary>
		/// 시작 되기 직전 비동기 호출됨.
		/// </summary>
		protected virtual async Task OnWillStartAsync()
		{
			await Task.CompletedTask;
		}

		/// <summary>
		/// 종료 되기 직전 비동기 호출됨.
		/// </summary>
		protected virtual async Task OnWillFinishAsync()
		{
			await Task.CompletedTask;
		}

		/// <summary>
		/// 비동기 시작.
		/// </summary>
		public async Task StartAsync(string[] arguments, CancellationToken cancellationToken)
		{
			try
			{
				var builder = WebApplication.CreateBuilder(arguments);
				OnInitialize(builder);

				await OnWillStartAsync();
				await WebApplication.RunAsync(cancellationToken);
				
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
			}
			finally
			{
				await OnWillFinishAsync();
			}
		}

		/// <summary>
		/// 싱글톤 인스턴스 반환.
		/// </summary>
		public T GetSignletonInstance<T>()
		{
			return WebApplication.Services.GetRequiredService<T>();
		}

		/// <summary>
		/// 비동기 실행.
		/// </summary>
		public static async Task RunAsync(string[] arguments, CancellationToken cancellationToken)
		{
			var applicationHandler = new TApplicationHandler();
			await applicationHandler.StartAsync(arguments, cancellationToken);
		}

		/// <summary>
		/// 비동기 실행.
		/// </summary>
		public static async Task RunAsync(string[] arguments)
		{
			var applicationHandler = new TApplicationHandler();
			await applicationHandler.StartAsync(arguments, CancellationToken.None);
		}

		/// <summary>
		/// 동기 실행.
		/// </summary>
		public static void Run(params string[] arguments)
		{
			var applicationTask = RunAsync(arguments, CancellationToken.None);
			//Task.WaitAll(applicationTask);
			var awaiter = applicationTask.GetAwaiter();
			awaiter.GetResult();
		}
	}
}