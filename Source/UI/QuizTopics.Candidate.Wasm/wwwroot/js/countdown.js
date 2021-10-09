(function () {
    var countdown;
    var netObjectReference;
    window.simpleCountdown = {
        setNetObject: function(dotNetObject) {
            netObjectReference = dotNetObject;
        },
        initialize: function(secondsLeft) {
            clearInterval(countdown);
            countdown = setInterval(function() {
                    secondsLeft--;
                    (secondsLeft === 1)
                        ? document.getElementById("plural").textContent = ""
                        : document.getElementById("plural").textContent = "s";
                    document.getElementById("countdown").textContent = secondsLeft;
                    if (secondsLeft === 0) {
                        FailQuestion();
                    }
                },
                1000);
        },
        stop: function() {
            clearInterval(countdown);
        }
    };

    function FailQuestion() {
        netObjectReference.invokeMethodAsync("FailQuestionAsync");
    }
})();