﻿using CodeBase.Hero;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class HeroFactory : IGameObjectFactory
    {
        private readonly HeroInputService _heroInputService;

        public HeroFactory(HeroInputService heroInputService)
        {
            _heroInputService = heroInputService;
        }

        public GameObject Instantiate { get; private set; }

        public GameObject Create(Transform transform)
        {
            GameObject hero = Object
                .Instantiate(Resources.Load<GameObject>(AssetPath.HeroPath), transform.position, Quaternion.identity);
            Instantiate = hero;
            hero.GetComponent<HeroMove>().Construct(_heroInputService.GetInputService());
            hero.GetComponent<HeroAttack>().Construct(_heroInputService.GetInputService());
            return hero;
        }
    }
}