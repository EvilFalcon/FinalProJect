using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.StaticData
{
    public class StaticDataService
    {
        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;

        public StaticDataService()
        {
            LoadMonsters();
        }

        private void LoadMonsters()
        {
            _monsters = Resources.LoadAll<MonsterStaticData>(nameof(StaticData) + "/Monsters")
                .ToDictionary(x => x.MonsterTypeId, x => x);
        }

        public MonsterStaticData ForMonster(MonsterTypeId typeId) =>
            _monsters.TryGetValue(typeId, out MonsterStaticData staticData) 
                ? staticData 
                : null;
    }
}