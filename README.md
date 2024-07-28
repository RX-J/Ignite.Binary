# The Ignite.Binary class extends the type byte[] and all primitive data types, whereby these can simply be converted into a byte[] and a byte[] into one of the primitive data types.

**The following data types can be used:**
- ```byte```
- ```ushort```
- ```uint```
- ```ulong```
---
- ```sbyte```
- ```short```
- ```int```
- ```long```
---
- ```nint```
- ```nuint```
---
- ```float```
- ```double```
- ```decimal```
---
- ```bool```
- ```char```
- ```string```
---

**To call the ```Bytes``` function, you can use:**

```cs
var bytes_int = Ignite.Binary.Bytes (100); // bytes_int now contains the bytes of the int value "100", which looks like this: 100-0-0-0
var bytes_string = Ignite.Binary.Bytes ("Hello World!"); // bytes_string now contains the bytes of the int value "100", which looks like this: 72-101-108-108-111-32-87-111-114-108-100-33
```

**To call the ```Convert``` class, you can use:**

```cs
var bytes_int = Ignite.Binary.Bytes (100);
var bytes_string = Ignite.Binary.Bytes ("Hello World!");

var value_int = Ignite.Binary.Convert<int> (bytes_int); // this will convert the byte array to an int
var value_string = Ignite.Binary.Convert<string> (bytes_string); // this will convert the byte array to an string
```

**To use the short form of the ```Bytes``` and ```Convert``` function you can write:**

```cs
using Ignite.Binary;

var bytes_int = 100.Bytes ();
var bytes_string = "Hello World!".Bytes (); 

var value_int = bytes_int.Convert<int> ();
var value_string = bytes_string.Convert<string> ();
```

**The format for ```Bytes``` is:**

```cs
Ignite.Binary.Bytes (VALUE);

// or

using Ignite.Binary;

VALUE.Bytes ();
```

**The format for ```Convert``` is:**

```cs
Ignite.Binary.Convert<TYPE> (BYTE_ARRAY);

// or

using Ignite.Binary;

BYTE_ARRAY.Convert<TYPE> ();
```