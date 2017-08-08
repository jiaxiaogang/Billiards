using UnityEngine;
using System.Collections;
/// <summary>
/// 此类为每个item项的父类,需要有父类来记录下当前选中的item和控制只能同时选中一项item所以父类还是应该写的.
/// </summary>
public class ItemSuper : MonoBehaviour {

    public static string OnSelectedItemValueIP;//当前选中项的IP全局变量
    private GameObject lastSelectedItem;//最后一个被选中的项

    public GameObject LastSelectedItem
    {
        get { return lastSelectedItem; }
        set { 
            //当值发生改变时,先将当前值下的gameObject的对勾去掉;
            lastSelectedItem.GetComponentInChildren<UIToggle>().value = false;
            lastSelectedItem = value; 
        }
    }
    //在最后选中项将要改变的时候.先将最后选中项的选中状态去掉,再将当前选中项赋值给LastSelectedItem变量
    private string itemValueIP;

    public string ItemValueIP
    {
        get { return itemValueIP; }
        set { itemValueIP = value; }
    }
    private string itemValueroomName;

    public string ItemValueroomName
    {
        get { return itemValueroomName; }
        set { itemValueroomName = value; }
    }

}
