﻿using UnityEngine;
using System.Collections;

/**
 * Extensions to Unity's LayerMask class
 * 
 */

public static class LayerMaskExtensions  
{

	/**
	 * Check if a particular gameobject is included in the layermask.
	 * 
	 * Example:
	 * 		LayerMask layerMask;
	 * 		GameObject g;
	 * 		if (layerMask.Contains(g))
	 * 		{
     *     		// the object matches the layer mask
	 * 		}
	 */

	public static bool Contains(this LayerMask layerMask, GameObject gameObject) 
	{
		return (layerMask.value & (1 << gameObject.layer)) != 0;
	}
}
