module PhoneBook.Tests

open NUnit.Framework
open PhoneBook.Commands
open FsUnit

[<Test>]
let ``Add entry to phone book Test`` () =
    let book = addEntry "Vasilii" "89001231212" []
    book.Length |> should equal 1
    book.Head.Name |> should equal "Vasilii"
    book.Head.Phone |> should equal "89001231212"

[<Test>]
let ``Find phone by name Test`` () =
    let book = [{Name = "Vasilii"; Phone = "89001231212"}]
    findPhoneByName "Vasilii" book|> Option.map (fun entry -> entry.Phone) |>
        should equal (Some "89001231212")

    findPhoneByName "Ivan" book|> Option.map (fun entry -> entry.Phone) |>
        should equal None

[<Test>]
let ``Find name by phone Test`` () =
    let book = [{Name = "Vasilii"; Phone = "89001231212"}]
    findNameByPhone "89001231212" book |> Option.map (fun entry -> entry.Name) |>
        should equal (Some "Vasilii")

    findNameByPhone "89001231213" book |> Option.map (fun entry -> entry.Name) |>
        should equal None

[<Test>]
let ``Load data Test`` () =
    loadFromFile "../../../TestData/LoadTest.txt" |>
        should equal (Some [{Name = "Vasilii"; Phone = "89001231212"}])

[<Test>]
let ``Save and load data Test`` () =
    let book = [{Name = "Vasilii"; Phone = "89001231212"}]
    saveToFile "../../../TestData/SaveLoadTest.txt" book
    loadFromFile "../../../TestData/SaveLoadTest.txt" |>
        should equal (Some book)

    loadFromFile "../../../TestData/LoadTest1.txt" |>
        should equal None
    
