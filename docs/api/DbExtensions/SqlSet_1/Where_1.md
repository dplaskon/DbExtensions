SqlSet&lt;TResult>.Where Method (String, Object[])
==================================================
Filters the set based on a predicate.

**Namespace:** [DbExtensions][1]  
**Assembly:** DbExtensions (in DbExtensions.dll)

Syntax
------

```csharp
public SqlSet<TResult> Where(
	string predicate,
	params Object[] parameters
)
```

### Parameters

#### *predicate*
Type: [System.String][2]  
A SQL expression to test each row for a condition.

#### *parameters*
Type: [System.Object][3][]  
The parameters to apply to the *predicate*.

### Return Value
Type: [SqlSet][4]&lt;[TResult][4]>  
A new [SqlSet&lt;TResult>][4] that contains elements from the current set that satisfy the condition.

See Also
--------
[SqlSet&lt;TResult> Class][4]  
[DbExtensions Namespace][1]  

[1]: ../README.md
[2]: http://msdn.microsoft.com/en-us/library/s1wwdcbf
[3]: http://msdn.microsoft.com/en-us/library/e5kfa45b
[4]: README.md