using System;
using System.Collections.Generic;

namespace Demineur.Elements
{
    public class Board
    {
        private List<List<Block>> map;

        // 31 * 25 -> 775px large
        // 16 * 25 −> 400px haut

        public readonly uint mapSizeX = 31;
        public readonly uint mapSizeY = 16;

        public readonly uint mapWidth;
        public readonly uint mapHeight;

        public List<List<Block>> Map
        {
            get => this.map;
            set => this.map = value;
        }

        public Board()
        {
            this.map = new List<List<Block>>();
            this.mapWidth = this.mapSizeX * Block.CASE_SIZE;
            this.mapHeight = this.mapSizeY * Block.CASE_SIZE;
            this.InitBoard();
        }

        public Block getBlockAtPos(int x, int y)
        {
            return this.map[y][x];
        }

        private void InitBoard()
        {
            uint blockSize = Block.CASE_SIZE;
            Random random = new Random();

            uint i = 0;
            for(int y = 0; y < mapSizeY; y++)
            {
                this.map.Add(new List<Block>());
                for(int x = 0; x < mapSizeX; x++)
                {
                    this.map[y].Add(new Block((uint)x, (uint)y, (uint)(blockSize * x), (uint)(blockSize * y), random.Next(0, 100) > 83, false, false, 0));
                    i++;
                }
            }

            for(int y_for = 0; y_for < mapSizeY; y_for++)
            {
                for(int x_for = 0; x_for < mapSizeX; x_for++)
                {
                    for(int y = -1; y <= 1; y++)
                    {
                        for(int x = -1; x <= 1; x++)
                        {
                            if(x == 0 && y == 0)
                                continue;

                            if(
                            (x < 0 && x_for + x < 0) ||
                            (x > 0 && x_for + x >= this.mapSizeX) ||
                            (y < 0 && y_for + y < 0) ||
                            (y > 0 && y_for + y >= this.mapSizeY)
                            )
                                continue;

                            if(this.map[y_for + y][x_for + x].IsMine)
                                this.map[y_for][x_for].MinesAround++;
                        }
                    }
                }
            }
        }
    }
}