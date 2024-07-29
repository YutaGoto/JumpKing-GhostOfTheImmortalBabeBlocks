using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using JumpKing_GhostOfTheImmortalBabeBlocks.Models;
using System;

namespace JumpKing_GhostOfTheImmortalBabeBlocks.Behaviours
{
    internal class RainGravity : IBlockBehaviour
    {
        public float BlockPriority => 2f;
        private float yBaseVelocity;

        public bool IsPlayerOnBlock { get; set; }

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
                IsPlayerOnBlock = behaviourContext.CollisionInfo.StartOfFrameCollisionInfo.IsCollidingWith<Blocks.RainGravity>();
            }

            if (IsPlayerOnBlock)
            {
                yBaseVelocity = AdjustYVelocity();
                bodyComp.Velocity.Y = Math.Max(bodyComp.Velocity.Y, yBaseVelocity);
            }

            if (bodyComp.Velocity.Y > 0 && IsPlayerOnBlock)
            {
                bodyComp.Velocity.Y = Math.Min(bodyComp.Velocity.Y, 10f);
            }

            return inputYVelocity;
        }


        public float ModifyGravity(float inputGravity, BehaviourContext behaviourContext)
        {
            return inputGravity * (IsPlayerOnBlock ? 1.15f : 1.0f);
        }

        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            return true;
        }

        private float AdjustYVelocity()
        {
            int baseFrame = JumpChargeCalc.JumpFrames;
            switch (baseFrame)
            {
                case 0:  return  0.000f;
                case 1:  return -0.700f;
                case 2:  return -0.950f;
                case 3:  return -1.200f;
                case 4:  return -1.450f;
                case 5:  return -1.700f;
                case 6:  return -1.950f;
                case 7:  return -2.200f;
                case 8:  return -2.450f;
                case 9:  return -2.700f;
                case 10: return -2.950f;
                case 11: return -3.200f;
                case 12: return -3.450f;
                case 13: return -3.700f;
                case 14: return -3.950f;
                case 15: return -4.200f;
                case 16: return -4.450f;
                case 17: return -4.698f;
                case 18: return -4.950f;
                case 19: return -5.200f;
                case 20: return -5.450f;
                case 21: return -5.655f;
                case 22: return -5.950f;
                case 23: return -6.203f;
                case 24: return -6.462f;
                case 25: return -6.715f;
                case 26: return -6.970f;
                case 27: return -7.228f;
                default: return -7.435f;
            }
        }
    }
}
