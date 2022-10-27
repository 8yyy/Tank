using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battle_City
{
    public partial class Form1 : Form
    {
        private Thread _gameMainThread;
        //private Graphics _g;
        private static Graphics _windowG;
        private static Bitmap _tempBmp;
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            //画布
            _windowG = this.CreateGraphics();
            //GameFramework.g = _g;

            //绘制一张图片，和窗体一样大
            _tempBmp = new Bitmap(450, 450);
            Graphics bmpG = Graphics.FromImage(_tempBmp);
            GameFramework.g = bmpG;

            _gameMainThread = new Thread(new ThreadStart(GameMainThread));
            _gameMainThread.Start();
        }

        //控制游戏逻辑的子线程
        public static void GameMainThread()
        {
            GameFramework.Start();

            int sleepTime = 1000 / 60;

            while (true)
            {
                //清空画布，涂刷黑色背景
                GameFramework.g.Clear(Color.Black);

                GameFramework.Update();  //60fps

                _windowG.DrawImage(_tempBmp, 0, 0);

                Thread.Sleep(sleepTime); //休息1/60秒
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _gameMainThread.Abort();    //终止线程
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            GameFramework.KeyDown(e);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            GameFramework.KeyUp(e);
        }
    }
}
