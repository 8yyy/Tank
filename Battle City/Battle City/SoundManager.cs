using Battle_City.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City
{
    class SoundManager
    {
        private static SoundPlayer _startPlayer = new SoundPlayer();
        private static SoundPlayer _addPlayer = new SoundPlayer();
        private static SoundPlayer _blastPlayer = new SoundPlayer();
        private static SoundPlayer _firePlayer = new SoundPlayer();
        private static SoundPlayer _hitPlayer = new SoundPlayer();
        public static void InitSound()
        {
            _startPlayer.Stream= Resources.start;
            _addPlayer.Stream = Resources.add;
            _blastPlayer.Stream = Resources.blast;
            _firePlayer.Stream = Resources.fire;
            _hitPlayer.Stream = Resources.hit;
        }

        //游戏开始的音效，只需要在GameFramework中添加一次
        public static void PlayStart()
        {
            _startPlayer.Play();
        }
        //增加一个敌人，在GameOjectManager中的EnemyBorn
        public static void PlayAdd()
        {
            _addPlayer.Play();
        }
        public static void PlayBlast()
        {
            _blastPlayer.Play();
        }
        public static void PlayFire()
        {
            _firePlayer.Play();
        }
        public static void PlayHit()
        {
            _hitPlayer.Play();
        }
    }
}
