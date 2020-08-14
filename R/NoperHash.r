exponent <- function(x) { 
    if (x == 0) 0 
    else 1 + floor(log10(abs(x))) 
}

normMag <- function(x) {
    x / 10 ^ exponent(x)
}

normOper <- function(set) {
    mn <- mean(set)
    sd <- sd(set)
    
    initialValue <- mn
    currentValue <- 0
    
    meanMag <- normMag(mean(set))
    
    if (meanMag == 0 && sd == 0) {
        return(0)
    }
    
    i <- length(set)
    while (i >= 1) {
        x <- abs(set[i])
        y <- ifelse(i == length(set), abs(initialValue), currentValue)
        
        xExp <- exponent(x)
        xMag <- ifelse(!isTRUE(all.equal(x, 0)), x * `^`(10, xExp * -1), meanMag)
        
        yExp <- exponent(y)
        yMag <- y * `^`(10, yExp * -1)
        
        currentValue <- `^`(xMag, yMag)
        
        i <- i-1
    }
    
    currentValue
}

tests <- function() {
    setA <- c(5,6,4.6,6.3,5,4.3,5.2)
    setAchanged <- c(5,6,4.61,6.3,5,4.3,5.2)
    setAdisp <- c(setA,91282712351322)
    setB <- c(0.00002,0.000035,0.000046,0.000019,0.000022,0.000016)
    setBdisp <- c(setB,4718239212)
    setC <- c(515147213,515147217,515147211,515147220,515147208,515147216)
    setCdisp <- c(setC,0.000001293)
    
    cat("A", normOper(setA), "\n",
        "Achanged", normOper(setAchanged), "\n",
        "Achanged", normOper(setAchanged), "\n",
        "Achanged", normOper(setAchanged), "\n",
        "Adisp", normOper(setAdisp), "\n",
        "B", normOper(setB), "\n",
        "Bdisp", normOper(setBdisp), "\n",
        "C", normOper(setC), "\n",
        "Cdisp", normOper(setCdisp), "\n\n")
    
    samples <- list(
        c(1429041290,1429041350,1429041410,1429041470,1429041530,1429041590,1429041650,1429041710,
          1429041770,1429041830),
        c(24.43,24.35,24.32,24.31,24.36,24.44,26.87,27.68,28.25,28.57), 
        c(4054,0,237,2009,4001,4019,6368,10670,6340,1816), 
        c(226,0,21,156,205,240,446,519,400,127), 
        c(1.0146699999999999*10^-7,0,0.0833333,0.633333,1.9,1.5,2.76667,5.2,3.7,0.8)
    )
    
    for (sample in samples) {
        cat(sample, "=", normOper(sample), "\n")
    }
    
    testSuite <- function() {
        s=c(5,6,4.6,6.3,5,4.3,5.2);
        s2=c(5,5.2,4.6,6.3,5,4.3,5.2);
        cat("mean:", 
            !isTRUE(all.equal(mean(s), mean(s2))) && 
            !isTRUE(all.equal(sd(s), sd(s2))) && 
            !isTRUE(all.equal(normOper(s), normOper(s2))), "\n")
        
        s=c(5,6,4.6,6.3,5,4.3,5.2);
        s2=c(5,5.2,5.4,6.3,5,4.3,5.2);
        s3=c(5,6,6.3,4.6,5,4.3,5.2);
        cat("stddev:", 
            isTRUE(all.equal(mean(s), mean(s2))) && isTRUE(all.equal(mean(s2), mean(s3))) &&
            !isTRUE(all.equal(sd(s), sd(s2))) && isTRUE(all.equal(sd(s), sd(s3))) &&
            !isTRUE(all.equal(normOper(s), normOper(s2))) && !isTRUE(all.equal(normOper(s), normOper(s3))), "\n")
        
        s=c(5,63,4.6,6.3,5,4.3,5.2);
        s2=c(5,63,6.3,4.6,5,4.3,5.2);
        s3=c(5,6.3,4.6,63,5,4.3,5.2);
        cat("subtraction:", 
            isTRUE(all.equal(mean(s), mean(s2))) && isTRUE(all.equal(mean(s2), mean(s3))) &&
            isTRUE(all.equal(sd(s), sd(s2))) && isTRUE(all.equal(sd(s2), sd(s3))) &&
            !isTRUE(all.equal(normOper(s), normOper(s2))) && isTRUE(all.equal(normOper(s), normOper(s3))), "\n")
        
        s=c(5,63,4.6,6.2,5,4.3,5.2);
        s2=c(5,63,4.6,62,5,4.3,5.2);
        s3=c(5,6.3,46,62,5,4.3,5.2);
        s4=c(5,63,4.6,6.3,5,4.3,5.2);
        s5=c(50,6.3,46,0.63,500,0.043,.52);
        cat("asymmetric:", 
            !isTRUE(all.equal(mean(s), mean(s2))) && !isTRUE(all.equal(mean(s2), mean(s3))) &&
            !isTRUE(all.equal(mean(s4), mean(s5))) &&
            !isTRUE(all.equal(sd(s), sd(s2))) && !isTRUE(all.equal(sd(s2), sd(s3))) &&
            !isTRUE(all.equal(sd(s4), sd(s5))) &&
            !isTRUE(all.equal(normOper(s), normOper(s2))) && !isTRUE(all.equal(normOper(s), normOper(s3))) &&
            !isTRUE(all.equal(normOper(s4), normOper(s5))), "\n")
        
        s=c(5,62,4.6,6.2,5,4.3,5.2);
        s2=c(5,6.2,4.6,62,5,4.3,5.2);
        cat("symmetric:", 
            isTRUE(all.equal(mean(s), mean(s2))) && 
            isTRUE(all.equal(sd(s), sd(s2))) && 
            isTRUE(all.equal(normOper(s), normOper(s2))), "\n")
        
        s=c(1429041290,1429041350,1429041410,1429041470,1429041530,1429041590,
            1429041650,1429041710,1429041770,1429041830);
        s2=c(24.43,24.35,24.32,24.31,24.36,24.44,26.87,27.68,28.25,28.57);
        s3=c(4054,0,237,2009,4001,4019,6368,10670,6340,1816);
        s4=c(226,0,21,156,205,240,446,519,400,127);
        s5=c(145,0,5,38,114,90,166,312,222,48);
        s6=c(0.000000101467,0,0.0833333,0.633333,1.9,1.5,2.76667,5.2,3.7,0.8);
        s7=c(0,0,0);
        cat("samples:", 
            isTRUE(all.equal(normOper(s), 0.37965870745378555)) && 
            isTRUE(all.equal(normOper(s2), 0.49196617766541745)) && 
            isTRUE(all.equal(normOper(s3), 0.6001859386298001)) && 
            isTRUE(all.equal(normOper(s4), 0.5062227481033681)) && 
            isTRUE(all.equal(normOper(s5), 0.5498883933485951)) && 
            isTRUE(all.equal(normOper(s6), 0.6145925169648433)) && 
            isTRUE(all.equal(normOper(s7), 0)), "\n");
        
        s=c(0,1429041290,1429041350,1429041410,1429041470,1429041530);
        s2=c(1429041290,1429041350,1429041410,1429041470,1429041530);
        s3=c(0,0,0,24.43,24.35,24.32,24.31,24.36,24.44,26.87,27.68,28.25);
        s4=c(24.43,24.35,24.32,24.31,24.36,24.44,26.87,27.68,28.25);
        cat("leadingZero:",
            !isTRUE(all.equal(normOper(s), 0)) &&
            !isTRUE(all.equal(normOper(s3), 0)) && 
            !isTRUE(all.equal(normOper(s), normOper(s2))) && 
            !isTRUE(all.equal(normOper(s3), normOper(s4))), "\n\n")
    }
    
    testSuite()
    
    asymTest <- function() {
        s=c(5,63,4.6,6.2,5,4.3,5.2);
        s2=c(5,63,4.6,62,5,4.3,5.2);
        s3=c(5,6.3,46,62,5,4.3,5.2);
        s4=c(5,63,4.6,6.3,5,4.3,5.2);
        s5=c(50,6.3,46,0.63,500,0.043,.52);
        cat("s =",normOper(s),"\n",
            "s2 =",normOper(s2),"\n",
            "s3 =",normOper(s3),"\n",
            "s4 =",normOper(s4),"\n",
            "s5 =",normOper(s5),"\n")
        cat(
            !isTRUE(all.equal(normOper(s), normOper(s2))) && 
            !isTRUE(all.equal(normOper(s2), normOper(s3))) && 
            !isTRUE(all.equal(normOper(s4), normOper(s5))), "\n\n")
    }
    
    asymTest()
    
    symTest <- function() {
        s=c(5,62,4.6,6.2,5,4.3,5.2);
        s2=c(5,6.2,4.6,62,5,4.3,5.2);
        s3=c(5,62,6.2,4.6,5,4.3,5.2);
        cat("s =",normOper(s),"\n",
            "s2 =",normOper(s2),"\n",
            "s3 =",normOper(s3),"\n")
        cat(
            isTRUE(all.equal(normOper(s), normOper(s2))) && 
            !isTRUE(all.equal(normOper(s2), normOper(s3))))
    }
    
    symTest()
}

tests()