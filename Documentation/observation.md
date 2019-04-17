<a id="observation"></a>

## Serialization and Initialization

Observation:
*	Reset in Unity Mode will try to call parameterless constructor and field initializer of the target.
	If no such constructor, set every serialized field back to default.
*	1.	For serializable C# classes with parameterless constructor, 
		both parameterless constructor and field initializer will be called during serialization.
	2.	For serializable C# classes without parameterless constructor,
		neither parameterless constructor nor field initializer will be called during serialization.
		Everything is left default value.
	3.	The existence of Field initializer relies on the existence of parameterless constructor during Unity serialization.
*	1.	For Scriptable Object, OnEnable will be executed multiple times.
		OnEnable will potentially overwrite data, since it is called after deserialzation.
		So, it is not suitable for initialization.
	2.	For Scriptable Object, Awake will be executed only once when the SO is created.
		Upon Reset in Editor Mode, everything is returned back to uninitialized(default) state.
		When Adding new seriliazed fields to old SOs, the SOs that have been created won't do the initialization for new fields.
		SO, it is not suitable for initialization, and it should be renamed to OnCreate.
	3.	Field initializer is not suitable since it relies on parameterless constructors.
	3.	For Scriptable Object, parameterless constructors will be executed 
		when new instance created, Reset in Editor Mode and adding new fields.
		So, this is the best place for initialization in Scriptable Object.


Conlcusion:
*	Initialize everything in Awake/Start for MonoBehaviour.
*	Initialize everything in the parameterless constructor for Serializable C# classes.
*	Initialize everything in the parameterless constructor for Scriptable Objects.
*	Initialize static fields in field initializers.

## List vs Hashset: Add and Remove

Observation:
*	For small size, List.Add is MUCH better than Hashset.
*	For small size, List.Remove is worse than Hashset but acceptable.
*	For large size, Hashset is MUCH better than List with respect to Add and Remove.

Cause:
*	Add and Remove in Hashset are in constant time.
*	Add and Remove in List are in linear time.

Conclusion:
*	List is used when interating through elements.
*	Hashset is used for loop-up.

## Direct Access vs Array vs Dictionary

Observation:
*	Array index is always better than Dictionary look-up.
*	Array index with enum casted to int is worse than direct access but pretty close.

Cause:
*	Dictionary needs to calculate hash, Array doesn't.

Conclusion:
*	For a collection of enum-object pair with fixed length, Array is a good choice.
*	Don't put value-type as keys in Dictionary to avoid boxing.

## String field vs String literal

Observation:
*	They have the same performance.

Cause:
*	String literal is stored internally(not proved yet).

Conclusion:
*	I prefer String field.

## Foreach vs For loop

Observation:
*	Cannot modify the collection during Foreach, but can do so in For loop.
*	Foreach and For loop have almost the same performance.

Cause:
*	IEnumerator vs Item[int32] Access

Conclusion:
*	I prefer Foreach as it ensures the collection is not modifiable.
