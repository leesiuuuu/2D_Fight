using UnityEngine;

public class BodyCol : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D cols)
    {
        if(cols.gameObject.name == "AttackCol")
        {
            DummyManager.instance.HitBody = true;
            DummyManager.instance.HitFoot = false;
            DummyManager.instance.HitHead = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "AttackCol")
        {
            DummyManager.instance.HitBody = false;
        }
    }
}
