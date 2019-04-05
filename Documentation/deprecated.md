<a id="deprecated"></a>
## Prologue

The basic design pattern behind this project is inspired by 
Entity Component System(ECS), Interface-based Programming,
Component-based Architecture and Composition-over-Inheritance.

Although ECS is very great for large-scale games,
it is not ready yet for production and it is kind of counterintuitive when making small games.
Hence, I spent several months on experimenting with my personal project, Rally the Brave,
and finally discovered an architecture called Entity Template Container Behaviour(ETCB).

## What is the truth(history) behind ETCB?

It is really hard to find a proper architecture.
Sometimes I wish I could get rid of all Inheritance and Interfaces 
so that the whole structure of projects is simplified.
Rest of the time, I think it is so good to let everything implement Interfaces or base abstract classes,
in order to achieve the highest scalability and the lowest decoupling.

So, guess what? The former is kind of "lazy" programming,
and in later development when you try to modify/fix/add contents it is just painful.
But the latter is also kinda over-engineering, complex and unfriendly.
All Interitance and Interfaces can do is to make sure the "contract" is right,
they do not break the "connections" nor guanrantee that the "result" is what you want.

Then, I was stuck at Interface-based Programming and appealed for new inspirations.
After massive researches, suddenly, an article about Unity ECS saved my life and brought me to a new world(though not ECS world).
In the new world, everything is split into two types: 
one type includes replaceable logic components with Interfaces and
another type includes irreplaceable data components just like an identity card.
After a long time of refinement,
this final architecture called ETCB was born in the new world.

## Entity Template Container Behaviour(ETCB)

ETCB is a hybrid architecutre specialized in game development, 
which classifies all game things in to three types: Entity, Template, Container and Behaviour.

The basic idea is:

*	A game Entity is defined by Templates like Shape, Color, Health and so on.
*	Players interact with the game world by invoking Behaviours.
*	Behaviours may invoke other Behaviours through Interfaces.
*	Behaviours finally manipulate Containers in Entities.
*	Players receive responses from Container modifications(represented as graphic, UI elements and so on).
*	Simple enough.

## Entity

Entities are the basic game things that players can see, interact with, control and so on.
They are generally comprised of Containers and/or Behaviours.

Entities are conceptually classified into three types:

1.	GameObjects that have both Containers and Behaviours attached. 
	This kind of Entities have their Behaviour autonomy and each one is treated independently.
2.	GameObjects that have only Containers attached but do have their Behaviours.
	E.g., if hundreds of enenmies are attacking at the same time, having Update() on each of them would be inefficient.
	Instead, a system-level Bahavior will collect all of them and and let them attack all together.
3.	Entities can be Containers if they do not need any benefits from GameObjects.
	Just like the second type of Entities, there would also exist Behaviours that processes such Entities.

## Template

Templates refer to:

*	data strcutures that are read-only during life-time.
*	properties that are pre-defined in editor.
*	unique assets/instances that are shared across the whole project.

Templates are typically classified as:
*	Scriptable Objects and their assets/instances.
*	Serializable C# classes that serve as parts of Scriptable Object.
*	External static data like database.

## Container

Containers are MonoBehaviour or C# class data containers that represent the properties an Entity holds.

Containers usually also contain Templates and other Containers.

## Behaviour

Behaviours are simply MonoBehaviours that interact directly with game worlds, i.e.,
they implement functions and process data in Containers and game worlds.

When Behaviours must communicate with each other, they should implement Interfaces so that they become replaceable
(just like replaceable Components in Component-based Architecture).

Behaviours are classfied as Manager and System:
1.	A System Behaviour is very similar to System in ECS. It collects all Containers with the same type, and process them all together.
2.	A Manager Behaviour is attached together with the Containers it needs on the same GameObject. 
	So, we are giving those GameObjects autonomy when they should behave like an independent Entity.

## How to work with ETCB?

