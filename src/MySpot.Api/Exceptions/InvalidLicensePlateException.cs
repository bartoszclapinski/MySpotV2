using System;

namespace MySpot.Api.Exceptions;

public class InvalidLicensePlateException(string licensePlate)
    : CustomException($"Invalid license plate: '{licensePlate}'.")
{
    public string LicensePlate { get; } = licensePlate;

}
