namespace Homework1

/// Module implementing functions for homework.
module Functions =
    /// Function calculating the n-th Fibonacci number.
    let rec fibonacci n =
        let rec fibo acc1 acc2 i =
            if i <= 1 then
                Some acc1
            else
                fibo acc2 (acc1 + acc2) (i - 1)
        if n < 0 then
            None
        else
            fibo 1 1 n

    /// Function calculating the factorial of n.
    let rec factorial n =
        let rec fact acc i =
            if i <= 1 then
                Some acc
            else
                fact (acc * i) (i - 1)
        
        if n < 0 then
            None
        else
            fact 1 n
