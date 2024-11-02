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
        // 현재 노드의 키와 입력된 키가 일치하는지 확인
        if (Input.GetKeyDown(currentNode.InputKey))
        {
            ExecuteAttack(currentNode.AttackName);

            // 다음 동작이 있을 경우 다음 노드로 이동
            if (currentNode.NextMoves.Count > 0)
            {
                currentNode = currentNode.NextMoves[0];
                ComboTimer = ComboResetTime;
            }
            else
            {
                // 다음 동작이 없으면 초기 상태로 콤보를 리셋
                ResetCombo();
            }
        }
        else if (Input.anyKeyDown)
        {
            // 다른 키가 눌리면 콤보를 리셋
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
