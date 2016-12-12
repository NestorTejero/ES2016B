using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadingScreen : MonoBehaviour
{
    public Image bar;
    public Text percent;
    public float time;

    private void Start()
    {
        //loadingBar.fillAmount = 0;
    }


    private void Update()
    {
        if (time == 100)
        {
            SceneManager.UnloadScene(1);
            SceneManager.LoadScene(1);
        }

        if (time < 100)
            time += Time.deltaTime*7;

        if (time >= 100)
            time = 100;

        percent.text = "" + (int) time + "%";
        bar.transform.localScale = new Vector3(time/100, 1, 1);
    }

    // Skip the Loading Screen
    public void Skip()
    {
        SceneManager.UnloadScene(1);
        SceneManager.LoadScene(1);
    }
}