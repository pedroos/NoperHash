NoperHash is a low-collision float hash that takes positive numbers and outputs a unique number in the 0-1 range. It allows computing hashes for lists of arbitrary size.

Note: the precision of NoperHash has not been validated.

### Algorithm

Cumulative exponentiation is performed. To be able to exponentiate indefinitely, numbers are first normalized to a 0-1 range. Then, to preserve ordering when normalized, the mean of the list is taken before hashing and prepended to the list.
Other measures implemented:

* Zeroes are substituted by the mean to prevent stabilization around zero values and zero results for leading-zero lists
* Inputs are normalized to absolutes to avoid producing complex numbers, since the mean is modified
* All-zero lists are checked and return zero

The resulting value is a double in the 0-1 range.

The Mathematical base of the algorithm is described in <a href="https://pedroos.github.io/an_exponentiation_based_float_hash.html">this</a> article.

### Tests

The algorithm has had performance and collisions tests run.

1.  Words test: from  [dwyl's english-words](https://github.com/dwyl/english-words), serial hashing of more than 466,000 English words to measure time;
2.  Collisions test: similar to the words test, reusing the dictionary, without measuring time, and building a list of results to check for repetitions.

The results have been:

- Hashing 460,000 words: 962ms
- Collisions: 0

The actual collision figure still has to be evaluated more extensively against larger inputs.

#### Performance

Sample output from `Tests.ListSize()`, for the non-generic and generic versions:

```
List size: 1000000, elapsed: 93 ms
List size: 1000000, elapsed: 92 ms
List size: 10000000, elapsed: 919 ms
List size: 10000000, elapsed: 889 ms
List size: 100000000, elapsed: 9086 ms
List size: 100000000, elapsed: 9153 ms
List size: 1000000000, elapsed: 95607 ms
List size: 1000000000, elapsed: 92017 ms
```

Machine: AMD Ryzen 5 5600G processor, 3901 Mhz with 16.0 GB RAM.

Accuracy has been tested on high-magnitude and low-magnitude lists:
 `[1429041290,1429041350,1429041410,1429041470,1429041530,1429041590,1429041650,1429041710, 1429041770,1429041830]`
 `[0.000000101467,0.0,0.0833333,0.633333,1.9,1.5,2.76667,5.2,3.7,0.8]`

#### Complexity

Performance is linear to the size of the input.

### Limitations

The by-design limitations are:

#### Changes of all numbers by factors of 10
If all numbers in the list are substituted by their 10's or .1's multiplications, for example, the algorithm will yield the same value. Note: this is not an *individual value* change, but a *whole list* change.

#### Swaps of numbers which are multiples of 10 between themselves
For example, the lists 
`[ 5, 6, 4.6, 6.3, 46  ]`
and 
`[ 5, 6, 46 , 6.3, 4.6 ]`
yield the same hash.

### Building and running

To run the tests, type:

```
dotnet run
```

To build, type:

```
dotnet build
```

### Using

To get the hash of a string, use:

```
using static System.Text.Encoding;
using PedroOs.NoperHash;

double hash = NoperHash.Get(UTF8.GetBytes("my_string").ToDoubles());
```

To get the hash of a list of numbers, use:

```
using static System.Text.Encoding;
using PedroOs.NoperHash;

double hash = NoperHash.Get(new int[] { 1, 2, 3 }.ToDoubles());
```

Note only positive numbers as input are supported. To convert unsigned arrays to signed, you could use a function such as:

```
double[] Prepare(int[] arr) => arr.Select(x => (double)(x + int.MinValue)).ToArray();
```

To verify the proximity of a result to a certain value, use:

```
// False
bool approx = 0.6.Approximately(0.3, 0.3);
// True
bool approx = 0.6.Approximately(0.3, 0.4);
```
