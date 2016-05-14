using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
	public enum BasicLandTypes
	{
		Plains,
		Mountain,
		Forest,
		Swamp,
		Island
	}

	public enum ManaTypes
	{
		Uncolored,
		White,
		Red,
		Blue,
		Green,
		Black
	}

	public enum Players
	{
		PlayerOne,
		PlayerTwo,
		PlayerThree,
		PlayerFour
	}

	public enum CardTypes
	{
		Artifact,
		Creature,
		Enchantment,
		Hero,
		Instant,
		Land,
		Phenomenon,
		Plane,
		Planeswalker,
		Scheme,
		Sorcery,
		Tribal,
		Vanguard
	}
	
	public enum Zones
	{
		Library,
		Hand,
		Battlefield,
		Graveyard,
		Stack,
		Exile,
		Command
	}

	public enum Phases
	{
		Beginning,
		PrecombatMain,
		Combat,
		PostcombatMain,
		Ending
	}

	public enum Steps
	{
		Untap,
		Upkeep,
		Draw,
		BeginningOfCombat,
		DeclareAttackers,
		DeclareBlockers,
		CombatDamage,
		EndOfCombat,
		End,
		Cleanup
	}

	public enum AbilityTypes
	{
		Triggered,
		Activated,
		Passive,
		DelayedTriggered,
		Replacement
	}
}
