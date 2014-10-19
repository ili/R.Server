using System;
using System.Collections;
using System.ComponentModel;

using BLToolkit.ComponentModel;
using BLToolkit.Mapping;
using BLToolkit.Validation;

namespace R.Common.ObjectModel
{
	[Serializable, Trimmable]
	public abstract class ObjectBase<T> : ICustomTypeDescriptor
		where T : ObjectBase<T>
	{
		#region ICustomTypeDescriptor Members

		[NonSerialized]
		private CustomTypeDescriptorImpl _descriptor;
		private CustomTypeDescriptorImpl  Descriptor
		{
			get { return _descriptor ?? (_descriptor = new CustomTypeDescriptorImpl(GetType())); }
		}

		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return Descriptor.GetAttributes();
		}

		string ICustomTypeDescriptor.GetClassName()
		{
			return Descriptor.GetClassName();
		}

		string ICustomTypeDescriptor.GetComponentName()
		{
			return Descriptor.GetComponentName();
		}

		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return Descriptor.GetConverter();
		}

		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return Descriptor.GetDefaultEvent();
		}

		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return Descriptor.GetDefaultProperty();
		}

		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return Descriptor.GetEditor(editorBaseType);
		}

		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return Descriptor.GetEvents(attributes);
		}

		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return Descriptor.GetEvents();
		}

		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			return Descriptor.GetProperties(attributes);
		}

		private static Hashtable _hashDescriptors = new Hashtable();

		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			PropertyDescriptorCollection col = (PropertyDescriptorCollection)_hashDescriptors[GetType()];

			if (col == null)
			{
				col = Descriptor.GetProperties();

				_hashDescriptors.Add(GetType(), col);
			}

			return col;
		}

		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return Descriptor.GetPropertyOwner(pd);
		}

		#endregion

		#region Validation

		public virtual void Validate()
		{
			Validator.Validate(this);
		}

		public virtual bool IsValid(string fieldName)
		{
			return Validator.IsValid(this, fieldName);
		}

		public virtual string[] GetErrorMessages(string fieldName)
		{
			return Validator.GetErrorMessages(this, fieldName);
		}

		#endregion

		public virtual T Clone()
		{
			return (T)Map.ObjectToObject(this, GetType(), null);
		}
	}
}
