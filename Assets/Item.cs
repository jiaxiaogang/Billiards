using UnityEngine;
using System.Collections;

public class Item : ItemSuper {
    /// <summary>
    /// 选中当前item的时候执行此方法,将当前选中项报给ItemSuper类.
    /// </summary>
	public void OnSelectedThisItem()
    {
        LastSelectedItem = this.gameObject;
    }
}
