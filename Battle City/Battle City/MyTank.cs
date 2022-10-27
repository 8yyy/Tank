using Battle_City.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battle_City
{
    class MyTank:Movething
    {
        public bool IsMoving { get; set; }
        public int HP { get; set; } //设置默认血量

        //出生点
        private int _originalX;
        private int _originalY;
        public MyTank(int x,int y,int speed)
        {
            IsMoving = false;
            //初始坐标
            X = x;
            Y = y;
            _originalX = x;
            _originalY = y;
            //初始速度
            Speed = speed;
            //四个方向的图片
            BitmapDown = Resources.MyTankDown;
            BitmapUp = Resources.MyTankUp;
            BitmapRight = Resources.MyTankRight;
            BitmapLeft = Resources.MyTankLeft;
            //初始方向
            Dir = Direction.Up;
            HP = 4;
        }

        public void KeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    Dir = Direction.Up;
                    IsMoving = true;
                    break;
                case Keys.S:
                    Dir = Direction.Down;
                    IsMoving = true;
                    break;
                case Keys.A:
                    Dir = Direction.Left;
                    IsMoving = true;
                    break;
                case Keys.D:
                    Dir = Direction.Right;
                    IsMoving = true;
                    break;
                case Keys.Space:
                    _Attack();
                    break;
            }
        }

        private void _Attack()
        {
            SoundManager.PlayFire();
            //发射子弹
            int x = this.X;
            int y = this.Y;
            //计算子弹位置
            switch (Dir)
            {
                case Direction.Up:
                    x += Width / 2;
                    break;
                case Direction.Down:
                    x += Width / 2;
                    y += Height;
                    break;
                case Direction.Left:
                    y += Height/2;
                    break;
                case Direction.Right:
                    x += Width;
                    y += Height / 2;
                    break;
            }
            GameObjectManager.CreateBullet(x,y,Tag.MyTank,Dir);
        }

        public void KeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    Dir = Direction.Up;
                    IsMoving = false;
                    break;
                case Keys.S:
                    Dir = Direction.Down;
                    IsMoving = false;
                    break;
                case Keys.A:
                    Dir = Direction.Left;
                    IsMoving = false;
                    break;
                case Keys.D:
                    Dir = Direction.Right;
                    IsMoving = false;
                    break;
            }
        }

        public override void Update()
        {
            _MoveCheck();   //移动检查
            _Move();
            base.Update();
        }

        private void _MoveCheck()
        {
            #region 检查有没有超出窗体边界，判断四个方向即可
            if (Dir == Direction.Up)
            {
                if (Y - Speed < 0)
                {
                    IsMoving = false;
                    return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + Speed + Height > 450)
                {
                    IsMoving = false;
                    return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X - Speed < 0)
                {
                    IsMoving = false;
                    return;
                }
            }
            else
            {
                if (X + Speed + Height > 450)
                {
                    IsMoving = false;
                    return;
                }
            }
            #endregion
            //检查有没有和其他元素发生碰撞

            Rectangle rectangle = GetRectangle();

            switch (Dir)
            {
                case Direction.Up:
                    rectangle.Y -= Speed;
                    break;
                case Direction.Down:
                    rectangle.Y += Speed;
                    break;
                case Direction.Left:
                    rectangle.X -= Speed;
                    break;
                case Direction.Right:
                    rectangle.X += Speed;
                    break;
            }

            if (GameObjectManager.IsCollidedWall(rectangle) != null)
            {
                IsMoving = false;
                return;
            }
            if (GameObjectManager.IsCollidedSteel(rectangle) != null)
            {
                IsMoving = false;
                return;
            }
            if (GameObjectManager.IsCollidedBoss(rectangle) != false)
            {
                IsMoving = false;
                return;
            }
        }

        private void _Move()
        {
            if (IsMoving == false) return;
            switch (Dir)
            {
                case Direction.Up:
                    Y -= Speed;
                    break;
                case Direction.Down:
                    Y += Speed;
                    break;
                case Direction.Left:
                    X -= Speed;
                    break;
                case Direction.Right:
                    X += Speed;
                    break;
            }
        }
        //代表我的坦克受到了攻击
        public void TakeDamage()
        {
            HP--;
            //回出生点
            if(HP <=0)
            {
                X = _originalX;
                Y = _originalY;
                HP = 4;
                Dir = Direction.Up;
            }
        }
    }
}
