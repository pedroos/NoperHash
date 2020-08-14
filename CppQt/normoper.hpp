#ifndef NORMOPER_H
#define NORMOPER_H

#include <QVector>
#include <QDebug>

namespace NormOper
{
    const double EPS = std::pow(10, -9);
    
    inline bool double_equals(double a, double b, const double epsilon)
    {
        return std::abs(a - b) < epsilon;
    }
    
    inline double exp10 (double arg) {
        if (double_equals(arg, 0, EPS)) return 0;
        return 1 + std::floor(std::log10(std::fabs(arg)));
    }
    
    template<typename T>
    inline double mean(const QVector<T> vec) 
    {
        return std::accumulate(vec.begin(), vec.end(), 0.0) / vec.size();
    }
    
    template<typename T> 
    double stddev(const QVector<T> vec) 
    {
        double sum = std::accumulate(std::begin(vec), std::end(vec), 0.0);
        double m =  sum / vec.size();
        
        double accum = 0.0;
        std::for_each (std::begin(vec), std::end(vec), [&](const double d) {
            accum += (d - m) * (d - m);
        });
        
        double stdev = sqrt(accum / (vec.size()-1));
        return stdev;
    }
    
    struct NormOperResult 
    {
        double mean;
        double stdDev;
        double result;
    };
    
    template<typename T>
    NormOperResult normoper(const QVector<T> vec)
    {
        NormOperResult result;
        
        double mn = mean(vec);
        result.mean = mn;
        
        double sd = stddev(vec);
        result.stdDev = sd;
        
        double initialValue = mn;
        double currentValue = 0;
        
        // Calculate normalized mean
        double meanMag = mn * std::pow(10, exp10(mn) * -1);
        
        // Check that the sequence is not all zeroes
        if (double_equals(meanMag, 0.0, EPS) && double_equals(sd, 0.0, EPS)) 
        {
            result.result = 0.0;
            return result;
        }

#ifdef QT_DEBUG
        QStringList _vec;
        QStringList _values;
#endif
        
        for (int i = vec.length() - 1; i >= 0; i--)
        {
            double x = std::abs(vec[i]);
            double y = i == vec.length() - 1 ? std::abs(initialValue) : currentValue;
            
            // Calculate normalized x and y
            
            double xMag = !double_equals(x, 0.0, EPS) ? // Guard from zeroes
                            x * std::pow(10, exp10(x) * -1) 
                              : meanMag;
            
            double yMag = y * std::pow(10, exp10(y) * -1);
            
            currentValue = std::pow(xMag, yMag);
            
#ifdef QT_DEBUG
            _vec.prepend(QString::number(vec[i]));
            if (i >= vec.length() - 10) 
            {
                _values.append(QString::number(currentValue));
            }
            else if (i == vec.length() - 10)
            {
                _values.append("...");
            }
#endif
        }
        
#ifdef QT_DEBUG
        qDebug().nospace() << "        (debug) {" << _vec.join(", ") << "} = {" << 
            _values.join(", ") << "} = " << currentValue;
#endif
        
        result.result = currentValue;
        return result;
    }
}

#endif // NORMOPER_H
