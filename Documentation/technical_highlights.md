<a id="TECThnical_highlights"></a>
## Prologue

The basic design pattern behind this project is inspired by Object-Oriented Programming(OOP) Principles,
Entity Component System(ECS), Interface-based Programming and Component-based Architecture.

I spent several months on learning from those TECThniques,
taking their benefits into my design,
and experimenting with my personal project, Rally the Brave,
and finally discovered an architecture called Template Entity Component Task Task(TECT).

## What is the truth(history) behind TECT?

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

Then, I was stuck with my architecture design and appealed for new inspirations.
After massive researches, suddenly, an article about Unity ECS saved my life and brought me to a new world.
I started to learn about ECS and successfully "stole" a lot of fantastic ideas from it.
Then after another long time of refinement,
an architecture called Entity Template Container Behaviour(ETCB) was born in the new world.

However, I was wrong. 
I thought it would be the end to my jouney, but in fact it was just an incentive for my next architecture design.
I found several serious problems with ETCB(if interested please visit it from the link in the front page),
and Swarm Robotics is just much more intuitive, suitable for OOP(C#) and flexible.

So, I came back to my initial coding style along with some souvenirs from each TECThniques.
Hopefully, this archiTECTutre called Template Entity Component Task(TECT) would be my final habitat.

## Template Entity Component Task(TECT)

TECT is a hybrid archiTECTutre specialized in game development, 
which classifies game elements in to four types: Template, Entity, Component and Task.

The basic idea is:

*	A property is what an Entity HAS.
*	A behaviour is what an Entity DOES.
*	A functionality is what others can DO to an Entity.
*	A Template predefines properties/behaviours of an Entity.
*	A Task is a replaceable implementation of properties/behaviours.
*	A Component is a functionality handler that holds Tempaltes and invokes Tasks.
*	An Entity is composed of Components.
*	Entities are updated through Components.
*	Players interact with the game world by entering inputs.
*	Those inputs in turns trigger corresponding functionalities.
*	This may further invoke other Entities' functionalities.
*	Players perceive changes of Entities through graphic, UI elements and so on.

## Purporses

All the features that TECT has are made to achieve goals like:
1.	Maximum scalability and flexibility.
2.	Clean, maintanable and easy to understand.
3.	Open and adaptable to future changes.
4.	Productive and efficient.

## TECT Work Flow

1.	Create plans like Overview, User Manual or something similar to specify:
	*	what kind of game objects we need.
	*	what kind of properties game objects may have.
	*	what kind of behaviours game objects may have.
	*	what kind of functionalities are required to make the game objects interactive.
2.	According to Step 1, map those concepts to C#: 
	*	create Templates based on properties/behaviours of game objects.
	*	For each game object, we map its properties/behaviours/functionalities to several Components.
	*	Create Tasks to realize these properties/behaviours.
	*	we put Components together to create Entities.
	*	See <a href="#mapping">details</a>
3.	Refactor and Extension. See <a href="#refactor_extension">details</a>.

## Template

Templates refer to shared data structures with unique instances/assets
that are predefined outside runtime and read-only during life-time,
and represent the properties/behaviours of Entitiies.
In order to compose different Entities with the same Templates,
The existance or absence of certain properties/behaviours must be designed and processed carefully. See <a href="#dynamic_behaviour">details</a>.

## Entity

An Entity is a conceptual identity of a game object that helps designers to build the game world.
Game worlds are composed of Entities; Entities are composed of Components.
All of these are achieved through composition and delegation. See <a href="#composition">details</a>.

## Components

A Component is a module that handles functionalities of an Entity and groups relevant properties/behaviours together.
Since a functionality represents a kind of interaction, a Component is also called interaction handler.
So in order to make a Component meaningful, it must have at least one functionality in hands,
otherwise it can be replaced by one or more Tasks.

In TECT Components are very helpful to:
*	represent different concerns of an Entity
*	group related Templates and Tasks together.
*	reduce the weight of a Entity and other Components.
*	delegate jobs to smaller pieces.
*	modularize Entitiies properly.

## Task

Tasks are concrete implementations of properties/behaviours,
and they must be contained within other contexts like Entities or Components.
This means Tasks should always be bound to certain contexts,
and are only visible to those contexts(parents).

<a id="mapping"></a>
## Mapping from TECT to C\#

To realize all these TECT conceptions,
we must map them to C# conceptions:
*	In a composition relationship,
	parents refer to bigger pieces that contain other pieces,
	and children refer to smaller pieces that are contained within other pieces.
	Children might parents as well.
*	In a delegatation relationship,
	parents refer to the higher levels that delegate jobs downward,
	and children refer to the lower levels that receive the jobs.
	Children might be parents as well. 
*	Functionalities are any methods visible to the public.
	They should be always grouped by Interfaces.
	*	However, Interfaces for Strategy Pattern are NOT counted as functionalities.
		Because they are only visible to their parents.
*	Tempaltes are represented by:
	1.	Scriptable Objects and their assets/instances.
	2.	Serializable C# classes that serve as parts of Scriptable Object.
	3.	External data containers like database.
*	Entities are represented by:
	1.	GameObject Entities that are composed of MonoBehvaiour Components.
		Such Entities are chosen when an object should be actually placed in game world, be visible to users and interact physically with others.
		We usually use Transform to represent a GameObject Entity,
		and use GetComponents<Interface> to retrieve the functionalities we are interested in.
	2.	C# Entities that are composed of C# Components.
		Such Entities are chosen when an Entity does not benefit from functionalities of GameObjects.
		We usually use a single Interface that may be composed of other Interfaces to represent a C# Entity,
		so we could know all the functionalitites by looking at its Interface composition. See <a href="#interface_segregation">details</a>.

*	Components are represented by:
	1.	MonoBehaviour Components that derive from MonoBehaviours
		that allow them to directly interact with game worlds(Unity Engine).
		They should be always encapsulated by Unity Engine methods or Interfaces.
	2.	C# Components that derive from System.Object.
		Such Components are usually contained within MonoBehaviour Components,
		through which they are allowed to interact with game worlds indirctly.
		They should always encapsulalted by Intefaces.
*	Components are either encapsulated by Interfaces or MonoBehaviours. See <a href="#composition">details</a>.
*	Tasks are represented by method groups, Strategies, Delegates and so on,
	and they should be only visible to their parents.
	(See <a href="#dynamic_task">details</a>.)

## Cautions about GameObject/MonoBehaviour/ScriptableObject?

*	Instantiating and Destroying GameObjects/MonoBehaviours/ScritableObjects can be expensive.
	*	Do not invoke a large number of such operations in a single frame.
	*	Object Pooling can be a way to avoid costs incurred by instantiation and destruction.
*	Even empty GameObjects that bahave like containers could incur costs if users are not careful, because:
	*	changing parents' Transform can further change all children's Transform.
	*	the more objects in hierarchy, slower the Unity Service Locator is.
*	Unity Engine callbacks like Update are slow. See <a href="#gameobject_optimization">details</a>.

## How to choose the right type of Templates?

Choose ScriptableObjects because:
*	they are designed to store pre-defined, unique and immutable data.
*	their assets are stable and independent instances throughout the whole project, which avoids duplicate data and saves memory.
*	they assets can be adjusted easily in the inspector just like a prefab,
	so designers and artists don't have to understand programming in order to manipulate SctiptableObject assets.
*	they can be referred by fields/variables in scritpts.

Choose external databases because:
*	they store and modify persistent user data on disk.
*	they restore data from disk next time the app is open.
*	they are "external" so that players can make their own Mods.

<a id="gameobject_optimization"></a>
## Optimization about Entities and Components

In fact, due to some special optimizations, GameObject Entities and MonoBehaviour Components can be further classified into three types:
1.	Autonomous Entities with Autonomous Components that independently utilize Unity Engine callbacks like Update.
	Just like Swarm Robotics, they are managed in a decentralized manner.
2.	System Entities with System Components, which aggregate hundreds, thousands of indentical Entities and manage them all together.
3.	Proxy Entities with Proxy Components that are often instantiated in a large scale and thus should be managed by System Entities.

Here you may see the trade-off behind all these optimizations:
1.	C# Entities and Components are the cheapest, but they are handled in the most complex way.
2.	System/Proxy Entities and Components fall in the middle range of this trade-off.
	We want benefits from Unity Engine but don't want to pay much performance,
	so we add complexity to compensate our users.
3.	Autonomous Entities and Components are the most expensive,
	but they are really handy and comprehensive.

<a id="dynamic_task"></a>
## Dynamic Task

To achieve the maximum scalability and flexibility, instead of a fixed sequence of codes(Static Task),
the implementations of Entities in TECT can be defined dynamically by data in Templates.
Normally implementation branches are made with if/switch statements, but they are inflexible, hard to read and sometimes cause performance spikes in tight loops.
Thus, we want to move the conditional checks from execution methods to initilization methods.
I.e. implementations are chosen at initialization and can be changed in runtime instead of if/switch branch statements.
Not all Tasks need to be dynamic, since Tasks are only visible to their parents we could refactor them and make them dynamic on demand easily.

There are several ways to achieve Dynamic Task in TECT, and each of them has its own use cases.
1.	Delegate
	*	Delegate is a pointer variables referring to a behaviour(function),
		which can be reassigned to other functions with the same signature.
	*	Delegate is used when:
		*	implementations are simple.
		*	implementations are only meaningful in local scope.
		*	implementations are so heavily and closely related to context that
			Strategies would add complexity and indirection.
2.	Strategy Pattern
	*	A strategy is a behavioural class encapsulated by an Interface,
		which can be swapped by other Strategies encapsulated by the same Interface.
	*	Strategy is used when:
		*	implementations have their own states.
		*	implementations can be independent from context and reusable.
		*	implementations are too complex or too long as Delegates.
3.	"Owns-a" MonoBehaviour (not suggested)
	*	This is the basic way that Unity provides us in a compositional manner.
	*	GameObjects gain different implementations by swapping MonoBehaviours.
	*	Swapped MonoBehaviours should keep functionalities unchanged.
	*	This could be more expensive and prone to errors than other approaches
		since any MonoBehaviour can be syntactically replaced with any MonoBehaviour.

The basic ideas of Dynamic Tasks are taken from Data Driven Programming,
where Templates are considered as input data and implementations will match the pattern with them.
To put it more concrete, a game is considered as a program that receives inputs from two parties: designers and players.
Designers tell the program how to build the game world through Templates,
and players interact with the game world by entering all kinds of commands.

Dynamic Task also works pretty well with Factory, Object Pool, Command and Null Object Patterns:
*	Factory Pattern
	*	A Factory receives inputs from Templates and initializes corresponding processing patterns.
	*	It could expose a way for users to select implementations.
	*	It could also hide the initialization from users.
	*	It makes later changes easier.
*	Object Pool
	*	An Object Pool optimizes initialization of objects.
	*	A Factory can be turned into an Object Pool without affecting other codes.
*	Command Pattern
	*	A Command wraps an implementation as an object.
	*	It makes an operation executable from other domains, queueable and undoable.
*	Null Object Pattern
	*	One of the disadvantages of TECT is that we need to handle data existance in Templates,
		and this is where Null Object Pattern kicks in.
	*	Absent properties and behaviours are handled by default Null Object implementations.

<a id="composition"></a>
## Composition

A very important feature of TECT is that it introduces genericity and abstraction in a compositional way.
This means at least three things:
1.	Instead of what an object IS, one should focus on what an object HAS and DOES. For example,
	*	a Potion with 100 healing power and 0 attack power is a HealthPotion,
		whereas a Potion with 0 healing power and 100 attack power is an AttackPotion.
	*	a duck with ability to swim and no skills is a normal duck,
		whereas a duck with ability to fly and a skill called fireball is an ultimate elite flame duck.
2.	Instead of HOW an object does something, one should always be intersted in only WHAT an object does.
	*	a lesser health Potion can heal 10 HP and a great health potion can heal 100 HP,
		but we only care about the fact that Potion can heal.
	*	a duck can swim slowly and a swift duck can swim really fast,
		but we only want to know that a duck can swim.
3.	Components can be reused across several Entities and Tasks can be reused across several Components.

And...composition is basically a form of delegation.
In TECT we could achieve composition by simply delegating the implementations of properties/behaviours/functionalities from the root down to the nodes and leaves.
Here is the analog to a tree structure:
*	Entities are roots that delegate their jobs to other nodes.
*	Components can be nodes that delegate jobs further down to other nodes.
*	Components can be leaves that handle other Entities.
*	Tasks are leaves that implement the jobs.
*	If the delegation tree has gone too deep (more than 4 layers), 
	it's time to introduce new Entities to flatten our structures.

In order to achieve such delegation, we would have a heavy use of Abstraction: interface and inheritance.
Unity by nature delegate all jobs from GameObject Entities to their MonoBehaviour Components.
Because all scripts attached to GameObject must inherit from MonoBehaviour
so that implementations of functionalities like Update and OnCollisionEnter are always exposed to GameObject Entities (and Unity Engine).
This also potentially implies that every MonoBehaviour is a Component with all Unity Engine functionalities,
and they are not suitable for Dynamic Tasks.

But we do not have to go that deep in order to mimic Unity's MonoBehaviour for C# Entities.
Since C# allows a class to implement multiple interfaces,
we could simply map one interface to one or more functionalities,
and delegate these functionalities/interfaces to Components that contain Tasks implementing those functionalities/interfaces.
In addition, we could delegate a single functionality to multiple Components if necessary,
but this need to be desinged carefully as it may lead to unexpected results.

By the way, a C# Entity is also known as parasitic Entity because it must live inside a MonoBehaviour Component of another GameObject Entity.
As I've said in previous section, in order to interact with the game world we must connnect C# Entities with Unity Engine
so that C# Entities will get processed, and the most common connectors are MonoBehaviours.
It is worth it to mention that once a GameObject Entity decides to deal with such parasitic Entities,
the GameObject Entity must expose new functionalities(Interfaces) to adapt the parasitic Entities.
I.e. the GameObject Entity creates new Components (encapsulated by Interfaces/Unity Engine) to handle those functionalities of those parasitic Entities.

Double Dynamic happens when a Dynamic Task is further split into other Dynamic Tasks,
which causes the former Dynamic Task losing its purposes.
This problem is often caused by poor design that makes properties/behaviours/functionalities unclear at first place.
There are two solutions for it:
1.	Delegate some functionalities to the former Dynamic Task and make it a Component.
2.	Flatten the tree structure by delete the former dynamic Task, and move its work to the latter Dynamic Tasks.

<a id="interface_segregation"></a>
## Interface Segregation

The whole point of Interfaces is to make clients only dependent on the parts(functioanlities) they are interested in.

But it's never easy to split functionalities into properly sized Interfaces to follow Single Responsibility Principle(SRP) and Interface Segregation Principle(ISP),
especially when Components should be built on Interfaces.
It would be too late to split functionalities AFTER we know what clients actually want.
So we have to make good plans to predict clients' interests:
1.	Create all Entities conceptually and list their properties/behaviours/functionalities before we create any Component/Task.
2.	Figure out all potential interactions between all Entities.
3.	Create one Interface for each kind of interaction.
4.	Map one or more closely-related Interfaces to each Component.
5.	For any missed requirements from clients, use Interface inheritance to compose a new one or split into new ones.

For C# Entities, we would create comprehensive Interfaces composed of smaller Interfaces that represent all functionalities they hold.
(Although we shouldn't do so for Components).
The purpose behind the comprehensive Interfaces is to mimic the reflection/Service Locator Pattern 
that GetComponent<Interface> or similar Unity methods have been utilizing.
For example,
*	StatusManager is a Component that stores and manages all Statuses on its parent character.
*	Its jobs include processing Statuses as time passes, sending descriptions of Statuses to UI elements and so on.
*	It has two options to store Statues: concrete classes Status or an Interface IStatus composed of all its functionalities.
*	At this moment, the StatusManager is exactly the client that needs to know ALL functionalities of the Status class.
*	When other clients need partial functionalities of the Status class, the StatusManager upcasts IStatus to something like IDescribable and return to them.

## Dependency Resolve

To make sure that every object is self-contained,
we usually need to provide them with their required dependencies.

For C# Entities and C# Components,
dependencies can be resolved by manual injection like Factories and constructors,
or automatic resolving like Zenject.

For GameObejct Entities and MonoBehaviour Components,
Unity provides user two ways to inject dependencies:
1.	Service Locator Pattern
	*	Asking for dependencies through Unity methods like GetComponent<>.
	*	This needs to be done carefully since most services are not cheap and have limited scopes.
2.	Editor Operations
	*	setting up prefabs/GameObjects in inspector.
	*	setting up ScriptableObject assets in inspector.
	*	dragging and dropping serialized fields.
	*	and other editor operations.

<a id="refactor_extension"></a>
## Refactor and Extension

Before starting this topic, 
there are three concepts that are helpful for understanding: Underlying Changes, Breaking Changes and Polymorphic Open Closed Principle(OCP).

Underlying Changes refer to any non-functional modifications to implementations without changing external behaviours.
I.e. changes done to an object will never lead to changes to any other ojects.

On the other hand, Breaking Changes are modifications to external behaviours that lead to "broken" systems and further changes to other objects.
They can be easily triggered by:
*	changes to public parts of an object.
*	changes to Enum.
*	changes to Interface.
*	changes to Struct.
*	changes to Templates.
*	changes to Creational behaviours(like constructors and factories)

Since the tranditioncal inheritance-way OCP introduced by Meyer brings tight coupling 
and requires programmers to have a good insight into futures,
OCP has been redefined to this "strategic" and "Polymorphic" version.
Polymorphic OCP basically agree that objects should be open to Underlying Changes and closed to Breaking Changes.

Now, it's time to talk about refactor and extension.
As I said in previous sections, all features in TECT are aiming to make it easy
to improve the codes during development(refactor) and modify/add/delete contents from our projects during maintenance(extension).
So here are some rules about refactor and extension:

1.	Refactor codes once code smells like duplicate codes are deTECTted.
2.	Always rewrite plans before rewriting codes due to any requirement changes.
3.	Utilize Dynamic Tasks as much as possible to adapt future changes.
4.	For breaking changes to existing systems:
	*	Find all related dependencies and predict potential influences on them.
	*	Introduce new codes, deprecate old codes and migrate to new codes.
		Factory combined with Interface makes this progress much easier.
	*	Comprehensive plans made before this project started should reduce costs here.
	*	Be REALLY REALLY REALLY careful.

Games are really really prone to changes by nature 
because there always comes a time when we need to adjust the exsiting contents or add new contents,
especially for multiplayer/online/mobile games.
Indeed, we cannot predict what our clients need in the future,
sometimes we do not even know what our clients really want NOW.

When people say that we should have a good insight into the future,
they actually mean that we make our codes extensible enough in order to ACCEPT future changes.
But I would like to interpret "good insight" as abilities to write a so comprehensive plan for projects
so that we could REDUCE what we need to update in the futures.
I.e. changes are always hard to deal with no matter how great your design is,
so instead we should have a great and comprehensive starting point to avoid costly changes in the futures.

## Drawbacks of TECT

1.	Deep Abstraction hierarchy
	*	Composition brings a tree hierarchy for functionalities.
	*	Dynamic Tasks break things into even smaller pieces.
	*	We need a clear diagram representing the hierarchy to avoid confusion.
2.	Worse performance than Data Oriented Design with ECS
	*	Indirection causes method calls extra costs.
	*	Comprehensive individuals need extra memory to store dependencies.
	*	This is basically a trade-off between Abstraction and performance in C#.
3.	Templates-based existance processing
	*	This is a trade-off between genericity and simplicity.
	*	Most time Components are bound to certain Templates.
	*	We need to handle data existance by checking array size, zero values and somtimes booleans.
	*	This requires extra carefulness when initializing objects based on Templates.

## Singleton by Zenject! (suspect)

Singleton pattern is powerful and necessary in game development,
but how it is implemented is still controversial in Unity,
because:
*	a singleton must be unique.
*	a singleton may be passed to any arbitrary object(and we don't even know who needs it).
*	a singleton may intantiate other singletons(just like a bootstrap).

This is where Zenject kicks in, and solves all problems.
Zenject is a Dependency Inject plugin in Unity,
and the most use of Zenject in TECT is to achieve Singleton pattern.

But, what does it do with Singleton Pattern?
*	Zenject is just a root layer singleton.
*	Zenject keep track of singletons and make sure they are unique.
*	Any other singletons are registered to Zenject.
*	When on demand, Zenject instantiates required singletons.
*	When on demand, Zenject injects those singletons into any object that is asking for the references.

## Odin Inspector

Odin Inspector is crucial if we want Interfaces to be serialized in Unity.
Otherwise we should forget about drag and drop, use GetComponent<> or similar Unity methods to retrieve what we want.

Be careful and follow the rules when using Odin Inspector:
*	Don't add "readonly" to serialize field.
*	Don't overuse it as it is not fast as Unity Serialization.

## Event Aggregator

An Event Aggregator is a Singleton that
*	"acts as a single source of events"(<a href="https://martinfowler.com/eaaDev/EventAggregator.html">Source</a>).
*	aggregates and listens on Subjects' events.
*	forwards these events to Observers that have registered for the events.
*	adds one level of indirection to event handling.
*	decreases coupling further than Delegates.
*	reduces complexity further than Observer Pattern.

In TECT, Event Aggregator = Zenject(Singleton) + Delegate + Interface + Observer Pattern.

## Name Convention

Prefix:
*	"MB" means it derives from MonoBehaviour.
*	"SO" means it derives from Scriptable Object.
*	"E" means it is an enum.
*	"I" means it is an Interface.
*	"S" means it is a primitive data Struct.
*	"F" means it is a Factory.
*	Otherwise it derives from System.Object.

Suffix:
*	"Tpl" means it is a Template.
*	"Mgr" means it is a manager.

## Glossary

*	Subject: a source object that fires an event.
*	Observer: an object that listens on an event that a Subject fires.
*	C# class: a class that does not inherits from Unity Engine classes.
*	Unity Engine class: a class that inherits from Unity Engine classes.

## Thanks to TECThniques

*	Component-based Architecture
*	Entity Component System(ECS)
*	Data Driven Programming
*	Swarm Robotics
*	Interface-based Programming
*	Composition over Inheritance
*	Object Pooling
*	Design Patterns like Strategy, Factory and so on.
*	Deprecated Entity Template Container Behaviour(ETCB)
*	You Ain't Gonna Need It(YAGNI)
