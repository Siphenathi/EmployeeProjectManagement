using System;
using System.Transactions;

namespace EmployeeProjectManagement.Service.Tests.Utilities
{
	public class TransactionScopeWrapper : IDisposable
	{
		private readonly TransactionScope _scope;
		public TransactionScopeWrapper()
		{
			_scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
		}

		public void Dispose()
		{
			_scope.Dispose();
		}
    }
}
