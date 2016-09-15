using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using ApsisBowlingApp.Models;

namespace ApsisBowlingApp.Unit_Tests
{
    [TestFixture]
    public class ScoreCalculator
    {
        private ApsisBowlingApp.Service.ScoreCalculator _scoreCalculator;

        [OneTimeSetUp]
        public void InitTest()
        {
            _scoreCalculator = new ApsisBowlingApp.Service.ScoreCalculator();
        }
        // Testing the calculatemethod without spare or strike
        [Test]
        public void CalculateWithoutStrikesAndSpares()
        {
            var scoreViewModel = new ScoreViewModel()
            {
                Frames = new List<FrameViewModel>()
            };

            var total = 40;

            for (var i = 0; i < 10; i++)
            {
                var frame = new FrameViewModel()
                {
                    First = 1,
                    Second = 3
                };

                scoreViewModel.Frames.Add(frame);
            }

            Assert.AreEqual(total, _scoreCalculator.Calculate(scoreViewModel).TotalScore);
        }

        // Testing the calculatemethod with strikes in all frames
        [Test]
        public void CalculateStrike()
        {
            var scoreViewModel = new ScoreViewModel()
            {
                Frames = new List<FrameViewModel>()
            };

            var total = 300;

            for (var i = 0; i < 10; i++)
            {
                var frame = new FrameViewModel()
                {
                    First = 10,
                    Second = i == 9 ? 10 : 0,
                    Third = i == 9 ? 10 : 0
                };

                scoreViewModel.Frames.Add(frame);
            }

            Assert.AreEqual(total, _scoreCalculator.Calculate(scoreViewModel).TotalScore);
        }

        // Testing the calculatemethod with spares in all frames
        [Test]
        public void CalculateSpare()
        {
            var scoreViewModel = new ScoreViewModel()
            {
                Frames = new List<FrameViewModel>()
            };

            var total = 168;

            for (var i = 0; i < 10; i++)
            {
                var frame = new FrameViewModel()
                {
                    First = 7,
                    Second = 3,
                    Third = i == 9 ? 5 : 0
                };

                scoreViewModel.Frames.Add(frame);
            }

            Assert.AreEqual(total, _scoreCalculator.Calculate(scoreViewModel).TotalScore);
        }

        // Testing the calculatemethod with random values
        [Test]
        public void CalculateRandom()
        {
            var scoreViewModel = new ScoreViewModel()
            {
                Frames = new List<FrameViewModel>()
            };

            var total = 157;

            for (var i = 0; i < 10; i++)
            {
                var frame = new FrameViewModel()
                {
                    First = i + 2,
                    Second = i + 3,
                    Third = i == 9 ? 5 : 0
                };

                scoreViewModel.Frames.Add(frame);
            }

            Assert.AreEqual(total, _scoreCalculator.Calculate(scoreViewModel).TotalScore);
        }

        // Testing the calculatemethod with spare followed by a none spare/strike
        // to check if it follows the bowling rules
        [Test]
        public void CalculateSpareMethod()
        {
            var scoreViewModel = new ScoreViewModel()
            {
                Frames = new List<FrameViewModel>(),
                TotalScore = 0
            };

            var total = 13;

            scoreViewModel.Frames.Add(new FrameViewModel()
            {
                First = 9,
                Second = 1
            });

            scoreViewModel.Frames.Add(new FrameViewModel()
            {
                First = 3,
                Second = 2
            });

            _scoreCalculator.CalculateSpare(scoreViewModel, scoreViewModel.Frames[0], 0);

            Assert.AreEqual(total, scoreViewModel.TotalScore);
        }

        // Testing the calculatemethod with one strike followed by a none spare/strike
        // to check if it follows the bowling rules
        [Test]
        public void CalculateSimpleStrikeMethod()
        {
            var scoreViewModel = new ScoreViewModel()
            {
                Frames = new List<FrameViewModel>(),
                TotalScore = 0
            };

            var total = 15;

            scoreViewModel.Frames.Add(new FrameViewModel()
            {
                First = 10
            });

            scoreViewModel.Frames.Add(new FrameViewModel()
            {
                First = 3,
                Second = 2
            });

            _scoreCalculator.CalculateStrike(scoreViewModel, scoreViewModel.Frames[0], 0);

            Assert.AreEqual(total, scoreViewModel.TotalScore);
        }

        // Testing the calculatemethod with two strikes in a row followed by a none spare/strike
        // to check if it follows the bowling rules
        [Test]
        public void CalculateDubbleStrikeMethod()
        {
            var scoreViewModel = new ScoreViewModel()
            {
                Frames = new List<FrameViewModel>(),
                TotalScore = 0
            };

            var total = 26;

            scoreViewModel.Frames.Add(new FrameViewModel()
            {
                First = 10
            });

            scoreViewModel.Frames.Add(new FrameViewModel()
            {
                First = 10
            });

            scoreViewModel.Frames.Add(new FrameViewModel()
            {
                First = 6,
                Second = 1
            });

            _scoreCalculator.CalculateStrike(scoreViewModel, scoreViewModel.Frames[0], 0);

            Assert.AreEqual(total, scoreViewModel.TotalScore);
        }
    }
}