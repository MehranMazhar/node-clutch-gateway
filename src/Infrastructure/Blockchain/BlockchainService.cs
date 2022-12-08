using NodeClutchGateway.Application.Blockchain;
using NodeClutchGateway.Domain.Blockchain;
using NodeClutchGateway.Infrastructure.Persistence.Context;

namespace NodeClutchGateway.Infrastructure.Blockchain;
public class BlockchainService : IBlockchainService
{
    private readonly ApplicationDbContext _context;
    private List<Transaction> _pendingTransactions;

    public BlockchainService(ApplicationDbContext context) => _context = context;

    public void AddTransaction(Transaction transaction)
    {
        _pendingTransactions.Add(transaction);
    }

    public void MineBlock(string minerAddress)
    {
        var block = new Block(_pendingTransactions);
        _context.Add(block);
        _context.SaveChanges();
    }
}
