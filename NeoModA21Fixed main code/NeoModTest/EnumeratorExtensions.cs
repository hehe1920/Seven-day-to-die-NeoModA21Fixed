namespace NeoModTest
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public static class EnumeratorExtensions
    {
        [IteratorStateMachine(typeof(<ToEnumerable>d__0<>))]
        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> enumerator)
        {
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        [CompilerGenerated]
        private sealed class <ToEnumerable>d__0<T> : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private T <>2__current;
            public IEnumerator<T> <>3__enumerator;
            private int <>l__initialThreadId;
            private IEnumerator<T> enumerator;

            [DebuggerHidden]
            public <ToEnumerable>d__0(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                switch (this.<>1__state)
                {
                    case 0:
                        this.<>1__state = -1;
                        while (this.enumerator.MoveNext())
                        {
                            this.<>2__current = this.enumerator.Current;
                            this.<>1__state = 1;
                            return true;
                        Label_003C:
                            this.<>1__state = -1;
                        }
                        return false;

                    case 1:
                        goto Label_003C;
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                EnumeratorExtensions.<ToEnumerable>d__0<T> d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = (EnumeratorExtensions.<ToEnumerable>d__0<T>) this;
                }
                else
                {
                    d__ = new EnumeratorExtensions.<ToEnumerable>d__0<T>(0);
                }
                d__.enumerator = this.<>3__enumerator;
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            T IEnumerator<T>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

