using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicLegionInfected.Core
{
    public class AsyncEvent
    {
        private event Func<Task> _handler;

        public void Add(Func<Task> handler) => _handler += handler;
        public void Remove(Func<Task> handler) => _handler -= handler;

        public async Task InvokeAsync()
        {
            Delegate[] invocationList = _handler?.GetInvocationList();
            if (invocationList != null)
            {
                IEnumerable<Task> tasks = invocationList.Cast<Func<Task>>()
                    .Select(function => function());
                await Task.WhenAll(tasks);
            }
        }
    }

}