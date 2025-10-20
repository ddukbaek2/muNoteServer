using Microsoft.AspNetCore.Mvc;
using System;


namespace muNoteServer
{
	/// <summary>
	/// API 핸들러 기반 클래스. (컨트롤러)
	/// </summary>
	[ApiController]
	public class BaseApiHandler : ControllerBase, IDisposable
	{
		/// <summary>
		/// 해제 되었는지 여부.
		/// </summary>
		private bool m_IsDisposed;

		/// <summary>
		/// 생성됨.
		/// </summary>
		public BaseApiHandler()
		{
			m_IsDisposed = false;
		}

		/// <summary>
		/// 파괴됨.
		/// </summary>
		~BaseApiHandler()
		{
			if (m_IsDisposed)
				return;

			m_IsDisposed = true;
			OnDispose(explicitDisposing: false);
		}

		/// <summary>
		/// 해제됨.
		/// </summary>
		protected virtual void OnDispose(bool explicitDisposing)
		{
		}

		/// <summary>
		/// 해제.
		/// </summary>
		public void Dispose()
		{
			if (m_IsDisposed)
				return;

			m_IsDisposed = true;
			OnDispose(explicitDisposing: true);
			GC.SuppressFinalize(this);
		}
	}
}