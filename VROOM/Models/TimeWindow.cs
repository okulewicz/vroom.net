using Newtonsoft.Json;
using System;
using VROOM.Converters;

namespace VROOM
{
    public readonly struct TimeWindow
    {
        public int Start { get; }
        public int End { get; }
    }
}