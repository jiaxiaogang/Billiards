using UnityEngine;
using System.Collections;

public class SingleGame : GameModeController
{
    public override void StartGame()
    {
        base.StartGame();
    }
    //public void StartSingleGame()
    //{
    //    //第一步:先把UI三球显示深度调节回2以免后面出错
    //    GameObject[] gos = GameObject.FindGameObjectsWithTag("threeball");
    //    for (int i = 0; i < gos.Length; i++)
    //    {
    //        gos[i].gameObject.GetComponent<UISprite>().depth = 2;
    //    }
    //    //第二步:这里需要把UIROOT禁用掉,启动BallArmFather 和 Plane;
    //    Menu.SetActive(false);
        
    //    motherBallCamera.GetComponent<Camera>().depth = 0;
    //    motherBallCamera.GetComponent<Animator>().SetTrigger("Transition");
    //    TouchControl.SetActive(true);
    //}
}
