using ErikMaths;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing;
using JumpKing.Player;

namespace JumpKing_GhostOfTheImmortalBabeBlocks.Behaviours
{
    public class CursedIce : IBlockBehaviour
    {
        public float BlockPriority => 2f;

        public bool IsPlayerOnBlock { get; set; }
        public bool IsPlayerTouchWall { get; set; }
        private bool isCursed = false;

        public CursedIce() { }

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
            BodyComp bodyComp = behaviourContext.BodyComp;
            isCursed = false;

            if (IsPlayerOnBlock && bodyComp.IsOnBlock<Blocks.CursedIce>())
            {
                if (bodyComp.IsOnGround) isCursed = true;
                if (bodyComp.Velocity.Y < 0.0f && isCursed)
                {
                    if (bodyComp.Velocity.X > 0.0f) bodyComp.Position.X -= 5.0f;
                    else if (bodyComp.Velocity.X < 0.0f) bodyComp.Position.X += 5.0f;
                    bodyComp.Velocity.X *= -1.0f;
                }
            }

            return inputXVelocity;
        }

        public float ModifyYVelocity(float inputYVelocity, BehaviourContext behaviourContext)
        {
            return inputYVelocity;
        }

        public float ModifyGravity(float inputGravity, BehaviourContext behaviourContext)
        {
            return inputGravity;
        }

        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            if (behaviourContext.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.CursedIce>();
            }

            BodyComp bodyComp = behaviourContext.BodyComp;

            if (IsPlayerOnBlock)
            {
                bodyComp.Velocity.X = ErikMath.MoveTowards(bodyComp.Velocity.X, 0f, PlayerValues.ICE_FRICTION);
            }

            return true;
        }
    }
}
