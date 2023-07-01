using CodeBase.Infrastructure.Service.SaveLoad;
using UnityEngine;

namespace CodeBase.Logic
{
    [RequireComponent(typeof(BoxCollider))]
    public class SaveTrigger : MonoBehaviour
    {
        private SaveLoadService _saveLoadService;
        private BoxCollider _boxCollader;

        public void Init(SaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _boxCollader = GetComponent<BoxCollider>();
            _boxCollader.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();
            Debug.Log("Save Progres");
            gameObject.SetActive(false);
        }
    }
}