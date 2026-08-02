using System;

namespace System.Threading
{
	// Token: 0x0200004A RID: 74
	public class ReaderWriterLockSlim : IDisposable
	{
		// Token: 0x0600017A RID: 378 RVA: 0x00006938 File Offset: 0x00004B38
		public void EnterReadLock()
		{
			this.TryEnterReadLock(-1);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00006944 File Offset: 0x00004B44
		public bool TryEnterReadLock(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			if (this.read_locks == null)
			{
				throw new ObjectDisposedException(null);
			}
			if (Thread.CurrentThread == this.write_thread)
			{
				throw new LockRecursionException("Read lock cannot be acquired while write lock is held");
			}
			this.EnterMyLock();
			ReaderWriterLockSlim.LockDetails readLockDetails = this.GetReadLockDetails(Thread.CurrentThread.ManagedThreadId, true);
			if (readLockDetails.ReadLocks != 0)
			{
				this.ExitMyLock();
				throw new LockRecursionException("Recursive read lock can only be aquired in SupportsRecursion mode");
			}
			readLockDetails.ReadLocks++;
			while (this.owners < 0 || this.numWriteWaiters != 0U)
			{
				if (millisecondsTimeout == 0)
				{
					this.ExitMyLock();
					return false;
				}
				if (this.readEvent == null)
				{
					this.LazyCreateEvent(ref this.readEvent, false);
				}
				else if (!this.WaitOnEvent(this.readEvent, ref this.numReadWaiters, millisecondsTimeout))
				{
					return false;
				}
			}
			this.owners++;
			this.ExitMyLock();
			return true;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00006A50 File Offset: 0x00004C50
		public void ExitReadLock()
		{
			this.EnterMyLock();
			if (this.owners < 1)
			{
				this.ExitMyLock();
				throw new SynchronizationLockException("Releasing lock and no read lock taken");
			}
			this.owners--;
			this.GetReadLockDetails(Thread.CurrentThread.ManagedThreadId, false).ReadLocks--;
			this.ExitAndWakeUpAppropriateWaiters();
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00006AB4 File Offset: 0x00004CB4
		public void EnterWriteLock()
		{
			this.TryEnterWriteLock(-1);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00006AC0 File Offset: 0x00004CC0
		public bool TryEnterWriteLock(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			if (this.read_locks == null)
			{
				throw new ObjectDisposedException(null);
			}
			if (this.IsWriteLockHeld)
			{
				throw new LockRecursionException();
			}
			this.EnterMyLock();
			ReaderWriterLockSlim.LockDetails readLockDetails = this.GetReadLockDetails(Thread.CurrentThread.ManagedThreadId, false);
			if (readLockDetails != null && readLockDetails.ReadLocks > 0)
			{
				this.ExitMyLock();
				throw new LockRecursionException("Write lock cannot be acquired while read lock is held");
			}
			while (this.owners != 0)
			{
				if (this.owners == 1 && this.upgradable_thread == Thread.CurrentThread)
				{
					this.owners = -1;
					this.write_thread = Thread.CurrentThread;
					IL_0178:
					this.ExitMyLock();
					return true;
				}
				if (millisecondsTimeout == 0)
				{
					this.ExitMyLock();
					return false;
				}
				if (this.upgradable_thread == Thread.CurrentThread)
				{
					if (this.upgradeEvent == null)
					{
						this.LazyCreateEvent(ref this.upgradeEvent, false);
					}
					else
					{
						if (this.numUpgradeWaiters > 0U)
						{
							this.ExitMyLock();
							throw new ApplicationException("Upgrading lock to writer lock already in process, deadlock");
						}
						if (!this.WaitOnEvent(this.upgradeEvent, ref this.numUpgradeWaiters, millisecondsTimeout))
						{
							return false;
						}
					}
				}
				else if (this.writeEvent == null)
				{
					this.LazyCreateEvent(ref this.writeEvent, true);
				}
				else if (!this.WaitOnEvent(this.writeEvent, ref this.numWriteWaiters, millisecondsTimeout))
				{
					return false;
				}
			}
			this.owners = -1;
			this.write_thread = Thread.CurrentThread;
			goto IL_0178;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00006C4C File Offset: 0x00004E4C
		public void ExitWriteLock()
		{
			this.EnterMyLock();
			if (this.owners != -1)
			{
				this.ExitMyLock();
				throw new SynchronizationLockException("Calling ExitWriterLock when no write lock is held");
			}
			if (this.upgradable_thread == Thread.CurrentThread)
			{
				this.owners = 1;
			}
			else
			{
				this.owners = 0;
			}
			this.write_thread = null;
			this.ExitAndWakeUpAppropriateWaiters();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00006CAC File Offset: 0x00004EAC
		public void EnterUpgradeableReadLock()
		{
			this.TryEnterUpgradeableReadLock(-1);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006CB8 File Offset: 0x00004EB8
		public bool TryEnterUpgradeableReadLock(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			if (this.read_locks == null)
			{
				throw new ObjectDisposedException(null);
			}
			if (this.IsUpgradeableReadLockHeld)
			{
				throw new LockRecursionException();
			}
			if (this.IsWriteLockHeld)
			{
				throw new LockRecursionException();
			}
			this.EnterMyLock();
			while (this.owners != 0 || this.numWriteWaiters != 0U || this.upgradable_thread != null)
			{
				if (millisecondsTimeout == 0)
				{
					this.ExitMyLock();
					return false;
				}
				if (this.readEvent == null)
				{
					this.LazyCreateEvent(ref this.readEvent, false);
				}
				else if (!this.WaitOnEvent(this.readEvent, ref this.numReadWaiters, millisecondsTimeout))
				{
					return false;
				}
			}
			this.owners++;
			this.upgradable_thread = Thread.CurrentThread;
			this.ExitMyLock();
			return true;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006DA4 File Offset: 0x00004FA4
		public void ExitUpgradeableReadLock()
		{
			this.EnterMyLock();
			this.owners--;
			this.upgradable_thread = null;
			this.ExitAndWakeUpAppropriateWaiters();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00006DC8 File Offset: 0x00004FC8
		public void Dispose()
		{
			this.read_locks = null;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00006DD4 File Offset: 0x00004FD4
		public bool IsWriteLockHeld
		{
			get
			{
				return this.RecursiveWriteCount != 0;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00006DE4 File Offset: 0x00004FE4
		public bool IsUpgradeableReadLockHeld
		{
			get
			{
				return this.RecursiveUpgradeCount != 0;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00006DF4 File Offset: 0x00004FF4
		public int RecursiveUpgradeCount
		{
			get
			{
				return (this.upgradable_thread != Thread.CurrentThread) ? 0 : 1;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00006E10 File Offset: 0x00005010
		public int RecursiveWriteCount
		{
			get
			{
				return (this.write_thread != Thread.CurrentThread) ? 0 : 1;
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00006E2C File Offset: 0x0000502C
		private void EnterMyLock()
		{
			if (Interlocked.CompareExchange(ref this.myLock, 1, 0) != 0)
			{
				this.EnterMyLockSpin();
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00006E48 File Offset: 0x00005048
		private void EnterMyLockSpin()
		{
			int num = 0;
			for (;;)
			{
				if (num < 3 && ReaderWriterLockSlim.smp)
				{
					Thread.SpinWait(20);
				}
				else
				{
					Thread.Sleep(0);
				}
				if (Interlocked.CompareExchange(ref this.myLock, 1, 0) == 0)
				{
					break;
				}
				num++;
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00006E9C File Offset: 0x0000509C
		private void ExitMyLock()
		{
			this.myLock = 0;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00006EA8 File Offset: 0x000050A8
		private void ExitAndWakeUpAppropriateWaiters()
		{
			if (this.owners == 1 && this.numUpgradeWaiters != 0U)
			{
				this.ExitMyLock();
				this.upgradeEvent.Set();
			}
			else if (this.owners == 0 && this.numWriteWaiters > 0U)
			{
				this.ExitMyLock();
				this.writeEvent.Set();
			}
			else if (this.owners >= 0 && this.numReadWaiters != 0U)
			{
				this.ExitMyLock();
				this.readEvent.Set();
			}
			else
			{
				this.ExitMyLock();
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00006F48 File Offset: 0x00005148
		private void LazyCreateEvent(ref EventWaitHandle waitEvent, bool makeAutoResetEvent)
		{
			this.ExitMyLock();
			EventWaitHandle eventWaitHandle;
			if (makeAutoResetEvent)
			{
				eventWaitHandle = new AutoResetEvent(false);
			}
			else
			{
				eventWaitHandle = new ManualResetEvent(false);
			}
			this.EnterMyLock();
			if (waitEvent == null)
			{
				waitEvent = eventWaitHandle;
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00006F84 File Offset: 0x00005184
		private bool WaitOnEvent(EventWaitHandle waitEvent, ref uint numWaiters, int millisecondsTimeout)
		{
			waitEvent.Reset();
			numWaiters += 1U;
			bool flag = false;
			this.ExitMyLock();
			try
			{
				flag = waitEvent.WaitOne(millisecondsTimeout, false);
			}
			finally
			{
				this.EnterMyLock();
				numWaiters -= 1U;
				if (!flag)
				{
					this.ExitMyLock();
				}
			}
			return flag;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00006FE0 File Offset: 0x000051E0
		private ReaderWriterLockSlim.LockDetails GetReadLockDetails(int threadId, bool create)
		{
			int i;
			ReaderWriterLockSlim.LockDetails lockDetails;
			for (i = 0; i < this.read_locks.Length; i++)
			{
				lockDetails = this.read_locks[i];
				if (lockDetails == null)
				{
					break;
				}
				if (lockDetails.ThreadId == threadId)
				{
					return lockDetails;
				}
			}
			if (!create)
			{
				return null;
			}
			if (i == this.read_locks.Length)
			{
				Array.Resize<ReaderWriterLockSlim.LockDetails>(ref this.read_locks, this.read_locks.Length * 2);
			}
			lockDetails = (this.read_locks[i] = new ReaderWriterLockSlim.LockDetails());
			lockDetails.ThreadId = threadId;
			return lockDetails;
		}

		// Token: 0x04000151 RID: 337
		private static readonly bool smp = Environment.ProcessorCount > 1;

		// Token: 0x04000152 RID: 338
		private int myLock;

		// Token: 0x04000153 RID: 339
		private int owners;

		// Token: 0x04000154 RID: 340
		private Thread upgradable_thread;

		// Token: 0x04000155 RID: 341
		private Thread write_thread;

		// Token: 0x04000156 RID: 342
		private uint numWriteWaiters;

		// Token: 0x04000157 RID: 343
		private uint numReadWaiters;

		// Token: 0x04000158 RID: 344
		private uint numUpgradeWaiters;

		// Token: 0x04000159 RID: 345
		private EventWaitHandle writeEvent;

		// Token: 0x0400015A RID: 346
		private EventWaitHandle readEvent;

		// Token: 0x0400015B RID: 347
		private EventWaitHandle upgradeEvent;

		// Token: 0x0400015C RID: 348
		private readonly LockRecursionPolicy recursionPolicy;

		// Token: 0x0400015D RID: 349
		private ReaderWriterLockSlim.LockDetails[] read_locks = new ReaderWriterLockSlim.LockDetails[8];

		// Token: 0x0200004B RID: 75
		private sealed class LockDetails
		{
			// Token: 0x0400015E RID: 350
			public int ThreadId;

			// Token: 0x0400015F RID: 351
			public int ReadLocks;
		}
	}
}
