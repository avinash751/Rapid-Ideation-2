using UnityEngine;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SessionOverManager : MonoBehaviour
{
    [SerializeField] private GameObject menuObject;    // The end-of-session menu object
    [SerializeField] private GameObject gameUIObject; // The in-game UI object to deactivate
    [SerializeField] private Button restartButton;    // Restart button
    [SerializeField] private Button quitButton;       // Quit button

    private void OnEnable()
    {
        // Subscribe to the OnObjectParkingFinished event
        ShipDock.OnObjectParkingFinished += HandleSessionOver;

        // Assign button listeners
        restartButton.onClick.AddListener(RestartScene);
        quitButton.onClick.AddListener(QuitApplication);
        menuObject.SetActive(false);
    }

    private void OnDisable()
    {
        // Unsubscribe from the OnObjectParkingFinished event
        ShipDock.OnObjectParkingFinished -= HandleSessionOver;

        // Remove button listeners
        restartButton.onClick.RemoveListener(RestartScene);
        quitButton.onClick.RemoveListener(QuitApplication);
    }

    private void HandleSessionOver()
    {
        // Activate the menu object
        if (menuObject != null)
        {
            menuObject.SetActive(true);
        }

        // Deactivate the game UI
        if (gameUIObject != null)
        {
            gameUIObject.SetActive(false);
        }
    }

    private void RestartScene()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void QuitApplication()
    {

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

