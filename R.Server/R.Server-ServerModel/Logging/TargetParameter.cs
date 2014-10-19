using System;

namespace R.Server.ServerModel
{
	public class TargetParameter
	{
		public TargetParameter(string name, string value)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

			Name = name;
			Value = value;
		}

		public string Name { get; private set; }
		public string Value { get; private set; }
	}
}