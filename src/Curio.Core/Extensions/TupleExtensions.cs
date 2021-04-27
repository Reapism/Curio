using System;
using System.Collections.Generic;

namespace Curio.Core.Extensions
{
    public static class TupleExtensions
    {
        public static Tuple<T1, T2> AsTuple<T1, T2>(this KeyValuePair<T1, T2> args)
        {
            return Tuple.Create<T1, T2>(args.Key, args.Value);
        }

        public static Tuple<T1, T2> AsTuple<T1, T2>(this (T1, T2) args)
        {
            return Tuple.Create<T1, T2>(args.Item1, args.Item2);
        }

        public static Tuple<T1, T2, T3> AsTuple<T1, T2, T3>(this (T1, T2, T3) args)
        {
            return Tuple.Create<T1, T2, T3>(args.Item1, args.Item2, args.Item3);
        }

        public static Tuple<T1, T2, T3, T4> AsTuple<T1, T2, T3, T4>(this (T1, T2, T3, T4) args)
        {
            return Tuple.Create<T1, T2, T3, T4>(args.Item1, args.Item2, args.Item3, args.Item4);
        }
    }
}
