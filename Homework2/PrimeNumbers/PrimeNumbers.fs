namespace Homework2

module PrimeNumbers =
    /// A function to check numbers for a prime number.
    let isPrime n =
        {2..int(sqrt(float n))} |> Seq.forall (fun x -> n % x <> 0)
    
    /// Function generating an infinite sequence of prime numbers.
    let primes =
        Seq.initInfinite (fun x -> x + 2) |> Seq.filter isPrime

