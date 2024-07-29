using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using JumpKing_GhostOfTheImmortalBabeBlocks.Models;
using System;

namespace JumpKing_GhostOfTheImmortalBabeBlocks.Behaviours
{
    public class SpecialRainGravity : IBlockBehaviour
    {
        public float BlockPriority => 2f;

        public bool IsPlayerOnBlock { get; set; }
        private float yBaseVelocity = -7f;

        public bool AdditionalXCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            return false;
        }

        public bool AdditionalYCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            return false;
        }

        public float ModifyXVelocity(float inputXVelocity, BehaviourContext behaviourContext)
        {
            return inputXVelocity;
        }

        public float ModifyYVelocity(float inputYVelocity, BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;
            if (behaviourContext?.CollisionInfo?.StartOfFrameCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.StartOfFrameCollisionInfo.IsCollidingWith<Blocks.SpecialRainGravity>();
            }

            // in not onGround
            if (IsPlayerOnBlock)
            {
                yBaseVelocity = AdjustYVelocity();
                bodyComp.Velocity.Y = Math.Max(bodyComp.Velocity.Y, yBaseVelocity);
            }

            if (bodyComp.Velocity.Y > 0 && IsPlayerOnBlock)
            {
                bodyComp.Velocity.Y = Math.Min(bodyComp.Velocity.Y + 0.15f, 10f);
            }

            return inputYVelocity;
        }


        public float ModifyGravity(float inputGravity, BehaviourContext behaviourContext)
        {
            return inputGravity;
        }

        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            return true;
        }

        private float AdjustYVelocity()
        {
            int baseFrame = JumpChargeCalc.JumpFrames;
            if (baseFrame <= 22)
            {
                return -7.05f;
            }
            else if (baseFrame == 23)
            {
                return -7.038f;
            }
            else if (baseFrame == 24)
            {
                return -7.026f;
            }
            else if (baseFrame == 25)
            {
                return -7.014f;
            }
            else if (baseFrame == 26)
            {
                return -7.002f;
            }
            else if (baseFrame == 27)
            {
                return -6.991f;
            }
            else
            {
                return -6.988f;
            }
        }
    }
}
