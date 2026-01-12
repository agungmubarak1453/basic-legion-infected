using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicLegionInfected.Core
{
	public class AsyncEventGeneric<T> where T : class
	{
		private event Func<T, Task> _handler;

		public void Add(Func<T, Task> handler) => _handler += handler;
		public void Remove(Func<T, Task> handler) => _handler -= handler;

		public async Task InvokeAsync(T argument)
		{
			Delegate[] invocationList = _handler?.GetInvocationList();
			if (invocationList != null)
			{
				IEnumerable<Task> tasks = invocationList.Cast<Func<T, Task>>()
					.Select(function => function(argument));
				await Task.WhenAll(tasks);
			}
		}
	}

}