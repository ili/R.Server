using System;

namespace R.Client.Model
{
	public class UIControllerInfo
	{
		private readonly Type _controllerType;
		private readonly Type _modelType;
		private readonly string _layer = "";

		public UIControllerInfo(Type controllerType, Type modelType, string layer)
		{
			if (controllerType == null)
				throw new ArgumentNullException("controllerType");
			if (modelType == null)
				throw new ArgumentNullException("modelType");
			_controllerType = controllerType;
			_modelType = modelType;
			_layer = layer;
		}

		public Type ControllerType
		{
			get { return _controllerType; }
		}

		public Type ModelType
		{
			get { return _modelType; }
		}

		public string Layer
		{
			get { return _layer; }
		}
	}
}
