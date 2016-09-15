(function () {
    'use strict';

    angular.module('app')
        .controller('MainController', mainController);

    function mainController($http) {
        var vm = this;

        // Controller properties
        vm.submit = submit;
        vm.reset = reset;
        vm.checkNumber = checkNumber;
        vm.checkLastRound = checkLastRound;
        vm.isCurrentRound = isCurrentRound;

        // Validate the last round so the last input is disabled or not disabled
        function checkLastRound(e) {
            var frame = e.frame;

            if (frame.Round === 10 && frame.First) {
                var first = parseInt(translateValue(frame.First));
                var second = parseInt(translateValue(frame.Second));

                if (first < 10 && (isNaN(second) || isNaN(first + second) || (first + second) < 10)) {
                    return true;
                } else {
                    return false;
                }
            }

            return true;
        }

        function translateValue(value) {
            if (value && typeof value === 'string') {
                if (value.toLowerCase() === 'x') {
                    return 10;
                } else if (value === '/') {
                    return 10;
                }
                return value;
            }
            return 0;
        }

        // Validating so you can only type numbers except X and /, and it also changes 10 to X and first value + second value to / if its 10
        function checkNumber(e) {
            if (e.frame.First === 'x') {
                e.frame.First = 'X';
            }
            if (e.frame.Second === 'x') {
                e.frame.Second = 'X';
            }
            if (e.frame.Third === 'x') {
                e.frame.Third = 'X';
            }

            var regex = /^[0-9]*$/;    // allow only numbers [0-9] 
            if ((e.frame.Second && !regex.test(e.frame.Second) && e.frame.Second !== 'X' && e.frame.Second !== '/') || (e.frame.First && !regex.test(e.frame.First) && e.frame.First !== 'X') || (e.frame.Third && !regex.test(e.frame.Third) && e.frame.Third !== 'X' && e.frame.Third !== '/')) {
                alert('Only numbers from 0-9, / or X are allowed');
                e.frame.First = null;
                e.frame.Second = null;
                e.frame.Third = null;
            } else {
                var first = parseInt(e.frame.First);
                var second = parseInt(e.frame.Second);
                var third = parseInt(e.frame.Third);
                if (e.frame.Round !== 10) {
                    if (first >= 10) {
                        e.frame.First = 'X';
                    }

                    else if (first + second >= 10) {
                        e.frame.Second = '/';
                    }
                }
                else {
                    if (first >= 10) {
                        e.frame.First = 'X';
                    }
                    if (second >= 10) {
                        e.frame.Second = 'X';
                    }
                    if (third >= 10) {
                        e.frame.Third = 'X';
                    }

                    if (first + second >= 10) {
                        e.frame.Second = '/';
                    }
                    if (second + third >= 10) {
                        e.frame.Third = '/';
                    }
                }
            }
        }

        // Makes a post request to API with the score chart and gets a the score result in response
        // It also change the result from the API to X and /
        function submit() {
            var score = vm.score;
            editValues(score, function (result) {
                $http.post('/api/bowling/getscore', result).success(function (res) {
                    vm.score = res;

                    for (var i = 0; i < vm.score.Frames.length; i++) {
                        vm.score.Frames[i].Round = i + 1;
                        if (vm.score.Frames[i].First === 10) {
                            vm.score.Frames[i].First = 'X';
                        }
                        if (vm.score.Frames[i].Second === 10) {
                            vm.score.Frames[i].Second = 'X';
                        }
                        if (vm.score.Frames[i].Third === 10) {
                            vm.score.Frames[i].Third = 'X';
                        }
                        if (vm.score.Frames[i].First + vm.score.Frames[i].Second === 10) {
                            vm.score.Frames[i].Second = '/';
                        }
                        if (vm.score.Frames[i].Second + vm.score.Frames[i].Third === 10) {
                            vm.score.Frames[i].third = '/';
                        }
                    }
                });
            });
        }

        function isCurrentRound(index) {

            if (vm.score.Frames[index].Round && vm.score.Frames[index].Round === 1) {
                return false;
            }

            if (vm.score.Frames[index].Round && vm.score.Frames[index].Round > 1) {
                if (vm.score.Frames[index - 1].First && vm.score.Frames[index - 1].Second || vm.score.Frames[index - 1].First === 'X') {
                    return false;
                }
            }

            return true;
        }

        // Changing X and / to numbers
        function editValues(score, callback) {
            for (var i = 0; i < score.Frames.length; i++) {
                if (score.Frames[i].First === 'X') {
                    score.Frames[i].First = 10;
                } else if (!score.Frames[i].First && (score.Frames[i].Second || score.Frames[i].Third)) {
                    score.Frames[i].First = 0;
                }

                if (score.Frames[i].Second === 'X') {
                    score.Frames[i].Second = 10;
                } else if (score.Frames[i].Second === '/') {
                    score.Frames[i].Second = 10 - score.Frames[i].First;
                } else if (!score.Frames[i].Second && score.Frames[i].First && score.Frames[i].First !== 10) {
                    score.Frames[i].Second = 0;
                }

                if (score.Frames[i].Third === 'X') {
                    score.Frames[i].Third = 10;
                } else if (score.Frames[i].Third === '/') {
                    score.Frames[i].Third = 10 - score.Frames[i].Second;
                }



                if (i === score.Frames.length - 1) {
                    callback(score);
                }
            }
        }

        // Resets the score chart to original value
        function reset() {
            vm.score.Frames = [
                {
                    "Round": 1,
                },
                {
                    "Round": 2,
                },
                {
                    "Round": 3,
                },
                {
                    "Round": 4,
                },
                {
                    "Round": 5,
                },
                {
                    "Round": 6,
                },
                {
                    "Round": 7,
                },
                {
                    "Round": 8,
                },
                {
                    "Round": 9,
                },
                {
                    "Round": 10,
                }
            ];
        }

        // Score chart model
        vm.score = {
            Frames: [
                {
                    "Round": 1,
                },
                {
                    "Round": 2,
                },
                {
                    "Round": 3,
                },
                {
                    "Round": 4,
                },
                {
                    "Round": 5,
                },
                {
                    "Round": 6,
                },
                {
                    "Round": 7,
                },
                {
                    "Round": 8,
                },
                {
                    "Round": 9,
                },
                {
                    "Round": 10,
                }
            ]
        };

    }

})();