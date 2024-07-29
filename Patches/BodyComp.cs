using EntityComponent;
using JumpKing.API;
using JumpKing.Level;
using JumpKing.Player;
using Microsoft.Xna.Framework;

namespace JumpKing_GhostOfTheImmortalBabeBlocks.Patches
{
    internal class BodyComp
    {
        public static void GetMultipliersPostfix(ref float __result)
        {
            ICollisionQuery collisionQuery = LevelManager.Instance;
            PlayerEntity player = EntityManager.instance.Find<PlayerEntity>();
            Rectangle hitbox = player.m_body.GetHitbox();
            collisionQuery.CheckCollision(hitbox, out Rectangle _, out AdvCollisionInfo info);
            if (info.IsCollidingWith<Blocks.SpecialRainGravity>()) __result *= 1.28f;
            if (info.IsCollidingWith<Blocks.RainGravity>()) __result *= 1.28f;
        }
    }
}
