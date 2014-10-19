#pragma warning disable 1692
using System;
using System.Collections.Generic;
using System.Threading;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// ������� ��� �������� �������� ����.
	/// </summary>
	public delegate E CreateElement<K, E>(K key);

	/// <summary>
	/// ��� ���������. ��� �������� ��������� ��������.
	/// </summary>
	public class ElementsCache<K, E>
	{
		private readonly CreateElement<K, E> _elementCreator;
		private readonly Dictionary<K, E> _elements = new Dictionary<K, E>();
		private readonly ReaderWriterLock _cacheLock = new ReaderWriterLock();

		/// <summary>
		/// �������������� ���������.
		/// </summary>
		public ElementsCache(CreateElement<K, E> elementCreator)
		{
			_elementCreator = elementCreator;
		}

		/// <summary>
		/// ��������� ���������.
		/// </summary>
		public CreateElement<K, E> ElementCreator
		{
			get { return _elementCreator; }
		}

		/// <summary>
		/// ��������� ���� ������.
		/// </summary>
		public ICollection<K> Keys
		{
			get
			{
				using (_cacheLock.GetReaderLock())
				{
					var keys = new List<K>(_elements.Keys);
					return keys;
				}
			}
		}

		/// <summary>
		/// ��������� ������� �� ����.
		/// </summary>
		public E Get(K key)
		{
#pragma warning disable CompareNonConstrainedGenericWithNull
			if (key == null)
#pragma warning restore CompareNonConstrainedGenericWithNull
				throw new ArgumentNullException("key");
			using (_cacheLock.GetReaderLock())
			{
				E element;
				if (!_elements.TryGetValue(key, out element))
					using (_cacheLock.UpgradeToWriterLock())
						if (!_elements.TryGetValue(key, out element))
						{
							element = _elementCreator(key);
							_elements.Add(key, element);
							OnAfterElementCreated(key, element);
						}
				return element;
			}
		}

		/// <summary>
		/// ���������� ����� �������� ������ ��������.
		/// </summary>
		protected virtual void OnAfterElementCreated(K key, E element)
		{
		}

		/// <summary>
		/// ���������� ������� ���������� ��������.
		/// </summary>
		public bool TryGet(K key, out E element)
		{
#pragma warning disable CompareNonConstrainedGenericWithNull
			if (key == null)
#pragma warning restore CompareNonConstrainedGenericWithNull
				throw new ArgumentNullException("key");
			using (_cacheLock.GetReaderLock())
				return _elements.TryGetValue(key, out element);
		}

		/// <summary>
		/// ����������, �������� �� ��� ������� � ��������� ������.
		/// </summary>
		public bool Contains(K key)
		{
#pragma warning disable CompareNonConstrainedGenericWithNull
			if (key == null)
#pragma warning restore CompareNonConstrainedGenericWithNull
				throw new ArgumentNullException("key");
			using (_cacheLock.GetReaderLock())
				return _elements.ContainsKey(key);
		}

		/// <summary>
		/// ������� ������� ���� �� ��������� �����.
		/// </summary>
		public virtual void Drop(K key)
		{
#pragma warning disable CompareNonConstrainedGenericWithNull
			if (key == null)
#pragma warning restore CompareNonConstrainedGenericWithNull
				throw new ArgumentNullException("key");
			using (_cacheLock.GetWriterLock())
				_elements.Remove(key);
		}

		/// <summary>
		/// �������� ���.
		/// </summary>
		public virtual void Reset()
		{
			using (_cacheLock.GetWriterLock())
				_elements.Clear();
		}
	}
}