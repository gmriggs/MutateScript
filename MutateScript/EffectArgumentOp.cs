using System;

using MutateScript.Enum;

namespace MutateScript
{
    public partial class EffectArgument
    {
        public static EffectArgument operator +(EffectArgument a, EffectArgument b)
        {
            switch (a.Type)
            {
                case EffectArgumentType.Double:

                    switch (b.Type)
                    {
                        case EffectArgumentType.Double:
                            return new EffectArgument(a.DoubleVal + b.DoubleVal);
                        case EffectArgumentType.Int:
                            return new EffectArgument(a.DoubleVal + b.IntVal);
                    }
                    break;

                case EffectArgumentType.Int:

                    switch (b.Type)
                    {
                        case EffectArgumentType.Double:
                            return new EffectArgument((int)(a.IntVal + b.DoubleVal));
                        case EffectArgumentType.Int:
                            return new EffectArgument(a.IntVal + b.IntVal);
                    }
                    break;
            }

            Console.WriteLine($"EffectArgument.Add() - invalid type {a.Type}, {b.Type}");

            return null;
        }

        public static EffectArgument operator -(EffectArgument a, EffectArgument b)
        {
            switch (a.Type)
            {
                case EffectArgumentType.Double:

                    switch (b.Type)
                    {
                        case EffectArgumentType.Double:
                            return new EffectArgument(a.DoubleVal - b.DoubleVal);
                        case EffectArgumentType.Int:
                            return new EffectArgument(a.DoubleVal - b.IntVal);
                    }
                    break;

                case EffectArgumentType.Int:

                    switch (b.Type)
                    {
                        case EffectArgumentType.Double:
                            return new EffectArgument((int)(a.IntVal - b.DoubleVal));
                        case EffectArgumentType.Int:
                            return new EffectArgument(a.IntVal - b.IntVal);
                    }
                    break;
            }

            Console.WriteLine($"EffectArgument.Subtract() - invalid type {a.Type}, {b.Type}");

            return null;
        }

        public static EffectArgument operator *(EffectArgument a, EffectArgument b)
        {
            switch (a.Type)
            {
                case EffectArgumentType.Double:

                    switch (b.Type)
                    {
                        case EffectArgumentType.Double:
                            return new EffectArgument(a.DoubleVal * b.DoubleVal);
                        case EffectArgumentType.Int:
                            return new EffectArgument(a.DoubleVal * b.IntVal);
                    }
                    break;

                case EffectArgumentType.Int:

                    switch (b.Type)
                    {
                        case EffectArgumentType.Double:
                            return new EffectArgument((int)(a.IntVal * b.DoubleVal));
                        case EffectArgumentType.Int:
                            return new EffectArgument(a.IntVal * b.IntVal);
                    }
                    break;
            }

            Console.WriteLine($"EffectArgument.Multiply() - invalid type {a.Type}, {b.Type}");

            return null;
        }

        public static EffectArgument operator /(EffectArgument a, EffectArgument b)
        {
            switch (a.Type)
            {
                case EffectArgumentType.Double:

                    switch (b.Type)
                    {
                        case EffectArgumentType.Double:
                            return b.DoubleVal != 0 ? new EffectArgument(a.DoubleVal / b.DoubleVal) : a;
                        case EffectArgumentType.Int:
                            return b.IntVal != 0 ? new EffectArgument(a.DoubleVal / b.IntVal) : a;
                    }
                    break;

                case EffectArgumentType.Int:

                    switch (b.Type)
                    {
                        case EffectArgumentType.Double:
                            return b.DoubleVal != 0 ? new EffectArgument((int)(a.IntVal / b.DoubleVal)) : a;
                        case EffectArgumentType.Int:
                            return b.IntVal != 0 ? new EffectArgument(a.IntVal / b.IntVal) : a;
                    }
                    break;
            }

            Console.WriteLine($"EffectArgument.Divide() - invalid type {a.Type}, {b.Type}");

            return null;
        }

        public static bool operator <(EffectArgument a, EffectArgument b)
        {
            if (a.Type != b.Type)
            {
                Console.WriteLine($"EffectArgument.LessThan() - type mismatch {a.Type} {b.Type}");
                return false;
            }

            switch (a.Type)
            {
                case EffectArgumentType.Double:
                    return a.DoubleVal < b.DoubleVal;

                case EffectArgumentType.Int:
                    return a.IntVal < b.IntVal;
            }
            return false;
        }

        public static bool operator >(EffectArgument a, EffectArgument b)
        {
            if (a.Type != b.Type)
            {
                Console.WriteLine($"EffectArgument.GreaterThan() - type mismatch {a.Type} {b.Type}");
                return false;
            }

            switch (a.Type)
            {
                case EffectArgumentType.Double:
                    return a.DoubleVal > b.DoubleVal;

                case EffectArgumentType.Int:
                    return a.IntVal > b.IntVal;
            }
            return false;
        }
    }
}
