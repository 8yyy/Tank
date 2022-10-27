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
    class GameObjectManager
    {
        //装红墙的列表
        private static List<NotMovething> _wallList = new List<NotMovething>();
        //装白墙的列表
        private static List<NotMovething> _steelList = new List<NotMovething>();
        //Boss
        private static NotMovething _boss;
        //MyTank
        private static MyTank _myTank;
        //EnemyTank
        private static List<EnemyTank> _tankList = new List<EnemyTank>();
        //Bullet
        private static List<Bullet> _bulletList = new List<Bullet>();
        //Explosion
        private static List<Explosion> _explosionList = new List<Explosion>();

        private static int _enemyBurnSpeed = 80;//80帧生成一个敌人
        private static int _enemyBurnCount = 60;//敌人的计数器

        //创建子弹
        public static void CreateBullet(int x, int y, Tag tag, Direction dir)
        {
            Bullet bullet = new Bullet(x, y, 8, dir, tag);
            _bulletList.Add(bullet);
        }
        //检测销毁子弹
        public static void CheckAndDestroyBullet()
        {
            List<Bullet> needToDestroy = new List<Bullet>();
            foreach (var item in _bulletList)
            {
                if (item.IsDestroy == true)
                {
                    needToDestroy.Add(item);
                }
            }
            foreach (var item in needToDestroy)
            {
                _bulletList.Remove(item);
            }
        }
        //检测销毁爆炸
        public static void CheckAndDestroyExplosion()
        {
            List<Explosion> needToDestroy = new List<Explosion>();
            foreach (var item in _explosionList)
            {
                if (item.IsNeedDestroy == true)
                {
                    needToDestroy.Add(item);
                }
            }
            foreach (var item in needToDestroy)
            {
                _explosionList.Remove(item);
            }
        }

        //销毁红墙
        public static void DestroyWall(NotMovething wall)
        {
            _wallList.Remove(wall);
        }

        //销毁敌人的坦克
        public static void DestroyEnemyTank(EnemyTank enemyTank)
        {
            _tankList.Remove(enemyTank);
        }

        //创建我的坦克
        public static void CreateMyTank()
        {
            int x = 5 * 30;
            int y = 14 * 30;
            _myTank = new MyTank(x, y, 10);
        }

        //创建爆炸
        public static void CreateExplosion(int x, int y)
        {
            Explosion exp = new Explosion(x, y);
            _explosionList.Add(exp);
        }

        public static void Update()
        {
            foreach (var item in _wallList)
            {
                item.Update();
            }

            foreach (var item in _steelList)
            {
                item.Update();
            }
            foreach (var item in _tankList)
            {
                item.Update();
            }
            CheckAndDestroyBullet();
            //foreach (var item in _bulletList)
            //{
            //    item.Update();
            //}

            for (int i = 0; i < _bulletList.Count; i++)
            {
                _bulletList[i].Update();
            }

            CheckAndDestroyExplosion();
            foreach (var item in _explosionList)
            {
                item.Update();
            }
            _boss.Update();
            _myTank.Update();

            _EnemyBurn();
        }
        private static Point[] _points = new Point[3];
        public static void Start()
        {
            _points[0].X = 0; _points[0].Y = 0;    //一号敌人左上角
            _points[1].X = 7*30; _points[1].Y = 0;    //二号敌人在中间
            _points[2].X = 14 * 30; _points[1].Y = 0;    //三号敌人在右上角
        }

        private static void _EnemyBurn()
        {
            _enemyBurnCount++;
            if (_enemyBurnCount < _enemyBurnSpeed) return;
            SoundManager.PlayAdd();
            //0-2
            Random rd = new Random();
            int index = rd.Next(0, 3);  //生成0-2之间的随机值，不包括最大数3
            Point position = _points[index];
            int enemyType = rd.Next(1, 5);
            switch (enemyType)
            {
                case 1:
                    _CreanteEnemyTank1(position.X, position.Y);
                    break;
                case 2:
                    _CreanteEnemyTank2(position.X, position.Y);
                    break;
                case 3:
                    _CreanteEnemyTank3(position.X, position.Y);
                    break;
                case 4:
                    _CreanteEnemyTank4(position.X, position.Y);
                    break;
            }

            _enemyBurnCount = 0;
        }
        //创建敌人坦克
        private static void _CreanteEnemyTank1(int x,int y)
        {
            EnemyTank tank = new EnemyTank(x, y,10,Resources.GrayDown,Resources.GrayUp,Resources.GrayRight,Resources.GrayLeft);
            _tankList.Add(tank);
        }
        private static void _CreanteEnemyTank2(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 10, Resources.GreenDown, Resources.GreenUp, Resources.GreenRight, Resources.GreenLeft);
            _tankList.Add(tank);
        }
        private static void _CreanteEnemyTank3(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 15, Resources.QuickDown, Resources.QuickUp, Resources.QuickRight, Resources.QuickLeft);
            _tankList.Add(tank);
        }
        private static void _CreanteEnemyTank4(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 5, Resources.SlowDown, Resources.SlowUp, Resources.SlowRight, Resources.SlowLeft);
            _tankList.Add(tank);
        }

        //印刷
        //public static void DrawMyTank()
        //{
        //    _myTank.DrawSelf();
        //}
        //public static void DrawMap()
        //{
        //    foreach (var item in _wallList)
        //    {
        //        item.DrawSelf();
        //    }

        //    foreach (var item in _steelList)
        //    {
        //        item.DrawSelf();
        //    }

        //    _boss.DrawSelf();
        //}

        //初始化地图
        public static void CreateMap()
        {
            //创建第一二堵墙
            CreateWall(1, 1, 5, Resources.wall,_wallList);
            CreateWall(3, 1, 5, Resources.wall, _wallList);
            //创建第三四堵墙，矮一点只需要修改count即可
            CreateWall(5, 1, 4, Resources.wall, _wallList);           
            CreateWall(7, 1, 3, Resources.wall, _wallList);
            CreateWall(9, 1, 4, Resources.wall, _wallList);
            CreateWall(11, 1, 5, Resources.wall, _wallList);
            CreateWall(13, 1, 5, Resources.wall, _wallList);

            //在中间创建一堵白墙
            CreateWall(7, 5, 1, Resources.steel, _steelList);
            
            CreateWall(0, 7, 1, Resources.steel, _steelList);
            CreateWall(14, 7, 1, Resources.steel, _steelList);

            //创建中间的红墙
            CreateWall(2, 7, 1, Resources.wall, _wallList);
            CreateWall(3, 7, 1, Resources.wall, _wallList);
            CreateWall(4, 7, 1, Resources.wall, _wallList);
            CreateWall(6, 7, 1, Resources.wall, _wallList);
            CreateWall(7, 6, 2, Resources.wall, _wallList);
            CreateWall(8, 7, 1, Resources.wall, _wallList);
            CreateWall(10, 7, 1, Resources.wall, _wallList);
            CreateWall(11, 7, 1, Resources.wall, _wallList);
            CreateWall(12, 7, 1, Resources.wall, _wallList);

            //创建下半部分的红墙
            CreateWall(1, 9, 5, Resources.wall, _wallList);
            CreateWall(3, 9, 5, Resources.wall, _wallList);
            //创建第三四堵墙，矮一点只需要修改count即可
            CreateWall(5, 9, 3, Resources.wall, _wallList);
            CreateWall(6, 10, 1, Resources.wall, _wallList);
            CreateWall(7, 10, 2, Resources.wall, _wallList);
            CreateWall(8, 10, 1, Resources.wall, _wallList);
            CreateWall(9, 9, 3, Resources.wall, _wallList);
            CreateWall(11, 9, 5, Resources.wall, _wallList);
            CreateWall(13, 9, 5, Resources.wall, _wallList);

            //创建Boss周围的红墙
            CreateWall(6, 13, 2, Resources.wall, _wallList);
            CreateWall(7, 13, 1, Resources.wall, _wallList);
            CreateWall(8, 13, 2, Resources.wall, _wallList);

            CreateBoss(7,14,Resources.Boss);
        }

        //创建红墙和白墙
        public static void CreateWall(int x, int y,int count,Image img,List<NotMovething> List)
        {
             
            int xPosition = x * 30;
            int yPosition = y * 30;
            for (int i = yPosition; i < yPosition+count*30; i+=15)
            {
                //一堵红墙由两块wall构成 (xPosition,i) (xPosition+15,i)
                NotMovething wall1 = new NotMovething(xPosition, i , img);
                NotMovething wall2 = new NotMovething(xPosition+15, i, img);
                List.Add(wall1);
                List.Add(wall2);
            }
        }

        //创建Boss
        private static void CreateBoss(int x,int y,Image img)
        {
            int xPosition = x * 30;
            int yPosition = y * 30;
            _boss = new NotMovething(xPosition, yPosition, img);
        }

        public static void KeyDown(KeyEventArgs e)
        {
            _myTank.KeyDown(e);
        }

        public static void KeyUp(KeyEventArgs e)
        {
            _myTank.KeyUp(e);
        }

        public static NotMovething IsCollidedWall(Rectangle rt)
        {
            //遍历所有的红墙
            foreach (var item in _wallList)
            {
                //判断是否与传递过来的矩形发生碰撞
                if (item.GetRectangle().IntersectsWith(rt))
                {
                    return item;
                }
            }
            return null;
        }

        public static NotMovething IsCollidedSteel(Rectangle rt)
        {
            //遍历所有的钢铁
            foreach (var item in _steelList)
            {
                //判断是否与传递过来的矩形发生碰撞
                if (item.GetRectangle().IntersectsWith(rt))
                {
                    return item;
                }
            }
            return null;
        }
        
        public static bool IsCollidedBoss(Rectangle rt)
        {
            return _boss.GetRectangle().IntersectsWith(rt);
        }

        public static EnemyTank IsCollidedEnemyTank(Rectangle rt)
        {
            foreach (var item in _tankList)
            {
                if (item.GetRectangle().IntersectsWith(rt))
                {
                    return item;
                }
            }
            return null;
        }
        /*
         由于敌人坦克的子弹碰撞到我的坦克不是直接销毁的
         而是要调用我的坦克的成员，比如说血量，所以这里不能用bool类型的返回值

        */
        public static MyTank IsCollidedMyTank(Rectangle rt)
        {
            if (_myTank.GetRectangle().IntersectsWith(rt))
            {   
                return _myTank;
            }
            return null;
        }
    }
}
