using UnityEngine;

namespace Kitchen
{
    public class Tool : MonoBehaviour
    {
        private Inventory _inventory;

        private void Start()
        {
            _inventory = GetComponent<Inventory>();
        }
    }
}
