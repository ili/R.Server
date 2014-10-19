using System;

namespace R.Client.Model
{
	[AttributeUsage(AttributeTargets.Class)]
	public class UIControllerAttrbute : Attribute
	{
		private readonly Type _modelType;
		private readonly string _layer;

		public UIControllerAttrbute(Type modelType, string layer)
		{
			_modelType = modelType;
			_layer = layer;
		}

		/// <summary>
		/// Model type.
		/// </summary>
		public Type ModelType
		{
			get { return _modelType; }
		}

		/// <summary>
		/// Layer of extension.
		/// </summary>
		public string Layer
		{
			get { return _layer; }
		}
	}
}
