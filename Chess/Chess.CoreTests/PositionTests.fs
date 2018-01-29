namespace Chess.Core.Test

open NUnit.Framework
open FsUnitTyped.TopLevelOperators
open Chess.Core.Position
open Expecto

module PositionTests =

    [<InitialGameStateTest>]
    type InitialGameStateTest () =

        let ``Initial game position should contain a given piece`` pos = 
            fun piece ->
            initialPosition.PiecePlacement |> Map.toSeq
            |> shouldContain (pos, piece)
            
        [<SetUp>]
        member __.Setup () =
            ()

        [<Test>]
        member __.``Initial game state should contain thirty-two pieces in total`` () =
            initialPosition.PiecePlacement
            |> shouldHaveLength 32

        [<Test>]
        member __.``Initial game state should contain eight white pawns in row two`` () =
            initialPosition.PiecePlacement
            |> Map.filter (fun x -> (fun x -> match x with | White Pawn -> true |_ -> false)) 
            |> Map.toSeq 
            |> Seq.map (fun (OnBoard (c, r):OnBoardPosition, _) -> r)
            |> Set.ofSeq 
            |> shouldEqual ([Row 2] |> Set.ofSeq)
        
        [<Test>]
        member __.``Initial game state should contain eight black pawns in row seven`` () =
            initialPosition.PiecePlacement |> Map.toSeq
            |> Seq.filter (fun (_, piece) -> piece = Black Pawn)
            |> Seq.map (fun (OnBoard (c, r):OnBoardPosition, _) -> r)
            |> Set.ofSeq
            |> shouldEqual ([Row 7] |> Set.ofSeq)
        
        [<Test>]
        member __.``Initial game state should contain the correct white non-pawn pieces`` () =
            [
                (OnBoard(Col 1, Row 1), White Rook  );
                (OnBoard(Col 8, Row 1), White Rook  );
                (OnBoard(Col 2, Row 1), White Knight);
                (OnBoard(Col 7, Row 1), White Knight);
                (OnBoard(Col 3, Row 1), White Bishop);
                (OnBoard(Col 6, Row 1), White Bishop);
                (OnBoard(Col 4, Row 1), White Queen );
                (OnBoard(Col 5, Row 1), White King  );
            ]
            |> Map.ofSeq
            |> Map.iter ``Initial game position should contain a given piece``
            
        [<Test>]
        member __.``Initial game state should contain the correct black non-pawn pieces`` () =
            [
                (OnBoard(Col 1, Row 8), Black Rook   );
                (OnBoard(Col 8, Row 8), Black Rook   );
                (OnBoard(Col 2, Row 8), Black Knight );
                (OnBoard(Col 7, Row 8), Black Knight );
                (OnBoard(Col 3, Row 8), Black Bishop );
                (OnBoard(Col 6, Row 8), Black Bishop );
                (OnBoard(Col 4, Row 8), Black Queen  );
                (OnBoard(Col 5, Row 8), Black King   );
            ]
            |> Map.ofSeq
            |> Map.iter ``Initial game position should contain a given piece``
