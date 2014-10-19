using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using JetBrains.Annotations;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// Reflection helper methods.
	/// </summary>
	public static class ReflectionHelper
	{
		/// <summary>
		/// Return custom attributes, specified by <see cref="T"/> type argument.
		/// </summary>
		[NotNull]
		public static IEnumerable<T> GetCustomAttributes<T>(
			[NotNull] this ICustomAttributeProvider attrsProvider,
			bool inherit)
			where T : Attribute
		{
			if (attrsProvider == null)
				throw new ArgumentNullException("attrsProvider");

			return
				attrsProvider
					.GetCustomAttributes(typeof (T), inherit)
					.Cast<T>();
		}

		/// <summary>
		/// Return custom attribute, specified by <see cref="T"/> type argument.
		/// </summary>
		[CanBeNull]
		public static T GetCustomAttribute<T>(
			[NotNull] this ICustomAttributeProvider attrsProvider,
			bool inherit)
			where T : Attribute
		{
			if (attrsProvider == null)
				throw new ArgumentNullException("attrsProvider");

			return
				attrsProvider
					.GetCustomAttributes<T>(inherit)
					.FirstOrDefault();
		}
	}
}