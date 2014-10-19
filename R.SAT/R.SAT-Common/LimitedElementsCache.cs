using System;
using System.Collections.Generic;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// Кеш элементов с ограничением максимального размера.
	/// </summary>
	public class LimitedElementsCache<K, E> : ElementsCache<K, E>
	{
		private readonly int _maxSize;
		private readonly Queue<K> _queue = new Queue<K>();

		/// <summary>
		/// Инициализирует экземпляр.
		/// </summary>
		public LimitedElementsCache(CreateElement<K, E> elementCreator, int maxSize)
			: base(elementCreator)
		{
			_maxSize = maxSize;
		}

		/// <summary>
		/// Максимальный размер кеша.
		/// </summary>
		public int MaxSize
		{
			get { return _maxSize; }
		}

		/// <summary>
		/// Вызывается после создания нового элемента.
		/// </summary>
		protected override void OnAfterElementCreated(K key, E element)
		{
			base.OnAfterElementCreated(key, element);
			_queue.Enqueue(key);
			if (_queue.Count > MaxSize)
				base.Drop(_queue.Dequeue());
		}

		/// <summary>
		/// Удаляет элемент кеша по заданному ключу.
		/// </summary>
		public override void Drop(K key)
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// Сбросить кеш.
		/// </summary>
		public override void Reset()
		{
			base.Reset();
			_queue.Clear();
		}
	}
}