module Tests

open System
open Xunit
open FsUnit.Xunit
open Chess.Core.BasicFunctions
open Chess.Core.BasicTypes
open Chess.Core.BasicTypes

[<Fact>]
let ``Initial game state should contain 16 pawns, 2 kings, 2 queens, 4 rooks, 4 knights, 4 bishops (half per color)`` () =
    
    createInitialGameState
    |> List.filter (fun piece -> piece = Pawn) // wie kann man nur Pawns filtern?
    |> should haveCount 16