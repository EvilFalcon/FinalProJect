using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class HudFactory
    {
        public void Create()
        {
            Object.Instantiate(Resources.Load<GameObject>(AssetPath.HudPath));
        }
    }
}