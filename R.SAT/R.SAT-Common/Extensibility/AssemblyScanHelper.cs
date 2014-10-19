using System;
using System.Collections.Generic;
using System.Reflection;

namespace Rsdn.SmartApp
{
	/// <summary>
	/// ��������������� ����� ��� ������ �� ��������.
	/// </summary>
	public class AssemblyScanHelper
	{
		private readonly Dictionary<Assembly, Type[]> _assemblies =
			new Dictionary<Assembly, Type[]>();

		private Type[] _types;

		/// <summary>
		/// �������� ������.
		/// </summary>
		public void AddAssembly(Assembly asm)
		{
			if (asm == null)
				throw new ArgumentNullException("asm");
			if (_assemblies.ContainsKey(asm))
				throw new ArgumentException("Assembly '" + asm.GetName().Name + "' already added");
			_types = null;
			_assemblies.Add(asm, asm.GetTypes());
		}

		/// <summary>
		/// �������� ������ �� ����, ������� � ��� ����������.
		/// </summary>
		public void AddAssembly(Type type)
		{
			if (type == null)
				throw new ArgumentNullException("type");
			AddAssembly(type.Assembly);
		}

		/// <summary>
		/// �������� ������ �� �� �����.
		/// </summary>
		public void AddAssembly(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");
			var asm = Assembly.Load(name);
			if (asm == null)
				throw new ArgumentException("Could not load assembly '" + name + "'");
			AddAssembly(asm);
		}

		/// <summary>
		/// �������� ���� ����������� ������.
		/// </summary>
		public Type[] GetTypes()
		{
			if (_types == null)
			{
				var typeList = new List<Type>();
				foreach (var asmTypes in _assemblies.Values)
					typeList.AddRange(asmTypes);
				_types = typeList.ToArray();
			}
			return _types;
		}
	}
}