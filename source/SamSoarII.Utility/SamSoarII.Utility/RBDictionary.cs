using System;
using System.Collections;
using System.Collections.Generic;

namespace SamSoarII.Utility;

public class RBDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable where TKey : IComparable<TKey>
{
	private sealed class Node
	{
		public byte Color;

		public Node Left;

		public Node Right;

		public TKey key;

		public TValue value;

		public Node(TKey key, TValue value = default(TValue), byte Color = 0)
		{
			this.key = key;
			this.value = value;
			this.Color = Color;
		}
	}

	private const byte RED = 0;

	private const byte BLACK = 17;

	private Node root = null;

	private List<KeyValuePair<TKey, TValue>> list;

	private bool isModified;

	private int count;

	public IEnumerable<TKey> Keys
	{
		get
		{
			using IEnumerator<KeyValuePair<TKey, TValue>> enumerator = GetEnumerator();
			while (enumerator.MoveNext())
			{
				yield return enumerator.Current.Key;
			}
		}
	}

	public IEnumerable<TValue> Values
	{
		get
		{
			using IEnumerator<KeyValuePair<TKey, TValue>> enumerator = GetEnumerator();
			while (enumerator.MoveNext())
			{
				yield return enumerator.Current.Value;
			}
		}
	}

	public int Count => count;

	public bool IsReadOnly => false;

	ICollection<TKey> IDictionary<TKey, TValue>.Keys => new List<TKey>(Keys);

	ICollection<TValue> IDictionary<TKey, TValue>.Values => new List<TValue>(Values);

	public TValue this[TKey key]
	{
		get
		{
			return _GetValue(root, key);
		}
		set
		{
			_SetValue(root, key, value);
		}
	}

	public RBDictionary()
	{
		count = 0;
		list = new List<KeyValuePair<TKey, TValue>>();
		isModified = false;
	}

	private TValue _GetValue(Node node, TKey key)
	{
		if (node == null)
		{
			throw new InvalidOperationException("The key is not exist!");
		}
		int num = key.CompareTo(node.key);
		if (num == 0)
		{
			return node.value;
		}
		if (num > 0 && node.Right != null)
		{
			return _GetValue(node.Right, key);
		}
		if (num < 0 && node.Left != null)
		{
			return _GetValue(node.Left, key);
		}
		return node.value;
	}

	private void _SetValue(Node node, TKey key, TValue value)
	{
		if (node == null)
		{
			throw new InvalidOperationException("The key is not exist!");
		}
		int num = key.CompareTo(node.key);
		if (num == 0)
		{
			node.value = value;
			isModified = true;
		}
		else if (num > 0)
		{
			_SetValue(node.Right, key, value);
		}
		else
		{
			_SetValue(node.Left, key, value);
		}
	}

	public void Add(TKey key, TValue value)
	{
		Node node = new Node(key, value, 0);
		root = Insert(root, node);
		count++;
		root.Color = 17;
		isModified = true;
	}

	private Node Insert(Node parent, Node node)
	{
		if (parent == null)
		{
			return node;
		}
		ref TKey key = ref node.key;
		TKey key2 = parent.key;
		int num = key.CompareTo(key2);
		if (num == 0)
		{
			throw new InvalidOperationException("The key you added has already exist!");
		}
		if (num > 0)
		{
			parent.Right = Insert(parent.Right, node);
		}
		else
		{
			parent.Left = Insert(parent.Left, node);
		}
		if (parent.Right?.Color == 0)
		{
			parent = RotateLeft(parent);
		}
		if (parent.Left?.Color == 0 && parent.Left.Left?.Color == 0)
		{
			parent = RotateRight(parent);
		}
		if (parent.Left?.Color == 0 && parent.Right?.Color == 0)
		{
			FlipColor(parent);
		}
		return parent;
	}

	public void Remove(TKey key)
	{
		root = Delete(root, key);
		if (root != null)
		{
			root.Color = 17;
		}
		count--;
		isModified = true;
	}

	private Node Delete(Node node, TKey key)
	{
		if (node == null)
		{
			throw new InvalidOperationException("The key you delete is not exist!");
		}
		int num = key.CompareTo(node.key);
		if (num == 0)
		{
			if (node.Left?.Color == 0 && node.Right == null)
			{
				node = RotateRight(node);
				node.Right = null;
			}
			else if (node.Right == null)
			{
				node = null;
			}
			else
			{
				Node node2 = node;
				if (node.Right.Left != null)
				{
					node2 = node.Right;
					while (node2.Left.Left != null)
					{
						node2 = node2.Left;
					}
				}
				ExchangeNode(ref node, node2);
				if (Analyze(ref node))
				{
					node.Right.Right = Delete(node.Right.Right, key);
				}
				else
				{
					node.Right = Delete(node.Right, key);
				}
			}
		}
		else if (num > 0)
		{
			if (node.Right != null)
			{
				if (Analyze(ref node))
				{
					node.Right.Right = Delete(node.Right.Right, key);
				}
				else
				{
					node.Right = Delete(node.Right, key);
				}
			}
			else
			{
				node.Right = Delete(node.Right, key);
			}
		}
		else
		{
			if (node.Left != null)
			{
				Analyze(ref node, isRight: false);
			}
			node.Left = Delete(node.Left, key);
		}
		if (node != null)
		{
			if (node.Right?.Color == 0)
			{
				node = RotateLeft(node);
			}
			if (node.Left?.Color == 0 && node.Left.Left?.Color == 0)
			{
				node = RotateRight(node);
			}
			if (node.Left?.Color == 0 && node.Right?.Color == 0)
			{
				FlipColor(node);
			}
		}
		return node;
	}

