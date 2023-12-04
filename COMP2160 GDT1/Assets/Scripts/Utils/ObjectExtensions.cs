﻿using UnityEngine;
using System.Collections;

/**
 * Extension methods for the Object class
 */

public static class ObjectExtensions  
{

	/**
	 * A simplified version of Object.FindObjectOfType() that eliminates the need for type-casting.
	 *
	 * Example:
	 * 		Player player = FindObjectOfType<Player>();
	 */

	public static T FindObjectOfType<T>(this Object o) where T : Object 
	{
		return (T) Object.FindObjectOfType (typeof(T));
	}
		
	/**
	 * A simplified version of Object.FindObjectsOfType() that eliminates the need for type-casting.
	 *
	 * Example:
	 * 		Coins[] coins = FindObjectsOfType<Coin>();
	 */

	public static T[] FindObjectsOfType<T>(this Object o) where T : Object 
	{
		return (T[]) Object.FindObjectsOfType (typeof(T));
	}
}
