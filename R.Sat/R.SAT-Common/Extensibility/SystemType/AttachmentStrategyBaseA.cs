using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Linq;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// ������� generic-���������� ��������� ��� <see cref="Type"/>.
	/// </summary>
	public abstract class AttachmentStrategyBase<TAttribute> : AttachmentStrategyBase
		where TAttribute : Attribute
	{
		/// <summary>
		/// �������������� ���������.
		/// </summary>
		protected AttachmentStrategyBase() : base(new [] {typeof (TAttribute)})
		{}

		/// <summary>
		/// ���������� ����������.
		/// </summary>
		protected abstract void Attach(ExtensionAttachmentContext context, TAttribute attribute);

		/// <summary>
		/// ���������� ����������.
		/// </summary>
		protected override void Attach(ExtensionAttachmentContext context, CustomAttributeData attributeData)
		{
			var ctor = attributeData.Constructor;
			var srcType = ctor.DeclaringType;
			var roMode = srcType.Assembly.ReflectionOnly;
			if (roMode)
			{
				if (srcType.AssemblyQualifiedName == null)
					throw new ExtensibilityException("Invalid attribute type");
				srcType = Type.GetType(srcType.AssemblyQualifiedName, true);
				Debug.Assert(srcType != null, "attrType != null");
				ctor =
					srcType
						.GetConstructor(
							ctor
								.GetParameters()
								.Select(info => info.ParameterType)
								.ToArray());
				Debug.Assert(ctor != null, "ctor != null");
			}
			var attr =
				ctor
					.Invoke(
						attributeData
							.ConstructorArguments
							.Select<CustomAttributeTypedArgument, object>(GetArgValue)
							.ToArray());
			if (attributeData.NamedArguments != null)
				foreach (var arg in attributeData.NamedArguments)
				{
					var propInfo = !roMode ? (PropertyInfo)arg.MemberInfo : srcType.GetProperty(arg.MemberInfo.Name);
					propInfo.SetValue(attr, arg.TypedValue.Value, null);
				}
			Attach(context, (TAttribute)attr);
		}

		private object GetArgValue(CustomAttributeTypedArgument arg)
		{
			if (!arg.ArgumentType.IsArray)
				return arg.Value;
			var elemsCollection = (ReadOnlyCollection<CustomAttributeTypedArgument>)arg.Value;
			var elemType = arg.ArgumentType.GetElementType();
			Debug.Assert(elemType != null, "elemType != null");
			var array = Array.CreateInstance(elemType, elemsCollection.Count);
			for (var i = 0; i < array.Length; i++)
				array.SetValue(elemsCollection[i].Value, i);
			return array;
		}
	}
}