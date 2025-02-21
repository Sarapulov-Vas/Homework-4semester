namespace Homework1

/// Module implementing functions for homework.
module Functions =
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
