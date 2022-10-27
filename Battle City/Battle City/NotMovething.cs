using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City
{
    //不可移动的物体
    class NotMovething:GameObject
    {
        private Image _img;
        public Image Img { get { return _img; } 
            set {
                _img = value;
                Width = _img.Width;
                Height = Img.Height;
            }
        }

        protected override Image GetImage()
        {
            return Img;
        }

        public NotMovething(int x,int y,Image img)
        {
            X = x;
            Y = y;
            Img = img;
        }
    }
}
