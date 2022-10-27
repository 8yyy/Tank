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
    //游戏状态
    enum GameState
    {
        Running,
        GameOver
    }
    class GameFramework
    {
        //默认游戏状态为Running
        private static GameState _gameState = GameState.Running;
        public static Graphics g;
        public static void Start()
        {
            //创建地图只需要初始化一次
            GameObjectManager.CreateMap();
            GameObjectManager.CreateMyTank();
            GameObjectManager.Start();
            SoundManager.InitSound();
            SoundManager.PlayStart();
        }
        //Update就是我们的帧率FPS
        public static void Update()
        {
            //每帧都需要绘制地图
            //GameObjectManager.DrawMap();
            //GameObjectManager.DrawMyTank();
            GameObjectManager.Update();

            if (_gameState == GameState.Running)
            {
                GameObjectManager.Update();
            }
            else
            {
                _GameOverUpdate();
            }
        }

        private static void _GameOverUpdate()
        {
             int x = 450 / 2 - Resources.GameOver.Width / 2;
            int y = 450 / 2 - Resources.GameOver.Height / 2;
            g.DrawImage(Resources.GameOver, x, y);
        }

        public static void ChangeToGameOver()
        {
            _gameState = GameState.GameOver;
        }

        public static void KeyDown(KeyEventArgs e)
        {
            GameObjectManager.KeyDown(e);
        }

        public static void KeyUp(KeyEventArgs e)
        {
            GameObjectManager.KeyUp(e);
        }
    }
}
