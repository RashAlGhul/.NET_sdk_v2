BaseCache.Get&lt;T> Method (String, Boolean)
============================================
Return a cached value based on the key

**Namespace:** [GSMA.MobileConnect.Cache][1]  
**Assembly:** GSMA.MobileConnect (in GSMA.MobileConnect.dll)

Syntax
------

```csharp
public Task<T> Get<T>(
	string key,
	bool removeIfExpired = true
)
where T : ICacheable

```

#### Parameters

##### *key*
Type: [System.String][2]  
Key (Required)

##### *removeIfExpired* (Optional)
Type: [System.Boolean][3]  
 If value should be removed if it is retrieved and found to be expired, should be set to false if a fallback value is required for if the next call for the required resource fails.

#### Type Parameters

##### *T*
Type of value to be returned by cache

#### Return Value
Type: [Task][4]&lt;**T**>  
The cached value if preset, null otherwise
#### Implements
[ICache.Get&lt;T>(String, Boolean)][5]  


See Also
--------

#### Reference
[BaseCache Class][6]  
[GSMA.MobileConnect.Cache Namespace][1]  

[1]: ../README.md
[2]: http://msdn.microsoft.com/en-us/library/s1wwdcbf
[3]: http://msdn.microsoft.com/en-us/library/a28wyd50
[4]: http://msdn.microsoft.com/en-us/library/dd321424
[5]: ../ICache/Get__1.md
[6]: README.md
[7]: ../../_icons/Help.png