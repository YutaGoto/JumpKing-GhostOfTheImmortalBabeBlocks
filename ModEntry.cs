using HarmonyLib;
using JumpKing.Level;
using JumpKing.MiscEntities.WorldItems.Inventory;
using JumpKing.MiscEntities.WorldItems;
using JumpKing.Mods;
using JumpKing.Player;
using System;
using System.Reflection;
using EntityComponent;
using JumpKing.API;
using JumpKing_GhostOfTheImmortalBabeBlocks.Models;

namespace JumpKing_GhostOfTheImmortalBabeBlocks
{
    [JumpKingMod("YutaGoto.JumpKing_GhostOfTheImmortalBabeBlocks")]
    public static class ModEntry
    {
        public static readonly string harmonyId = "YutaGoto.JumpKing_GhostOfTheImmortalBabeBlocks";
        public static Harmony harmony = new Harmony(harmonyId);

        /// <summary>
        /// Called by Jump King before the level loads
        /// </summary>
        [BeforeLevelLoad]
        public static void BeforeLevelLoad()
        {
            LevelManager.RegisterBlockFactory(new BlockFactory());
            PatchWithHarmony();
        }

        /// <summary>
        /// Called by Jump King when the level unloads
        /// </summary>
        [OnLevelUnload]
        public static void OnLevelUnload() { }

        /// <summary>
        /// Called by Jump King when the Level Starts
        /// </summary>
        [OnLevelStart]
        public static void OnLevelStart()
        {
            PlayerEntity player = EntityManager.instance.Find<PlayerEntity>();
            ICollisionQuery collisionQuery = LevelManager.Instance;

            if (player != null && collisionQuery != null)
            {
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.SpecialRainGravity), new Behaviours.SpecialRainGravity());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.RainGravity), new Behaviours.RainGravity());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.RestrainedIce), new Behaviours.RestrainedIce());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.CursedIce), new Behaviours.CursedIce());
                player.m_body.RegisterBlockBehaviour(typeof(Blocks.WallJump), new Behaviours.WallJump(collisionQuery));
            }
        }

        /// <summary>
        /// Called by Jump King when the Level Ends
        /// </summary>
        [OnLevelEnd]
        public static void OnLevelEnd() { }

        /// <summary>
        /// Setups the Harmony patching
        /// </summary>
        private static void PatchWithHarmony()
        {
            new JumpChargeCalc(harmony);

            MethodInfo isOnBlockMethodBlock = typeof(BodyComp).GetMethod("IsOnBlock", new Type[] { typeof(Type) });
            MethodInfo postfixIsOnBlockPostfixMethod = typeof(ModEntry).GetMethod("IsOnBlockPostfix");
            originalIsOnBlock = harmony.Patch(isOnBlockMethodBlock);
            harmony.Patch(isOnBlockMethodBlock, postfix: new HarmonyMethod(postfixIsOnBlockPostfixMethod));

            MethodInfo isGetMultipliers = typeof(BodyComp).GetMethod("GetMultipliers");
            MethodInfo postfixGetMultipliers = typeof(Patches.BodyComp).GetMethod("GetMultipliersPostfix");
            harmony.Patch(isGetMultipliers, postfix: new HarmonyMethod(postfixGetMultipliers));

            MethodInfo isWearingSkin = AccessTools.TypeByName("SkinManager").GetMethod("IsWearingSkin", new Type[] { typeof(Items) });
            MethodInfo postfixIsWearingSkin = typeof(Patches.SkinManager).GetMethod("IsWearingSkinPostfix");
            harmony.Patch(isWearingSkin, postfix: new HarmonyMethod(postfixIsWearingSkin));


            MethodInfo hasItemEnabled = typeof(InventoryManager).GetMethod("HasItemEnabled", new Type[] { typeof(Items) });
            MethodInfo postfixHasItemEnabled = typeof(Patches.InventoryManager).GetMethod("HasItemEnabledPostfix");
            harmony.Patch(hasItemEnabled, postfix: new HarmonyMethod(postfixHasItemEnabled));
        }

        private static MethodInfo originalIsOnBlock;

        public static void IsOnBlockPostfix(object __instance, ref bool __result, Type __0)
        {
            if (__0 == typeof(IceBlock) && originalIsOnBlock != null)
            {
                __result = (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(IceBlock) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.RestrainedIce) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.CursedIce) });
            }

            if (__0 == typeof(SnowBlock) && originalIsOnBlock != null)
            {
                __result = (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(SnowBlock) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.WallJump) });
            }

            if (__0 == typeof(SandBlock) && originalIsOnBlock != null)
            {
                __result = (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(SandBlock) }) ||
                           (bool)originalIsOnBlock.Invoke(null, new object[] { (BodyComp)__instance, typeof(Blocks.WallJump) });
            }
        }
    }
}
