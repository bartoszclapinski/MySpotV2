namespace MySpot.Api.Exceptions;

public class InvalidEntityIdException(Guid value) : CustomException($"Invalid entity ID: {value}.")
{
		public Guid Value { get; } = value;	
}