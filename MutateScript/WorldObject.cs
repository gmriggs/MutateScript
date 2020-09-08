using System;
using System.Collections.Generic;
using System.Text;

using MutateScript.Enum;

namespace MutateScript
{
    public class WorldObject
    {
        public string Name;
        public uint Guid;
        
        public int? GetProperty(PropertyInt propInt)
        {
            return null;
        }

        public double? GetProperty(PropertyFloat propFloat)
        {
            return null;
        }

        public bool? GetProperty(PropertyBool propBool)
        {
            return null;
        }

        public uint? GetProperty(PropertyDataId propDID)
        {
            return null;
        }

        public void SetProperty(PropertyInt propInt, int? value)
        {

        }

        public void SetProperty(PropertyFloat propFloat, double? value)
        {

        }

        public void SetProperty(PropertyBool propBool, bool? value)
        {

        }

        public void SetProperty(PropertyDataId propDID, uint? value)
        {

        }
    }
}
