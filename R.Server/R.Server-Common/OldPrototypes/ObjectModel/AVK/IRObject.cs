namespace R.Server.Common.ObjectModel
{
	/// <summary>
	/// Базовый интерфейс всех объектов сайта
	/// </summary>
	/// <remarks>ID может быть типа Guid, это надо обсуждать</remarks>
	public interface IRObject
	{
		int ID { get; }
	}
}