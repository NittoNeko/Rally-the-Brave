<a id="technical_issues"></a>

## Remove elements from a collection while iterating through it

Situation:
*	A and B are elements from a collection C.
*	While iterating through C to update A and B, the operations of A sometimes remove B from C, which wont't be allowed.

Cause:
*	Calling non-local methods in a loop leads to unpredictable and complex flow.
*	We cannot guarantee that the collection won't be modified during the loop.

Accepted Solution:
*	Use a flag like IsDeleted to mark elements.
*	If the flag of an element is true, skip this element.

Potential Solution:
1.	*	Iterate the collection in reverse.
	*	This works only if you delete CURRENT element.
2.	*	Set the reference to the element in the collection to null.
	*	Check null for every access of elements.
	*	Similar to accepted solution, but much prone to error.
	*	NullReferenceException is likely to be thrown if not properly handled.
3.	*	Iterate the collection first, 
		extract all operations that may modify the collection,
		and execute them after loop.
	*	Not all operations can be taken out.

Conclusion: 
*	flags are much safer than direct modifications.

## Deal with events that happen in the same frame

Situation:
*	A and B are events happening at the same frame.
*	When A takes effects, B should be stopped.
*	Let A go first and skip B?
*	Or let B go first and then A?

Cause:
*	Games are calculated frame by frame, so events may happen at the same frame.
*	All events in the same frame are of equal priority.

Accpeted Solution:
*	The ordering doesn't matter at all, the right logic matters.
*	Make sure the logic is right in whichever ordering.
*	Design necessary mechanisms to avoid unwanted results,
	instead of trying to control the complex flow,
	because we do not have FULL control over script execution order in Unity.

Potential Solution:
1.	*	Always execute A first, and skip B.
	*	Insert checks to skip invalid events.
	*	Not guaranteed due to the complex flow.
2.	*	Always execute B first, and then A.
	*	Require complex code structures.
	*	Again not guaranteed due to the complex flow.

Conclusion: 
*	Focus on the right logic, instead of the right ordering.

## Deal with Serialization

Situation:
*	"readonly" modifier not serializable.
*	Interface not serializable.
*	Initialize

Cause:
*	Built-in Unity Serialization is fast but supports no high level functionality.
*	Interfaces are neccessary in ECBS.
*	Comprehensive Scriptable Objects are neccessary in ECBS.

Accepted Solution:
*	Use the plug-in Odin Inspector.

Conclusion:
*	Trade-off between performance and flexibility.
