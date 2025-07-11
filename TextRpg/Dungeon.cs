using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg
{
    public struct Dungeon
    {
        public string name;
        public int defLimit;
        public float clearGold;

        public Dungeon(string name, int defLimit, float gold)
        {
            this.name = name;
            this.defLimit = defLimit;
            this.clearGold = gold;
        }
    }

    class DungeonResult
    {
        public bool IsClear {  get; set; }
        public float BeforeHp { get; set; }
        public float AfterHp { get; set; }
        public float BeforeGold { get; set; }
        public float AfterGold { get; set; }
        public bool LevelUp { get; set; }
    }
}
