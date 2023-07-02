using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void SetValue(float value) =>
            _image.fillAmount = value;
    }
}