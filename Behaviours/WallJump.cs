using HarmonyLib;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using Microsoft.Xna.Framework;
using System;

namespace JumpKing_GhostOfTheImmortalBabeBlocks.Behaviours
{
    public class WallJump : IBlockBehaviour
    {
        private readonly ICollisionQuery m_collisionQuery;

        public float BlockPriority => 2f;
        public bool IsPlayerOnBlock { get; set; }

        public WallJump(ICollisionQuery collisionQuery)
        {
            m_collisionQuery = collisionQuery ?? throw new System.ArgumentNullException("collisionQuery");
        }
        public bool AdditionalXCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;
            if (info.IsCollidingWith<Blocks.WallJump>() && !IsPlayerOnBlock)
            {
                bodyComp.Velocity.X = 0f;
            }
            return false;
        }

        public bool AdditionalYCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;
            if (info.IsCollidingWith<Blocks.WallJump>() && !IsPlayerOnBlock)
            {
                bodyComp.Velocity.Y = 0f;
            }
            return false;
        }

        public float ModifyXVelocity(float inputXVelocity, BehaviourContext behaviourContext)
        {
            return inputXVelocity;
        }

        public float ModifyYVelocity(float inputYVelocity, BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;
            if (IsPlayerOnBlock && bodyComp.Velocity.Y > 0f)
            {
                return 0f;
            }

            return inputYVelocity;
        }

        public float ModifyGravity(float inputGravity, BehaviourContext behaviourContext)
        {
            return inputGravity;
        }

        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;
            Rectangle hitbox = bodyComp.GetHitbox();
            m_collisionQuery.CheckCollision(hitbox, out Rectangle _, out AdvCollisionInfo info);
            IsPlayerOnBlock = info.IsCollidingWith<Blocks.WallJump>();
            if (IsPlayerOnBlock)
            {
                if (bodyComp.Velocity.X == 0f)
                {
                    bodyComp.Velocity.Y = 0f;
                }
                else
                {
                    bodyComp.Velocity.Y = Math.Min(0f, bodyComp.Velocity.Y);
                }
                Traverse.Create(bodyComp).Field("_knocked").SetValue(false);
                Traverse.Create(bodyComp).Field("_is_on_ground").SetValue(true);
            }
            return true;
        }
    }
}
