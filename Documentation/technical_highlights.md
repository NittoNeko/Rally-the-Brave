<a id="technical_highlights"></a>
## Prologue

The basic design pattern behind this project is inspired by 
Entity Component System(ECS), Interface-based Programming and Component-based Architecture.

I spent several months on learning from those techniques, 
taking their benefits into my design, 
and experimenting with my personal project, Rally the Brave,
and finally discovered an architecture called Template Interface Entity(TIE).

## What is the truth(history) behind TIE?

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

So, I came back to my initial coding style along with some souvenirs from each techniques.
Hopefully, this architecutre called Template Interface Entity(TIE) would be my final habitat.

## Template Interface Entity(TIE)

TIE is a hybrid architecutre specialized in game development, 
which classifies all game things in to three types: Template, Interface and Entity.

The basic idea is:

*	A Entity may include predefined Templates that describe a Game Object.
*	A Game Object is comprised of Entites like Character, Iventory and so on.
*	Entities are updated as time passes.
*	Players interact with the game world by invoking Interfaces like Take Damage.
*	Entites may invoke other Entites through Interfaces.
*	Players perceive changes of Entities through graphic, UI elements and so on.

## Template

Templates refer to shared data structures with unique instances/assets
that are predefined in editor and read-only during life-time.

Templates are typically classified as:
*	Scriptable Objects and their assets/instances.
*	Serializable C# classes that serve as parts of Scriptable Object.
*	External static data like database.

## Interface

Interfaces in TIE act as communication bridges between objects that need to talk with each other.
*	When passing parameters, Interfaces should be passed instead of concrete objects.
	However, exceptions include:
	*	stable classes like .net API and Unity Engine API that are considered reliable.
	*	immutable data structs that are similar to primitive types.
*	The public parts of classes should always be encapsulated by Interfaces.
	However, exceptions include:
	*	private classes that are used locally.
	*	utility classes that are stable and closed to modification.

For example:
*	when A depends on B's Behaviours it should rely on only WHAT B does(Interface) instead of HOW B does something(specific implementation).
*	when A provides some services for others, it extracts those services and put them into an Interface.

## Entity

Entities are the basic components that build up the game world.
They are independent and autonomous objects that contain data and behaviours.

They are conceptually classified into three types:

1.	MonoBehaviours attached to Game Objects that interact directly with game worlds through Unity Engine functions.
2.	C# classes that would finally interact with game worlds indirectly through other MonoBehaviour Entities.

## How to work with TIE?

1.	Specify what kind of Templates are needed in order to define different aspects of Game Objects.
2.	Specify what kind of functionalities are needed in order to make the game interactive.
3.	Specify what kind of Entities are needed in order to achieve these functionalities.
4.	Create Interfaces to encapsulate Entities.
5.	Design, Debug and Release.
6.	Add new features(See <a href="#new-content">details</a>).

## Takeaway from different techniques

*	Component-based Architecture
	*	A Game Object is just like a container for Components.
	*	Entities are replaceable Components for Game Objects.
*	Entity Component System(ECS)
	* 	A large number(thousands) of Game Objects should be handled by a manager.
*	Swarm Robotics
	*	Objects are generally handled in a decentralised manner.
	*	Objects accomplish their tasks individually.
*	Interface-based Programming
	*	Use Interface to decouple classes.
	*	Multiple Interfaces.

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

## What type an Entity should be?

*	If a game thing needs to be actually placed in game world, be visible and interact physically with others,
	then the game thing should be a Game Object and of course all its Entities should be MonoBehaviours.
*	Anything else should be a C# class.

## Why not Game Objects all the time?

*	Instantiating and Destroying GameObjects and Scritable Objects are expensive.
*	Even empty GameObjects could incur costs if users are not careful.
*	Unity Engine functions like Update are so slow that if you are handling thousands of Game Objects,
	you should let one manager Game Object handle all others together.

<a id="new-content"></a>
## Composition vs Inheritance

Composition is the main work flow that Unity suggests,
but that does not mean we should abandon Inheritance,
because they are serving for different purposes.

Choose Composition when:
*	Templates are going to be defined.
	*	A Bleeding is a Status Template has a description and a health modifier.
	*	A Holy is a Status Template has a description and a attribute modifier.
*	Game Objects are going to be defined.
	*	A duck is a Game Object that has a duck image and swim ability.
	*	A cat is a Game Object that has a cat image and jump ability.
*	a new feature that is NOT closely related to existing contents is added.
	*	A flame duck a Game Object that can swim and shoot fire balls.
	*	Shooting fire balls are not related to swimming, so it can be added as a component directly.

Choose Inheritance when:
*	existing behaviours are going to be modified.
	*	A duck has a swim ability that swims forward.
	*	A coward duck inherits from duck that has a swim ability that swims backward.
*	a new feature that IS closely related to existing contents is added.
	*	A brave duck inherits from duck that dashes only forward when swimming.
	*	This new feature is HARD to achieve by simply add a new component.
	*	Creating a similar Component to replace the old one would cause duplicate codes.

In conclusion,
*	A Game Object is COMPOSED of Entities.
*	Entity vairants can INHERIT from existing Entities.

## Zenject(DI, IoC and Singleton)!

Singleton pattern is powerful and necessary in game development,
but how it is implemented is still controversial in Unity,
because:
*	a singleton must be unique.
*	a singleton may be passed to any arbitrary object(and we don't even know who needs it).
*	a singleton may intantiate other singletons(just like a bootstrap).

This is where Zenject kicks in, and solves all problems.
Zenject is a Dependency Inject plugin in Unity,
and the most use of Zenject in TIE is to achieve Singleton pattern.

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

In TIE, Event Aggregator = Zenject(Singleton) + Delegate + Interface + Observer Pattern.

## Delegate

*	Generally delegate should be declared in Interfaces only.
*	Delegate should not pass concrete class parameters.

## Name Convention

Prefix:
*	"MB" means it derives from MonoBehaviour.
*	"SO" means it derives from Scriptable Object.
*	"E" means it is an enum.
*	"I" means it is an Interface.
*	"S" means it is a data Struct.
*	Otherwise it derives from System.Object.

Suffix:
*	"Tpl" means it is a Template.
*	"Mgr" means it is a manager.



## Glossary

*	Subject: a source object that fires an event.
*	Observer: an object that listens on an event that a Subject fires.
*	C# class: a class that does not inherits from Unity Engine classes.
*	Unity Engine class: a class that inherits from Unity Engine classes.
