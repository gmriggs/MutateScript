﻿using System;

using MutateScript.Enum;

namespace MutateScript
{
    public partial class EffectArgument
    {
        public EffectArgumentType Type;

        // EffectArgumentType.Quality
        public StatType StatType;
        public int StatIdx;

        // EffectArgumentType.Int
        public int IntVal;

        // EffectArgumentType.Double
        public double DoubleVal;

        // EffectArgumentType.Random
        public float MinVal;
        public float MaxVal;

        public EffectArgument() { }
        
        public EffectArgument(StatType statType, int propIdx)
        {
            Type = EffectArgumentType.Quality;
            StatType = statType;
            StatIdx = propIdx;
        }

        public EffectArgument(int val)
        {
            Type = EffectArgumentType.Int;
            IntVal = val;
        }

        public EffectArgument(double val)
        {
            Type = EffectArgumentType.Double;
            DoubleVal = val;
        }

        public EffectArgument(float minVal, float maxVal)
        {
            Type = EffectArgumentType.Random;
            MinVal = minVal;
            MaxVal = maxVal;
        }

        public EffectArgument(EffectArgument other)
        {
            if (other == null)
                return;

            Type = other.Type;

            StatType = other.StatType;
            StatIdx = other.StatIdx;

            IntVal = other.IntVal;

            DoubleVal = other.DoubleVal;

            MinVal = other.MinVal;
            MaxVal = other.MaxVal;
        }

        public object GetValue()
        {
            switch (Type)
            {
                case EffectArgumentType.Double:
                    return DoubleVal;
                case EffectArgumentType.Int:
                    return IntVal;
            }
            return null;
        }

        // output conversions
        public double ToDouble()
        {
            switch (Type)
            {
                case EffectArgumentType.Int:
                    return IntVal;
                case EffectArgumentType.Double:
                    return DoubleVal;
            }
            Console.WriteLine($"EffectArgument.ToDouble() - invalid type {Type}");
            return 0.0;
        }

        public int ToInt()
        {
            switch (Type)
            {
                case EffectArgumentType.Int:
                    return IntVal;
                case EffectArgumentType.Double:
                    return (int)DoubleVal;
            }
            Console.WriteLine($"EffectArgument.ToDouble() - invalid type {Type}");
            return 0;
        }

        // gdle custom
        
        public bool IsValid = false;

        public bool ResolveValue(WorldObject item)
        {
            // type:enum - invalid, double, int32, quality (2 int32s: type and quality), float range (min, max), variable index (int32)
            switch (Type)
            {
                case EffectArgumentType.Double:
                case EffectArgumentType.Int:
                    // these are ok as-is
                    IsValid = true;
                    break;

                case EffectArgumentType.Quality:

                    switch (StatType)
                    {
                        case StatType.Int:

                            Type = EffectArgumentType.Int;
                            var intVal = item.GetProperty((PropertyInt)StatIdx);
                            if (intVal != null)
                            {
                                IntVal = intVal.Value;
                                IsValid = true;
                            }
                            break;

                        case StatType.Bool:

                            Type = EffectArgumentType.Int;
                            IntVal = Convert.ToInt32(item.GetProperty((PropertyBool)StatIdx) ?? false);
                            IsValid = true;
                            break;

                        case StatType.Float:

                            Type = EffectArgumentType.Double;
                            var doubleVal = item.GetProperty((PropertyFloat)StatIdx);
                            if (doubleVal != null)
                            {
                                DoubleVal = doubleVal.Value;
                                IsValid = true;
                            }
                            break;

                        case StatType.DID:

                            Type = EffectArgumentType.Int;
                            IntVal = (int)(item.GetProperty((PropertyDataId)StatIdx) ?? 0);
                            IsValid = true;
                            break;
                    }

                    break;

                case EffectArgumentType.Random:

                    DoubleVal = ThreadSafeRandom.Next(MinVal, MaxVal);
                    Type = EffectArgumentType.Double;
                    IsValid = true;
                    break;

                case EffectArgumentType.Variable:

                    /*if (IntVal < 0 || IntVal >= GTVariables.Count)
                        break;

                    this = GTVariables[IntVal];
                    IsValid = true;*/
                    Console.WriteLine($"TODO: EffectArgumentType.Variable");
                    break;
            }

            return IsValid;
        }

        public bool StoreValue(WorldObject item)
        {
            // here the resolved value (result) is applied to the qualities specified by our value

            if (!IsValid)
                return false;

            switch (Type)
            {
                case EffectArgumentType.Quality:

                    switch (StatType)
                    {
                        case StatType.Int:
                            item.SetProperty((PropertyInt)StatIdx, ToInt());
                            break;

                        case StatType.Bool:
                            item.SetProperty((PropertyBool)StatIdx, Convert.ToBoolean(ToInt()));
                            break;

                        case StatType.Float:
                            item.SetProperty((PropertyFloat)StatIdx, ToDouble());
                            break;

                        case StatType.DID:
                            item.SetProperty((PropertyDataId)StatIdx, (uint)ToInt());
                            break;
                    }
                    break;

                case EffectArgumentType.Variable:

                    // TODO
                    /*if (IntVal < 0 || IntVal > GTVariables.Count)
                        break;

                    GTVariables[IntVal] = result;*/
                    break;
            }
            return true;
        }
    }
}
