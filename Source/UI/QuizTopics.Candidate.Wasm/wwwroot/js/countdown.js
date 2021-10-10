(function () {
    var countdown;
    var netObjectReference;

    window.simpleCountdown = {
        setNetObject: function (dotNetObject) {
            netObjectReference = dotNetObject;
        },
        init: function (secondsLeft) {
            clearInterval(countdown);

            var interval = 100 / secondsLeft;
            var width = 0;
            const elem = document.getElementById("countdown-bar");
            elem.style.width = 0 + "%" ;

            countdown = setInterval(function () {
                if (secondsLeft === 0) {
                    failQuestion();
                } else {
                    secondsLeft--;
                    width += interval;
                    elem.style.width = width + "%" ;
                }
            }, 1000);
        },
        stop: function () {
            clearInterval(countdown);
        }
    };

    function failQuestion() {
        netObjectReference.invokeMethodAsync("FailQuestionAsync");
    }
})();