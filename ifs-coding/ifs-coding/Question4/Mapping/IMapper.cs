using System.Collections.Generic;

namespace ifs_coding.Question4.Mapping
{
    public interface IMapper<TIn, TOut>
    {
        IEnumerable<TOut> Map(IEnumerable<TIn> input);
        TOut Map(TIn input);
    }
}
