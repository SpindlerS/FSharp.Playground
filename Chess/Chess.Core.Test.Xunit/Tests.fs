namespace Chess.Core.Test

open System
open Xunit
open FsUnit.Xunit
open Chess.Core.Position
open FsUnitTyped.TopLevelOperators

module PositionTests = 
    //[<Fact>]
    //let ``My test`` () =
    //    Assert.True(true)
    
    [<AutoOpen>]
    module Dependencies = 
        let ``Initial game position should contain a given piece`` pos = 
            fun piece ->
            initialPosition.PiecePlacement |> Map.toSeq
            |> shouldContain (pos, piece)

    [<Fact>]
    let ``Initial game state should contain thirty-two pieces in total`` () =
        let actual = initialPosition.PiecePlacement
                        |> Map.toSeq |> Seq.length 
        Assert.Equal(32, actual)

    [<Fact>]
    let ``Initial game state should contain eight white pawns in row two`` () =
        initialPosition.PiecePlacement
        |> Map.filter (fun x -> (fun x -> match x with | White Pawn -> true |_ -> false)) 
        |> Map.toSeq 
        |> Seq.map (fun (OnBoard (c, r):OnBoardPosition, _) -> r)
        |> Set.ofSeq 
        |> shouldEqual ([Row 2] |> Set.ofSeq)
        
    [<Fact>]
    let  ``Initial game state should contain eight black pawns in row seven`` () =
        initialPosition.PiecePlacement |> Map.toSeq
        |> Seq.filter (fun (_, piece) -> piece = Black Pawn)
        |> Seq.map (fun (OnBoard (c, r):OnBoardPosition, _) -> r)
        |> Set.ofSeq
        |> shouldEqual ([Row 7] |> Set.ofSeq)
    
    let theoryData = new TheoryData<OnBoardPosition,Piece>()
    theoryData.Add(OnBoard(Col 1, Row 1), White Rook)
    theoryData.Add(OnBoard(Col 8, Row 1), White Rook)
    theoryData.Add(OnBoard(Col 2, Row 1), White Knight)
    theoryData.Add(OnBoard(Col 7, Row 1), White Knight)
    theoryData.Add(OnBoard(Col 3, Row 1), White Bishop)
    theoryData.Add(OnBoard(Col 6, Row 1), White Bishop)
    theoryData.Add(OnBoard(Col 4, Row 1), White Queen)
    theoryData.Add(OnBoard(Col 5, Row 1), White King)
   
    [<Theory>]
    [<MemberData("theoryData")>] 
    let ``Initial game state should contain the correct white non-pawn pieces`` (pos:OnBoardPosition, piece:Piece) =      
        let actual = initialPosition.PiecePlacement.TryFind pos = Some piece
        Assert.True(actual)
            
    [<Fact>]
    let  ``Initial game state should contain the correct black non-pawn pieces`` () =
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
        
    [<Fact>]
    let  ``Initial game state should have the following notation`` () =
        initialPosition.Notation()
        |> shouldEqual "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"
