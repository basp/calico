// <copyright file="StringExtensions.cs" company="TMG">
// Copyright (c) TMG. All rights reserved.
// </copyright>

namespace Calico
{
    public static class StringExtensions
    {
        public static string Capitalize(this string s)
        {
            var u = s.ToLowerInvariant();
            var c = u.Substring(0, 1).ToUpperInvariant();
            return $"{c}{u.Substring(1)}";
        }
    }
}
