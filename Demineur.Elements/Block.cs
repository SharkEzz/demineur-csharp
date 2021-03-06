using SFML.Graphics;
using SFML.System;

namespace Demineur.Elements
{
    public class Block
    {
        public const uint CASE_SIZE = 25;

        private RectangleShape shape;

        private uint idX;
        private uint idY;

        private uint posX;
        private uint posY;

        private bool isMine;
        private bool isOpened;
        private bool isChecked;

        private uint minesAround;

        public Block(uint idX, uint idY, uint posX, uint posY, bool isMine, bool isOpened, bool isChecked, uint minesAround)
        {
            this.idX = idX;
            this.idY = idY;
            this.posX = posX;
            this.posY = posY;
            this.isMine = isMine;
            this.isOpened = isOpened;
            this.isChecked = isChecked;
            this.minesAround = minesAround;
            this.shape = new RectangleShape(new Vector2f(CASE_SIZE, CASE_SIZE))
            {
                Position = new Vector2f(this.posX, this.posY),
                OutlineThickness = 1,
                OutlineColor = new Color(0, 0, 0),
                FillColor = new Color(255, 255, 255)
            };
        }

        #region Accesseurs

        public uint IDX
        {
            get => this.idX;
            set => this.idX = value;
        }

        public uint IDY
        {
            get => this.idY;
            set => this.idY = value;
        }

        public RectangleShape Shape
        {
            get => this.shape;
            set => this.shape = value;
        }

        public uint PosX
        {
            get => this.posX;
            set => this.posX = value;
        }

        public uint PosY
        {
            get => this.posY;
            set => this.posY = value;
        }

        public bool IsMine
        {
            get => this.isMine;
            set => this.isMine = value;
        }

        public bool IsOpened
        {
            get => this.isOpened;
            set => this.isOpened = value;
        }

        public bool IsChecked
        {
            get => this.isChecked;
            set => this.isChecked = value;
        }

        public uint MinesAround
        {
            get => this.minesAround;
            set => this.minesAround = value;
        }

        public Text Text
        {
            get => new Text(this.minesAround != 0 ? this.minesAround.ToString() : "", GameLoop.f, 16)
            {
                Position = new Vector2f(this.posX + Block.CASE_SIZE / 4, this.posY + Block.CASE_SIZE / 7)
            };
        }

        #endregion

        public void OpenBlock()
        {
            this.isOpened = true;
            this.shape = new RectangleShape(new Vector2f(CASE_SIZE, CASE_SIZE))
            {
                Position = new Vector2f(this.posX, this.posY),
                OutlineThickness = 1,
                OutlineColor = new Color(0, 0, 0),
                FillColor = new Color(80, 0, 200)
            };
        }

        public void CheckBlock()
        {
            this.isChecked = !this.isChecked;

            this.shape = new RectangleShape(new Vector2f(CASE_SIZE, CASE_SIZE))
            {
                Position = new Vector2f(this.posX, this.posY),
                OutlineThickness = 1,
                OutlineColor = new Color(0, 0, 0),
                FillColor = this.isChecked ? new Color(255, 20, 20) : new Color(255, 255, 255)
            };
        }
    }
}