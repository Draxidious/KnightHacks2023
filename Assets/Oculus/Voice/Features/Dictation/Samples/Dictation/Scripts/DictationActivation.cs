/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * This source code is licensed under the license found in the
 * LICENSE file in the root directory of this source tree.
 */

using Meta.WitAi.Dictation;
using UnityEngine;
using UnityEngine.Serialization;

namespace Meta.Voice.Samples.Dictation
{
    public class DictationActivation : MonoBehaviour
    {
        [FormerlySerializedAs("dictation")]
        [SerializeField] private DictationService _dictation;
        private bool first = true;
        //[SerializeField] private GameObject chatGPT;

        public void ToggleActivation()
        {
            if (first)
            {
                first = false;
                return;
            }

            if (_dictation.MicActive )
            {
                _dictation.Deactivate();
            }
            else
            {
                _dictation.Activate();
            }
        }
    }
}
