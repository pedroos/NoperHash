*Psobo.NoperHash* is an experimental low-collision hash. The algorithm aims to be low-collision in practice, with performance as a second consideration. 

### Algorithm

Numbers are exponentiated in pairs. To be able to exponentiate indefinitely, numbers are first normalized to a 0-1 range. Then, to preserve ordering when normalized, the mean of the list is taken before hashing and prepended to the list.
Other measures to prevent corner cases and assure ordering are implemented.

### Tests

The algorithm has been run against Fnv1a, Murmur3, PRH and XX in performance and collisions tests ([source code](https://github.com/psobo/NoperHash/blob/master/CSharp/NoperHashTests/NoperHashTests.cs)) ([report](https://psobo.com/blog/exponentiation_based_float_hash_2.html)).

1.  Words test: from  [dwyl's english-words](https://github.com/dwyl/english-words), serial hashing of more than 466,000 English words to measure time;
2.  Collisions test: similar to the words test, reusing the dictionary, without measuring time, and building a list of results to check for repetitions.

The results have been:

```
# Hashing 460,000 words
Fnv1: 8ms
Murmur: 11ms
PRH: 158ms
XX: 12ms
Noper: 1317ms

# Collisions
Fnv1: 23
Murmur: 25
XX: 16
PRH: 271
Noper: 0
```

Accuracy has been tested on high-magnitude and low-magnitude lists:
 `[1429041290,1429041350,1429041410,1429041470,1429041530,1429041590,1429041650,1429041710, 1429041770,1429041830]`
 `[0.000000101467,0.0,0.0833333,0.633333,1.9,1.5,2.76667,5.2,3.7,0.8]`

### Limitations

Currently, the by-design limitations are:

#### Changes of all numbers by factors of 10
If all numbers in the list are substituted by their 10's or .1's multiplications, for example, the algorithm will yield the same value. Note: this is not an *individual value* change, but a *whole list* change.

#### Swaps of numbers which are multiples of 10 between themselves
For example, the lists 
`[5,6,4.6,6.3,46]`
and 
`[5,6,46,6.3,4.6]`
yield the same hash.

## Areas of focus

Some areas of focus for development incude:

 - Find collision cases
 - Find corner cases
 - Improve performance


## Usage

#### C#

As a float list hash:

```
using Psobo.NoperHash;

var s1 = new List<double>() { 5,6,4.6,6.3,5,4.3,5.2 };
double hash = NoperHash.Calc(s1);
Console.Write(hash);
// Output: 0.59531675915888593
```
As a string hash:

```
using Psobo.NoperHash;



string s2 = "Sample string";
var bytes = System.Text.Encoding.UTF8.GetBytes(s2);
double hash = NoperHash.Calc(bytes);
Console.Write(hash);
// Output: 0.83278353153724771
```

#### Other languages

Implementations in C++/QT, Wolfram, and R languages are present in their respective folders.

## Links

* A mathematical description of the algorithm can be found at [An exponentiation float-based hash](https://pedroos.github.io/an_exponentiation_based_float_hash.html).

## License

MIT license.
