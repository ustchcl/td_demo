/****************************************************************************
 * Copyright (c) 2017 liangxie
 * Copyright (c) 2018 liangxie
 * 
 * http://qframework.io
 * https://github.com/liangxiegame/QFramework
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 ****************************************************************************/

namespace QFramework
{
    using UnityEngine;
    using UniRx;
    using System;

    public static class UniRxExtension
    {
        public static void Example()
        {
            var bs = new BehaviorSubject<int>(0);
            bs.Subscribe(_ => Log.I("count: {0}", _));
            bs.Update(_ => _ + 1);
        }

        public static void Update<T>(this BehaviorSubject<T> bs, Func<T, T> update)
        {
            bs.OnNext(update(bs.Value));
        }

        public static Func<int, int> Add(this int self) 
        {
            return _ => _ + self;
        }

        public static Func<float, float> Add(this float self)
        {
            return _ => _ + self;
        }
    }
}