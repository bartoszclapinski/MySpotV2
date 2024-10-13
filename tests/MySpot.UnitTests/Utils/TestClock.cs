using MySpot.Api.Services;
using MySpot.Api.ValueObjects;

namespace MySpot.UnitTests.Utils;

public class TestClock : IClock
{

	public Date Current() => new(new DateTime(2024, 10, 13));

}