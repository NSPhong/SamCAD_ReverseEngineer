using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;

namespace SamSoarII.Collection.ObjectModel;

public class MorenanBasicalCollection<T> : INotifyCollectionChanged, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
{
	private bool isdisposed = false;

	private List<T> list;

	private Mutex mutex;

	public bool IsDisposed => isdisposed;

	public T this[int id]
	{
		get
		{
			mutex.WaitOne();
			T result = ((id >= 0 && id < list.Count()) ? list[id] : default(T));
			mutex.ReleaseMutex();
			return result;
		}
		set
		{
			mutex.WaitOne();
			if (id >= 0 && id < list.Count())
			{
				T val = list[id];
				list[id] = value;
				this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, val, id));
			}
			mutex.ReleaseMutex();
		}
	}

	public int Count
	{
		get
		{
			mutex.WaitOne();
			int result = list.Count();
			mutex.ReleaseMutex();
			return result;
		}
	}

	public bool IsReadOnly => false;

	public event NotifyCollectionChangedEventHandler CollectionChanged;

	public MorenanBasicalCollection()
	{
		list = new List<T>();
		mutex = new Mutex();
	}

	public void Dispose()
	{
		this.mutex.WaitOne();
		Mutex mutex = this.mutex;
		list = null;
		this.mutex = null;
		mutex.ReleaseMutex();
	}

	public void Add(T item)
	{
		mutex.WaitOne();
		list.Add(item);
		this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, list.Count()));
		mutex.ReleaseMutex();
	}

	public void AddRange(IEnumerable<T> items)
	{
		mutex.WaitOne();
		int startingIndex = list.Count();
		list.AddRange(items);
		this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items.ToArray(), startingIndex));
		mutex.ReleaseMutex();
	}

	public bool Remove(T item)
	{
		mutex.WaitOne();
		int num = list.IndexOf(item);
		if (num < 0 || num >= list.Count())
		{
			mutex.ReleaseMutex();
			return false;
		}
		list.RemoveAt(num);
		this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, num));
		mutex.ReleaseMutex();
		return true;
	}

	public void RemoveAt(int id)
	{
		mutex.WaitOne();
		if (id < 0 || id >= list.Count())
		{
			mutex.ReleaseMutex();
			return;
		}
		T val = list[id];
		list.RemoveAt(id);
		this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, val, id));
		mutex.ReleaseMutex();
	}

	public void RemoveRange(int start, int count)
	{
		mutex.WaitOne();
		if (start < 0 || start >= list.Count())
		{
			mutex.ReleaseMutex();
			return;
		}
		if (start + count > list.Count())
		{
			mutex.ReleaseMutex();
			return;
		}
		List<T> range = list.GetRange(start, count);
		list.RemoveRange(start, count);
		this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, range, start));
		mutex.ReleaseMutex();
	}

	public void RemoveRange(IEnumerable<T> items)
	{
		mutex.WaitOne();
		list = list.Except(items).ToList();
		this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		mutex.ReleaseMutex();
	}

	public void Insert(int id, T item)
	{
		mutex.WaitOne();
		list.Insert(id, item);
		this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, id));
		mutex.ReleaseMutex();
	}

	public void InsertRange(int id, IEnumerable<T> items)
	{
		mutex.WaitOne();
		list.InsertRange(id, items);
		this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items.ToArray(), id));
		mutex.ReleaseMutex();
	}

	public int IndexOf(T item)
	{
		mutex.WaitOne();
		int result = list.IndexOf(item);
		mutex.ReleaseMutex();
		return result;
	}

	public void Clear()
	{
		mutex.WaitOne();
		List<T> changedItems = list;
		list = new List<T>();
		this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, changedItems, 0));
		mutex.ReleaseMutex();
	}

	public bool Contains(T item)
	{
		mutex.WaitOne();
		bool result = list.Contains(item);
		mutex.ReleaseMutex();
		return result;
	}

	public void CopyTo(T[] array, int start)
	{
		mutex.WaitOne();
		for (int i = 0; i < list.Count() && i + start < array.Length; i++)
		{
			array[start + i] = list[i];
		}
		mutex.ReleaseMutex();
	}

	public IEnumerator<T> GetEnumerator()
	{
		mutex.WaitOne();
		IEnumerable<T> enumerable = list.ToArray();
		mutex.ReleaseMutex();
		return enumerable.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
