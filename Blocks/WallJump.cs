using JumpKing.Level;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpKing_GhostOfTheImmortalBabeBlocks.Blocks
{
    public class WallJump : BoxBlock
    {
        protected override bool canBlockPlayer => false;
        public WallJump(Rectangle p_collider) : base(p_collider) { }
    }
}
