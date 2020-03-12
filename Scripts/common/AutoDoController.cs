using UnityEngine; 
using System; 
using System.Collections; 
    public class AutoDoController: MonoBehaviour { 
        private int _count = 0;
        private int count {
            get {
                return _count;
            }
            set {
                _count = value;
                action(_count > 0);
            }
            
        }
        private float delay = 0.0f;
        private Action<bool> action;
        private Coroutine coroutine;

        // cb :: bool -> void 
        // 所有Reset Cancel和倒计时结束都会触发cb
        public void Init(Action<bool> cb, float delayTime) {
            action = cb;
            delay = delayTime;
        }

        public void Cancel() {
            count = 0;
            action(false);
            if (coroutine != null) {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }

        public void Reset() {
            if (count == 0) {
                StartTask();
            } else {
                _count = 0;
                StopCoroutine(coroutine);
                StartTask();
            }
        }

        private void StartTask() {
            coroutine = StartCoroutine(countDown());
        }

        private IEnumerator countDown() {
            count += 1;
            yield return new WaitForSeconds(delay);
            count -= 1; 
        }
    }