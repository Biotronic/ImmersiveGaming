using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biotronic
{
    public abstract class Enums<Temp> where Temp : class
    {
        public static TEnum Parse<TEnum>(string name) where TEnum : struct, Temp
        {
            return (TEnum)Enum.Parse(typeof(TEnum), name);
        }

        public static TEnum Parse<TEnum, TSource>(TSource source)
            where TEnum : struct, Temp
            where TSource : struct, Temp
        {
            return (TEnum)Enum.Parse(typeof(TEnum), source.ToString());
        }

        public static IEnumerable<TEnum> Values<TEnum>() where TEnum : struct, Temp
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
        }

        public static IEnumerable<string> Names<TEnum>() where TEnum : struct, Temp
        {
            return Enum.GetNames(typeof(TEnum));
        }

        public static TEnum FromIntValue<TEnum>(ulong value) where TEnum : struct, Temp
        {
            return Parse<TEnum>(value.ToString());
        }

        public static IEnumerable<TEnum> Flags<TEnum>(TEnum value) where TEnum : struct, Temp
        {
            var intValue = ToIntValue(value);
            var values = Values<TEnum>().ToArray();
            int flag = 0;
            while (flag < 64)
            {
                if (((1UL << flag) & intValue) != 0)
                {
                    yield return FromIntValue<TEnum>(1UL << flag);
                }
                flag++;
            }
        }

        public static ulong ToIntValue<TEnum>(TEnum value) where TEnum : struct, Temp
        {
            return Convert.ToUInt64(value);
        }

        private static IEnumerable<ulong> FlagValues<TEnum>() where TEnum : struct, Temp
        {
            return Values<TEnum>().Select(ToIntValue).Where(a=>a != 0);
        }

        public static IEnumerable<TEnum> FlagCombinations<TEnum>() where TEnum : struct, Temp
        {
            return new[] { new[]{0UL} }.Concat(FlagValues<TEnum>().Subsets()).Select(a => FromIntValue<TEnum>(a.Aggregate(0UL, (b, c) => b | c)));
        }
    }
    public abstract class Enums : Enums<Enum> { }

    public static partial class ExtensionMethods
    {
        public static IEnumerable<IEnumerable<T>> Subsets<T>(this IEnumerable<T> set)
        {
            var tmp = set.ToArray();
            if (tmp.Length == 0)
            {
                return new [] { new T[]{} };
            }
            else
            {
                var fst = tmp.First();
                var a = tmp.Skip(1).Subsets();
                var b = a.Select(item => item.Concat(new[]{fst}));

                return a.Concat(b);
            }
        }

        public static IEnumerable<T> Repeat<T>(this T value)
        {
            while (true)
            {
                yield return value;
            }
        }

        public static IEnumerable<T> Repeat<T>(this T value, uint times)
        {
            while (times --> 0)
            {
                yield return value;
            }
        }

        public static IEnumerable<T> ZipFilter<T>(this IEnumerable<T> a, IEnumerable<bool> b)
        {
            return a.Zip(b).Where(tmp => tmp.Item2).Select(tmp => tmp.Item1);
        }

        public static IEnumerable<Tuple<T1, T2>> Zip<T1, T2>(this IEnumerable<T1> a, IEnumerable<T2> b)
        {
            return a.Zip(b, Tuple.Create);
        }

        public static IEnumerable<TResult> SelectWhere<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult?> selector) where TResult : struct
        {
            return source.Select(selector).Where(a => a.HasValue).Select(a => a.Value);
        }

        public static Rectangle? TryUnion(this IEnumerable<Rectangle> rs)
        {
            return rs.First().TryUnion(rs.Skip(1).ToArray());
        }

        public static Rectangle? TryUnion(this Rectangle r1, params Rectangle[] rs)
        {
            var tmp = rs.ToList();
            Rectangle result = r1;
            while (tmp.Count > 0)
            {
                bool foundAdjacent = false;
                for (int i = 0; i < tmp.Count; ++i)
                {
                    var r2 = tmp[i];
                    if (result.AdjacentTo(r2))
                    {
                        result = Rectangle.Union(result, r2);
                        tmp.RemoveAt(i);
                        foundAdjacent = true;
                        break;
                    }
                }
                if (!foundAdjacent)
                {
                    return null;
                }
            }
            return result;
        }

        public static bool AdjacentTo(this Rectangle r1, Rectangle r2)
        {
            return (r1.Top == r2.Bottom || r1.Bottom == r2.Top || r1.Left == r2.Right || r1.Right == r2.Left) && !r1.IntersectsWith(r2);
        }

        public static string StringJoin(this IEnumerable<string> r, string sep = "")
        {
            return string.Join(sep, r);
        }

        public static Rectangle UnionBounds(this Rectangle r1, params Rectangle[] rs)
        {
            if (rs.Length == 0)
            {
                return r1;
            }
            else if (rs.Length > 1)
            {
                return r1.UnionBounds(rs[0].UnionBounds(rs.Skip(1).ToArray()));
            }
            else
            {
                var r2 = rs[0];
                var p1 = new Point(
                    Math.Min(r1.X, r2.X),
                    Math.Min(r1.Y, r2.Y)
                    );
                var s1 = new Size(
                    Math.Max(r1.Right, r2.Right) - p1.X,
                    Math.Max(r1.Bottom, r2.Bottom) - p1.Y
                    );
                return new Rectangle(p1, s1);
            }
        }
    }
}
