using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyrimMouseHider
{
    public abstract class Enums<Temp> where Temp : class
    {
        public static TEnum Parse<TEnum>(string name) where TEnum : struct, Temp
        {
            return (TEnum)Enum.Parse(typeof(TEnum), name);
        }

        public static IEnumerable<TEnum> Values<TEnum>() where TEnum : struct, Temp
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
        }

        public static IEnumerable<string> Names<TEnum>() where TEnum : struct, Temp
        {
            return Enum.GetNames(typeof(TEnum));
        }
    }
    public abstract class Enums : Enums<Enum> { }

    public static partial class ExtensionMethods
    {
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
