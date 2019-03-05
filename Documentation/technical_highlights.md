## Original Ideas

The basic design pattern behind this project is inspired by 
Entity Component System(ECS) and Interface-based Programming.
Since official ECS is not ready in Unity yet,
here I suggest a similar architecure called Entity Container Behavior(ECB).

Three basic elements in ECB include Container, Behavior and Entity:
1.	Containers are classes that have no major logic and serve purely as data structures,
	In most cases, a Container is simply a ScriptableObject(SO) asset.
	In the rest of cases, DCs are just C# classes that could contain or be contained with SOs.
2.	Behaviors are simply implementations of Interfaces that present certain functionalities.
	Each functionality may have several Behaviors.
3.	Entities are typically MonoBehaviors composed of Containers and Behaviors.
	In another word, Entities implement relevant Behaviors in order to process data in Containers.

## How they work together


## Why Container be Scriptable Object?

*	SctiptableObjects are designed to store data.
*	Designers and artists don't have to understand programming in order to use SO assets.
*	Variations are easy to make by adjusting variables in SO assets.
*	SO assets are stable and unique instances throughout the whole project.

## Why separate data from others?

*	Reusable data structures for other Entities.
*	Scriptable Objects are independent assets by design.


## Why Behavior implement Interfaces?

*	The same piece of data could be processed in various ways.
*	The same functionality can be implemented in various ways.
*	A functionality means what it DOES instead of what it IS.
*	Communications between classes that do not depend on specific implementation
	provides great flexibility.

## Why Entity be MonoBehavior?

*	Wherever you put logic it must finally interact with the game world.
*	MonoBehaviors provide abilities to interact with the game world.

## What's the difference from traditional OOP?
*	Inheritance
	*	In OOP, new variations often come from Inheritance (e.g., Status -> Buff -> AttackBuff).
	*	In ECB, new variations are created by modifying Containers and Behaviors
		(e.g., Status with an attribute modifier that modifies Attack).

## Aggressive or Conservative increment?

There is always a situation where new features are to be added to existing contents.
Should we be aggressive or Conservative?

Aggresive increment means we create new Behaviors (and potentially new Entities) to adapt new features.
*	When new features differ very much from existing contents.
*	When solely modifying existing contents do not make sense because new features are unique.

On the other hand, Conservative increment suggests to adding new data to existing Containers and modifying existing Behaviors.
*	If new features are so similar to existing contents that new Behaviors will result in duplicate/similar codes.
*	If new features are likely to become common and shared between existing contents.

## Drawback

*	Code duplication which might be solved by auxiliary classes, extension methods and so on.
*	Entities must be attached to Gameobjects.
*	Adding unneccessary complexities and indirection to codes.
*	Extra memory usage and expensive virtual/delegate calls would be involved.