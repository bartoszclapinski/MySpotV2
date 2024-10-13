using MySpot.Application.Services;
using MySpot.Core.ValueObjects;

namespace MySpot.UnitTests.Utils;

public class TestClock : IClock
{

	public Date Current() => new(new DateTime(2024, 10, 13));

}