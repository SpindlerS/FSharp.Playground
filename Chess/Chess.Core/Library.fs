namespace Chess.Core

module BasicTypes =
    
    type Color = White | Black
    
    //type Row = A|B|C|D|E|F|G|H
    //type Column = 1|2|3|4|5|6|7|8
    type Column = Column of int
    type Row = Row of int
    
    type Position = 
        | OnBoard of Column * Row // aufgrund der Bezeichnung von Positionen nach 'a4', 'f1', etc.
        | OffBoard

    type PieceType = King | Queen | Bishop | Knight | Rook | Pawn
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

    type CastlingType = KingSide | QueenSide

    type SimpleMove = 
        | MovePiece of Piece * Position
        | Castling of CastlingType
        | EnPassant of Piece
        | Promotion of Piece * PieceType
        | Check of Piece * Position
        | Ending
    
    type WhiteMove = WhiteMove of SimpleMove
    type BlackMove = BlackMove of SimpleMove
    type Move = WhiteMove * BlackMove
    
    type Game = Move list
    type GameState = Piece list

    // initial Board
    let initialGameState: GameState =
        
        let createPawns color row = [1..8] |> List.map (fun column -> (color, Pawn, OnBoard(Column(column), row)))

        let createNonPawnPieces color row = 
            [ 
                (color, Knight, OnBoard(Column(2), row)); (color, Knight, OnBoard(Column(7), row));
                (color, Bishop, OnBoard(Column(3), row)); (color, Bishop, OnBoard(Column(6), row));
                (color, Rook, OnBoard(Column(1), row)); (color, Rook, OnBoard(Column(8), row));
                (color, Queen, OnBoard(Column(4), row)); 
                (color, King, OnBoard(Column(5), row)); 
            ]
            
        (createPawns White (Row 2)) 
        @ (createPawns Black (Row 7)) 
        @ (createNonPawnPieces White (Row 1)) 
        @ (createNonPawnPieces Black (Row 8))
        
    // MakeMove: (GameState * Move) -> GameState
    