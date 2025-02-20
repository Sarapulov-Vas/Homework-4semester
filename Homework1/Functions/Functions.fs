namespace Homework1

module Functions =
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