using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
   public void StartBtn()
   {
    SceneManager.LoadScene("SampleScene");
   }
}
