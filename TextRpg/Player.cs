using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg
{
    internal class Player
    {
        private static Player instance;
        public static Player Instance 
        {
            get 
            { 
                if(instance == null)
                {
                    instance = new Player();
                }
                return instance;
            } 
        }

        public int Level { get; set; }
        public string Name { get; set; }
        public string Job {get; set;}
        public float Hp { get; set; }
        public float Gold { get; set; }
        public int ClearTime { get; set; }
        public float BaseStrengh { get; set; }
        public float BaseDefence { get; set; }

        public float TotalStrengh
        { 
            get {
                return BaseStrengh + AddStrengh;
            }
        }
        public float TotalDefence
        {
            get {  
                return BaseDefence + AddDefence; 
            }
        }

        private float _addStrengh;
        private float _addDefence;
        public float AddStrengh {
            get
            {
                return _addStrengh;
            }
        }
        public float AddDefence {
            get
            {
                return _addDefence;
            }
        }

        public Items EquipWeapon { get; private set; }
        public Items EquipArmor { get; private set; }

        public void EquipItem(Items item)
        {
            if (!Inv.Contains(item)) return;

            if (item.itemType == ItemType.Weapon) 
            {
                if(EquipWeapon != null)
                {
                    EquipWeapon.isEquip = false;
                }

                if(EquipWeapon == item)
                {
                    EquipWeapon = null;
                    item.isEquip = false;
                }
                else
                {
                    EquipWeapon = item;
                    item.isEquip = true;
                }
            }
            else if(item.itemType == ItemType.Armor)
            {
                if (EquipArmor != null)
                {
                    EquipArmor.isEquip = false;
                }

                if(EquipArmor == item)
                {
                    EquipArmor = null;
                    item.isEquip = false;
                }
                else
                {
                    EquipArmor = item;
                    item.isEquip = true;
                }
            }
            UpdateStatus();
        }

        public void UpdateStatus()
        {
            _addStrengh = 0;
            _addDefence = 0;
            foreach (var item in Inv)
            {
                if (item.isEquip)
                {
                    _addStrengh += item.ItemAtk;
                    _addDefence += item.ItemDef;
                }
            }

        }

        public List<Items> Inv { get; set; }

        private Player(){
            Level = 1;
            BaseStrengh = 10;
            BaseDefence = 5;
            Hp = 100f;
            Gold = 1500f;
            Inv = new List<Items>();
            UpdateStatus();
        }
    }
}
