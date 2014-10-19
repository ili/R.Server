using System;
using System.Collections.Generic;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// ��� ��������� � ������������ ������������� �������.
	/// </summary>
	public class LimitedElementsCache<K, E> : ElementsCache<K, E>
	{
		private readonly int _maxSize;
		private readonly Queue<K> _queue = new Queue<K>();

		/// <summary>
		/// �������������� ���������.
		/// </summary>
		public LimitedElementsCache(CreateElement<K, E> elementCreator, int maxSize)
			: base(elementCreator)
		{
			_maxSize = maxSize;
		}

		/// <summary>
		/// ������������ ������ ����.
		/// </summary>
		public int MaxSize
		{
			get { return _maxSize; }
		}

		/// <summary>
		/// ���������� ����� �������� ������ ��������.
		/// </summary>
		protected override void OnAfterElementCreated(K key, E element)
		{
			base.OnAfterElementCreated(key, element);
			_queue.Enqueue(key);
			if (_queue.Count > MaxSize)
				base.Drop(_queue.Dequeue());
		}

		/// <summary>
		/// ������� ������� ���� �� ��������� �����.
		/// </summary>
		public override void Drop(K key)
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// �������� ���.
		/// </summary>
		public override void Reset()
		{
			base.Reset();
			_queue.Clear();
		}
	}
}