1.	Specify what kind of functionalities we need in order to make the game interactive.
2.	Specify what kind of Entities are needed in order to achieve these functionalities.
3.	Create Templates that describe Entities.
4.	Create Containers according to Templates and Entites.
5.	Create Behaviours that manipulate Containers.
6.	Create Interfaces for certain Entities whose Behaviours are required by other Entities.
7.	Create Entities by integrating Containers and Behaviours altogether.
8.	Design, Debug and Release.
9.	Add new features(See <a href="#new-content">details</a>).

## What's the matter with Component-based Architecture?

Let me describe ETCB in Component-based Architecture:
*	Containers are irreplaceable Components.
*	Behaviours are replaceable Components.
*	Entities are basically comprised of different Components(Containers and/or Behaviours).

## What is the duty of Interface-based Programming?

Traditionally, Interface forces users to fulfill its "Contract" in order to make sure a certain functionality is implemented.
But in ETCB, it serves as only a communication bridge between objects that need to talk.
For example:
*	when A depends on B's Behaviours it should rely on only WHAT B does(Interface) instead of HOW B does something(specific implementation).
*	when A provides some services for others, it extracts those services and put them into an Interface.

## What's the difference from traditional OOP?

For example, a character is going to take Bleeding effect from an attack:
*	In tradition OOP with Inheritance and virtual methods, the procedures could be:
	1.	Bleeding : Status is an object that holds a reference to the character and a function called Tick().
	2.	Character : Creature is an object that holds an array of statuses and processes characters' information.
	3.	Character.TakeStatus(Bleeding) adds Bleeding to Character triggered by users' actions.
	4.	We call Character.UpdateStatus() to interate through all Bleeding statues and call their Tick().
	5.	Tick() then calls Character.TakeDamage(Power).
	6.	TakeDamage(Power) reduces Character's Health by Power.
*	Whereas in ETCB with Composition and Interfaces:
	1.	CharCtn and BleedCtn are data Containers.
	2.	StatusMgr : IStatusTaker is a third-party Behaviour attached to an Entity called Player.
	3.	CombatMgr : IDamageable is another Behaviour attached to the Entity Player.
	4.	IStatusTaker.TakeStatus(BleedCtn) adds BleedCtn to CharCtn triggered by users' actions.
	5.	IStatusTaker.Update() calls IDamagable.TakeDamage(BleedCont.Power).
	6.	IDamagable.TakeDamage(Power) decreases CharCtn's Health by Power.

So here are several important points from examples above:
*	Separation - we separate what a Bleeding Effect IS from what it DOES.
*	Replaceable - we let TakeStatus() implement Interfaces because it is triggered outside StatusMgr,
	and Interfaces make sure it can be replaced by other implementations without affecting callers.
*	Third-party - major logic is placed neither in Character nor Bleeding Status, but outside of them.
*	Independent - no one really owns Behaviours, since they are independent processors and can be placed anywhere if necessary.

What else is different?
*	Composition-over-Inheritance
	*	In traditional OOP, new variations often come from Inheritance (e.g., Status -> Buff -> AttackBuff).
	*	In ETCB, new variations are created by modifying Containers. E.g., HealthPotion = 50 Healing Power + 0 Attack Increase, AttackPotion = 0 Healing Power + 10 Attack Increase
		and MysticPotion = 100 Healing Power + 20 Attack Increase. 
*	New Rules vs. Old Principles
	*	You would see some new rules are established in ETCB.
	*	You would also see some old principles in OOP are broken.

## What should an Entity be? A GameObject or a C# class?

*	If the Entity needs to be actually placed in game world, be visible and interact physically with others, a GameObject is required.
*	Anything else should be a C# class.
*	Keep the number of GameObjects and Components in GameObjects as fewer as possible.

## Why not every Entity be GameObject or Scritable Object?

*	Instantiating and Destroying GameObjects and Scritable Objects are expensive.
*	Even empty GameObjects and Components could incur costs if users are not careful.
*	We should have only one unique instance for each kinds of game things, and let others refer to these instances.

## Why Templates be Scriptable Objects?

