#include <QDebug>
#include <QVector>
#include "normoper.hpp"

namespace NormOper 
{
    void tests() 
    {
        QVector<double> s1, s2, s3, s4, s5, s6;
        bool ok;
        const double TEST_EPS = std::pow(10, -6);
        
        qInfo() << "Test 1: increase a value without proportionately decreasing another";
        
        s1 = {5,6,4.6,6.3,5,4.3,5.2};
        s2 = {5,5.2,4.6,6.3,5,4.3,5.2};
        s3 = {}; s4 = {}; s5 = {};
        ok = 
                ! double_equals(mean(s1), mean(s2), EPS) && 
                ! double_equals(stddev(s1), stddev(s2), EPS) && 
                ! double_equals(normoper(s1).result, normoper(s2).result, EPS);
        
        qInfo() << "    " << (ok ? "OK" : "NOT OK");
        
        qInfo() << "Test 2: non-symmetric exchange of values";
        
        s1 = {5,6,4.6,6.3,5,4.3,5.2};
        s2 = {5,5.2,5.4,6.3,5,4.3,5.2};
        s3 = {5,6,6.3,4.6,5,4.3,5.2};
        s4 = {}; s5 = {};
        ok = 
                double_equals(mean(s1), mean(s2), EPS) && 
                    double_equals(mean(s2), mean(s3), EPS) && 
                ! double_equals(stddev(s1), stddev(s2), EPS) && 
                    double_equals(stddev(s1), stddev(s3), EPS) && 
                ! double_equals(normoper(s1).result, normoper(s2).result, EPS) && 
                    ! double_equals(normoper(s1).result, normoper(s3).result, EPS);
        
        qDebug() << "        mean(s1) and mean(s2) difference is " << mean(s1) - mean(s2) << ".";
        qDebug() << "        mean(s2) and mean(s3) difference is " << mean(s2) - mean(s3) << ".";
        qDebug() << "        stddev(s1) and stddev(s3) difference is " << stddev(s1) - stddev(s3) << ".";
        
        qInfo() << "    " << (ok ? "OK" : "NOT OK");
        
        qInfo() << "Test 3: non-multiple of ten mutual exchange of values";
        
        s1 = {5,63,4.6,6.3,5,4.3,5.2};
        s2 = {5,63,6.3,4.6,5,4.3,5.2};
        s3 = {5,6.3,4.6,63,5,4.3,5.2};
        s4 = {}; s5 = {};
        ok = 
                double_equals(mean(s1), mean(s2), EPS) && 
                    double_equals(mean(s2), mean(s3), EPS) && 
                double_equals(stddev(s1), stddev(s2), EPS) && 
                    double_equals(stddev(s2), stddev(s3), EPS) && 
                ! double_equals(normoper(s1).result, normoper(s2).result, EPS) && 
                    double_equals(normoper(s1).result, normoper(s3).result, EPS);
        
        qDebug() << "        mean(s1) and mean(s2) difference is " << mean(s1) - mean(s2) << ".";
        qDebug() << "        mean(s2) and mean(s3) difference is " << mean(s1) - mean(s2) << ".";
        qDebug() << "        stddev(s1) and stddev(s2) difference is " << stddev(s1) - stddev(s2) << ".";
        qDebug() << "        stddev(s2) and stddev(s3) difference is " << stddev(s2) - stddev(s3) << ".";
        qDebug() << "        calculate(s1) and calculate(s3) difference is " << normoper(s1).result - normoper(s3).result << ".";
        
        qInfo() << "    " << (ok ? "OK" : "NOT OK");
        
        qInfo() << "Test 4: asymmetric changes, including orders of 10";
        
        s1 = {5,63,4.6,6.2,5,4.3,5.2};
        s2 = {5,63,4.6,62,5,4.3,5.2};
        s3 = {5,6.3,46,62,5,4.3,5.2};
        s4 = {5,63,4.6,6.3,5,4.3,5.2};
        s5 = {50,6.3,46,0.63,500,0.043,.52};
        ok = 
                ! double_equals(mean(s1), mean(s2), EPS) && 
                    ! double_equals(mean(s2), mean(s3), EPS) && 
                    ! double_equals(mean(s4), mean(s5), EPS) && 
                ! double_equals(stddev(s1), stddev(s2), EPS) && 
                    ! double_equals(stddev(s2), stddev(s3), EPS) && 
                    ! double_equals(stddev(s4), stddev(s5), EPS) && 
                ! double_equals(normoper(s1).result, normoper(s2).result, EPS) && 
                    ! double_equals(normoper(s2).result, normoper(s3).result, EPS) && 
                    ! double_equals(normoper(s4).result, normoper(s5).result, EPS);
        
        qInfo() << "    " << (ok ? "OK" : "NOT OK");
        
        qInfo() << "Test 5: changes in sign";
        
        s1 = {5,6,4.6,6.3,5,4.3,5.2};
        s2 = {5,-6,4.6,6.3,5,4.3,5.2};
        s3 = {}; s4 = {}; s5 = {};
        
        ok = 
                ! double_equals(mean(s1), mean(s2), EPS) && 
                ! double_equals(stddev(s1), stddev(s2), EPS) && 
                ! double_equals(normoper(s1).result, normoper(s2).result, EPS);
        
        qInfo() << "    " << (ok ? "OK" : "NOT OK");
        
        qInfo() << "Test 6: symmetric orders of 10 exchanges between mutual multiple of ten elements";
        
        s1 = {5,62,4.6,6.2,5,4.3,5.2};
        s2 = {5,6.2,4.6,62,5,4.3,5.2};
        s3 = {}; s4 = {}; s5 = {};
        ok = 
                double_equals(mean(s1), mean(s2), EPS) && 
                double_equals(stddev(s1), stddev(s2), EPS) && 
                double_equals(normoper(s1).result, normoper(s2).result, EPS);
        
        qDebug() << "        mean(s1) and mean(s2) difference is " << mean(s1) - mean(s2) << ".";
        qDebug() << "        stddev(s1) and stddev(s2) difference is " << stddev(s1) - stddev(s2) << ".";
        qDebug() << "        calculate(s1) and calculate(s2) difference is " << normoper(s1).result - normoper(s2).result << ".";
        
        qInfo() << "    " << (ok ? "OK" : "NOT OK");
        
        qInfo() << "Test 7: sample series";
        
        s1 = {1429041290,1429041350,1429041410,1429041470,1429041530,1429041590,1429041650,1429041710,1429041770,1429041830};
        QVector<quint32> s1b = {1429041290,1429041350,1429041410,1429041470,1429041530,1429041590,1429041650,1429041710,1429041770,1429041830};
        s2 = {24.43,24.35,24.32,24.31,24.36,24.44,26.87,27.68,28.25,28.57};
        s3 = {4054,0,237,2009,4001,4019,6368,10670,6340,1816};
        s4 = {226,0,21,156,205,240,446,519,400,127};
        s5 = {145,0,5,38,114,90,166,312,222,48};
        s6 = {0.000000101467,0.,0.0833333,0.633333,1.9,1.5,2.76667,5.2,3.7,0.8};
        QVector<double> s7 = {0.0,0.0,0.0};
        ok = 
                double_equals(normoper(s1).result, 0.379659, TEST_EPS) && 
                double_equals(normoper(s1b).result, 0.379659, TEST_EPS) && 
                double_equals(normoper(s2).result, 0.491966, TEST_EPS) && 
                double_equals(normoper(s3).result, 0.600186, TEST_EPS) && 
                double_equals(normoper(s4).result, 0.506223, TEST_EPS) && 
                double_equals(normoper(s5).result, 0.549888, TEST_EPS) && 
                double_equals(normoper(s6).result, 0.614593, TEST_EPS) && 
                double_equals(normoper(s7).result, 0.0, TEST_EPS);
        
        qDebug() << "        calculate(s1) and result difference is " << normoper(s1).result - 0.379659 << ".";
        qDebug() << "        calculate(s1b) and result difference is " << normoper(s1b).result - 0.379659 << ".";
        qDebug() << "        calculate(s2) and result difference is " << normoper(s2).result - 0.491966 << ".";
        qDebug() << "        calculate(s3) and result difference is " << normoper(s3).result - 0.600186 << ".";
        qDebug() << "        calculate(s4) and result difference is " << normoper(s4).result - 0.506223 << ".";
        qDebug() << "        calculate(s5) and result difference is " << normoper(s5).result - 0.549888 << ".";
        qDebug() << "        calculate(s6) and result difference is " << normoper(s6).result - 0.614593 << ".";
        
        qInfo() << "    " << (ok ? "OK" : "NOT OK");
        
        qInfo() << "Test 8: begin with zero";
        
        s1 = {0,1429041290,1429041350,1429041410,1429041470,1429041530};
        s2 = {1429041290,1429041350,1429041410,1429041470,1429041530};;
        s3 = {0,0,0,24.43,24.35,24.32,24.31,24.36,24.44,26.87,27.68,28.25,28.57};
        s4 = {24.43,24.35,24.32,24.31,24.36,24.44,26.87,27.68,28.25,28.57};
        s5 = {}; s6 = {};
        ok = 
                ! double_equals(normoper(s1).result, 0.0, EPS) && 
                ! double_equals(normoper(s3).result, 0.0, EPS) && 
                ! double_equals(normoper(s1).result, normoper(s2).result, EPS) && 
                ! double_equals(normoper(s3).result, normoper(s4).result, EPS);
        
        qInfo() << "    " << (ok ? "OK" : "NOT OK");
    }
}
