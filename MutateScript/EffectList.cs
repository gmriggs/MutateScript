using System.Collections.Generic;

namespace MutateScript
{
    public class EffectList
    {
        public float Chance;
        public List<Effect> Effects = new List<Effect>();

        public bool TryMutate(WorldObject wo)
        {
            var success = true;

            foreach (var effect in Effects)
                success &= effect.TryMutate(wo);      // stop completely on failure?

            return success;
        }
    }
}
