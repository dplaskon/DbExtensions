Extensions.CreateCommand Method (DbCommandBuilder, DbConnection, String, Object[])
==================================================================================
Creates and returns a [DbCommand][1] object using the provided *commandText* as a composite format string (as used on [Format(String, Object[])][2]), where the format items are replaced with appropiate parameter names, and the objects in the *parameters* array are added to the command's [Parameters][3] collection.

**Namespace:** [DbExtensions][4]  
**Assembly:** DbExtensions (in DbExtensions.dll)

Syntax
------

```csharp
public static DbCommand CreateCommand(
	this DbCommandBuilder commandBuilder,
	DbConnection connection,
	string commandText,
	params Object[] parameters
)
```

#### Parameters

##### *commandBuilder*
Type: [System.Data.Common.DbCommandBuilder][5]  
The command builder used to create the parameter names.

##### *connection*
Type: [System.Data.Common.DbConnection][6]  
The connection used to create the command.

##### *commandText*
Type: [System.String][7]  
The command text.

##### *parameters*
Type: [System.Object][8][]  
 The array of parameters to be passed to the command. Note the following behavior: If the number of objects in the array is less than the highest number identified in the command string, an exception is thrown. If the array contains objects that are not referenced in the command string, no exception is thrown. If a parameter is null, it is converted to DBNull.Value.

#### Return Value
Type: [DbCommand][1]  
 A new [DbCommand][1] object whose [CommandText][9] property is initialized with the *commandText* parameter, and whose [Parameters][3] property is initialized with the values from the *parameters* parameter. 
#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type [DbCommandBuilder][5]. When you use instance method syntax to call this method, omit the first parameter. For more information, see [Extension Methods (Visual Basic)][10] or [Extension Methods (C# Programming Guide)][11].

See Also
--------

#### Reference
[Extensions Class][12]  
[DbExtensions Namespace][4]  

[1]: http://msdn.microsoft.com/en-us/library/852d01k6
[2]: http://msdn.microsoft.com/en-us/library/b1csw23d
[3]: http://msdn.microsoft.com/en-us/library/9czdkzd1
[4]: ../README.md
[5]: http://msdn.microsoft.com/en-us/library/1b38ttdd
[6]: http://msdn.microsoft.com/en-us/library/c790zwhc
[7]: http://msdn.microsoft.com/en-us/library/s1wwdcbf
[8]: http://msdn.microsoft.com/en-us/library/e5kfa45b
[9]: http://msdn.microsoft.com/en-us/library/9d2hk99t
[10]: http://msdn.microsoft.com/en-us/library/bb384936.aspx
[11]: http://msdn.microsoft.com/en-us/library/bb383977.aspx
[12]: README.md