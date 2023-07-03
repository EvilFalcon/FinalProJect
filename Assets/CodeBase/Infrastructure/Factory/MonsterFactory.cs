using CodeBase.Enemy;
using CodeBase.Logic;
using CodeBase.StaticData;
using CodeBase.UI;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Infrastructure.Factory
{
    public class MonsterFactory
    {
        private readonly StaticDataService _staticData;
        private readonly HeroFactory _heroFactory;

        public MonsterFactory(StaticDataService staticData, HeroFactory heroFactory)
        {
            _staticData = staticData;
            _heroFactory = heroFactory;
        }

        public GameObject Create(MonsterTypeId monsterType, Transform transform)
        {
            IHeroTransform heroTransform = _heroFactory.Instantiate.GetComponent<IHeroTransform>();

            MonsterStaticData monsterData = _staticData.ForMonster(monsterType);
            GameObject monster = Object.Instantiate(monsterData.Prefab, transform.position, Quaternion.identity, transform);

            NavMeshAgent navMeshAgent = monster.GetComponent<NavMeshAgent>();
            monster.GetComponent<AgentMoveToPlayer>().Construct(navMeshAgent);
            EnemyAnimator enemyAnimator = monster.GetComponent<EnemyAnimator>();

            monster.GetComponent<IEnemyHealth>().SetValue(monsterData.Hp);
            monster.GetComponent<ActorUI>().Construct(monster.GetComponent<IHealth>());
            navMeshAgent.speed = monsterData.MoveSpeed;
            Attack enemyAttack = monster.GetComponent<Attack>();
            enemyAttack.Constructor(enemyAnimator, heroTransform.Transform, monsterData.Cleavage, monsterData.Damage, monsterData.EffectiveDistance);
            ProviderAttackRange attackRange = monster.GetComponent<ProviderAttackRange>();
            attackRange.Constructor(enemyAttack);

            return monster;
        }
    }
}