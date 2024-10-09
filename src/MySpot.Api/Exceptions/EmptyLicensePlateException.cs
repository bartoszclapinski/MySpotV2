using System;

namespace MySpot.Api.Exceptions;

public sealed class EmptyLicensePlateException() : CustomException("License plate cannot be null or empty.");
