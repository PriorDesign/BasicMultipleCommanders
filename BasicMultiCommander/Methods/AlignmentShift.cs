using Kingmaker;
using Kingmaker.DialogSystem.Blueprints;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.View.Spawners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Kingmaker.UnitLogic.Mechanics.Actions.ContextActionHealStatDamage;

namespace BasicMultiCommander.Methods
{
    public static class DebuptyAlignmentShift
    {
        public static void DebuptyApplyAlignmentShift(IAlignmentShiftProvider provider, UnitEntityData unit)
        {
            AlignmentShift alignmentShift = provider.AlignmentShift;
            if (alignmentShift.Value >= 1)
            {
                unit.Descriptor.Alignment.Shift(provider);
            }
        }
        public static bool deputyPetIsControlledBySpawner(UnitEntityData u)
        {
            bool companionSpawner = u.Get<UnitPartCompanion>().m_Spawner.IsNull;

           return !companionSpawner;
            

           
        }
    }


    
}
