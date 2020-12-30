using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
	public int damage;
	protected int accuracy;

	protected float projectileSpeed;
	protected float cooldown;
	protected float timer;

  public GameObject bullet;
	protected GameObject gunHolder;

	public bool AllyHeld = true;
	protected GameObject player;

	public abstract void Attack();
	public abstract void UpdateAccuracy(int change);
	public abstract void UpdateDamage(int change);
	public abstract void UpdateFireSpeed(float change);
	public abstract void UpdateTimer();

	public void SetCooldown(float duration)
	{
		this.cooldown = duration;
	}
	public void SetAccuracy(int accuracy)
	{
		this.accuracy = accuracy;
	}
	public void SetDamage(int damage)
	{
		this.damage = damage;
	}

	protected Vector3 GetSpread()
	{
		return accuracy * (Vector3.up * Random.value * (Random.value > 0.5 ? 1 : -1) + 
				Vector3.right * Random.value * (Random.value > 0.5 ? 1 : -1) + 
				Vector3.forward * Random.value * (Random.value > 0.5 ? 1 : -1));
	}
}
