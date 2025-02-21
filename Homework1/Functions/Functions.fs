namespace Homework1

/// Module implementing functions for homework.
module Functions =
    /// Function that implements list reversal.
    let rec listReverse list =
        let rec reverse list reverseList =
            if List.isEmpty list then
                reverseList
            else
                reverse (List.tail list) (List.head list :: reverseList)

        reverse list []
