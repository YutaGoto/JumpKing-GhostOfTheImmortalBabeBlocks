using EntityComponent;
using JumpKing.MiscEntities.WorldItems;
using JumpKing.Player;

namespace JumpKing_GhostOfTheImmortalBabeBlocks.Patches
{
    internal class InventoryManager
    {
        /// <summary>
        /// for SnakeRing item. patch for HasItemEnabled method that is attatch to InventoryManager to disable SnakeRing item
        /// </summary>
        /// <param name="__result"></param>
        /// <param name="p_item"></param>
        public static void HasItemEnabledPostfix(ref bool __result, Items p_item)
        {
            PlayerEntity player = EntityManager.instance.Find<PlayerEntity>();
            if (player != null)
            {
                if (
                    player.m_body.IsOnBlock<Blocks.CursedIce>() ||
                    player.m_body.IsOnBlock<Blocks.RestrainedIce>()
                )
                {
                    if (p_item == Items.SnakeRing)
                    {
                        __result = false;
                    }
                }
            }
        }
    }
}
