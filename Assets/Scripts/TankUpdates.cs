using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankUpdates : MonoBehaviour
{
    [SerializeField] private LeavleCharacteristic[] leavleCharacteristic;
    [Space]
    [SerializeField] private TankMove _tankMove;
    [SerializeField] private TankGun _tankGun;
    [SerializeField] private SpriteRenderer _tankRenderer;
    public int Leavle { get; private set; }

    public void UpdateLeavle()
    {
        Leavle++;
        ChangeCharacteristic();
    }

    public void ResetLeavle()
    {
        Leavle = 0;
        ChangeCharacteristic();
    }

    private void ChangeCharacteristic()
    {
        LeavleCharacteristic nextLeavle = leavleCharacteristic[Leavle];
        _tankMove.Speed = nextLeavle.TankSpeed;
        _tankGun.UpdateCharacteristic(nextLeavle.ShootSpeed, nextLeavle.ShootPeriod);
        _tankRenderer.color = nextLeavle.Color;
    }

    [System.Serializable]
    public class LeavleCharacteristic
    {
        public float TankSpeed = 1;
        public float ShootSpeed = 1;
        public float ShootPeriod = 1;
        public Bullet Bullet;
        public bool UseStandartBullet = true;
        public Color Color;
    }
}
