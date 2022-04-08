var prevTime, stopwatchInterval, elapsedTime = 0;

var updateTime = function () {
    var tempTime = elapsedTime;
    var milliseconds = tempTime % 1000;
    tempTime = Math.floor(tempTime / 1000);
    var seconds = tempTime % 60;
    tempTime = Math.floor(tempTime / 60);
    var minutes = tempTime % 60;
    tempTime = Math.floor(tempTime / 60);
    var hours = tempTime % 60;

    var time = hours + " : " + minutes + " : " + seconds + "." + milliseconds;

    $("#time").text(time);
};

$("#timerStartButton").click(function () {
    if (!stopwatchInterval) {
        stopwatchInterval = setInterval(function () {
            if (!prevTime) {
                prevTime = Date.now();
            }

            elapsedTime += Date.now() - prevTime;
            prevTime = Date.now();

            updateTime();
        }, 50);
    }
});

$("#timerPauseButton").click(function () {
    if (stopwatchInterval) {
        clearInterval(stopwatchInterval);
        stopwatchInterval = null;
    }
    prevTime = null;
});

$("#resetButton").click(function () {
    $("#pauseButton").click();
    elapsedTime = 0;
    updateTime();
});

$(document).ready(function () {
    updateTime();
});



function AddAntwoordVak() {
    var table = document.getElementById("addVraagTable");
    var tableRow = document.createElement("tr");
    var emptyTableData = document.createElement("td");
    var filledTableData = document.createElement("td");
    var input = document.createElement("input");

    input.setAttribute("type", "text");
    input.setAttribute("name", "txtFoutAntwoord[]");
    input.required = true;
    filledTableData.appendChild(input);

    tableRow.appendChild(emptyTableData);
    tableRow.appendChild(filledTableData);

    table.appendChild(tableRow);
}

var expanded = false;
function showCheckboxes() {
    var checkboxes = document.getElementById("checkboxes");
    if (!expanded) {
        checkboxes.style.display = "block";
        expanded = true;
    } else {
        checkboxes.style.display = "none";
        expanded = false;
    }
}