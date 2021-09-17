using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringBenchmarks
{
  [MemoryDiagnoser]
    public class StringMasking
    {
        private const string _clearValue = "Password1234!";

        [Benchmark]
        public string MaskNaiveWay()
        {
            var firstChars = _clearValue.Substring(0, 3);
            var length = _clearValue.Length - 1;

            for (int i = 0; i < length; i++)
            {
                firstChars += "*";
            }
            return firstChars;

        }

        [Benchmark]
        public string MaskByChangingArrayChars()
        {
            var firstChars = _clearValue.ToCharArray();
            var length = _clearValue.Length - 1;

            for (int i = 3; i < length; i++)
            {
                firstChars[i] = '*';
            }
            return firstChars.ToString();

        }

        [Benchmark]
        public string MaskByStringBuilder()
        {
            var builder = new StringBuilder(_clearValue.Substring(0, 3));
            var length = _clearValue.Length - 1;

            for (int i = 0; i < length; i++)
            {
                builder.Append("*");
            }
            return builder.ToString();

        }

        [Benchmark]
        public string MaskByStringConstractor()
        {
            var firstChars = _clearValue.Substring(0, 3);
            var length = _clearValue.Length - 3;

            return firstChars + new string('*', length);
        }

        [Benchmark]
        public string MaskStringCreate()
        {
            return string.Create(_clearValue.Length, _clearValue, (span, value) =>
            {
                value.AsSpan().CopyTo(span);
                span[3..].Fill('*');
            });
        }

        [Benchmark]
        public string MaskUsingSpan()
        {

            Span<char> span = new Span<char>(_clearValue.ToCharArray());
            span[3..].Fill('*');
            return span.ToString();
        }
    }
}
