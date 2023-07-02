using UnityEngine;

namespace CodeBase.Services.Input
{
    public class HeroInputService
    {
        public IInputService GetInputService()
        {
            if (Application.isEditor)
                return new MobileInputService();
            
            return new StandaloneInputService();
        }
    }
}