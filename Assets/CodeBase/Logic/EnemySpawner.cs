using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Service.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private MonsterTypeId _monsterType;
        private string _id;
        private bool _slain;
        private MonsterFactory _factory;
        private EnemyDeath _enemy;

        private void Awake()
        {
            _id = GetComponent<UniqueId>().Id;
        }

        public void LoadProgres(PlayerProgress playerProgress)
        {
            if (playerProgress.KillData.ClearedSpawners.Contains(_id))
                _slain = true;
            else
            {
                Spawn();
            }
        }

        public void Construct(MonsterFactory factory)
        {
            _factory = factory;
        }

        private void Spawn()
        {
            GameObject monster = _factory.Create(_monsterType, transform);
            _enemy = monster.GetComponent<EnemyDeath>();
            _enemy.Happend += Slay;
        }

        private void Slay()
        {
            if (_enemy != null)
                _enemy.Happend -= Slay;
            
            _slain = true;
        }

        public void UpdateProgress(PlayerProgress playerProgress)
        {
            if (_slain)
                playerProgress.KillData.ClearedSpawners.Add(_id);
        }
    }
}