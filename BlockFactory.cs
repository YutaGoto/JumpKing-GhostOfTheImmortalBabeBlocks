using JumpKing.API;
using JumpKing.Level.Sampler;
using JumpKing.Level;
using JumpKing.Workshop;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using JumpKing_GhostOfTheImmortalBabeBlocks.Blocks;

namespace JumpKing_GhostOfTheImmortalBabeBlocks
{
    internal class BlockFactory: IBlockFactory
    {
        private static readonly Color CODE_SPECIAL_RAIN_GRAVITY = new Color(255, 190, 190);
        private static readonly Color CODE_RAIN_GRAVITY = new Color(255, 192, 192);
        private static readonly Color CODE_WALL_JUMP = new Color(16, 16, 16);

        private static readonly Color CODE_RESTRAINED_ICE = new Color(128, 128, 0);
        private static readonly Color CODE_CURSED_ICE = new Color(144, 144, 0);



        private readonly HashSet<Color> supportedBlockCodes = new HashSet<Color>
        {
            CODE_SPECIAL_RAIN_GRAVITY,
            CODE_RAIN_GRAVITY,
            CODE_WALL_JUMP,
            CODE_RESTRAINED_ICE,
            CODE_CURSED_ICE,
        };

        private readonly HashSet<Color> solidBlocksCode = new HashSet<Color>
        {
            CODE_WALL_JUMP,
            CODE_RESTRAINED_ICE,
            CODE_CURSED_ICE,
        };

        public bool CanMakeBlock(Color blockCode, Level level)
        {
            return supportedBlockCodes.Contains(blockCode);
        }

        public bool IsSolidBlock(Color blockCode)
        {
            return solidBlocksCode.Contains(blockCode);
        }

        public IBlock GetBlock(Color blockCode, Rectangle blockRect, JumpKing.Workshop.Level level, LevelTexture textureSrc, int currentScreem, int x, int y)
        {
            if (blockCode == CODE_SPECIAL_RAIN_GRAVITY)
            {
                return new SpecialRainGravity(blockRect);
            }
            else if (blockCode == CODE_RAIN_GRAVITY)
            {
                return new RainGravity(blockRect);
            }
            else if (blockCode == CODE_WALL_JUMP)
            {
                return new WallJump(blockRect);
            }
            else if (blockCode == CODE_RESTRAINED_ICE)
            {
                return new RestrainedIce(blockRect);
            }
            else if (blockCode == CODE_CURSED_ICE)
            {
                return new CursedIce(blockRect);
            }
            else
            {
                throw new InvalidOperationException($"{typeof(BlockFactory).Name} is unable to create a block of Color code ({blockCode.R}, {blockCode.G}, {blockCode.B})");
            }
        }

    }
}
