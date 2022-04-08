function GetFormData() {
    var antwoordenLabels = document.querySelectorAll(".AntwoordLabel.active");
    var checkedAnswerIds = [];
    for (var i = 0; i < antwoordenLabels.length; i++) {
        var label = antwoordenLabels[i];
        var test = label.childNodes[1].attributes.value.nodeValue;
        checkedAnswerIds.push(test);
    }

    post("/Game/CheckAnswers", checkedAnswerIds);
}


function post(path, params, method = 'post') {

    const form = document.createElement('form');
    form.method = method;
    form.action = path;

    for (i = 0; i < params.length; i++) {
        const hiddenField = document.createElement('input');
        hiddenField.type = 'hidden';
        hiddenField.name = i;
        hiddenField.value = params[i];

        form.appendChild(hiddenField);
    }

    document.body.appendChild(form);
    form.submit();
}