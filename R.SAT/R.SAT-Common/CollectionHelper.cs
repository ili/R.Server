using System;
using System.Collections;
using System.Collections.Generic;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// Утилиты для работы с коллекциями.
	/// </summary>
	public static class CollectionHelper
	{
		#region Delegates
		/// <summary>
		/// Делегат, описывающий метод преобразования коллекции к словарю.
		/// </summary>
		public delegate TKey GetKey<TKey, TValue>(TValue source);

		/// <summary>
		/// Селектор.
		/// </summary>
		public delegate T2 Selector<T1, T2>(T1 source);
		#endregion

		/// <summary>
		/// Преобразует <see cref="IEnumerable"/> старого стиля в <see cref="IEnumerable{T}"/>
		/// </summary>
		/// <remarks>
		/// Элементы, равные null или элементы несовпадающего типа выбрасываются.
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
		/// Преобразует <see cref="ICollection"/> в массив указанного типа.
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
		/// Преобразует <see cref="ICollection{T}"/> в массив указанного типа.
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
		/// Фильтрует перечисление.
		/// </summary>
		public static IEnumerable<T> Filter<T>(this IEnumerable<T> enumerable, Predicate<T> leave)
		{
			foreach (var elem in enumerable)
				if (leave(elem))
					yield return elem;
		}

		/// <summary>
		/// Преобразует <see cref="ICollection"/> в массив указанного типа и выполняет сортировку
		/// переданной реализацией <see cref="IComparer{T}"/>.
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
		/// Преобразует <see cref="ICollection"/> в массив указанного типа и выполняет сортировку
		/// переданным делегатом.
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
		/// Преобразует <see cref="ICollection{T}"/> в массив указанного типа и выполняет сортировку
		/// переданным делегатом.
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
		/// Преобразует <see cref="IEnumerable"/> в массив указанного типа.
		/// </summary>
		public static T[] ToArray<T>(this IEnumerable collection) where T : class
		{
			var res = new List<T>(Convert<T>(collection));
			return res.ToArray();
		}

		/// <summary>
		/// Преобразует входную коллекцию в выходной массив при помощи переданного делегата.
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
		/// Преобразует generic-перечисление к словарю.
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
		/// Преобразует перечисление к словарю.
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
		/// Возвращает реверсивный вариант сравнения.
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
		/// Выполняет переданное действие для всех элементов перечисления.
		/// </summary>
		public static void ForEach<T>(this IEnumerable<T> src, Action<T> action)
		{
			foreach (var element in src)
				action(element);
		}
	}
}