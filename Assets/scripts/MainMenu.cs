using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelsManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject startPanel;
    public GameObject levelsPanel;
    public int targetCores = 6;
    public string nextScene = "Level2";
    public string menuScene = "MainMenu";
    public string level1Scene = "Level1";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel.activeSelf) Resume();
            else Pause();
        }

        if (EnergyCores.score == targetCores) Show(winPanel);
    }

    public void ShowLevels()
    {
        if (startPanel != null) startPanel.SetActive(false);
        if (levelsPanel != null) levelsPanel.SetActive(true);
    }

    public void LoadLevel1()
    {
        Load(level1Scene);
    }

    public void ShowLose() { Show(losePanel); }

    void Show(GameObject panel)
    {
        panel.SetActive(true);
        Freeze();
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Freeze();
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    void Freeze()
    {
        Time.timeScale = 0f;
    }

    void Load(string scene)
    {
        EnergyCores.score = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }

    public void Retry() { Load(SceneManager.GetActiveScene().name); }

    public void NextLevel()
    {
        if (Application.CanStreamedLevelBeLoaded(nextScene))
            Load(nextScene);
        else
            Load(menuScene);
    }

    public void GoToMenu() { Load(menuScene); }
}