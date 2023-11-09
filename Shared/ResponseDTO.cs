﻿
namespace QHSE.Shared
{
    public class ResponseDTO<T>
    {
        public bool status { get; set; }
        public string? msg { get; set; }
        public T? value { get; set; }
        public string? token { get; set; }
    }
}
