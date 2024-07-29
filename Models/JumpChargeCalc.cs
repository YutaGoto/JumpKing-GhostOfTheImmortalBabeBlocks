using BehaviorTree;
using HarmonyLib;
using JumpKing.Player;

namespace JumpKing_GhostOfTheImmortalBabeBlocks.Models
{
    [HarmonyPatch(typeof(JumpState))]
    internal class JumpChargeCalc
    {
        private static float previous_timer { get; set; }

        public static int JumpFrames { get; internal set; }

        public JumpChargeCalc(Harmony harmony)
        {
            harmony.Patch(AccessTools.Method(typeof(JumpState), "MyRun"), null, new HarmonyMethod(AccessTools.Method(GetType(), "Run")));
        }

        private static void Run(TickData p_data, BTresult __result, JumpState __instance)
        {
            if (__result != BTresult.Failure)
            {
                float m_timer = (float)Traverse.Create(__instance).Field("m_timer").GetValue();
                if (__result == BTresult.Success)
                {
                    m_timer = previous_timer + p_data.delta_time * __instance.body.GetMultipliers();
                }
                if (__instance.last_result != 0)
                {
                    JumpFrames = -1;
                }
                JumpFrames++;
                previous_timer = m_timer;
            }
        }
    }
}
