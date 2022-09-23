using System.Collections.Concurrent;

namespace Product.Supplier.Magalu.Worker.Backend.Application.Usecases.Shared.Resume
{
    public class IntegratedResume
    {
        private readonly ConcurrentDictionary<string, int> _resume;

        public int Actives =>
            _resume[nameof(Actives)];

        public int Inactives =>
            _resume[nameof(Inactives)];

        public int Total =>
            _resume[nameof(Total)];

        public IntegratedResume(int maxDegreeOfParallelism)
        {
            _resume = new(maxDegreeOfParallelism, 3)
            {
                [nameof(Actives)] = 0,
                [nameof(Inactives)] = 0,
                [nameof(Total)] = 0
            };
        }

        public void Add(bool active)
        {
            _resume[nameof(Total)] += 1;
            _resume[active ? nameof(Actives) : nameof(Inactives)] += 1;
        }

        public override string ToString() =>
            $"{nameof(Actives)}: {Actives}, {nameof(Inactives)}: {Inactives}, {nameof(Total)}: {Total}";
    }
}
