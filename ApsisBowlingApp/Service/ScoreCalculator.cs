using ApsisBowlingApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApsisBowlingApp.Service
{
    public class ScoreCalculator
    {

        /// <summary>
        /// Calculates the score for each frame and also returning a total score
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public ScoreViewModel Calculate(ScoreViewModel vm)
        {
            vm.TotalScore = 0;

            for (int i = 0; i < vm.Frames.Count; i++)
            {
                var item = vm.Frames[i];
                // Checks so there is a valid value in the frame
                if (item.First.HasValue)
                {
                    // If its a Strike
                    if (item.First == 10)
                    {
                        CalculateStrike(vm, item, i);
                    }
                    // If its a Spare
                    else if ((item.First + item.Second) == 10)
                    {
                        CalculateSpare(vm, item, i);
                    }
                    // If its a Open
                    else
                    {
                        vm.TotalScore += item.Sum();
                    }
                    item.Score = vm.TotalScore;
                }
            }
            return vm;
        }

        /// <summary>
        /// Checks if it`s a spare and then calculates the points according to the next throw.
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="item"></param>
        /// <param name="currenIndex"></param>
        public void CalculateSpare(ScoreViewModel vm, FrameViewModel item, int currenIndex)
        {
            FrameViewModel nextItem = null;

            // Checking so there is a next frame
            if (currenIndex + 1 < vm.Frames.Count)
            {
                nextItem = vm.Frames[currenIndex + 1];
            }

            // If there is a next frame we know that this is not the last round
            if (nextItem != null)
            {
                vm.TotalScore += item.Sum() + nextItem.First;
            }
            else
            {
                vm.TotalScore += item.Sum();
            }
        }

        /// <summary>
        /// Checks if it`s a strike and then calculates the points according to the next two throws.
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="item"></param>
        /// <param name="currentIndex"></param>
        public void CalculateStrike(ScoreViewModel vm, FrameViewModel item, int currentIndex)
        {
            FrameViewModel nextItem = null;
            FrameViewModel nextItemAfterNext = null;

            // Checking so there is a next frame
            if (currentIndex + 1 < vm.Frames.Count)
            {
                nextItem = vm.Frames[currentIndex + 1];
            }

            // Checking so there is a frame after the next frame
            if (currentIndex + 2 < vm.Frames.Count)
            {
                nextItemAfterNext = vm.Frames[currentIndex + 2];
            }

            // If there is a next frame then we know this is not the last round
            if (nextItem != null)
            {
                if (nextItem.First == 10 && (nextItem.Second == 0 || !nextItem.Second.HasValue) && nextItemAfterNext != null)
                {
                    vm.TotalScore += item.First + nextItem.First + nextItemAfterNext.First;
                }
                else
                {
                    vm.TotalScore += item.First + nextItem.First + nextItem.Second;
                }

            }
            else if (item.Third.HasValue)
            {
                vm.TotalScore += (item.First + item.Second) + item.Third.Value;
            }
            else
            {
                vm.TotalScore += item.Sum();
            }
        }
    }
}