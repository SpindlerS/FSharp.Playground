namespace Chess.CoreTests.Tests

module InitialGameStateTests =

    open NUnit.Framework
    open FsUnitTyped.TopLevelOperators
    open Chess.Core.BasicTypes
    
    [<InitialGameStateTest>]
    type InitialGameStateTest () =

        let pawns color row = [1..8] |> List.map (fun column -> (color, Pawn, OnBoard(Column(column), row)))
        
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
            |> List.filter (fun (color, piece, _) -> piece = Pawn && color = White) 
            |> shouldEqual (pawns White (Row 2))
        
        [<Test>]
        member __.``Initial game state should contain eight black pawns in row seven`` () =

            initialGameState
            |> List.filter (fun (color, piece, _) -> piece = Pawn && color = Black) 
            |> shouldEqual (pawns Black (Row 7))
        
        [<Test>]
        member __.``Initial game state should contain the correct white non-pawn pieces`` () =
            
            [
                (White, Rook,   OnBoard(Column(1), Row(1)));
                (White, Rook,   OnBoard(Column(8), Row(1)));
                (White, Knight, OnBoard(Column(2), Row(1)));
                (White, Knight, OnBoard(Column(7), Row(1)));
                (White, Bishop, OnBoard(Column(3), Row(1)));
                (White, Bishop, OnBoard(Column(6), Row(1)));
                (White, Queen,  OnBoard(Column(4), Row(1)));
                (White, King,   OnBoard(Column(5), Row(1)));
            ] 
            |> List.iter ``Initial game state should contain a given piece``
            
        [<Test>]
        member __.``Initial game state should contain the correct black non-pawn pieces`` () =
           
            [
                (Black, Rook,   OnBoard(Column(1), Row(8)));
                (Black, Rook,   OnBoard(Column(8), Row(8)));
                (Black, Knight, OnBoard(Column(2), Row(8)));
                (Black, Knight, OnBoard(Column(7), Row(8)));
                (Black, Bishop, OnBoard(Column(3), Row(8)));
                (Black, Bishop, OnBoard(Column(6), Row(8)));
                (Black, Queen,  OnBoard(Column(4), Row(8)));
                (Black, King,   OnBoard(Column(5), Row(8)));
            ]
            |> List.iter ``Initial game state should contain a given piece``
