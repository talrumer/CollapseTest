using System.Collections.Generic;
using Collapse.Blocks;
using UnityEngine;

namespace Collapse
{
    /**
     * Partial class for separating the main functions that are needed to be modified in the context of this test
     */
    public partial class BoardManager
    {

        [SerializeField]
        float timeBetweenDisappearance = 0.03f;

        /**
         * Trigger a bomb
         */
        public void TriggerBomb(Bomb bomb)
        {
            // if ()
            explosionTrigger(bomb.GridPosition.x + 1, bomb.GridPosition.y);
            explosionTrigger(bomb.GridPosition.x + 1, bomb.GridPosition.y + 1);
            explosionTrigger(bomb.GridPosition.x + 1, bomb.GridPosition.y - 1);

            explosionTrigger(bomb.GridPosition.x - 1, bomb.GridPosition.y);
            explosionTrigger(bomb.GridPosition.x - 1, bomb.GridPosition.y + 1);
            explosionTrigger(bomb.GridPosition.x - 1, bomb.GridPosition.y - 1);

            explosionTrigger(bomb.GridPosition.x, bomb.GridPosition.y + 1);
            explosionTrigger(bomb.GridPosition.x, bomb.GridPosition.y - 1);

            ScheduleRegenerateBoard();
        }

        public void explosionTrigger(int col, int row)
        {
            if (isOutOfBound(blocks, col, row)) return;
            Block block = blocks[col, row];

            if (block == null) return;

            if (block.Type == BlockType.Bomb)
            {
                ((Bomb)block).shakeAndTrigger();
            }
            else
            {
                TriggerMatch(block);
            }
        }

        /**
         * Trigger a match
         */
        public void TriggerMatch(Block block)
        {
            // Find all blocks in this match
            var results = new List<Block>();
            var tested = new List<(int row, int col)>();
            FindChainRecursive(block.Type, block.GridPosition.x, block.GridPosition.y, tested, results);

            // Trigger blocks
            for (var i = 0; i < results.Count; i++)
            {
                results[i].Triger(i * timeBetweenDisappearance);
            }

            // Regenerate
            ScheduleRegenerateBoard();
        }


        /**
         * Recursively collect all neighbors of same type to build a full list of blocks in this "chain" in the results list
         */
        private void FindChainRecursive(BlockType type, int col, int row, List<(int row, int col)> testedPositions,
            List<Block> results)
        {

            // getting bounds
            int colsCount = blocks.GetLength(0);
            int rowsCount = blocks.GetLength(1);

            // check if it's out of bounds
            if (isOutOfBound(blocks, col, row)) return;

            // null check (happens on neighbouring blocks quick clicks)
            if (blocks[col, row] == null) return;

            // check if we already visited this block
            if (testedPositions.Contains((col, row))) return;

            testedPositions.Add((col, row));

            if (blocks[col, row].Type == type)
            {
                results.Add(blocks[col, row]);
                FindChainRecursive(type, col + 1, row, testedPositions, results);
                FindChainRecursive(type, col - 1, row, testedPositions, results);
                FindChainRecursive(type, col, row + 1, testedPositions, results);
                FindChainRecursive(type, col, row - 1, testedPositions, results);
            }
        }

        public bool isOutOfBound<T>(T[,] mat, int col, int row)
        {
            int colsCount = mat.GetLength(0);
            int rowsCount = mat.GetLength(1);
            // check if it's out of bounds
            return (
                col >= colsCount ||
                col < 0 ||
                row >= rowsCount ||
                row < 0
            );
        }
    }
}