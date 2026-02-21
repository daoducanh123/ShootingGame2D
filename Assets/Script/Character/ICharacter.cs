using UnityEngine;

    public interface ICharacter
{
    void UpdateHpBar( ); void Healing(float healValue);
    void TakeDamage(float damageValue); 
    void SlowState(float slowValue);
    void FrozenState();
    void FireState(float fireDamagePerSecond, GameObject fireEffect);
    
}