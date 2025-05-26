using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 매니저 작성시 Monobehaviour 을 때어줘야 Managers에서 new로 생성가능
public class PlayerManager
{
    private Player _player;
    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }
}
