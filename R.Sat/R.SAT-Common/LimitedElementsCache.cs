using System;
using System.Collections.Generic;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// ��� ��������� � ������������ ������������� �������.
	/// </summary>
	public class LimitedElementsCache<TKey, TElement> : ElementsCache<TKey, TElement>
	{
		private readonly int _maxSize;
		private readonly Queue<TKey> _queue = new Queue<TKey>();

		/// <summary>
		/// �������������� ���������.
		/// </summary>
		public LimitedElementsCache(CreateElement<TKey, TElement> elementCreator, int maxSize)
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
		protected override void OnAfterElementCreated(TKey key, TElement element)
		{
			base.OnAfterElementCreated(key, element);
			_queue.Enqueue(key);
			if (_queue.Count > MaxSize)
				base.Drop(_queue.Dequeue());
		}

		/// <summary>
		/// ������� ������� ���� �� ��������� �����.
		/// </summary>
		public override void Drop(TKey key)
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