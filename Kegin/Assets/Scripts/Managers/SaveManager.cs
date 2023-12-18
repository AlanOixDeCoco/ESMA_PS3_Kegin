using Save;
using UnityEngine;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] private bool _resetSave = false;
        
        public PlayerSave PlayerSave { get; private set; }

        private void Start()
        {
            if (PlayerPrefs.HasKey("PlayerSave") && !_resetSave)
            {
                // if there is a save, don't create a new one.
                PlayerSave = JsonUtility.FromJson<PlayerSave>(PlayerPrefs.GetString("PlayerSave"));
                Debug.Log("Loaded existing save!");
                return;
            }

            // if there is no save, create a new one.
            PlayerSave = new PlayerSave();
            PlayerPrefs.SetString("PlayerSave", JsonUtility.ToJson(PlayerSave));
            PlayerPrefs.Save(); // save the new save. 
            Debug.Log("Created new save!");
        }


        public void Save()
        {
            PlayerPrefs.SetString("PlayerSave", JsonUtility.ToJson(PlayerSave));
            PlayerPrefs.Save();
            Debug.Log("Saved player save!");
        }
    }
}
