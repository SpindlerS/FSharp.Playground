namespace Chess.Core
open System
module BasicTypes =
    open BasicTypes
    
    type Color = White | Black
    
    type Column = A|B|C|D|E|F|G|H
    type Row = R1|R2|R3|R4|R5|R6|R7|R8
    type Position = OnBoard of Column * Row | OffBoard
    
    type PieceType = King|Queen|Bishop|Knight|Rook|Pawn
    type Piece = Color * PieceType * Position
    
    
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
    | MovePiece of Piece * Position
    | Castling of CastlingType
    | EnPassant of Piece
    | Promotion of Piece * PieceType
    | Check of Piece * Position
    | Ending of Ending
    
    type WhiteMove = WhiteMove of SimpleMove
    type BlackMove = BlackMove of SimpleMove

    type Move = WhiteMove * BlackMove
    
    type Game = Move list

    type GameState = Piece list

    let po = OnBoard (A, R2)
    let pi : Piece = (White, King, OnBoard (E, R1))
    // initial Board
    let initialGameState: GameState =
        [
            (White, Pawn, OnBoard (A, R2));
            (White, King, OnBoard (E, R1));
            (Black, King, OnBoard (E, R8)) // ...
        ]

    // example game (Anderssen -  Kieseritzki, London 1851)
    let immortalGame : Game =
        [
            (WhiteMove (MovePiece ((White, Pawn, OnBoard (E, R2)), OnBoard (E, R4))),
             BlackMove (MovePiece ((Black, Pawn, OnBoard (E, R7)), OnBoard (E, R5))));
            //...
            (WhiteMove (Check ((White, Bishop, OnBoard (D, R6)), OnBoard (E, R7))),
             BlackMove (Ending (Lose CheckMate)))
            //(WhiteMove (Ending (Lose Resignation)), BlackMove (Ending Win))              
        ]
    // MakeMove: (GameState * Move) -> GameState

