namespace CodeSample_2;

public sealed class ChunkedAsyncEnumerator<TValue> : IAsyncEnumerator<TValue>
{
    private int _index;
    private IEnumerable<TValue>? _chunkCollection;
    private readonly Func<Task<IEnumerable<TValue>>> _valueFactory;

    private readonly int _chunkSize;
    private int _lastChunkSize;

    private bool _noChunksProduced;

    public ChunkedAsyncEnumerator(int chunkSize, Func<Task<IEnumerable<TValue>>> valueFactory)
    {
        _index = -1;

        if (chunkSize <= 0)
        {
            throw new ArgumentException($"{nameof(chunkSize)} should be greater than zero.");
        }

        _chunkSize = chunkSize;

        _valueFactory = valueFactory
            ?? throw new ArgumentNullException(nameof(valueFactory));

        _noChunksProduced = true;
    }

    private async Task GetNextChunk()
    {
        _chunkCollection = await _valueFactory();
        _lastChunkSize = _chunkCollection.Count();
        _noChunksProduced = false;
    }

    public async ValueTask<bool> MoveNextAsync()
    {
        if (_noChunksProduced)
        {
            await GetNextChunk();
        }

        _index++;
        if (_index < _lastChunkSize)
        {
            return true;
        }

        if (_lastChunkSize < _chunkSize)
        {
            return false;
        }

        await GetNextChunk();
        _index = 0;
        return true;
    }

    public TValue Current => _chunkCollection!.ElementAt(_index);

    public async ValueTask DisposeAsync()
    {
        await Task.Yield();
        _index = -1;
        _chunkCollection = null;
        _noChunksProduced = true;
    }
}