*	Sctiptable Objects are designed to store pre-defined, unique and immutable data.
*	Sctiptable Object assets are stable and independent instances throughout the whole project, which avoids duplicate data and saves memory.
*	Designers and artists don't have to understand programming in order to manipulate Sctiptable Objects assets.
*	Scriptable Objects assets can be adjusted easily in the inspector just like a prefab.
*	Scriptable Objects can be referred by fields/variables in scritpts.

## Why Template be Database?

*	store and modify persistent user data on disk.
*	restore data from disk next time the app is open.
*	let players make their own Mods.

## Why separate data from logic?
	
*	data containers are easy to be serialized and stored permanently.
*	data containers are easy to be sent to other through any communication bridges like network.
*	Independent Behaviours are easy to read, modularized, reusable and maintainable.

## Why Containers not implement Interface?

Someone may ask why Containers do not implement Interfaces 
if ETCB is following Replaceable Components principle of Component-based Architecture.

Well, because it is the data that makes a game Entity what it is. 
If data are replaceable, the Entity would be undefined.
I.e., whenever A needs B's data, it should get specific information about B instead of replaceable properties.

## How to generate Container from Template?

*	If the Template can be used directly,
	just treat it as Container.
	*	For example, a Health Globe is used once and expired upon touching.
		So a Health Globe Template is enough since we do not need extra information.
*	If the Template needs extra independent data that are modified during runtime, 
	make a Container that refers to this Template plus the independent data.
	*	For example, a Weapon should have a variable called current durability,
		which is reduced per use. 
		In order to distinguish it from a new Weapon with full durability,
		we could creat two Weapon Container instances that refers to the same Weapon Template
		but have different durability.

<a id="unsafe-operation"></a>
## What are potential problems related to Containers?

Most people coming from OOP backgrounds think every object is a "complete" individual that has full control over both data and Behaviours. 
The individual decides how its data are accessed instead of putting them into a public place so that everyone can see/modify them.

But...I would say it's acceptable in game industry due to frequent data communications, 
and no architectures can really prevent people from doing unsafe operations.

So, read the following rules and eliminate unsafe operations:
*	Write comprehensive comments and documentations for projects.
*	if you need to read from a Container, just go and read it.
*	if you need to write to a Container, read the documentations to see if other Behaviours provide such functionalities.
	*	if yes, just go and use their functionalities.
	*	if yes but the existing implementations do not fulfill your requirements,
		ask their authors to provide new functionalities with new Interface contracts.
	*	if no, then create your own implementations that are safe, clean and public to others, 
		extract Interfaces from the public parts of implementations,
		and do not forget to write documentations or comments.

## Why put Behaviours in MonoBehaviour?

*	Wherever you put logic it must finally interact with the game world.
*	MonoBehaviours provide abilities to interact with the game world.
*	Lower level of indirection and abstraction 
	because we don't have to do things like calling A() that calls B() that calls C() that calls D() in order to call E().

## Why not pure data Containers(why logic in Containers)?

Because:
*	most of the time you need constructors and properties.
*	somtimes you need special getter and setter.
*	rest of the time you need some supplementary logic.

Feel free to put logic in Containers to avoid problems 
like duplicate codes, out of range, unnecassary indirection and so on.

Just bear in mind:
*	everything in Containers should be irreplaceable, inflexible and invariant.
*	if a piece of logic is prone to change, put it in Behaviours to prevent Domino Effect.

## Why not pure logical Behaviours(why data in Behaviours)?

Because:
*	without data, Behaviours have nothing to deal with.
*	some data don't have to be public.

Feel free to put data in Behaviours to avoid problems
like nothing to deal with, unnecessary data corruption and so on.

Just bear in mind:
*	everything in Behaviours should be replaceable, flexible and prone to change.
*	if a piece of data is necessary to make an Entity meaningful, put it in Containers.
*	if a piece of data is accessed by others, put it in Containers.

## Why are codes in ETCB reusable, maintainable and easy to understand?

*	At first glance, you know how data are organized in a Container.
*	At first glance, you know all properties of an Entity a Template defines.
*	At first glance, you know what functionalities a Interface is achieving.
*	At first glance, you know which Containers a Behaviour is dealing with.
*	You have only four types of elements to deal with: Entity, Template, Container or Behaviour.
*	To make them work, just match them and put them in the scene. Done.

