using UnityEngine;

public class BtmCol : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D cols)
    {
        if (cols.gameObject.name == "AttackCol")
        {
            DummyManager.instance.HitFoot = true;
            DummyManager.instance.HitBody = false;
            DummyManager.instance.HitHead = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "AttackCol")
        {
            DummyManager.instance.HitFoot = false;
        }
    }
}
