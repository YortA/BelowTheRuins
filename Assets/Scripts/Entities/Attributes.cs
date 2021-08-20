using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attributes : MonoBehaviour
{
	// health = life
    public int health = 10;
	// stamina = attacks (regenerates)
	public int stamina = 10;
    public int currrentHealth { get; private set; }
	// ## FoW distance based on radius
	public float fogDistance = 10f;


	private void Awake()
	{
		currrentHealth = health;
	}

	// DEBUGGING DAMAGE
	/*
	private void Update()
	{
		Keyboard kb = InputSystem.GetDevice<Keyboard>();

		if(kb.xKey.wasPressedThisFrame)
		{
			TakeDamage(1);
		}
	}
	*/
	public void TakeDamage(int damage)
	{
		// Damage[0, max]
		// we don't want to deal negative damage, that will heal the opponent (neg + neg = posi)
		damage = Mathf.Clamp(damage, 0, int.MaxValue);	// we'll never deal below zero damage
		currrentHealth -= damage;   // take damage

		// debug
		Debug.Log(transform.name + " takes " + damage + " damage.");

		// entity died
		if (currrentHealth <= 0)
		{
			ActorDied();
		}
	}

	// override our dying function (especially for PlayerAttributes.cs)
	public virtual void ActorDied()
	{
		//banana
	}
}
