using System.Collections.Generic;
using UnityEngine;

public class ComboNode
{
    public string AttackName;
    public KeyCode InputKey;
    public List<ComboNode> NextMoves;

    public ComboNode(string attackName, KeyCode InputKey)
    {
        AttackName = attackName;
        this.InputKey = InputKey;
        NextMoves = new List<ComboNode>();
    }
}
            
public class ComboManager : MonoBehaviour
{
    private ComboNode root;
    private ComboNode currentNode;
    private float ComboResetTime = 1f;
    private float ComboTimer;

    public int SummonerCount;

    void Start()
    {
        Tree_Init();
        currentNode = root;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        comboTimer();
    }
    void Tree_Init()
    {
        root = new ComboNode("Counter", KeyCode.K);
        var skill1 = new ComboNode("Skill1", KeyCode.U);
        var punch = new ComboNode("Punch", KeyCode.J);
        var skill2 = new ComboNode("Skill2", KeyCode.I);
        var skill3 = new ComboNode("Skill3", KeyCode.O);
        var Ult = new ComboNode("Ult", KeyCode.P);

        root.NextMoves.Add(skill1);
        skill1.NextMoves.Add(punch);
        punch.NextMoves.Add(skill2);
        skill2.NextMoves.Add(skill3);
        skill3.NextMoves.Add(Ult);
    }
    void HandleInput()
    {
        // ���� ����� Ű�� �Էµ� Ű�� ��ġ�ϴ��� Ȯ��
        if (Input.GetKeyDown(currentNode.InputKey))
        {
            ExecuteAttack(currentNode.AttackName);

            // ���� ������ ���� ��� ���� ���� �̵�
            if (currentNode.NextMoves.Count > 0)
            {
                currentNode = currentNode.NextMoves[0];
                ComboTimer = ComboResetTime;
            }
            else
            {
                // ���� ������ ������ �ʱ� ���·� �޺��� ����
                ResetCombo();
            }
        }
        else if (Input.anyKeyDown)
        {
            // �ٸ� Ű�� ������ �޺��� ����
            ResetCombo();
        }
    }

    void comboTimer()
    {
        if(ComboTimer > 0)
        {
            ComboTimer -= Time.deltaTime;
        }
        else
        {
            ResetCombo();
        }
    }
    void ExecuteAttack(string attackName)
    {
        Debug.Log("Executing " + attackName);
    }
    void ResetCombo()
    {
        currentNode = root;
    }
}
