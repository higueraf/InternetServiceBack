﻿namespace InternetServiceBack.Dtos.Common
{
    public class GenericResponseDto<T>
    {
        public int statusCode { get; set; }
        public T? data { get; set; }
        public string? message { get; set; }
    }
}
