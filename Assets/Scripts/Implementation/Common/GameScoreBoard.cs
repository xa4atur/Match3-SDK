using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Implementation.Common.Interfaces;
using Match3.Core.Models;
using Debug = UnityEngine.Debug;

namespace Implementation.Common
{
    public class GameScoreBoard : IGameScoreBoard
    {
        public void RegisterSolvedSequences(IEnumerable<ItemSequence<IUnityItem>> sequences)
        {
            RegisterSolvedSequencesAsync(sequences).Forget();
        }

        private async UniTask RegisterSolvedSequencesAsync(IEnumerable<ItemSequence<IUnityItem>> sequences)
        {
            foreach (var sequence in sequences)
            {
                RegisterSequenceScore(sequence);
                await UniTask.Yield();
            }
        }

        private void RegisterSequenceScore(ItemSequence<IUnityItem> sequence)
        {
            Debug.Log(
                $"<color=yellow>{sequence.Type}</color> sequence of <color=yellow>{sequence.SolvedGridSlots.Count}</color> elements");
        }
    }
}