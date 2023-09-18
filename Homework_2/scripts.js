const Orientations = { Horizontal: 'horizontal', Vertical: 'vertical' };
const Owner = { BOT: 'bot', USER: 'user' };

class Point{
    constructor(x, y) {
        this.x = x;
        this.y = y;
    }
}

class Ship{
    constructor(decksCount, orientation, position) {
        this.decksCount = decksCount;
        this.orientation = orientation;
        this.position = position;
    }
}

class Field{
    constructor(x, y) {
        this.X = x;
        this.Y = y;
        this.IsEmpty = true;
    }
}

var userShips = [[], [], [], []];
var botShips = [[], [], [], []];

function StartGame(){
    CreateBoardLayout('user');
    CreateBoardLayout('bot');

    const startGameButton = document.getElementById("start-game-button");
    startGameButton.style.cssText += "opacity: 0;";
    setTimeout(function (){
        startGameButton.remove();
    }, 500);
    ShowBoardTitles();
}

function ShowBoardTitles(){
    const boardTitle = document.getElementsByClassName('board-title');
    boardTitle[0].style.cssText += "opacity: 100%;";
    boardTitle[1].style.cssText += "opacity: 100%;";
}

function CreateBoardLayout(boardType){
    const boardLayout = document.getElementById(`${boardType}-board`);

    for (let y = 0; y < 10; y++){
        for (let x = 0; x < 10; x++){
            const newField = document.createElement("div");
            // //test
            const newPoint = document.createElement("p");
            newPoint.innerHTML = `${x}, ${y}`;
            newPoint.className = "test";
            newField.appendChild(newPoint);
            // //testEnd
            newField.className = "board-field";
            newField.id = `${boardType}_${x}_${y}`;
            boardLayout.appendChild(newField);
            boardLayout.style.cssText += "opacity: 100%;";
        }
    }
}

function CreateDisplayBoard(){
    let displayBoard = [];
    for (var i = 0; i < 9; i++){
        var column = [];
        for (var j = 0; j < 9; j++){
            column[j] = false;
        }
        displayBoard[i] = column;
    }
    return displayBoard;
}












