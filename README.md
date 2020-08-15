*Psobo.NoperHash* is a float list and string **hashing** algorithm. Its main characteristic is a low, possibly *very* low collision rate.

The algorithm aims to be a practically-zero collision algorithm. Performance is secondary; currently, no optimization effort has gone into its implementation, but performance as-is has been found to be acceptable for low (tens of thousands of numbers) volume of data, or for hashing single values on input.

Some anedoctal performance figures are: 466,000 words in a little over a second; or  250.000 floating point numbers in 2-and-some seconds. (These figures are approximate and language and machine-dependent.)

Accuracy has been tested on high-magnitude and low-magnitude lists, as in the following examples:
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

## Contributing

All contributions are welcome to the algorithms. Some areas of focus are:

 - Find corner cases
 - Improve performance
 - Produce language-specific packages

When contributing, please follow the code of conduct:

 - Be respectful
 - Preserve coding style

## Links

* A mathematical description of the algorithm can be found at [https://psobo.com/blog/an_exponentiation_based_float_hash.html](https://psobo.com/blog/an_exponentiation_based_float_hash.html).

* An article investigating performance and collision rate is at [https://psobo.com/blog/exponentiation_based_float_hash_2.html](https://psobo.com/blog/exponentiation_based_float_hash_2.html).

## License

MIT license.
