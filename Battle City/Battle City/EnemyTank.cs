using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City
{
    class EnemyTank:Movething
    {
        Random _r = new Random();
        public int ChangeDirSpeed { get; set; } //敌人改变方向的时间
        private int _changeDirCount = 0;        //敌人改变方向的计数器
        public int AttackSpeed { get; set; }    //控制子弹的发射速度
        private int _attackCount = 0;   //子弹的计数器
        public EnemyTank(int x, int y, int speed,Bitmap bmpDown, Bitmap bmpUp, Bitmap bmpRight, Bitmap bmpLeft)
        {
            //初始坐标
            X = x;
            Y = y;
            //初始速度
            Speed = speed;
            //四个方向的图片   
            BitmapDown = bmpDown;
            BitmapUp = bmpUp;
            BitmapRight = bmpRight;
            BitmapLeft = bmpLeft;
            //初始方向
            Dir = Direction.Down;
            AttackSpeed = 60;//1s发射一次子弹
            ChangeDirSpeed = 80;    //80帧改变一次方向
        }
        private void _AttackCheck()
        {
            _attackCount++;
            if (_attackCount < AttackSpeed) return;
            _Attack();
            _attackCount = 0;
        }
        private void _Attack()
        {
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
                    y += Height / 2;
                    break;
                case Direction.Right:
                    x += Width;
                    y += Height / 2;
                    break;
            }
            GameObjectManager.CreateBullet(x, y, Tag.EnemyTank, Dir);
        }
        //自动改变方向
        private void _AutoChangeDir()
        {
            _changeDirCount++;
            if (_changeDirCount<ChangeDirSpeed) return;
            _ChangeDirection();
            _changeDirCount = 0;
        }
        private void _ChangeDirection()
        {
            while (true)
            {
                Direction dir = (Direction)_r.Next(0, 4);  //Direction为0-3
                if(dir == Dir)
                {
                    continue;
                }
                else
                {
                    Dir = dir;
                    break;
                }
            }
            //更改方向后仍然可能存在障碍物,所以我们需要再执行一次_MoveCheck
            _MoveCheck();

        }

        
        public override void Update()
        {
            _MoveCheck();   //移动检查
            _Move();
            _AttackCheck();
            _AutoChangeDir();
            base.Update();
        }

        private void _MoveCheck()
        {
            #region 检查有没有超出窗体边界，判断四个方向即可
            if (Dir == Direction.Up)
            {
                if (Y - Speed < 0)
                {
                    _ChangeDirection();
                    return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + Speed + Height > 450)
                {
                    _ChangeDirection();
                    return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X - Speed < 0)
                {
                    _ChangeDirection();
                    return;
                }
            }
            else
            {
                if (X + Speed + Height > 450)
                {
                    _ChangeDirection();
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
                _ChangeDirection();
                return;
            }
            if (GameObjectManager.IsCollidedSteel(rectangle) != null)
            {
                _ChangeDirection();
                return;
            }
            if (GameObjectManager.IsCollidedBoss(rectangle) != false)
            {
                _ChangeDirection();
                return;
            }
        }

        private void _Move()
        {
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
    }
}
