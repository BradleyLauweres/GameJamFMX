using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Menu References")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;

    [Header("Settings")]
    [SerializeField] private bool pauseGameWhenMenuOpen = true;

    private Dictionary<string, GameObject> menus = new Dictionary<string, GameObject>();
    private GameObject currentActiveMenu;
    private bool isGamePaused = false;

    public static MenuManager Instance { get; private set; }

    private void Awake()
    {
        RegisterMenus();
    }

    private void Start()
    {
    }

    private void Update()
    {
    }

    private void RegisterMenus()
    {
        if (mainMenu != null) menus.Add("Main", mainMenu);
        if (settingsMenu != null) menus.Add("Settings", settingsMenu);

    }

    public void OpenMenu(string menuName)
    {
        if (!menus.ContainsKey(menuName))
        {
            Debug.LogWarning($"Menu '{menuName}' not found!");
            return;
        }

        if (currentActiveMenu != null)
        {
            currentActiveMenu.SetActive(false);
        }

        menus[menuName].SetActive(true);
        currentActiveMenu = menus[menuName];

        if (pauseGameWhenMenuOpen && menuName != "Main")
        {
            PauseGame();
        }
    }

    public void CloseAllMenus()
    {
        foreach (var menu in menus.Values)
        {
            menu.SetActive(false);
        }
        currentActiveMenu = null;

        if (isGamePaused)
        {
            ResumeGame();
        }
    }

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        isGamePaused = false;

        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(int buildIndex)
    {
        Time.timeScale = 1f;
        isGamePaused = false;

        SceneManager.LoadScene(buildIndex);
    }

    public void ReloadCurrentScene()
    {
        Time.timeScale = 1f;
        isGamePaused = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No next scene available!");
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void AddMenu(string menuName, GameObject menuObject)
    {
        if (!menus.ContainsKey(menuName))
        {
            menus.Add(menuName, menuObject);
            menuObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"Menu '{menuName}' already exists!");
        }
    }

    
}