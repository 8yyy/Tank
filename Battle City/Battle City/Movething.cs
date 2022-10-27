using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City
{
    //朝向
    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    class Movething :GameObject
    {
        //定义一个锁
        private Object _lock = new object();

        //四个方向
        public Bitmap BitmapUp { get; set; }
        public Bitmap BitmapDown { get; set; }
        public Bitmap BitmapLeft { get; set; }
        public Bitmap BitmapRight { get; set; }

        //移动速度
        public int Speed { get; set; }

        private Direction _dir;
        public Direction Dir
        {
            get { return _dir; }
            set {
                _dir = value;
                Bitmap bmp = null;
                switch (Dir)
                {
                    case Direction.Up:
                        bmp = BitmapUp;
                        break;
                    case Direction.Down:
                        bmp = BitmapDown;
                        break;
                    case Direction.Left:
                        bmp = BitmapLeft;
                        break;
                    case Direction.Right:
                        bmp = BitmapRight;
                        break;  
                }
                lock (_lock)
                {
                      Width = bmp.Width;
                      Height = bmp.Height;
                 }   
            }
        }
        protected override Image GetImage()
        {
            Bitmap bitmap = null;
            switch (Dir)
            {
                case Direction.Up:
                    bitmap = BitmapUp;
                    break;
                case Direction.Down:
                    bitmap = BitmapDown;
                    break;
                case Direction.Left:
                    bitmap = BitmapLeft;
                    break;
                case Direction.Right:
                    bitmap = BitmapRight;
                    break;
                default:
                    break;
            }
            //设置透明度
            bitmap.MakeTransparent(Color.Black);
            return bitmap;
        }

        public override void DrawSelf()
        {
            lock (_lock)
            {
                base.DrawSelf();
            }
        }
    }
}
