## Test Conclusion

Numbers may not be precise, but good enough for comparisons.

*	For small size, List<> add() is MUCH better than Hashset<>.
*	For small size, List<> remove() is worse than Hashset<> but acceptable. (20% slower in experiment)
*	For large size, Hashset<> is MUCH better than List<> with respect to add() and remove().

*	Array index is always better than Dictionary loop-up.
*	Array index is close to and somtimes better than direct access.
*	Array index with enum casted to int is worse than direct access but pretty acceptable. (30% slower in experiment)

*	Strings that are frequently used should be kept in memory.

*	Use "foreach" when you only want to access an array
*	Use "for" when you want full control of the array
*	"foreach" is generally slower than "for" but pretty acceptable. (10% slower)
*	"foreach" provides MUCH faster readonly functionality than other collections.

*	Compare UnityEngine.Object with null would be MUCH slower than iterating through a single-element/empty array,
	since such operations involve unmanaged C++ memory.
*	So use GetComponents instead of GetComponent to store references to other components 
	even for a single component, when you need to compare them with null.
