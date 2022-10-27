using Battle_City.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City
{
    enum Tag
    {
        MyTank,
        EnemyTank
    }
    class Bullet:Movething
    {
        //辨别子弹是MyTank还是EnemyTank的
        public Tag Tag { get; set; }
        //是否需要销毁
        public bool  IsDestroy { get; set; }
        public Bullet(int x, int y, int speed,Direction  dir,Tag tag)
        {
            IsDestroy = false;
            //初始坐标
            X = x;
            Y = y;
            //初始速度
            Speed = speed;
            //四个方向的图片
            BitmapDown = Resources.BulletDown;
            BitmapUp = Resources.BulletUp;
            BitmapRight = Resources.BulletRight;
            BitmapLeft = Resources.BulletLeft;
            Dir = dir;
            Tag = tag;  //运行敌人会自爆，找了半天错误在这写错了Tag = Tag;
            //由于子弹本身存在宽高的空白区域，导致显示问题，所以需要减去宽高
            X -= Width / 2;
            Y -= Height / 2;
   
        }

        public override void DrawSelf()
        {
            base.DrawSelf();
        }

        public override void Update()
        {
            _MoveCheck();   //移动检查
            _Move();
            base.Update();
        }

        private void _MoveCheck()
        {
            #region 检查有没有超出窗体边界，不需要预判
            //判断中间的小点，即子弹
            if (Dir == Direction.Up)
            {
                //高度一半，然后子弹是6，加上子弹的一半
                if (Y+Height/2 +3 < 0)
                {
                    IsDestroy = true;
                    return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + Height / 2 -3 > 450)
                {
                    IsDestroy = true;
                    return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X +Width/2-3 < 0)
                {
                    IsDestroy = true;
                    return;
                }
            }
            else
            {
                if (X + Width / 2 + 3 > 450)
                {
                    IsDestroy = true;
                    return;
                }
            }
            #endregion
            //检查有没有和其他元素发生碰撞

            Rectangle rectangle = GetRectangle();

            rectangle.X = X + Width / 2 - 3;
            rectangle.Y = Y + Height / 2 - 3;
            rectangle.Height = 3;
            rectangle.Width = 3;

            //爆炸的中心位置
            int xExplosion = X + Width / 2;
            int yExplosion = Y + Height / 2;

            NotMovething wall = null;
            //自身销毁，红墙销毁
            if ((wall= GameObjectManager.IsCollidedWall(rectangle)) != null)
            {
                IsDestroy = true;
                SoundManager.PlayBlast();
                GameObjectManager.DestroyWall(wall);
                GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                return;
            }
            //碰到钢铁自身销毁
            if (GameObjectManager.IsCollidedSteel(rectangle) != null)
            {
                GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                IsDestroy = true;
                return;
            }
            if (GameObjectManager.IsCollidedBoss(rectangle) != false)
            {
                SoundManager.PlayBlast();
                GameFramework.ChangeToGameOver();
                return;
            }

            if (Tag == Tag.MyTank)
            {
                EnemyTank enemyTank = null;
                if ((enemyTank = GameObjectManager.IsCollidedEnemyTank(rectangle)) != null)
                {
                    SoundManager.PlayBlast();
                    IsDestroy = true;
                    GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                    GameObjectManager.DestroyEnemyTank(enemyTank);
                    return;
                }
            }
            else
            {
                MyTank myTank = null;
                if ((myTank = GameObjectManager.IsCollidedMyTank(rectangle)) != null)
                {
                    SoundManager.PlayHit();
                    //子弹也要销毁和产生爆炸效果
                    IsDestroy = true;
                    GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                    //myTank血量减少
                    myTank.TakeDamage();
                    return;
                }
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
