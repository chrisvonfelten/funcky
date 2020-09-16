using System;
using System.Diagnostics.Contracts;

namespace Funcky.Monads
{
    public static class LazyExtensions
    {
        [Pure]
        public static Lazy<TResult> Select<T, TResult>(this Lazy<T> lazy, Func<T, TResult> selector)
            => new Lazy<TResult>(() => selector(lazy.Value));

        [Pure]
        public static Lazy<TResult> SelectMany<T, TResult>(this Lazy<T> lazy, Func<T, Lazy<TResult>> selector)
            => SelectMany(lazy, selector, (a, b) => b);

        [Pure]
        public static Lazy<TResult> SelectMany<T, TA, TResult>(this Lazy<T> lazy, Func<T, Lazy<TA>> selector, Func<T, TA, TResult> resultSelector)
            => new Lazy<TResult>(() =>
            {
                var first = lazy.Value;
                var second = selector(first).Value;
                return resultSelector(first, second);
            });
    }
}