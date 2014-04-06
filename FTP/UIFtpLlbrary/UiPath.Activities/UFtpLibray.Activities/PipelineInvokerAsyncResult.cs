using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Threading;
namespace FtpActivities
{
	internal class PipelineInvokerAsyncResult : System.IAsyncResult
	{
		private System.AsyncCallback callback;
		private object asyncState;
		private System.Threading.EventWaitHandle asyncWaitHandle;
		private System.Collections.ObjectModel.Collection<ErrorRecord> errorRecords;
		public System.Collections.ObjectModel.Collection<ErrorRecord> ErrorRecords
		{
			get
			{
				if (this.errorRecords == null)
				{
					this.errorRecords = new System.Collections.ObjectModel.Collection<ErrorRecord>();
				}
				return this.errorRecords;
			}
		}
		public System.Exception Exception
		{
			get;
			set;
		}
		public System.Collections.ObjectModel.Collection<PSObject> PipelineOutput
		{
			get;
			set;
		}
		public object AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}
		public System.Threading.WaitHandle AsyncWaitHandle
		{
			get
			{
				return this.asyncWaitHandle;
			}
		}
		public bool CompletedSynchronously
		{
			get
			{
				return false;
			}
		}
		public bool IsCompleted
		{
			get
			{
				return true;
			}
		}
		public PipelineInvokerAsyncResult(Pipeline pipeline, System.AsyncCallback callback, object state)
		{
			try
			{
				this.asyncState = state;
				this.callback = callback;
				this.asyncWaitHandle = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.AutoReset);
				pipeline.StateChanged += new System.EventHandler<PipelineStateEventArgs>(this.OnStateChanged);
			}
			catch (System.Exception exception)
			{
				this.Exception = exception;
				this.Complete();
				return;
			}
			pipeline.InvokeAsync();
		}
		private void Complete()
		{
			this.asyncWaitHandle.Set();
			if (this.callback != null)
			{
				this.callback(this);
			}
		}
		private void OnStateChanged(object sender, PipelineStateEventArgs args)
		{
			try
			{
				PipelineState state = args.PipelineStateInfo.State;
				Pipeline pipeline = sender as Pipeline;
				if (state == PipelineState.Completed)
				{
					this.PipelineOutput = pipeline.Output.ReadToEnd();
					this.ReadErrorRecords(pipeline);
					this.Complete();
				}
				else
				{
					if (state == PipelineState.Failed)
					{
						this.Exception = args.PipelineStateInfo.Reason;
						this.ReadErrorRecords(pipeline);
						this.Complete();
					}
					else
					{
						if (state == PipelineState.Stopped)
						{
							this.Complete();
						}
					}
				}
			}
			catch (System.Exception exception)
			{
				this.Exception = exception;
				this.Complete();
			}
		}
		private void ReadErrorRecords(Pipeline pipeline)
		{
			System.Collections.ObjectModel.Collection<object> collection = pipeline.Error.ReadToEnd();
			if (collection.Count != 0)
			{
				using (System.Collections.Generic.IEnumerator<object> enumerator = collection.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PSObject pSObject = (PSObject)enumerator.Current;
						ErrorRecord item = pSObject.BaseObject as ErrorRecord;
						this.ErrorRecords.Add(item);
					}
				}
			}
		}
	}
}
