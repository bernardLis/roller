using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamegeable
{
	void TakeHit(float damage, Vector3 hitPoint, Vector3 direction);
	void TakeDamage(float damage);

}
