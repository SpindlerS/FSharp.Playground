namespace Chess.CoreTests.Tests

module InitialGameStateTests =

    open NUnit.Framework
    open FsUnitTyped.TopLevelOperators
    open Chess.Core.BasicTypes
    
    [<InitialGameStateTest>]
    type InitialGameStateTest () =

        let pawns color row = [1..8] |> List.map (fun column -> (color, Pawn, OnBoard(Col column, row)))
        
        let ``Initial game state should contain a given piece`` (piece) =
            initialGameState
            |> shouldContain piece
            
        [<SetUp>]
        member __.Setup () =
            ()

        [<Test>]
        member __.``Initial game state should contain thirty-two pieces in total`` () =
            initialGameState
            |> shouldHaveLength 32

        [<Test>]
        member __.``Initial game state should contain eight white pawns in row two`` () =
            initialGameState
            |> Map.filter (fun (OnBoard (c, r), piece) -> piece = White Pawn) 
            |> shouldEqual (White Pawn (Row 2))
        
        [<Test>]
        member __.``Initial game state should contain eight black pawns in row seven`` () =
            initialGameState
            |> List.filter (fun (color, piece, _) -> piece = Pawn && color = Black) 
            |> shouldEqual (pawns Black (Row 7))
        
        [<Test>]
        member __.``Initial game state should contain the correct white non-pawn pieces`` () =
            [
                (OnBoard(Col 1, Row 1), White Rook);
                (OnBoard(Col 8, Row 1), White Rook);
                (OnBoard(Col 2, Row 1), White Knight);
                (OnBoard(Col 7, Row 1), White Knight);
                (OnBoard(Col 3, Row 1), White Bishop);
                (OnBoard(Col 6, Row 1), White Bishop);
                (OnBoard(Col 4, Row 1), White Queen);
                (OnBoard(Col 5, Row 1), White King);
            ]
            |> Map.ofList
            |> Map.iter ``Initial game state should contain a given piece``
            
        [<Test>]
        member __.``Initial game state should contain the correct black non-pawn pieces`` () =
           
            [
                (Black Rook,   OnBoard(Col 1, Row 8));
                (Black Rook,   OnBoard(Col 8, Row 8));
                (Black Knight, OnBoard(Col 2, Row 8));
                (Black Knight, OnBoard(Col 7, Row 8));
                (Black Bishop, OnBoard(Col 3, Row 8));
                (Black Bishop, OnBoard(Col 6, Row 8));
                (Black Queen,  OnBoard(Col 4, Row 8));
                (Black King,   OnBoard(Col 5, Row 8));
            ]
            |> List.iter ``Initial game state should contain a given piece``
