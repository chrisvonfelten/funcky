﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Funcky.Monads;
using Xunit.Sdk;
using static Funcky.Functional;

namespace Funcky.Xunit
{
    public static partial class FunctionalAssert
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [SuppressMessage("Microsoft.Usage", "CA2200", Justification = "Stack trace erasure intentional.")]
        public static Exception IsError<TResult>(Result<TResult> result)
        {
            try
            {
                return result.Match(
                    error: Identity,
                    ok: value => throw new IsErrorException(value));
            }
            catch (IsErrorException exception)
            {
                throw exception;
            }
        }
    }
}
