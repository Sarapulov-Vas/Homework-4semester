namespace Homework1

/// Module implementing functions for homework.
module Functions =
    /// Function implementing a series of powers of two.
    let rec degreeRange n m =
        let rec range currentValue i acc  =
            if i >= n + m then
                Some (List.rev acc)
            else
                range (currentValue * 2) (i + 1) (currentValue :: acc)

        if n >= 0 && m >= 0 then
            range (pown 2 n) n [] 
        else
            None
