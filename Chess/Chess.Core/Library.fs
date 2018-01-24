namespace Chess.Core
open System
module BasicTypes =
    
    type Color = White | Black
    
    type Column = Col of int
    type Row = Row of int
    type Position = OnBoard of Column * Row | OffBoard
    type Board = Position list list 
        
    type PieceType = King|Queen|Bishop|Knight|Rook|Pawn
    type Piece = 
        | White of PieceType 
        | Black of PieceType
        member x.Notation() =
            match x with
            | White Pawn    -> "P"
            | White Knight  -> "N"
            | White Bishop  -> "B"
            | White Rook    -> "R" 
            | White Queen   -> "Q"
            | White King    -> "K"
            | Black Pawn    -> "p"
            | Black Knight  -> "n"
            | Black Bishop  -> "b"
            | Black Rook    -> "r" 
            | Black Queen   -> "q"
            | Black King    -> "k"
            
        
    type Draw = 
        | StaleMate
        | OfferDraw              //TODO
        | ThreefoldRepetition
        | FiftyMoves
        | InsufficientMaterial
    type Lose = 
        | CheckMate
        | Resignation
        | Forfeit
        | OutOfTime
    type Ending = 
        | Lose of Lose
        | Draw of Draw
        | Win
    type CastlingType = KingSide|QueenSide
    type SimpleMove = 
    | Move of Piece * Position * Position
    | Check of Piece * Position * Position
    | Castling of CastlingType
    | EnPassant of Piece * Position
    | Promotion of Piece * Position * PieceType
    | Ending of Ending
    
    type WhiteMove = WhiteMove of SimpleMove
    type BlackMove = BlackMove of SimpleMove

    type Move = WhiteMove * BlackMove
    
    type Game = Move list

    type GameState = Map<Position, Piece>

    let po = OnBoard (Col 1, Row 2)
    let pi : Piece = White King
    pi.ToString()
    pi.Notation()
    
    // initial Board
    let initialGameState: GameState =
        let fs = [(Rook, 1); (Knight, 2); (Bishop, 3); 
                  (Queen, 4); (King, 5); 
                  (Bishop, 6); (Knight, 7); (Rook, 8)]
        [[1..8] |> List.map (fun x -> OnBoard (Col x, Row 2), White Pawn);
         [1..8] |> List.map (fun x -> OnBoard (Col x, Row 7), Black Pawn);
         fs     |> List.map (fun (f,c) -> OnBoard (Col c, Row 1), White f);
         fs     |> List.map (fun (f,c) -> OnBoard (Col c, Row 8), Black f)]
        |> List.concat
        |> Map.ofList

    initialGameState.TryFind(OnBoard(Col 1, Row 1))
    initialGameState.TryFind(OnBoard(Col 1, Row 2))
    initialGameState.TryFind(OnBoard(Col 5, Row 1))
    initialGameState.TryFind(OnBoard(Col 5, Row 8))
    
    // example game (Anderssen -  Kieseritzki, London 1851)
    let immortalGame: Game =
        [
        (WhiteMove (Move (White Pawn, OnBoard (Col 5, Row 2), OnBoard (Col 5, Row 4))),
         BlackMove (Move (Black Pawn, OnBoard (Col 5, Row 7), OnBoard (Col 5, Row 5))));
        //...
        (WhiteMove (Check (White Bishop, OnBoard (Col 4, Row 6), OnBoard (Col 5, Row 7))),
         BlackMove (Ending (Lose CheckMate)))
        //(WhiteMove (Ending (Lose Resignation)), BlackMove (Ending Win))              
        ]
        
    // MakeMove: GameState -> Move -> GameState

    type MakeBoard = int -> Board
    let MakeBoard n = 
        [for x in [1 .. n] do 
         yield [for y in [1 .. n] do 
                yield OnBoard (Col y, Row x)]]
    let chessBoard = MakeBoard 8
    
    chessBoard 
    |> List.map (fun a -> a |> List.map(fun a -> match a with | (OnBoard (Col x, Row y)) -> x | OffBoard -> -1)) 
    |> List.concat
    |> List.distinct
    |> List.filter (fun x -> x > 0)
    
    let BoardS board (f : int*int -> string) =
        let cols = 
            board 
            |> List.map (fun a -> a |> List.map(fun a -> match a with | (OnBoard (Col x, _)) -> x | OffBoard -> -1))
            |> List.concat  
            |> List.distinct
            |> List.filter (fun x -> x > 0)
        let rows = 
            board 
            |> List.map (fun a -> a |> List.map(fun a -> match a with | (OnBoard (_, Row y)) -> y | OffBoard -> -1))  
            |> List.concat
            |> List.distinct
            |> List.filter (fun x -> x > 0)
        let b = 
            [for r in rows |> List.rev do
                yield [for c in cols do yield f(r,c)]]
        b |> List.map (String.concat " ") |> (String.concat "\n") |> fun x -> "\n" + x + "\n"
    
    let Notation (piece : Piece option) =  
        match piece with
        | Some p -> p.Notation()
        | None -> "."  
    
    Notation (Some (Black King))  
    Notation None 
    initialGameState.TryFind(OnBoard (Col 5, Row 8))
    Notation(initialGameState.TryFind(OnBoard (Col 5, Row 1)))
    
    List.item 1 ['a'..'h']
    BoardS chessBoard (fun (r,c) -> (List.item (c-1) ['a'..'h']).ToString()+""+r.ToString())
    BoardS chessBoard (fun (r,c) -> Notation(initialGameState.TryFind(OnBoard (Col c, Row r))))
    