## What about Unity Engine classes?

Unity Engine classes are traditional OOP classes, 
which contain both data and logic at the same time.
Does using them break our ETCB rules?

No, because
*	ETCB aims to achieve low coupling and high scalability so that there is no Domino effect
*	as long as you are using (and you should use) the same version of Unity for projects from beginning to end,
	those Engine classes stay the same.

So, we have no reason to refuse Unity Engine classes.

<a id="new-content"></a>
## Aggressive or Conservative increment?

There always comes a time when new features are to be added to existing contents.

Should we be aggressive or conservative?

Aggresive increment means we create new Behaviours, Containers (and potentially new Entities) to adapt new features when:
*	new features differ very much from existing contents.
*	solely modifying existing contents do not make sense because new features are unique.

On the other hand, Conservative increment suggests to adding new data to existing Containers and modifying existing Behaviours if:
*	new features are so similar to existing contents that new Behaviours will result in duplicate/similar codes.
*	new features are likely to become common and shared between existing contents.

## Zenject(DI, IoC and Singleton)!

Singleton pattern is powerful and necessary in game development,
but how it is implemented is still controversial in Unity,
because:
*	a singleton must be unique.
*	a singleton may be passed to any arbitrary object(and we don't even know who needs it).
*	a singleton may intantiate other singletons(just like a bootstrap).

This is where Zenject kicks in, and solves all problems.
Zenject is a Dependency Inject plugin in Unity,
and the most use of Zenject in ETCB is to achieve Singleton pattern.

But, what does it do with Singleton Pattern?
*	Zenject is just a root layer singleton.
*	Zenject keep track of singletons and make sure they are unique.
*	Any other singletons are registered to Zenject.
*	When on demand, Zenject instantiates required singletons.
*	When on demand, Zenject injects those singletons into any object that is asking for the references.

## Event Aggregator

An Event Aggregator is a Singleton that
*	"acts as a single source of events"(<a href="https://martinfowler.com/eaaDev/EventAggregator.html">Source</a>).
*	aggregates and listens on Subjects' events.
*	forwards these events to Observers that have registered for the events.
*	adds one level of indirection to event handling.
*	decreases coupling further than Delegates.
*	reduces complexity further than Observer Pattern.

In ETCB, Event Aggregator = Zenject(Singleton) + Delegate + Interface + Observer Pattern.

## Drawback of ETCB

1.	Code duplication if not designed carefully. Auxiliary classes and extension methods might be helpful, but I suggest it's just better to be careful.
2.	Somtimes there exists a trade-off between memory space and scalability, so profile you game and see which side you really need.
3.	All data would be visible to the public, so unsafe operations may be involved(See <a href="#unsafe-operation">solutions</a>).
4.	Even as ETCB's father I am still feeling a little bit resistance against ETCB, beacuse I actually come from an OOP background. 
	But old principles are not always gonna be obeyed, and no rules are ever suitable for everyone.


## Name Convention

Prefix:
*	"MB" means it derives from MonoBehaviour.
*	"SO" means it derives from Scriptable Object.
*	"E" means it is an enum.
*	"I" means it is an Interface.
*	Otherwise it derives from System.Object.

Suffix:
*	"Mgr" means it is a Manager Behaviour.
*	"Sys" means it is a System Behaviour.
*	"Tpl" means it is a Template.
*	No suffix in most time means it is a data Container or non-GameObject Entity.
*	Rest of the time it may be:
	1.	a non-serializable data strucutre class.
	2.	a utility class.
	3.	a Wrapper class, and so on.


## Glossary

*	Domino Effect: one change causes another change, which causes further changes.
*	Unnecessary data corruption: a piece of data is put into an unrelated Container.
*	Subject: a source object that fires an event.
*	Observer: an object that listens on an event that a Subject fires.
*	C# class: a class that does not inherits from Unity Engine classes.
*	Unity Engine class: a class that inherits from Unity Engine classes.


