#include "tests.h"
#include "normoper.hpp"

int main()
{
    NormOper::tests();
    
    int a; int b;
    auto c = std::make_tuple<int, int>(0,1);
    std::toe(a, b) = c;
}
