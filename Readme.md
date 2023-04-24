NoperHash is a recreational low-collision hash.

### Algorithm

Cumulative exponentiation is performed. To be able to exponentiate indefinitely, numbers are first normalized to a 0-1 range. Then, to preserve ordering when normalized, the mean of the list is taken before hashing and prepended to the list.
Other measures implemented:

* Zeroes are substituted by the mean to prevent stabilization around zero values and zero results for leading-zero lists
* Inputs are normalized to absolutes to avoid producing complex numbers, since the mean is modified
* All-zero lists are checked and return zero

The resulting value is a double in the 0-1 range.

### Tests

The algorithm has had performance and collisions tests run.

1.  Words test: from  [dwyl's english-words](https://github.com/dwyl/english-words), serial hashing of more than 466,000 English words to measure time;
2.  Collisions test: similar to the words test, reusing the dictionary, without measuring time, and building a list of results to check for repetitions.

The results have been:

- Hashing 460,000 words: 962ms
- Collisions: 0

The actual collision figure still has to be evaluated more extensively against larger inputs.

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


## Usage

#### C#

```
using static System.Text.Encoding;
using static System.Console;

var s1 = new List<double>() { 5,6,4.6,6.3,5,4.3,5.2 };
WriteLine(NoperHash.Calc(s1));
// 0.59531675915888593

string s2 = "Sample string";
var bytes = UTF8.GetBytes(s2);
WriteLine(NoperHash.CalcStr(UTF8.GetBytes(bytes)));
// 0.83278353153724771
```