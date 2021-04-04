using System;
using System.Collections.Generic;
using Abilities;

namespace Modifiers
{
    public static class PrimitiveModifyingFunctions
    {
        public static Func<float, float, float> AdditionModifier = (f1, f2) => f1 + f2;
        
        public static readonly Func<float, float, float> DivisionModifier = (f1, f2) => f1 / f2;
    }
}