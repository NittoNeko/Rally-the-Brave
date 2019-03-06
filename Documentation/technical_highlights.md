## Prologue

The basic design pattern behind this project is inspired by 
Entity Component System(ECS), Interface-based Programming,
Component-based Architecture and Composition-over-Inheritance.

Although ECS is very great for large-scale games,
it is not ready yet for production and it is kind of counterintuitive when making small games.
Hence, I spent several months on experimenting with my personal project, Rally the Brave,
and finally discovered an architecture called Entity Container Behavior (ECB).

## Entity, Container and Behavior:
1.	Entities are conceptually classified into three types:
	1.	GameObjects that have both Containers and Behaviors attached. 
		This kind of Entities have their Behavior autonomy and 
	2.	GameObjects that have only Containers attached but do have their Behaviors.
		E.g., if hundreds of enenmies are attacking at the same time, having Update() on each of them would be inefficient.
		Instead, a manager-level Bahavior will collect all of them and and process them all together.
	3.	C# classes just like Containers because they do not need any functionality from GameObject.
2.	Containers are classes that contain no major logic and serve purely as data structures,
	In most cases, a Container is simply a ScriptableObject(SO) asset.
	In some cases, Containers are MonoBehaviors attached to Gameobjects.
	In the rest of cases, Containers are normal C# classes that is within other Containers.
3.	Behaviors are simply MonoBehaviors that interact directly with game worlds, i.e.,
	they implement functions and process data in Containers and game worlds.
	When Behaviors must communicate with each other, they should implement Interfaces so that they become replacable
	(just like replacable Components in Component-based Architecture).
	Behaviors could be classfied as Manager and System:
	1.	A System Behavior is very similar to System in ECS. It collects all Containers with the same type, and process them all together.
	2.	A Manager Behavior is attached together with the Containers it needs on the same GameObject. 
		So, we are giving those GameObjects autonomy when they should behave like an independent Entity.

## Basic Idea

*	A game Entity is defined by sets of data like Shape, Color, Position and so on.
*	Players interact with the game world by invoking Behaviors.
*	Behaviors manipulate data in Entities.
*	Players receive responses from data modifications.

## What's the difference from traditional OOP?

For example, a character is going to take Bleeding effect from an attack:
*	In tradition OOP, the procedures could be:
	1.	Bleeding is a class that holds a reference to the character and a function called Tick().
	2.	Character.TakeStatus(Bleeding) adds Bleeding to Character triggered by users' actions.
	3.	We call Character.UpdateStatus() to trigger Tick() of all Bleeding statues.
	4.	Tick() then calls Character.TakeDamage(Power) will be called.
	5.	TakeDamage(Power) reduces Character's Health by Power.
*	Whereas in ECB:
	1.	CharCont and BleedCont are pure data Containers, and StatusMgr : IStatusTaker is a third-party Behavior attached to an Entity called Player.
	2.	IStatusTaker.TakeStatus(BleedCont) adds BleddCont to CharCont triggered by users' actions.
	3.	IStatusTaker.Update() calls IDamagable.TakeDamage(BleedCont.Power).
	4.	IDamagable.TakeDamage(Power) decreases CharCont's Health by Power.

So here are several important points from examples above:
*	Separation - we separate what a Bleeding Effect IS from what it DOES.
*	Replacable - we let TakeStatus() implement Interfaces because it is triggered outside StatusMgr,
	and Interfaces make sure it can be replaced by other implementations without affecting callers.
*	Third-party - Major logic is placed neither in Character nor Bleeding Status, but outside of them.
*	Independent - no one really owns Behaviors, since they are independent processors and can be placed anywhere.

What else is different?
*	Composition-over-Inheritance
	*	In traditional OOP, new variations often come from Inheritance (e.g., Status -> Buff -> AttackBuff).
	*	In ECB, new variations are created by modifying data and behaviors
		(e.g., Status with an attribute modifier that modifies Attack).
*	Interface-based Programming
	*	In traditional OOP, anything could be passed as parameters.
	*	In ECB, only Interfaces and Containers are suggested to be passed to others.
		I.e., parameter receivers should not rely on specific implementations.

## How to work with ECB?

1.	Specify what kind of functionalities we need in order to make the game interactive.
2.	Specify what kind of Entities are needed in order to achieve these functionalities.
3.	Create Containers that describe Entities.
4.	Create Behaviors that manipulate Containers.
5.	Create Interfaces for certain Entities whose Behaviors are required by other Entities.
6.	Create Entities by integrating Containers and Behaviors altogether.
7.	Design, Debug and Release.
8.	Add new features(See <a href="new-content?">details</a>).

## What should an Entity be? A GameObject or a C# class?

*	If the Entity needs to be actually placed in game world, be visible and interact physically with others, a GameObject is required.
*	Anything else should be a C# class.

## Why not every Entity be GameObject?

*	Instantiating and Destroying GameObjects are expensive.
*	Even empty GameObjects could incur costs if users are not careful.

## Why Containers not implement Interface?

Someone may ask why Containers do not implement Interfaces 
if ECB is following Replacable Components principle of Component-based Architecture.

Well, because it is the data that makes a game Entity what it is. If data are replacable, the Entity would be undefined.

## Why separate data from logic(why pure data container)?

*	Pure data containers are easy to be serialized and stored permanently.
*	Pure data containers are easy to be sent to other through any communication bridges like network.
*	Separation makes codes easy to read, modularized, reusable and maintainable.

## Why Containers be Scriptable Objects?

*	Sctiptable Objects are designed to store pre-defined, unique and immutable data.
*	Sctiptable Object assets are stable and independent instances throughout the whole project, which avoids duplicate data and saves memory.
*	Designers and artists don't have to understand programming in order to manipulate Sctiptable Objects assets.
*	Variations are easy to make by adjusting variables in Sctiptable Objects assets. 
	E.g., HealthPotion = 50 Healing Power + 0 Attack Increase, AttackPotion = 0 Healing Power + 10 Attack Increase
	and MysticPotion = 100 Healing Power + 20 Attack Increase. 

## Why put Behaviors in MonoBehavior?

*	Wherever you put logic it must finally interact with the game world.
*	MonoBehaviors provide abilities to interact with the game world.
*	Lower level of indirection and abstraction 
	because we don't have to do things like calling A() that calls B() that calls C() that calls D() in order to call E().

<a id="new-content"></a>
## Aggressive or Conservative increment?

There always comes a time when new features are to be added to existing contents.

Should we be aggressive or conservative?

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
*	In some cases, ECB introduces a trade-off between performance and scalability.