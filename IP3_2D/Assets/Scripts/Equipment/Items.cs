using UnityEngine;
using System.Collections;

public class Items {

	public string name;
	public string description;
	public int damage;
	public int level;
	public int durability;
	
	public Items(string itemName, string itemDescription, int itemDamage, int itemLevel, int itemDurability)
	{
		name = itemName;
		description = itemDescription;
		damage = itemDamage;
		level = itemLevel;
		durability = itemDurability;
	}

	public virtual void SpecialAbility(){
		//items special ability code here
	}

}
