using Battle_City.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City
{
    //爆炸效果和Movething以及NotMovthing都不太像
    //需要绘制几个图片动态的切换
    class Explosion : GameObject
    {
        private int _playSpeed = 1;     //让每一张图片停留2帧
        
        private int _playCount = 0;    //计数器用于处理_playSpeed

        private int _index=0;

        public bool IsNeedDestroy { get; set; }     //判断爆炸是否需要销毁

        private Bitmap[] _bmpArray = new Bitmap[]
        {
            Resources.EXP1,
            Resources.EXP2,
            Resources.EXP3,
            Resources.EXP4,
            Resources.EXP5
        };
        public Explosion(int x,int y)
        {
            IsNeedDestroy = false;  
            foreach (var item in _bmpArray)
            {
                item.MakeTransparent(Color.Black);
            }
            //需要爆炸在正中心,取任意图的位置即可
            X = x - _bmpArray[0].Width / 2;
            Y = y - _bmpArray[0].Height / 2;
        }
        protected override Image GetImage()
        {
            if (_index > 4)
            {
                IsNeedDestroy = true;
                return _bmpArray[4];
            }
             return _bmpArray[_index];
        }

        public override void Update()
        {
            _playCount++;
            _index = (_playCount - 1) / _playSpeed;
            base.Update();
        }
    }
}
