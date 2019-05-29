using System;

namespace Inventory.Model.Monads
{
    public class Either<TLeft, TRight>
    {
        public static implicit operator Either<TLeft, TRight>(TLeft obj) =>
            new Left<TLeft, TRight>(obj);

        public static implicit operator Either<TLeft, TRight>(TRight obj) =>
            new Right<TLeft, TRight>(obj);

        public Either<TLeft, TNewRight> Map<TNewRight>(Func<TRight, TNewRight> MapFunc) =>

            this is Right<TLeft, TRight> right
                ? (Either<TLeft, TNewRight>)MapFunc(right)
                : (TLeft)(Left<TLeft, TRight>)this;

        public Either<TLeft, TRight> Map(Func<TRight, Either<TLeft, TRight>> ReMapFunc) =>

            this is Right<TLeft, TRight> right
                ? ReMapFunc(right)
                : (TLeft)(Left<TLeft, TRight>)this;

        public TRight Reduce(Func<TLeft, TRight> ReduceFunc) =>

            this is Left<TLeft, TRight> left
                ? ReduceFunc(left)
                : (Right<TLeft, TRight>)this;


        public Either<TLeft, TRight> Reduce(Func<TLeft, TRight> ReduceFunc, Func<TLeft, bool> when) =>

            this is Left<TLeft, TRight> left && when(left)
                ? ReduceFunc(left)
                : this;

        public Either<TLeft, TRight> Reduce<TException>(Func<TLeft, TRight> ReduceFunc) where TException : Exception =>
            Reduce(ReduceFunc, r => r is TException);
    }

    public class Right<TLeft, TRight> : Either<TLeft, TRight>
    {
        private TRight Content { get; }

        public Right(TRight content)
        {
            Content = content;
        }

        public static implicit operator TRight(Right<TLeft, TRight> obj) =>
            obj.Content;
    }

    public class Left<TLeft, TRight> : Either<TLeft, TRight>
    {
        private TLeft Content { get; }

        public Left(TLeft content)
        {
            Content = content;
        }

        public static implicit operator TLeft(Left<TLeft, TRight> obj) =>
            obj.Content;
    }
}
