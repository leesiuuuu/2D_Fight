using UnityEngine;

public class HeadCol : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D cols)
    {
        if (cols.gameObject.name == "AttackCol")
        {
            DummyManager.instance.HitHead = true;
            DummyManager.instance.HitBody = false;
            DummyManager.instance.HitFoot = false;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "AttackCol")
        {
            DummyManager.instance.HitHead = false;
        }
    }
}
