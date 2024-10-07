using System;

namespace MySpot.Api.Exceptions;

public sealed class EmptyLicensePlateException : CustomException
{
    public EmptyLicensePlateException() 
        : base("License plate cannot be null or empty.")
    {
    }
    
}
