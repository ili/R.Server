using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

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

		/// <summary>
		/// Checks if any element in array exists.
		/// </summary>
		public static bool Any<T>(this T[] array)
		{
			return array.Length != 0;
		}

		/// <summary>
		/// Returns first element, or specified value, if sequence empty.
		/// </summary>
		public static T FirstOrValue<T>([NotNull] this IEnumerable<T> source, T value)
		{
			if (source == null)
				throw new ArgumentNullException("source");
			foreach (var item in source)
				return item;
			return value;
		}

		/// <summary>
		/// Returns first element, or specified value, if sequence empty.
		/// </summary>
		public static T FirstOrValue<T>(
			[NotNull] this IEnumerable<T> source,
			T value,
			[NotNull] Func<T, bool> predicate)
		{
			if (source == null)
				throw new ArgumentNullException("source");
			if (predicate == null)
				throw new ArgumentNullException("predicate");
			foreach (var item in source.Where(predicate))
				return item;
			return value;
		}
	}
}