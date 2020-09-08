using System.Collections.Generic;

namespace MutateScript
{
    public class MutationOutcome
    {
        public List<EffectList> EffectLists = new List<EffectList>();

        public bool TryMutate(WorldObject wo, double rng)
        {
            var success = true;

            foreach (var effectList in EffectLists)
            {
                if (rng < effectList.Chance)
                    success &= effectList.TryMutate(wo);
            }
            return success;
        }
    }
}
