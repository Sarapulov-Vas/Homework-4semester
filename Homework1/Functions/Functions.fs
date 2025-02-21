namespace Homework1

/// Module implementing functions for homework.
module Functions =
    /// 
    let rec numberSearch list n =
        let rec search list n acc =
            if List.isEmpty list then
                None
            else if List.head list = n then
                Some acc
            else
                search (List.tail list) n (acc + 1) 
                
        search list n 0
