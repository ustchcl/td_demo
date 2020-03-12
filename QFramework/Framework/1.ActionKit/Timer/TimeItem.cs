/****************************************************************************
 * Copyright (c) 2017 snowcold
 * Copyright (c) 2017 liangxie
****************************************************************************/

namespace QFramework
{
    using System;
    
    public class TimeItem : IBinaryHeapElement, IPoolable, IPoolType
    {
        /*
         * tick:当前第几次
                 */

        private float                   mDelayTime;
        private bool                    mIsEnable = true;
        private int                     mRepeatCount;
        private int                     mRepeatCountSource;
        private float                   mSortScore;
		private Action<int>                mCallback;
        private int                     mCallbackTick;
        private int                     mHeapIndex;
        private bool                    mIsCache;
        private bool                    mPause = false;

		public static TimeItem Allocate(Action<int> callback, float delayTime, int repeatCount = 1)
        {
            TimeItem item = SafeObjectPool<TimeItem>.Instance.Allocate();
            item.Set(callback, delayTime, repeatCount);
            return item;
        }

		public void Set(Action<int> callback, float delayTime, int repeatCount)
        {
            mCallbackTick = 0;
            mCallback = callback;
            mDelayTime = delayTime;
            mRepeatCount = repeatCount;
            mRepeatCountSource = repeatCount;
            mPause = false;
        }

        public void OnTimeTick()
        {
            if (mPause) {
                return;
            }
            if (mCallback != null)
            {
                mCallback(++mCallbackTick);
            }

            if (mRepeatCount > 0)
            {
                --mRepeatCount;
            }
        }

		public Action<int> callback
        {
            get { return mCallback; }
        }

        public float SortScore
        {
            get { return mSortScore; }
            set { mSortScore = value; }
        }

        public int HeapIndex
        {
            get { return mHeapIndex; }
            set { mHeapIndex = value; }
        }

        public bool isEnable
        {
            get { return mIsEnable; }
        }

        public bool IsRecycled
        {
            get
            {
                return mIsCache;
            }

            set
            {
                mIsCache = value;
            }
        }

        public void Cancel()
        {
            if (mIsEnable)
            {
                mIsEnable = false;
                mCallback = null;
            }
        }

        public void Pause()
        {
            mPause = true;
        }

        public void Start() {
            mPause = false;
        }

        public void Stop() {
            mRepeatCount = 0;
        }

        public void Restart() {
            Set(mCallback, mDelayTime, mRepeatCountSource);
        }

        public bool NeedRepeat()
        {
            if (mRepeatCount == 0)
            {
                return false;
            }
            return true;
        }

        public float DelayTime()
        {
            return mDelayTime;
        }

        public void RebuildHeap<T>(BinaryHeap<T> heap) where T : IBinaryHeapElement
        {
            heap.RebuildAtIndex(mHeapIndex);
        }

        public void OnRecycled()
        {
            mCallbackTick = 0;
            mCallback = null;
            mIsEnable = true;
            mHeapIndex = 0;
    }

        public void Recycle2Cache()
        {
            //超出缓存最大值
			SafeObjectPool<TimeItem>.Instance.Recycle(this);
        }
    }
}
