using BLToolkit.Validation;

namespace R.Server.Common.ObjectModel
{
	internal class RequiredWithFormatAttribute : RequiredAttribute
	{
		public RequiredWithFormatAttribute(string formatField)
		{
			_formatField = formatField;
		}

		private string _formatField;

		public override bool IsValid(ValidationContext context)
		{
			object pf = context.TypeAccessor[_formatField].GetValue(context.Object);

			return pf == null || (PasswordFormat) pf == PasswordFormat.Clear
				?
				!base.IsValid(context)
				:
				base.IsValid(context);
		}
	}
}