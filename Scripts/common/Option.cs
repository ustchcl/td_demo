
// using UnityEngine;
using System;
// using System.Collections;
// using System.Collections.Generic;

namespace Hong {
    public delegate T IfNone<T>();
    public delegate void IfNone2();
    public class Maybe<T> {
        private T _value;
        private (string, T) data;

        public Maybe(T t) {
            if (t == null) {
                data = ("None", t);
            } else {
                data = ("Just", t);
            }
        }

        public Maybe() {
            data = ("None", default(T));
        }

        public U Fold<U>(Func<T, U> ifSome, IfNone<U> ifNone) {
            if (IsJust()) {
                return ifSome(data.Item2);
            } else {
                return ifNone();
            }
        }
        public void Fold(Action<T> ifSome, IfNone2 ifNone) {
            if (IsJust()) {
                ifSome(data.Item2);
            } else {
                ifNone();
            }
        }
        public T GetOrElse(T defaultValue) {
            if (IsJust()) {
                return data.Item2;
            } else {
                return defaultValue;
            }
        }

        public bool IsJust() {
            return data.Item1 == "Just";
        }

        public Maybe<U> Map<U>(Func<T, U> f) {
            if (IsJust()) {
                return new Maybe<U>(f(data.Item2));
            } else {
                return new Maybe<U>();
            }
        }
    }
}