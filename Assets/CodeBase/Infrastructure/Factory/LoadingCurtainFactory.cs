using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class LoadingCurtainFactory
    {
        public LoadingCurtain Create()
        {
            return Object.Instantiate(Resources.Load<LoadingCurtain>(AssetPath.CurtainPath));
        }
    }
}