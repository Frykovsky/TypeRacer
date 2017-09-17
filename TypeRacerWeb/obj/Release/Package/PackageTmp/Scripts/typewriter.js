function ClearIfSpace(event) {
    if (event.which === 32 || event.keyCode === 32) {
        //$("#typingblank").val(null);
        document.getElementById("typingblank").value = '';
        if (event.preventDefault) event.preventDefault();
    }
}
var begin;
var end;
var inc = 0;
var mistypes = 0;
var lastIndex = 0;
var wordinc = -1;
var textElement = $("#sampletextarea");
var innards = document.getElementById("sampletextarea");
var textik = innards.textContent || innards.innerText;

//var textik = textElement.text();
var contentReplace = textik.split(/\s+/);
var inputarea = $("#typingblank");
var readyforspace = 0;
var mistakes = 0;

function MarkTypedWord(event) {
    setTimeout(function () {
        if (wordinc === -1) {
            wordinc = 0;
            textElement.html(textik.substr(0, lastIndex) + textik.substr(lastIndex, textik.length).replace(contentReplace[wordinc], function (match) {
                lastIndex = lastIndex + match.length + 1;
                return '<span>' + contentReplace[wordinc] + '</span>';
            }));
        }
        if (readyforspace === 1) {
            readyforspace = 0;

            if (event.keyCode === 32 || event.which === 32) {
                //textElement.html( texty.replace('<span>'+contentReplace[wordinc]+'</span>', contentReplace[wordinc]));
                wordinc++;
                textElement.html(textik.substr(0, lastIndex) + textik.substr(lastIndex, textik.length).replace(contentReplace[wordinc], function (match) {
                    lastIndex = lastIndex + match.length + 1;
                    return '<span>' + contentReplace[wordinc] + '</span>';
                }));
            }
            document.getElementById("typingblank").removeEventListener("keypress", ClearIfSpace);
        }

        var typedWord = document.getElementById("typingblank").value;
        if (typedWord === contentReplace[wordinc].substr(0, typedWord.length)) {
            if (inputarea.hasClass("error")) {
                inputarea.removeClass("error");
            }
            if (typedWord.length === contentReplace[wordinc].length) {
                if (lastIndex >= textik.length - 1) {
                    document.getElementById("typingblank").remove();
                    document.getElementById("typinglabel").remove();
                    end = Date.now();
                    var speed = (textik.length * 60 / (parseFloat(end - begin) / 1000)).toFixed(0);
                    $("#countdown").html("speed:" + speed + " cpm");
                    var formsElement = document.getElementById("RaceSaviour");
                    formsElement.getElementsByClassName("speed")[0].value = speed;
                    formsElement.getElementsByClassName("mistakes")[0].value = mistakes;
                    $(function() {
                        $("#RaceSaviour").on("submit", function (e) {
                            e.preventDefault();
                            $.ajax({
                                url: $(this).closest("form").attr("action"),
                                type: 'post',
                                data: $(this).closest("form").serialize(),
                                success: function () {
                                }
                            });
                        });
                    }); 
                    $("#RaceSaviour").submit();
                    document.getElementById("RaceAgain").style.display = "block";
                } else {
                    readyforspace = 1;
                    document.getElementById("typingblank").addEventListener("keypress", ClearIfSpace, false);

                }
            }
        } else {
            inputarea.addClass("error");
            mistakes++;
        }
    }, 0);
}

function timerstarter() {
    document.getElementById("timer").remove();
    $("#countdown").html("The race will begin in <span id='countdowntimer'>5 </span> seconds");
    var timeleft = 5;
    var downloadTimer = setInterval(function () {
        timeleft--;
        document.getElementById("countdowntimer").textContent = timeleft;
        if (timeleft <= 0) {
            begin = Date.now();
            document.getElementById("typingblank").disabled = false;
            document.getElementById("typingblank").focus();
            clearInterval(downloadTimer);
            $("#countdown").html("Begin!");
        }
    }, 1000);
}
document.getElementById("timer").addEventListener("click", timerstarter, false);
document.getElementById("typingblank").addEventListener("keydown", MarkTypedWord, false);
//$("#typingblank").bind("keypress", MarkTypedWord);
//$("#typingblank").bind("keypress", ClearIfSpace);