using System;
using System.Collections;
using System.Collections.Generic;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// ������� ��� ������ � �����������.
	/// </summary>
	public static class CollectionHelper
	{
		#region Delegates
		/// <summary>
		/// �������, ����������� ����� �������������� ��������� � �������.
		/// </summary>
		public delegate TKey GetKey<TKey, TValue>(TValue source);

		/// <summary>
		/// ��������.
		/// </summary>
		public delegate T2 Selector<T1, T2>(T1 source);
		#endregion

		/// <summary>
		/// ����������� <see cref="IEnumerable"/> ������� ����� � <see cref="IEnumerable{T}"/>
		/// </summary>
		/// <remarks>
		/// ��������, ������ null ��� �������� �������������� ���� �������������.
		/// </remarks>
		public static IEnumerable<T> Convert<T>(IEnumerable oldStyleSrc) where T : class
		{
			foreach (var o in oldStyleSrc)
			{
				var elem = o as T;
				if (elem != null)
					yield return elem;
			}
		}

		/// <summary>
		/// ����������� <see cref="ICollection"/> � ������ ���������� ����.
		/// </summary>
		public static T[] ToArray<T>(this ICollection collection)
		{
			var res = new T[collection.Count];
			var i = 0;
			foreach (T elem in collection)
			{
				res[i] = elem;
				i++;
			}
			return res;
		}

		/// <summary>
		/// ����������� <see cref="ICollection{T}"/> � ������ ���������� ����.
		/// </summary>
		public static T[] ToArray<T>(this ICollection<T> collection)
		{
			var res = new T[collection.Count];
			var i = 0;
			foreach (var elem in collection)
			{
				res[i] = elem;
				i++;
			}
			return res;
		}

		/// <summary>
		/// ��������� ������������.
		/// </summary>
		public static IEnumerable<T> Filter<T>(this IEnumerable<T> enumerable, Predicate<T> leave)
		{
			foreach (var elem in enumerable)
				if (leave(elem))
					yield return elem;
		}

		/// <summary>
		/// ����������� <see cref="ICollection"/> � ������ ���������� ���� � ��������� ����������
		/// ���������� ����������� <see cref="IComparer{T}"/>.
		/// </summary>
		public static T[] ToArray<T>(ICollection collection, IComparer<T> comparer)
		{
			if (comparer == null)
				throw new ArgumentNullException("comparer");
			var res = ToArray<T>(collection);
			Array.Sort(res, comparer);
			return res;
		}

		/// <summary>
		/// ����������� <see cref="ICollection"/> � ������ ���������� ���� � ��������� ����������
		/// ���������� ���������.
		/// </summary>
		public static T[] ToArray<T>(ICollection collection, Comparison<T> comparison)
		{
			if (comparison == null)
				throw new ArgumentNullException("comparison");
			var res = ToArray<T>(collection);
			Array.Sort(res, comparison);
			return res;
		}

		/// <summary>
		/// ����������� <see cref="ICollection{T}"/> � ������ ���������� ���� � ��������� ����������
		/// ���������� ���������.
		/// </summary>
		public static T[] ToArray<T>(ICollection<T> collection, Comparison<T> comparison)
		{
			if (comparison == null)
				throw new ArgumentNullException("comparison");
			var res = ToArray(collection);
			Array.Sort(res, comparison);
			return res;
		}

		/// <summary>
		/// ����������� <see cref="IEnumerable"/> � ������ ���������� ����.
		/// </summary>
		public static T[] ToArray<T>(this IEnumerable collection) where T : class
		{
			var res = new List<T>(Convert<T>(collection));
			return res.ToArray();
		}

		/// <summary>
		/// ����������� ������� ��������� � �������� ������ ��� ������ ����������� ��������.
		/// </summary>
		public static TOut[] ConvertAll<TIn, TOut>(this ICollection<TIn> collection,
			Converter<TIn, TOut> converter)
		{
			var res = new TOut[collection.Count];
			var i = 0;
			foreach (var elem in collection)
			{
				res[i] = converter(elem);
				i++;
			}
			return res;
		}

		/// <summary>
		/// ����������� generic-������������ � �������.
		/// </summary>
		public static IDictionary<TKey, TValue> Convert2Dictionary<TKey, TValue>(
			this IEnumerable<TValue> source, GetKey<TKey, TValue> keyGetter)
		{
			var res = new Dictionary<TKey, TValue>();
			foreach (var val in source)
				res.Add(keyGetter(val), val);
			return res;
		}

		/// <summary>
		/// ����������� ������������ � �������.
		/// </summary>
		public static IDictionary<TKey, TValue> Convert2Dictionary<TKey, TValue>(
			this IEnumerable source, GetKey<TKey, TValue> keyGetter)
		{
			var res = new Dictionary<TKey, TValue>();
			foreach (TValue val in source)
				res.Add(keyGetter(val), val);
			return res;
		}

		/// <summary>
		/// ���������� ����������� ������� ���������.
		/// </summary>
		public static Comparison<T> ReverseComparision<T>(Comparison<T> srcComparision)
		{
			return
				(e1, e2) =>
					{
						var cr = srcComparision(e1, e2);
						return cr > 0 ? -1 : cr < 0 ? 1 : 0;
					};
		}

		/// <summary>
		/// ��������� ���������� �������� ��� ���� ��������� ������������.
		/// </summary>
		public static void ForEach<T>(this IEnumerable<T> src, Action<T> action)
		{
			foreach (var element in src)
				action(element);
		}
	}
}