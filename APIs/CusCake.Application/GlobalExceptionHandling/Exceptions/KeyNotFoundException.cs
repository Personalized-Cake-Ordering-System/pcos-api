﻿
namespace CusCake.Application.GlobalExceptionHandling.Exceptions
{
    public class KeyNotFoundException : Exception
    {
        public KeyNotFoundException(string? message) : base(message)
        {
        }
    }
}
