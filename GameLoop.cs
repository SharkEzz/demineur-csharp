using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using Demineur.Elements;

namespace Demineur
{
    public class GameLoop
    {
        private RenderWindow window;

        public static Font f = new Font("arial.ttf");

        private Board board;

        private bool shouldUpdate = true;

        private bool isLoose = false;

        public GameLoop()
        {
            this.board = new Board();
            this.window = new RenderWindow(new VideoMode(1280, 720), "Demineur");
            this.window.KeyPressed += this.OnKeyPress;
            this.window.Resized += (object sender, SizeEventArgs e) => this.shouldUpdate = true;
            this.window.MouseButtonPressed += this.HandleClick;
        }

        public void Loop()
        {
            while(window.IsOpen)
            {
                window.DispatchEvents();
                if(this.shouldUpdate)
                {
                    window.Clear();

                    if(!this.isLoose)
                    {
                        foreach(List<Block> blockList in this.board.Map)
                        {
                            foreach(Block b in blockList)
                            {
                                window.Draw(b.Shape);
                                if(b.IsOpened)
                                {
                                    window.Draw(b.Text);
                                }
                                window.Draw(new Text("Démineur", GameLoop.f)
                                {
                                    Position = new Vector2f(this.board.mapWidth + 100, this.board.mapHeight / 2)
                                });
                            }
                        }
                    }
                    else
                    {
                        window.Draw(new Text("Perdu :(", GameLoop.f));
                    }
                    
                    window.Display();
                    this.shouldUpdate = false;
                }
            }
        }

        private void HandleClick(object sender, MouseButtonEventArgs e)
        {
            if(!this.isLoose)
            {
                int x = (int)Math.Floor((double)e.X / 25);
                int y = (int)Math.Floor((double)e.Y / 25);

                Block b = this.board.getBlockAtPos(x, y);

                if(e.Button == Mouse.Button.Left && e.X <= this.board.mapWidth && e.Y <= this.board.mapHeight && e.X >= 0 && e.Y >= 0)
                {
                    if(!b.IsMine)
                        this.ComputeOpenBlocks(ref b);
                    else
                        this.isLoose = true;
                }
                else if(e.Button == Mouse.Button.Right && e.X <= this.board.mapWidth && e.Y <= this.board.mapHeight && e.X >= 0 && e.Y >= 0 && !b.IsOpened)
                {
                    b.CheckBlock();
                }

                this.shouldUpdate = true;
            }
            else
            {
                this.board = new Board();
                this.isLoose = false;
                this.shouldUpdate = true;
            }
            
        }

        private void ComputeOpenBlocks(ref Block initialBlock)
        {
            initialBlock.OpenBlock();

            // top
            int block_top_y = (int)initialBlock.IDY - 1;
            if(block_top_y >= 0 && initialBlock.MinesAround == 0)
            {
                Block b = this.board.getBlockAtPos((int)initialBlock.IDX, block_top_y);
                if(!b.IsMine && !b.IsOpened)
                    this.ComputeOpenBlocks(ref b);
            }
            // bottom
            int block_bottom_y = (int)initialBlock.IDY + 1;
            if(block_bottom_y < this.board.mapSizeY && initialBlock.MinesAround == 0)
            {
                Block b = this.board.getBlockAtPos((int)initialBlock.IDX, block_bottom_y);
                if(!b.IsMine && !b.IsOpened)
                    this.ComputeOpenBlocks(ref b);
            }

            // left
            int block_left_x = (int)initialBlock.IDX - 1;
            if(block_left_x >= 0 && initialBlock.MinesAround == 0)
            {
                Block b = this.board.getBlockAtPos(block_left_x, (int)initialBlock.IDY);
                if(!b.IsMine && !b.IsOpened)
                    this.ComputeOpenBlocks(ref b);
            }

            // right
            int block_right_x = (int)initialBlock.IDX + 1;
            if(block_right_x < this.board.mapSizeX && initialBlock.MinesAround == 0)
            {
                Block b = this.board.getBlockAtPos(block_right_x, (int)initialBlock.IDY);
                if(!b.IsMine && !b.IsOpened)
                    this.ComputeOpenBlocks(ref b);
            }
        }

        private void OnKeyPress(object sender, KeyEventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;

            if(e.Code == Keyboard.Key.Escape)
                window.Close();
        }
    }
}