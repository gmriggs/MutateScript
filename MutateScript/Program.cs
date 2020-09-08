using System;
using System.Collections.Generic;
using System.Linq;

using MutateScript.Enum;

namespace MutateScript
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var tSysMutationFilter = 0x38000001;
            
            var filename = @"Scripts\" + tSysMutationFilter.ToString("X8") + ".txt";

            var mutationFilter = Parser.Parse(filename);

            var lines = BuildScript(mutationFilter, tSysMutationFilter);

            foreach (var line in lines)
                Console.WriteLine(line);
        }

        public static List<string> BuildScript(MutationFilter mutationFilter, int tSysMutationFilter)
        {
            var lines = new List<string>();

            for (var mutationIdx = 0; mutationIdx < mutationFilter.Mutations.Count; mutationIdx++)
            {
                var mutation = mutationFilter.Mutations[mutationIdx];
                
                lines.Add($"0x{tSysMutationFilter:X8} Mutation #{mutationIdx + 1}:");
                lines.Add("");
                lines.Add($"Tier chances: {string.Join(", ", mutation.Chances)}");
                lines.Add("");

                foreach (var outcome in mutation.Outcomes)
                {
                    for (var effectListIdx = 0; effectListIdx < outcome.EffectLists.Count; effectListIdx++)
                    {
                        var effectList = outcome.EffectLists.ElementAt(effectListIdx);

                        lines.Add($"    - Chance {effectList.Chance}:");

                        for (var effectIdx = 0; effectIdx < effectList.Effects.Count; effectIdx++)
                        {
                            var effect = effectList.Effects.ElementAt(effectIdx);

                            //lines.Add($"        - EffectType: {(MutationEffectType)effect.EffectType}");

                            var curLine = "";

                            PropertyInt propInt = 0;

                            var args = new List<EffectArgument>();
                            if (effect.Quality != null)
                                args.Add(effect.Quality);
                            if (effect.Arg1 != null)
                                args.Add(effect.Arg1);
                            if (effect.Arg2 != null)
                                args.Add(effect.Arg2);
                            
                            for (var argIdx = 0; argIdx < args.Count; argIdx++)
                            {
                                var _arg = args.ElementAt(argIdx);

                                if (argIdx == 0)
                                {
                                    curLine += "        ";
                                }

                                if (argIdx == 1)
                                {
                                    switch (effect.Type)
                                    {
                                        case MutationEffectType.Assign:
                                        case MutationEffectType.AssignAdd:
                                        case MutationEffectType.AssignSubtract:
                                        case MutationEffectType.AssignMultiply:
                                        case MutationEffectType.AssignDivide:
                                            curLine += " = ";
                                            break;

                                        case MutationEffectType.Add:
                                        case MutationEffectType.AddMultiply:
                                        case MutationEffectType.AddDivide:
                                            curLine += " += ";
                                            break;

                                        case MutationEffectType.Subtract:
                                        case MutationEffectType.SubtractMultiply:
                                        case MutationEffectType.SubtractDivide:
                                            curLine += " -= ";
                                            break;

                                        case MutationEffectType.Multiply:
                                            curLine += " *= ";
                                            break;

                                        case MutationEffectType.Divide:
                                            curLine += " /= ";
                                            break;

                                        case MutationEffectType.AtLeastAdd:
                                            curLine += " >= ";
                                            break;

                                        case MutationEffectType.AtMostSubtract:
                                            curLine += " <= ";
                                            break;
                                    }
                                }

                                if (argIdx == 2)
                                {
                                    switch (effect.Type)
                                    {
                                        case MutationEffectType.AssignAdd:
                                            curLine += " + ";
                                            break;

                                        case MutationEffectType.AssignSubtract:
                                            curLine += " - ";
                                            break;

                                        case MutationEffectType.AssignMultiply:
                                        case MutationEffectType.AddMultiply:
                                        case MutationEffectType.SubtractMultiply:
                                            curLine += " * ";
                                            break;

                                        case MutationEffectType.AssignDivide:
                                        case MutationEffectType.AddDivide:
                                        case MutationEffectType.SubtractDivide:
                                            curLine += " / ";
                                            break;

                                        case MutationEffectType.AtLeastAdd:
                                            curLine += ", add ";
                                            break;

                                        case MutationEffectType.AtMostSubtract:
                                            curLine += ", sub ";
                                            break;
                                    }
                                }

                                var arg = new EffectArgument(_arg);

                                switch (arg.Type)
                                {
                                    case EffectArgumentType.Int:
                                        if (argIdx == 1 && propInt == PropertyInt.WieldRequirements)
                                            curLine += $"{(WieldRequirement)arg.IntVal}";
                                        else
                                            curLine += $"{arg.IntVal}";
                                        break;

                                    case EffectArgumentType.Double:
                                        curLine += $"{arg.DoubleVal}";
                                        break;

                                    case EffectArgumentType.Quality:

                                        switch (arg.StatType)
                                        {
                                            case StatType.Int:
                                                curLine += $"{(PropertyInt)arg.StatIdx}";
                                                if (argIdx == 0)
                                                    propInt = (PropertyInt)arg.StatIdx;
                                                break;

                                            case StatType.Bool:
                                                curLine += $"{(PropertyBool)arg.StatIdx}";
                                                break;

                                            case StatType.Float:
                                                curLine += $"{(PropertyFloat)arg.StatIdx}";
                                                break;

                                            case StatType.DID:
                                                curLine += $"{(PropertyDataId)arg.StatIdx}";
                                                break;

                                            default:
                                                curLine += $"Unknown StatType: {arg.StatType}, StatIdx: {arg.StatIdx}";
                                                break;
                                        }
                                        break;

                                    case EffectArgumentType.Random:
                                        curLine += $"Random({arg.MinVal}, {arg.MaxVal})";
                                        break;

                                    case EffectArgumentType.Variable:
                                        curLine += $"Variable[{arg.IntVal}]";
                                        break;

                                    default:
                                        curLine += $"Unknown EffectArgumentType: {arg.Type}";
                                        break;

                                }
                            }
                            lines.Add(curLine);
                        }
                        lines.Add("");
                    }
                }
            }
            return lines;
        }
    }
}
