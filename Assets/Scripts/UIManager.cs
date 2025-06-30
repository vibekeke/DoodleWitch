using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections; //For enumerator

//Unused script
public class UIManager : MonoBehaviour
{
    public PlayerScript player;
    public Button dashButton;
    public float dashCooldownTime = 3f;

    public void OnDashPress()
    {
        player.DashForward();
        
        StartCoroutine(HandleCooldown(dashButton, dashCooldownTime));
    }
    public void OnRestartPress() //We don't have a restart-button yet but it's a good thing to have
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Loads the current scene, aka. reload.
    }

    IEnumerator HandleCooldown(Button button, float cooldownTime)
    {
        button.interactable = false;

        //You can add animations and stuff here :)
        yield return new WaitForSeconds(cooldownTime);

        button.interactable = true;
    }
}
