using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Ginnay.ProxySpider;


namespace Wimlab.Utilities.ThreadSafeCollection
{
	public class TPriorityQueue<P, V> where P:IComparable<P>
	{

		private SortedDictionary<P, Queue<V>> list = new SortedDictionary<P, Queue<V>>();
		private ReaderWriterLockSlim rwlock = new ReaderWriterLockSlim();
		private int count = 0;

		public int Count
		{
			get
			{
				int c;
				rwlock.EnterReadLock();
				c = count;
				rwlock.ExitReadLock();
				return c;
			}
		}
		public bool Contains(V value)
		{
			foreach (Queue<V> q in list.Values)
			{
				if (q.Contains(value))
				{
					return true;
				}
			}
			return false;
		}
		public void Enqueue(P priority, V value)
		{
			rwlock.EnterWriteLock();
			{
				Queue<V> q;
				if (!list.TryGetValue(priority, out q))
				{
					q = new Queue<V>();
					list.Add(priority, q);
				}
				q.Enqueue(value);
				count++;
			}
			rwlock.ExitWriteLock();
		}
		public KeyValuePair<P, V> PeekPair()
		{
			KeyValuePair<P, V> v;
			rwlock.EnterReadLock();
			if (list.Count != 0)
			{
				var enumerator = list.GetEnumerator();
				enumerator.MoveNext();
				var pair = enumerator.Current;
				Queue<V> queue = pair.Value;
				v = new KeyValuePair<P, V>(pair.Key,queue.Peek());
			}
			else
			{
				throw new InvalidOperationException("Queue is empty");
			}
			rwlock.ExitReadLock();
			return v;
		}
		public V PeekValue()
		{
			return PeekPair().Value;
		}
		public V DequeueIfGreaterOrEqual(P priority)
		{
			V v = default(V);
			rwlock.EnterWriteLock();
			if (list.Count != 0)
			{
				var enumerator = list.GetEnumerator();
				enumerator.MoveNext();
				var pair = enumerator.Current;
				Queue<V> queue = pair.Value;
				P pri = pair.Key;
				if (priority.CompareTo(pri) <= 0)
				{
					v = queue.Dequeue();
					count--;
					if (queue.Count == 0)
					{
						// nothing left of the top priority
						list.Remove(pair.Key);
					}
				}
				
			}
			else
			{
				throw new InvalidOperationException("Queue is empty");
			}
			rwlock.ExitWriteLock();
			return v;
		}
		public V DequeueIfLessOrEqual(P priority)
		{
			V v = default(V);
			rwlock.EnterWriteLock();
			if (list.Count != 0)
			{
				var enumerator = list.GetEnumerator();
				enumerator.MoveNext();
				var pair = enumerator.Current;
				Queue<V> queue = pair.Value;
				P pri = pair.Key;
				if (priority.CompareTo(pri) >= 0)
				{
					v = queue.Dequeue();
					count--;
					if (queue.Count == 0)
					{
						// nothing left of the top priority
						list.Remove(pair.Key);
					}
				}
			}
			else
			{
				throw new InvalidOperationException("Queue is empty");
			}
			rwlock.ExitWriteLock();
			return v;
		}


		public V Dequeue()
		{
			V v = default(V);
			rwlock.EnterWriteLock();
			if (list.Count != 0)
			{
				var enumerator = list.GetEnumerator();
				enumerator.MoveNext();
				var pair = enumerator.Current;
				Queue<V> queue = pair.Value;
				v = queue.Dequeue();
				count--;
				if (queue.Count == 0)
				{
					// nothing left of the top priority
					list.Remove(pair.Key);
				}
			}
			else
			{
				throw new InvalidOperationException("Queue is empty");
			}
			rwlock.ExitWriteLock();
			return v;
		}
		public List<V> ToList()
		{
			rwlock.EnterReadLock();
			List<V> l = new List<V>();
			foreach (Queue<V> vs in list.Values)
			{
				foreach (V v in vs)
				{
					l.Add(v);
				}
			}
			rwlock.ExitReadLock();
			return l;
		}
		public void Clear()
		{
			rwlock.EnterWriteLock();
			list.Clear();
			count = 0;
			rwlock.ExitWriteLock();
		}
	}
}
