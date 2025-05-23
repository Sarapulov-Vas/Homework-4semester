﻿namespace Homework1

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

    /// The function implements the search for a number in the list.
    let rec numberSearch list n =
        let rec search list n acc =
            if List.isEmpty list then
                None
            else if List.head list = n then
                Some acc
            else
                search (List.tail list) n (acc + 1) 
                
        search list n 0

    /// Function that implements list reversal.
    let rec listReverse list =
        let rec reverse list reverseList =
            if List.isEmpty list then
                reverseList
            else
                reverse (List.tail list) (List.head list :: reverseList)

        reverse list []

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
