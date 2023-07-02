using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class HudFactory
    {
        public GameObject Create()
        {
            return Object.Instantiate(Resources.Load<GameObject>(AssetPath.HudPath));
        }
    }
}