	private bool Analyze(ref Node node, bool isRight = true)
	{
		bool result = false;
		if (isRight)
		{
			if (node.Right.Color == 17 && (node.Right.Left == null || node.Right.Left.Color == 17))
			{
				if (node.Left.Color == 17)
				{
					FlipColor(node, reverse: false);
				}
				else
				{
					node = RotateRight(node);
					FlipColor(node.Right, reverse: false);
					result = true;
				}
			}
		}
		else if (node.Left.Color == 17 && (node.Left.Left == null || node.Left.Left.Color == 17))
		{
			if (node.Right.Left?.Color == 0)
			{
				node.Right = RotateRight(node.Right);
				FlipColor(node, reverse: false);
				node = RotateLeft(node);
				FlipColor(node);
			}
			else
			{
				FlipColor(node, reverse: false);
			}
		}
		return result;
	}

	private void ExchangeNode(ref Node node1, Node node2)
	{
		if (node1 == node2)
		{
			Node right = node2.Right;
			right.Left = node1.Left;
			node1.Left = null;
			node1.Right = null;
			right.Right = node1;
			right.Color = node1.Color;
			node1.Color = 17;
			node1 = right;
		}
		else
		{
			Node left = node2.Left;
			byte color = left.Color;
			left.Color = node1.Color;
			node1.Color = color;
			left.Left = node1.Left;
			left.Right = node1.Right;
			node1.Left = null;
			node1.Right = null;
			node2.Left = node1;
			node1 = left;
		}
	}

	private Node RotateLeft(Node node)
	{
		Node right = node.Right;
		node.Right = right.Left;
		right.Left = node;
		right.Color = node.Color;
		node.Color = 0;
		return right;
	}

	private Node RotateRight(Node node)
	{
		Node left = node.Left;
		node.Left = left.Right;
		left.Right = node;
		left.Color = node.Color;
		node.Color = 0;
		return left;
	}

	private void FlipColor(Node node, bool reverse = true)
	{
		if (reverse)
		{
			node.Left.Color = 17;
			node.Right.Color = 17;
			node.Color = 0;
		}
		else
		{
			node.Left.Color = 0;
			node.Right.Color = 0;
			node.Color = 17;
		}
	}

	public bool ContainsKey(TKey key)
	{
		return _ExistKey(root, key);
	}

	private bool _ExistKey(Node node, TKey key)
	{
		if (node == null)
		{
			return false;
		}
		int num = key.CompareTo(node.key);
		if (num == 0)
		{
			return true;
		}
		if (num > 0)
		{
			return _ExistKey(node.Right, key);
		}
		return _ExistKey(node.Left, key);
	}

	bool IDictionary<TKey, TValue>.Remove(TKey key)
	{
		try
		{
			Remove(key);
		}
		catch (Exception)
		{
			return false;
		}
		return true;
	}

	public bool TryGetValue(TKey key, out TValue value)
	{
		try
		{
			value = _GetValue(root, key);
		}
		catch (Exception)
		{
			value = default(TValue);
			return false;
		}
		return true;
	}

	public void Add(KeyValuePair<TKey, TValue> item)
	{
		if (_ExistKey(root, item.Key))
		{
			this[item.Key] = item.Value;
		}
		else
		{
			Add(item.Key, item.Value);
		}
	}

	public void Clear()
	{
		_Dispose(root);
		isModified = true;
	}

	public bool Contains(KeyValuePair<TKey, TValue> item)
	{
		return ContainsKey(item.Key);
	}

	public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
	{
		for (int i = arrayIndex; i < array.Length; i++)
		{
			this[array[i].Key] = array[i].Value;
		}
		isModified = true;
	}

	public bool Remove(KeyValuePair<TKey, TValue> item)
	{
		try
		{
			Remove(item.Key);
		}
		catch (Exception)
		{
			return false;
		}
		return true;
	}

	public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
	{
		if (isModified)
		{
			list.Clear();
			_GenEnumerator(root, list);
			isModified = false;
		}
		return list.GetEnumerator();
	}

	private void _GenEnumerator(Node node, List<KeyValuePair<TKey, TValue>> list)
	{
		if (node != null)
		{
			_GenEnumerator(node.Left, list);
			list.Add(new KeyValuePair<TKey, TValue>(node.key, node.value));
			_GenEnumerator(node.Right, list);
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	private void _Dispose(Node node)
	{
		if (node != null)
		{
			_Dispose(node.Left);
			_Dispose(node.Right);
			node.Left = null;
			node.Right = null;
			node.key = default(TKey);
			node.value = default(TValue);
			node = null;
		}
	}
}
