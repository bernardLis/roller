using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LivingEntity : MonoBehaviour, IDamegeable
{
	public float startingHealth;
	protected float health;
	protected bool dead;

	public event Action OnDeath;

	protected virtual void Start()
	{
		health = startingHealth;
	}
	public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
	{
		TakeDamage(damage);
	}

	public virtual void TakeDamage(float damage)
	{
		health -= damage;

		if (health <= 0 && !dead)
		{
			Die();
		}
	}


	[ContextMenu("Self Destruct")]
	public virtual void Die()
	{
		dead = true;
		if (OnDeath != null)
		{
			OnDeath();
		}
		GameObject.Destroy(gameObject);
	}
}
